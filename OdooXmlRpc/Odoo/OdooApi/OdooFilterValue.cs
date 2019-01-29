using System.Collections;

namespace OdooXmlRpc.Odoo.OdooApi
{
    public class OdooFilterValue : ArrayList
    {
        public OdooFilterValue AddValue(object value)
        {
            Add(value);
            return this;
        }
    }
}