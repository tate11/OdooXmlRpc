using System.Collections.Generic;
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public interface IOdooBaseMap<T> where T: IEntity
    {
        List<T> Mapping(List<OdooRecord> records);
    }
}
