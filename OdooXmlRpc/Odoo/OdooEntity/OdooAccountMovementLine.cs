using System;

namespace OdooXmlRpc.Odoo.OdooEntity
{
    public class OdooAccountMovementLine : IEntity
    {
        public int id { get; set; }
        public OdooBaseEntity partner_id { get; set; }
        public DateTime date { get; set; }
        public OdooBaseEntity move_id { get; set; }
        public object refs { get; set; }
        public decimal line_balance { get; set; }
        public decimal amount_currency { get; set; }
        public OdooBaseEntity currency_id { get; set; }
    }
}
