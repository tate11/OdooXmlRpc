namespace OdooXmlRpc.Odoo.OdooEntity
{
    public class OdooResPartner:IEntity
    {
        public int id { get; set; }
        public bool customer { get; set; }
        public bool supplier { get; set; }
        public object name { get; set; }
        public object email { get; set; }
        public object phone { get; set; }
        public OdooBaseEntity tax_office_id { get; set; }
        public object vat { get; set; }
        public object street { get; set; }
        public object street2 { get; set; }
        public object Zip { get; set; }
        public OdooBaseEntity state_id { get; set; }
        public OdooBaseEntity county_id { get; set; }
        public OdooBaseEntity country_id { get; set; }
        public bool active { get; set; }
    }
}
