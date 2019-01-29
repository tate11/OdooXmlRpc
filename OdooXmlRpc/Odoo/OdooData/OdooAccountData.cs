using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooAccountData : OdooBaseDataModel
    {
        public OdooAccountData(OdooApi.OdooApi odooApi) : base(odooApi, "account.account")
        {
        }
    }
}
