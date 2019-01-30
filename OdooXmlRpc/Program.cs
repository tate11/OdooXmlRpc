using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooData;
using System;
using System.Linq;

namespace OdooXmlRpc
{
    class Program
    {
        static void Main(string[] args)
        {

            var cred = new OdooConnectionCredentials(
                "url",
                "dbname",
                "user",
                "password");

            var api = new Odoo.Odoo.OdooRpc(cred);


            var context = new OdooContext(api);

            var entity = context.ResPartner;

            //entity.Filter.Equal("code", "10201002");

            entity.Filter.ILike("property_account_payable", "320");

            entity.AddField("id")
                .AddField("name")
                .AddField("property_account_receivable");

            var start = DateTime.Now;

            var data = entity.Execute(true,1,10);

            var end = DateTime.Now;

            var dif = end - start;

            //var data1 = entity.ExecuteAsync(5, 10);

            //var data2 = entity.ExecuteThread(10, 500);

            Console.WriteLine("Dakika:{0} - Saniye:{1}",dif.TotalMinutes,dif.TotalSeconds);

            var a = entity.GetRecords();
            var b = a.Distinct().ToList();
        }
    }
}
