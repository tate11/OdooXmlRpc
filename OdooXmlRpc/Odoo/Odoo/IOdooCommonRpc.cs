using System;
using CookComputing.XmlRpc;

namespace OdooXmlRpc.Odoo.Odoo
{
    [XmlRpcUrl("common")]
    public interface IOdooCommonRpc : IXmlRpcProxy
    {
        [XmlRpcMethod("login")]
        int login(String dbName, string dbUser, string dbPwd);
    }
}
