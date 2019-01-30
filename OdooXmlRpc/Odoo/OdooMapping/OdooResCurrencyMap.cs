using System.Collections.Generic;
using System.Linq;
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public class OdooResCurrencyMap : OdooBaseMap, IOdooBaseMap<OdooResCurrency>
    {
        public List<OdooResCurrency> Mapping(List<OdooRecord> records)
        {
            return records.Select(x => new OdooResCurrency()
            {
                id = x.Id,
                name = x.GetValue("name"),
            }).ToList();
        }
    }
}
