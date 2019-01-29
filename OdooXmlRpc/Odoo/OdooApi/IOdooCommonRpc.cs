using System;
using CookComputing.XmlRpc;

namespace OdooXmlRpc.Odoo.OdooApi
{
    [XmlRpcUrl("common")]
    public interface IOdooCommonRpc : IXmlRpcProxy
    {
        [XmlRpcMethod("login")]
        int login(String dbName, string dbUser, string dbPwd);
    }
}
