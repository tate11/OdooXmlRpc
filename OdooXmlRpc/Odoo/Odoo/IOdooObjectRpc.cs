﻿using System;
using CookComputing.XmlRpc;

namespace OdooXmlRpc.Odoo.Odoo
{
    [XmlRpcUrl("object")]
    public interface IOdooObjectRpc : IXmlRpcProxy
    {
        [XmlRpcMethod("execute")]
        int create(String dbName, int userId, string dbPwd, string model, string method, XmlRpcStruct fieldValues);

        [XmlRpcMethod("execute")]
        int[] search(string dbName, int userId, string dbPwd, string model, string method, object[] filter, int? offset = null, int? limit = null);

        [XmlRpcMethod("execute")]
        object[] search_read(string dbName, int userId, string dbPwd, string model, string method, object[] filter, object[] fields, int? offset = null, int? limit = null);

        [XmlRpcMethod("execute")]
        int search_count(string dbName, int userId, string dbPwd, string model, string method, object[] filter);

        [XmlRpcMethod("execute")]
        object[] read(string dbName, int userId, string dbPwd, string model, string method, int[] ids, object[] fields);

        [XmlRpcMethod("execute")]
        bool write(string dbName, int userId, string dbPwd, string model, string method, int[] ids, XmlRpcStruct fieldValues);

        [XmlRpcMethod("execute")]
        bool unlink(string dbName, int userId, string dbPwd, string model, string method, int[] ids);
     
        [XmlRpcMethod("exec_workflow")]
        bool exec_workflow(string dbName, int userId, string dbPwd, string model, string action, int ids);

        [XmlRpcMethod("render_report")]
        string render_report(string dbName, int userId, string dbPwd, string report, int ids);
    }
}
