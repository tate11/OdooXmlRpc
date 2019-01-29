using System;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public abstract class OdooBaseMap
    {
        protected OdooBaseEntity ArrayToObject(object value)
        {
            var entity = new OdooBaseEntity();

            if (!(value is Array array))
            {
                entity.name = value;

            }
            else
            {
                entity.id = ((object[])array)[0];
                entity.name = ((object[])array)[1];
            }

            return entity;
        }
    }
}
