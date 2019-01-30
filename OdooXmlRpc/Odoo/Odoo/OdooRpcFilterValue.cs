using System.Collections;

namespace OdooXmlRpc.Odoo.Odoo
{
    public class OdooRpcFilterValue : ArrayList
    {
        public OdooRpcFilterValue AddValue(object value)
        {
            Add(value);
            return this;
        }
    }
}