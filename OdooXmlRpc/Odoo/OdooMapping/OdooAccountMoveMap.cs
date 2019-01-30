using System.Collections.Generic;
using System.Linq;
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public class OdooAccountMoveMap : OdooBaseMap, IOdooBaseMap<OdooAccountMove>
    {
        public List<OdooAccountMove> Mapping(List<OdooRecord> records)
        {
            return records.Select(x => new OdooAccountMove()
            {
                id = x.Id,
                document_number = x.GetValue("document_number"),
            }).ToList();
        }
    }
}
