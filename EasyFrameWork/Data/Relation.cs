using Easy.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    public class Relation
    {
        /// <summary>
        /// 关联表
        /// </summary>
        public string RelationTable { get; set; }
        /// <summary>
        /// 关联关系
        /// </summary>
        public DataEnumerate.RelationType RelationType { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 关联条件
        /// </summary>
        public string Conditions { get; set; }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            switch (RelationType)
            {
                case DataEnumerate.RelationType.InnerJoin:
                    builder.Append("INNER JOIN");
                    break;
                case DataEnumerate.RelationType.LeftJoin:
                    builder.Append("LEFT JOIN");
                    break;
                case DataEnumerate.RelationType.RightJoin:
                    builder.Append("RIGHT JOIN");
                    break;
                case DataEnumerate.RelationType.LeftOuterJoin:
                    builder.Append("LEFT OUTER JOIN");
                    break;
                case DataEnumerate.RelationType.RightOuterJoin:
                    builder.Append("RIGHT OUTER JOIN");
                    break;
                case DataEnumerate.RelationType.FullJoin:
                    builder.Append("FULL JOIN");
                    break;
                case DataEnumerate.RelationType.FullOuterJoin:
                    builder.Append("FULL OUTER JOIN");
                    break;
                default:
                    builder.Append("LEFT JOIN");
                    break;
            }
            builder.Append(" ");
            builder.Append(RelationTable);
            builder.Append(" ");
            builder.Append(Alias);
            builder.Append(" ON ");
            builder.Append(Conditions);
            builder.Append(" ");
            return builder.ToString();
        }
    }

    public class RelationHelper
    {
        List<Relation> relations;
        public RelationHelper(List<Relation> relation)
        {
            this.relations = relation;
        }
        public RelationHelper InnerJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.InnerJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }

        public RelationHelper LeftJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.LeftJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper RightJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.RightJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper LeftOuterJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.LeftOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper RightOuterJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.RightOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper FullJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.FullJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper FullOuterJoin(string table, string alias, string condition)
        {
            relations.Add(new Relation()
            {
                RelationTable = table,
                RelationType = DataEnumerate.RelationType.FullOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
    }
}
