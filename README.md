OdooXmlRpc .Net
================

Description
-----------
XmlRpc Web Service Client .NET is a C# implementation of XML-RPC, a popular
protocol that uses XML over HTTP to implement remote procedure calls.
This implementation can be used in .NET 4.5
This software was tested with Odoo ERP 8 and 11
Examples are available in the sandbox folder.


Features
--------
- Copyright: 2019 İsmail Eski <ismaileski@gmail.com>
- Repository: https://github.com/ieski/OdooXmlRpc
- License: LGPL 3
- Language: C#, .NET 4.5
- IDE: Visual Studio 2017
- Version: v1.0.0


Links
-----
- http://xmlrpc.scripting.com/
- https://en.wikipedia.org/wiki/XML-RPC


Example Query ResPartner
--------------------------------
- Source:

```cs
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooData;
using System;

namespace OdooXmlRpc
{
    class Program
    {
        static void Main(string[] args)
        {

            var cred = new OdooConnectionCredentials(
                "serverUrl",
                "dbName",
                "dbUser",
                "dbPassword");

            var api = new Odoo.Odoo.OdooRpc(cred);
            var context = new OdooContext(api);


            var resPartner = context.ResPartner;

            resPartner.Filter.Equal("vat", "TR28163539052");

            resPartner
                .AddField("id")
                .AddField("name")
                .AddField("child_ids");


            var data = resPartner.Execute(true,1,100);

            //Result to XML
            var xml = resPartner.ToXml();

            //Dynamic Access Entity
            data.ForEach(x =>
            {
                Console.WriteLine(((dynamic)x).name);
            });

            //Thread
            var data1 = resPartner.ExecuteAsync(5, 10);

            var data2 = resPartner.ExecuteThread(10, 500);
        }
    }
}

```


