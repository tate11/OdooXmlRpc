using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooAccountTypeData : OdooBaseDataModel
    {
        public OdooAccountTypeData(OdooApi.OdooApi odooApi) : base(odooApi, "account.account.type")
        {
        }
    }
}
