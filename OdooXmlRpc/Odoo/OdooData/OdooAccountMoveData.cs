using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooAccountMoveData : OdooBaseDataModel
    {
        public OdooAccountMoveData(OdooApi.OdooApi odooApi) : base(odooApi, "account.move")
        {
        }
    }
}
