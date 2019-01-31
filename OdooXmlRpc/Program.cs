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

            //entity.Filter.Equal("vat", "TR28163539052");

            resPartner
                .AddField("id")
                .AddField("name")
                .AddField("child_ids");


            var data = resPartner.Execute(true,1,100);

            //Dynamic Access Entity
            data.ForEach(x =>
            {
                Console.WriteLine(((dynamic)x).name);
            });

            //var data1 = entity.ExecuteAsync(5, 10);

            //var data2 = entity.ExecuteThread(10, 500);
        }
    }
}
