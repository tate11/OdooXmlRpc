using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooResPartnerData : OdooBaseDataModel
    {
        public OdooResPartnerData(OdooApi.OdooApi odooApi) : base(odooApi, "res.partner")
        {
        }
    }
}
