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
