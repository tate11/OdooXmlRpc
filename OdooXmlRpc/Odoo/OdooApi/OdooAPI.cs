using System.Net;
using System.Security.Cryptography.X509Certificates;
using CookComputing.XmlRpc;

namespace OdooXmlRpc.Odoo.OdooApi
{
    public class OdooApi
    {
        private readonly OdooConnectionCredentials _credentials;
        private readonly WebProxy _networkProxy;
        private readonly bool _serverCertificateValidation;
        private IOdooObjectRpc _objectRpc;

        public OdooApi(OdooConnectionCredentials credentials, bool immediateLogin = true, WebProxy networkProxy = null, bool serverCertificateValidation = true)
        {
            _serverCertificateValidation = serverCertificateValidation;
            _networkProxy = networkProxy;
            _credentials = credentials;

            if (immediateLogin)
            {
                Login();
            }
        }

        public bool Login()
        {
            if (_credentials.UserId != -1) return true;

            IOdooCommonRpc loginRpc = XmlRpcProxyGen.Create<IOdooCommonRpc>();
            loginRpc.Url = _credentials.CommonUrl;

            if (_networkProxy != null)
            {
                loginRpc.Proxy = _networkProxy;
            }

            if (_serverCertificateValidation)
            {
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }

            // Log in and get user id
            _credentials.UserId = loginRpc.login(_credentials.DbName, _credentials.DbUser, _credentials.DbPassword);

            // Create proxy for Object operations
            _objectRpc = XmlRpcProxyGen.Create<IOdooObjectRpc>();
            _objectRpc.Url = _credentials.ObjectUrl;
            _objectRpc.NonStandard = XmlRpcNonStandard.AllowStringFaultCode;
            _objectRpc.EnableCompression = false;
            return true;
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // Implement Server Certificate validation logic here, if needed.
            return true;
        }

        public int Create(string model, XmlRpcStruct fieldValues)
        {
            return _objectRpc.create(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "create", fieldValues);
        }

        public int[] Search(string model, object[] filter, int? offset = null, int? limit = null)
        {
            return _objectRpc.search(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "search", filter, offset, limit);
        }

        public object[] SearchAndRead(string model, object[] filter, object[] fields, int? offset = null, int? limit = null)
        {
            return _objectRpc.search_read(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "search_read", filter, fields, offset, limit);
        }

        public int Count(string model, object[] filter)
        {
            return _objectRpc.search_count(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "search_count", filter);
        }

        public object[] Read(string model, int[] ids, object[] fields)
        {
            return _objectRpc.read(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "read", ids, fields);
        }

        public bool Write(string model, int[] ids, XmlRpcStruct fieldValues)
        {
            return _objectRpc.write(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "write", ids, fieldValues);
        }

        public bool Remove(string model, int[] ids)
        {
            return _objectRpc.unlink(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, "unlink", ids);
        }

        public bool Execute_Workflow(string model, string action, int id)
        {
            return _objectRpc.exec_workflow(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, model, action, id);
        }

        public string Render_Report(string report, int id)
        {
            return _objectRpc.render_report(_credentials.DbName, _credentials.UserId, _credentials.DbPassword, report, id);
        }


        public OdooModel GetModel(string model)
        {
            return new OdooModel(model, this);
        }
    }
}
