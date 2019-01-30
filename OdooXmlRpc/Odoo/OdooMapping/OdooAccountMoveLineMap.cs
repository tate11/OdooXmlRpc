using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public class OdooAccountMoveLineMap : OdooBaseMap, IOdooBaseMap<OdooAccountMovementLine>
    {
        public List<OdooAccountMovementLine> Mapping(List<OdooRecord> records)
        {
            try
            {
                return records.Select(x => new OdooAccountMovementLine()
                {
                    id = x.Id,
                    partner_id = ArrayToObject(x.GetValue("partner_id")),
                    date = DateTime.ParseExact(x.GetValue("date").ToString(), "yyyy-MM-dd", CultureInfo.CurrentCulture),
                    move_id = ArrayToObject(x.GetValue("move_id")),
                    refs = x.GetValue("ref"),
                    line_balance = Convert.ToDecimal(x.GetValue("line_balance")),
                    amount_currency = Convert.ToDecimal(x.GetValue("amount_currency")),
                    currency_id = ArrayToObject(x.GetValue("currency_id")),
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}
