using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    public class PropertyDataInfo
    {
        public PropertyDataInfo(string propertyName)
        {
            this.Ignore = false;
            this.CanInsert = true;
            this.CanUpdate = true;
            this.IsPrimaryKey = false;
            this.IsIncreasePrimaryKey = false;
            this.PropertyName = propertyName;
            this.ColumnName = PropertyName;
        }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 是否为自增主键
        /// </summary>
        public bool IsIncreasePrimaryKey { get; set; }
        public int PrimaryKeyIndex { get; set; }
        public Func<Condition, Condition> Search;
        /// <summary>
        /// 增改查是忽略
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool CanUpdate { get; set; }
        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool CanInsert { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 对应的数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否是关系字段，即值是否来源其它表
        /// </summary>
        public bool IsRelation { get; set; }
        /// <summary>
        /// 关联表别名
        /// </summary>
        public string TableAlias { get; set; }
    }

    public class PropertyDataInfoHelper
    {
        PropertyDataInfo _DataConig;
        IDataViewMetaData _ViewMetaData;
        public PropertyDataInfoHelper(PropertyDataInfo item, IDataViewMetaData ViewMetaData)
        {
            _DataConig = item;
            _ViewMetaData = ViewMetaData;
        }
        /// <summary>
        /// 完全忽略，增改查都不管
        /// </summary>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper Ignore(bool? ignore = true)
        {
            bool nore = ignore ?? true;
            _DataConig.Ignore = nore;
            return this;
        }
        /// <summary>
        /// 是否可更新
        /// </summary>
        /// <param name="canUpdate"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper Update(bool? canUpdate = false)
        {
            bool update = canUpdate ?? false;
            _DataConig.CanUpdate = update;
            return this;
        }
        /// <summary>
        /// 是否可插入
        /// </summary>
        /// <param name="canInsert"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper Insert(bool? canInsert = false)
        {
            bool insert = canInsert ?? false;
            _DataConig.CanInsert = insert;
            return this;
        }
        /// <summary>
        /// 将字段和数据库列进行映射，如果名称不一样的话。
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper Mapper(string column)
        {
            _DataConig.ColumnName = column;
            return this;
        }
        /// <summary>
        /// 关联关系
        /// </summary>
        /// <param name="alias">表别名</param>
        /// <returns></returns>
        public PropertyDataInfoHelper Relation(string alias)
        {
            _DataConig.IsRelation = true;
            _DataConig.TableAlias = alias;
            this.Insert(false);
            this.Update(false);
            return this;
        }
        /// <summary>
        /// 列表搜索的特别处理,自定义条件ConditionString，条件的值，会自动Format进表达式
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper OnSearch(Func<Condition, Condition> fun)
        {
            _DataConig.Search = fun;
            return this;
        }
        /// <summary>
        /// 设置主键
        /// </summary>
        /// <returns></returns>
        public PropertyDataInfoHelper AsPrimaryKey()
        {
            if (!_DataConig.IsPrimaryKey)
            {
                _DataConig.PrimaryKeyIndex = _ViewMetaData.PrimarykeyCount;
                _ViewMetaData.PrimarykeyCount++;
                _DataConig.IsIncreasePrimaryKey = false;
                _DataConig.IsPrimaryKey = true;
                Update(false);
            }
            return this;
        }
        /// <summary>
        /// 设置为自增主键
        /// </summary>
        /// <returns></returns>
        public PropertyDataInfoHelper AsIncreasePrimaryKey()
        {
            if (!_DataConig.IsPrimaryKey)
            {
                _DataConig.PrimaryKeyIndex = _ViewMetaData.PrimarykeyCount;
                _ViewMetaData.PrimarykeyCount++;
                _DataConig.IsIncreasePrimaryKey = true;
                _DataConig.IsPrimaryKey = true;
                Insert(false);
                Update(false);
            }
            return this;
        }
    }
}
