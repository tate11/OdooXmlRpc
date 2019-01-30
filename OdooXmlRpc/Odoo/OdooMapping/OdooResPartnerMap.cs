using System;
using System.Collections.Generic;
using System.Linq;
using OdooXmlRpc.Odoo.Odoo;
using OdooXmlRpc.Odoo.OdooEntity;

namespace OdooXmlRpc.Odoo.OdooMapping
{
    public class OdooResPartnerMap : OdooBaseMap, IOdooBaseMap<OdooResPartner>
    {
        public List<OdooResPartner> Mapping(List<OdooRecord> records)
        {
            return records.Select(x => new OdooResPartner()
            {
                id = x.Id,
                customer = Convert.ToBoolean(x.GetValue("customer")),
                supplier = Convert.ToBoolean(x.GetValue("supplier")),
                name = x.GetValue("name"),
                email = x.GetValue("email"),
                phone = x.GetValue("phone"),
                tax_office_id = ArrayToObject(x.GetValue("tax_office_id")),
                vat = x.GetValue("vat"),
                street = x.GetValue("street"),
                street2 = x.GetValue("street2"),
                Zip = x.GetValue("zip"),
                state_id = ArrayToObject(x.GetValue("state_id")),
                county_id = ArrayToObject(x.GetValue("county_id")),
                country_id = ArrayToObject(x.GetValue("state_id")),
                active = Convert.ToBoolean(x.GetValue("active")),
            }).ToList();
        }
    }
}
