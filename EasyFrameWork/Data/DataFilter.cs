using Easy.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Data
{

    public class DataFilter
    {
        public DataFilter()
        {
            _Conditions = new List<Condition>();
            _ConditionGroups = new List<ConditionGroup>();
            _Orders = new List<Order>();
        }
        public DataFilter(List<string> updateProperties)
        {
            _Conditions = new List<Condition>();
            _ConditionGroups = new List<ConditionGroup>();
            _Orders = new List<Order>();
            this.UpdateProperties = updateProperties;
        }
        List<Condition> _Conditions;

        List<ConditionGroup> _ConditionGroups;

        List<Order> _Orders;
        public List<string> UpdateProperties { get; set; }

        public List<ConditionGroup> ConditionGroups
        {
            get { return _ConditionGroups; }
            set { _ConditionGroups = value; }
        }
        public List<Condition> Conditions
        {
            get { return _Conditions; }
            set { _Conditions = value; }
        }
        public List<Order> Orders
        {
            get { return _Orders; }
            set { _Orders = value; }
        }

        #region 条件
        public DataFilter Where(Condition condition)
        {
            _Conditions.Add(condition);
            return this;
        }
        public DataFilter Where(string property, DataEnumerate.OperatorType operatorType, object value)
        {
            _Conditions.Add(new Condition(property, operatorType, value));
            return this;
        }
        public DataFilter Where<T>(Expression<Func<T, object>> expression, DataEnumerate.OperatorType operatorType, object value)
        {
            string property = Common.GetLinqExpressionText(expression);
            Attribute.DataConfigureAttribute attribute = System.Attribute.GetCustomAttribute(typeof(T), typeof(Attribute.DataConfigureAttribute)) as Attribute.DataConfigureAttribute;
            if (attribute != null && attribute.MetaData.PropertyDataConfig.ContainsKey(property))
            {
                string propertyMap = attribute.MetaData.PropertyDataConfig[property].ColumnName;
                if (!string.IsNullOrEmpty(propertyMap))
                    property = propertyMap;
            }
            _Conditions.Add(new Condition(property, operatorType, value));
            return this;
        }
        public DataFilter Where(string condition)
        {
            Condition con = new Condition(condition, DataEnumerate.ConditionType.And);
            _Conditions.Add(con);
            return this;
        }
        public DataFilter Where(string condition, DataEnumerate.ConditionType conditionType)
        {
            Condition con = new Condition(condition, conditionType);
            _Conditions.Add(con);
            return this;
        }
        public DataFilter Where(ConditionGroup conditionGroup)
        {
            this.ConditionGroups.Add(conditionGroup);
            return this;
        }
        #endregion

        #region 排序
        public DataFilter OrderBy(Order item)
        {
            _Orders.Add(item);
            return this;
        }

        public DataFilter OrderBy(string property, DataEnumerate.OrderType order)
        {
            _Orders.Add(new Order(property, order));
            return this;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < ConditionGroups.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(ConditionGroups[i].ToString(true));
                }
                else
                {
                    builder.Append(ConditionGroups[i].ToString());
                }
            }
            for (int i = 0; i < Conditions.Count; i++)
            {
                if (i > 0 || builder.Length > 0)
                {
                    builder.Append(Conditions[i].ToString(true));
                }
                else
                {
                    builder.Append(Conditions[i].ToString());
                }
            }
            return builder.ToString();
        }
        public string GetOrderString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in Orders)
            {
                if (builder.Length == 0)
                {
                    builder.Append(" ORDER BY ");
                    builder.Append(item.ToString());
                }
                else
                {
                    builder.AppendFormat(",{0} ", item.ToString());
                }
            }
            return builder.ToString();
        }
        public string GetContraryOrderString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in Orders)
            {
                if (builder.Length == 0)
                {
                    builder.Append(" ORDER BY ");
                    builder.Append(item.ToString(true));
                }
                else
                {
                    builder.AppendFormat(",{0} ", item.ToString(true));
                }
            }
            return builder.ToString();
        }
        public List<KeyValuePair<string, object>> GetParameterValues()
        {
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();
            foreach (var item in ConditionGroups)
            {
                values.AddRange(item.GetKeyAndValue());
            }
            foreach (var item in Conditions)
            {
                if (item.Value != null)
                {
                    values.Add(item.GetKeyAndValue());
                }
            }
            return values;
        }
    }
}
