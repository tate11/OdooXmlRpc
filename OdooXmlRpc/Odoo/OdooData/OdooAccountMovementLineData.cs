using OdooXmlRpc.Odoo.OdooApi;

namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooAccountMovementLineData : OdooBaseDataModel
    {
        public OdooAccountMovementLineData(OdooApi.OdooApi odooApi) : base(odooApi, "account.move.line")
        {
        }
    }
}
