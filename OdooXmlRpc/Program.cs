using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdooXmlRpc.Odoo.OdooApi;
using OdooXmlRpc.Odoo.OdooData;

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

            var api = new OdooApi(cred);

            var entity = new OdooAccountData(api);

            //entity.Filter.Equal("code", "10201002");

            entity.AddField("code")
                .AddField("name");


            var data = entity.Execute(true);

            var data1 = entity.ExecuteAsync(5, 500);

            var data2 = entity.ExecuteThread(5, 500);
        }
    }
}
