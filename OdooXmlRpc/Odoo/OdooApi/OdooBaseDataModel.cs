using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OdooXmlRpc.Odoo.OdooApi
{
    public abstract class OdooBaseDataModel
    {
        private readonly OdooApi _odooApi;
        private OdooModel _model;
        private readonly string _modelName;

        public List<string> FieldNames { get; set; }
        public OdooFilter Filter { get; set; }

        private List<OdooRecord> _odooRecords;

        public OdooBaseDataModel(OdooApi odooApi, string modelName)
        {
            _odooApi = odooApi;
            _modelName = modelName;
            Filter = new OdooFilter();
            FieldNames = new List<string>();
        }

        public List<OdooRecord> GetRecords()
        {
            return _odooRecords;
        }

        /// <summary>
        /// Sonuçları XML Çevir
        /// </summary>
        /// <returns></returns>
        public XElement ToXml()
        {
            var root = new XElement(_modelName);

            foreach (var odooRecord in _odooRecords)
            {
                var element = new XElement("Item");
                foreach (var field in odooRecord.GetFields())
                {
                    element.Add(new XAttribute(field.Key, field.Value));
                }
                root.Add(element);
            }
            return root;
        }

        /// <summary>
        ///     Read parametresini True atarsan ODOO üzerinde READ işleminide yapar
        /// </summary>
        /// <param name="read"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<OdooRecord> Execute(bool read = false, int? offset = null, int? limit = null)
        {
            if (!_odooApi.Login())
            {
                _odooApi.Login();
            }

            _model = _odooApi.GetModel(_modelName);
            _model.AddFields(FieldNames);

            if (read)
            {
                _odooRecords = _model.SearchAndRead(Filter.ToArray(), offset, limit);

                return _odooRecords;
            }
            return _model.Search(Filter.ToArray());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadCount">Açılacak thread sayısı</param>
        /// <param name="requestSize">Tek seferde alınacak veri miktarı</param>
        /// <returns></returns>
        public List<OdooRecord> ExecuteThread(int threadCount, int requestSize)
        {
            if (!_odooApi.Login())
            {
                _odooApi.Login();
            }

            _model = _odooApi.GetModel(_modelName);
            _model.AddFields(FieldNames);

            threadCount = threadCount == 0 ? 1 : threadCount;

            var totalRecordCount = Count();
            _odooRecords = new List<OdooRecord>(totalRecordCount);


            //Thread başına düşecek kayıt sayısı
            var threadCountSize = Convert.ToInt32(Math.Ceiling((decimal)(totalRecordCount / threadCount))) + 1;

            //Toplam Kayıt Sayısı tek seferde alınacak request size'dan az ise tek thread aç
            if (totalRecordCount < requestSize)
            {
                threadCount = 1;
                requestSize = totalRecordCount;
                threadCountSize = requestSize;
            }
            else
            {
                requestSize = threadCountSize < requestSize ? threadCountSize : requestSize;
            }

            var threadList = new List<Thread>(threadCount);
            int startIndex = 0;

            for (var i = 0; i < threadCount; i++)
            {
                var t = new Thread((payload) =>
                {
                    var threadPayload = (ThreadPayload)payload;
                    var currentIndex = threadPayload.StartIndex;

                    while (true)
                    {

                        if (((currentIndex - threadPayload.StartIndex) + threadPayload.RequestSize) > threadPayload.ThreadCountSize)
                        {
                            threadPayload.RequestSize = threadPayload.ThreadCountSize - (currentIndex - threadPayload.StartIndex);
                        }

                        var records = threadPayload.Model.SearchAndRead(
                            Filter.ToArray(),
                            currentIndex,
                            threadPayload.RequestSize);

                        if (records.Count == 0) break;

                        lock (_odooRecords)
                        {
                            _odooRecords.AddRange(records);
                        }

                        currentIndex += threadPayload.RequestSize;

                        if ((currentIndex - threadPayload.StartIndex) >= threadPayload.ThreadCountSize) break;

                    }
                });

                threadList.Add(t);
                t.Start(new ThreadPayload(_model, startIndex, requestSize, threadCountSize));
           
                startIndex += threadCountSize;
            }

            while (true)
            {
                var any = threadList.Any(x => x.IsAlive);
                if (!any) break;
            }

            return _odooRecords;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadCount">Açılacak thread sayısı</param>
        /// <param name="requestSize">Tek seferde alınacak veri miktarı</param>
        /// <returns></returns>
        public List<OdooRecord> ExecuteAsync(int threadCount, int requestSize)
        {
            if (!_odooApi.Login())
            {
                _odooApi.Login();
            }

            _model = _odooApi.GetModel(_modelName);
            _model.AddFields(FieldNames);

            threadCount = threadCount == 0 ? 1 : threadCount;

            var totalRecordCount = Count();
            _odooRecords = new List<OdooRecord>(totalRecordCount);


            //Thread başına düşecek kayıt sayısı
            var threadCountSize = Convert.ToInt32(Math.Ceiling((decimal)(totalRecordCount / threadCount))) + 1;

            //Toplam Kayıt Sayısı tek seferde alınacak request size'dan az ise tek thread aç
            if (totalRecordCount < requestSize)
            {
                threadCount = 1;
                requestSize = totalRecordCount;
                threadCountSize = requestSize;
            }
            else
            {
                requestSize = threadCountSize < requestSize ? threadCountSize : requestSize;
            }

            var threadList = new Task[threadCount];
            int startIndex = 0;

            Action<object> action = (object payload) =>
            {
                var threadPayload = (ThreadPayload)payload;

                var currentIndex = threadPayload.StartIndex;

                while (true)
                {
                    if (((currentIndex - threadPayload.StartIndex) + threadPayload.RequestSize) > threadPayload.ThreadCountSize)
                    {
                        threadPayload.RequestSize = threadPayload.ThreadCountSize - (currentIndex - threadPayload.StartIndex);
                    }

                    List<OdooRecord> records = threadPayload.Model.SearchAndRead(
                        Filter.ToArray(),
                        currentIndex,
                        threadPayload.RequestSize);

                    if (records.Count == 0) break;

                    lock (_odooRecords)
                    {
                        _odooRecords.AddRange(records);
                    }

                    currentIndex += threadPayload.RequestSize;

                    if ((currentIndex - threadPayload.StartIndex) >= threadPayload.ThreadCountSize) break;
                }
            };


            for (var i = 0; i < threadCount; i++)
            {
                ThreadPayload parm = new ThreadPayload(_model, startIndex, requestSize, threadCountSize);
                var t = new Task(action, parm);
                t.Start();
                threadList[i] = t;
                startIndex += threadCountSize;
            }

            Task.WaitAll(threadList);

            return _odooRecords;
        }

        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (!_odooApi.Login())
            {
                _odooApi.Login();
            }
            return _model.Count(Filter.ToArray());
        }

        public OdooBaseDataModel AddField(string fieldName)
        {
            FieldNames.Add(fieldName);
            return this;
        }

        public OdooBaseDataModel AddFields(List<string> fieldsName)
        {
            FieldNames.AddRange(fieldsName);
            return this;
        }
    }

    class ThreadPayload
    {
        public OdooModel Model { get; set; }
        public int StartIndex { get; set; }
        public int RequestSize { get; set; }
        public int ThreadCountSize { get; set; }

        public ThreadPayload(OdooModel model, int startIndex, int requestSize, int threadCountSize)
        {
            StartIndex = startIndex;
            Model = model;
            RequestSize = requestSize;
            ThreadCountSize = threadCountSize;
        }
    }
}
