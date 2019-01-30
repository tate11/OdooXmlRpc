using System;
using System.Net;

namespace OdooXmlRpc.Odoo.OdooApi
{
    public class OdooConnectionCredentials
    {
        private readonly string _suffix_host_url = "xmlrpc";
        private readonly string _common_url = "common";
        private readonly string _object_url = "object";
        private readonly string _report_url = "report";

        private int _UserId = -1;

        public OdooConnectionCredentials(string serverUrl, string dbName, string dbUser, string dbPassword)
        {
            this.ServerUrl = serverUrl;
            this.DbName = dbName;
            this.DbUser = dbUser;
            this.DbPassword = dbPassword;

            //TLS v.1.1 ve v.1.2 desteği
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        public string ServerUrl { get; private set; }
        public string DbName { get; private set; }
        public string DbUser { get; private set; }
        public string DbPassword { get; private set; }
        public int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }

        public string CommonUrl
        {
            get
            {
                return String.Format("{0}/{1}/{2}", ServerUrl, _suffix_host_url, _common_url);
            }
        }

        public string ObjectUrl
        {
            get
            {
                return String.Format("{0}/{1}/{2}", ServerUrl, _suffix_host_url, _object_url);
            }
        }

        public string ReportUrl
        {
            get
            {
                return String.Format("{0}/{1}/{2}", ServerUrl, _suffix_host_url, _report_url);
            }
        }

    }
}
