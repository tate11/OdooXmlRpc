using CookComputing.XmlRpc;
using System.Collections.Generic;
using System.Dynamic;

namespace OdooXmlRpc.Odoo.Odoo
{
    public class OdooRecord: DynamicObject
    {
        private readonly OdooRpc _api;
        private readonly string _model;

        private readonly Dictionary<string, object> _fields = new Dictionary<string, object>();

        private readonly List<string> _modifiedFields = new List<string>();

        public OdooRecord(OdooRpc api, string model, int id)
        {
            _model = model;
            _api = api;
            Id = id;
        }

        public Dictionary<string, object> GetFields()
        {
            return _fields;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _fields.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _fields[binder.Name] = value;
            return true;
        }

        public bool SetValue(string field, object value)
        {
            if (_fields.ContainsKey(field))
            {
                if (!_modifiedFields.Contains(field))
                {
                    _modifiedFields.Add(field);
                }

                _fields[field] = value;
            }
            else
            {
                _fields.Add(field, value);
            }
            return true;
        }

        public object GetValue(string field)
        {
            if (_fields.ContainsKey(field))
            {
                if (_fields[field] is bool && !(bool)_fields[field])
                    return null;

                return _fields[field];
            }
            else
            {
                return null;
            }
        }

        public int Id { get; private set; } = -1;

        public void Save()
        {
            XmlRpcStruct values = new XmlRpcStruct();

            if (Id >= 0)
            {
                foreach (var field in _modifiedFields)
                {
                    values[field] = _fields[field];
                }

                _api.Write(_model, new int[1] { Id }, values);
            }
            else
            {
                foreach (var field in _fields.Keys)
                {
                    values[field] = _fields[field];
                }

                Id = _api.Create(_model, values);
            }
        }
    }
}
