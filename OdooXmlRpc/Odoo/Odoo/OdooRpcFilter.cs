using System.Collections;
using System.Collections.Generic;

namespace OdooXmlRpc.Odoo.Odoo
{
    public class OdooRpcFilter : ArrayList
    {
        public OdooRpcFilter Equal(string fieldName, object value)
        {
            var field = new object[] { fieldName, "=", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter Or()
        {
            Add("|");
            return this;
        }

        public OdooRpcFilter Not()
        {
            Add("!");
            return this;
        }

        public OdooRpcFilter And()
        {
            Add("&");
            return this;
        }

        public OdooRpcFilter ILike(string fieldName, object value)
        {
            var field = new object[] { fieldName, "ilike", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter Like(string fieldName, object value)
        {
            var field = new object[] { fieldName, "like", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter NotLike(string fieldName, object value)
        {
            var field = new object[] { fieldName, "not like", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter NotEqual(string fieldName, object value)
        {
            var field = new object[] { fieldName, "!=", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter GreaterThan(string fieldName, object value)
        {
            var field = new object[] { fieldName, ">", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter GreaterThanOrEqual(string fieldName, object value)
        {
            var field = new object[] { fieldName, ">=", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter LessThan(string fieldName, object value)
        {
            var field = new object[] { fieldName, "<", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter LessThanOrEqual(string fieldName, object value)
        {
            var field = new object[] { fieldName, "<=", value };
            Add(field);
            return this;
        }

        public OdooRpcFilter Between(string fieldName, object value1, object value2)
        {
            var field = new object[] { fieldName, "between", value1, "and", value2 };
            Add(field);
            return this;
        }

        public OdooRpcFilter In(string fieldName, OdooRpcFilterValue value)
        {
            var field = new object[] { fieldName, "in", value.ToArray() };
            Add(field);
            return this;
        }

        public OdooRpcFilter In(string fieldName, object[] values)
        {
            var field = new object[] { fieldName, "in", values };
            Add(field);
            return this;
        }

        public OdooRpcFilter In(string fieldName, List<object> values)
        {
            var field = new object[] { fieldName, "in", values.ToArray() };
            Add(field);
            return this;
        }

        public OdooRpcFilter In(string fieldName, int[] values)
        {
            var field = new object[] { fieldName, "in", values };
            Add(field);
            return this;
        }


        public OdooRpcFilter NotIn(string fieldName, OdooRpcFilterValue value)
        {
            var field = new object[] { fieldName, "not in", value.ToArray() };
            Add(field);
            return this;
        }

        public OdooRpcFilter NotIn(string fieldName, object[] values)
        {
            var field = new object[] { fieldName, "not in", values };
            Add(field);
            return this;
        }

        public OdooRpcFilter NotIn(string fieldName, List<object> values)
        {
            var field = new object[] { fieldName, "not in", values.ToArray() };
            Add(field);
            return this;
        }

        public OdooRpcFilter NotIn(string fieldName, int[] values)
        {
            var field = new object[] { fieldName, "not in", values };
            Add(field);
            return this;
        }

        public OdooRpcFilter ChildOf(string fieldName, string values)
        {
            var field = new object[] { fieldName, "child_of", values };
            Add(field);
            return this;
        }
    }
}