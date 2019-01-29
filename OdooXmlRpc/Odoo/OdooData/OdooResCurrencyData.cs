using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooResCurrencyData : OdooBaseDataModel
    {
        public OdooResCurrencyData(OdooApi.OdooApi odooApi) : base(odooApi, "res.currency")
        {
        }
    }
}
