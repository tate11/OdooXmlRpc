namespace OdooXmlRpc.Odoo.OdooData
{
    public class OdooContext : IOdooContext
    {
        private readonly Odoo.OdooRpc _odooApi;

        public OdooContext(Odoo.OdooRpc odooApi)
        {
            _odooApi = odooApi;
        }

        public OdooDataService Account => new OdooDataService(_odooApi, "account.account");
        public OdooDataService AccountMove => new OdooDataService(_odooApi, "account.move");
        public OdooDataService AccountMoveLine => new OdooDataService(_odooApi, "account.move.line");
        public OdooDataService AccountType=> new OdooDataService(_odooApi, "account.account.type");
        public OdooDataService ResCurrency => new OdooDataService(_odooApi, "res.currency");
        public OdooDataService ResPartner=> new OdooDataService(_odooApi, "res.partner");
    }

    public interface IOdooContext
    {
    }
}
