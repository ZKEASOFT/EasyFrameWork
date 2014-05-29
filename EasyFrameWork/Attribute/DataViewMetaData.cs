using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Easy.HTML;
using Easy.HTML.Tags;
using Easy.Data;
using Easy.Constant;
using Easy.Models;

namespace Easy.Attribute
{
    /// <summary>
    /// 数据元数据特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataViewMetaData<T> : IDataViewMetaData
    {
        public DataViewMetaData()
        {
            this.Alias = "T0";
            this.TargetType = typeof(T);
            foreach (var item in this.TargetType.GetProperties())
            {
                TypeCode code;
                if (item.PropertyType.Name == "Nullable`1")
                {
                    code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    code = Type.GetTypeCode(item.PropertyType);
                }
                switch (code)
                {
                    case TypeCode.Boolean: ViewConfig(item.Name).AsCheckBox(); break;
                    case TypeCode.Char: ViewConfig(item.Name).AsTextBox().MaxLength(1).RegularExpression(Constant.RegularExpression.Letters); break;
                    case TypeCode.DateTime: ViewConfig(item.Name).AsTextBox().FormatAsDate(); break;
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64: ViewConfig(item.Name).AsTextBox().RegularExpression(Constant.RegularExpression.PositiveIntegersAndZero); break;
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64: ViewConfig(item.Name).AsTextBox().RegularExpression(Constant.RegularExpression.Integer); break;
                    case TypeCode.Object:
                        {
                            ViewConfig(item.Name).AsHidden().Ignore(); break;
                        }
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        {
                            TextBoxHtmlTag tag = ViewConfig(item.Name).AsTextBox().RegularExpression(Easy.Constant.RegularExpression.Float);
                            if (code == TypeCode.Decimal)
                            {
                                tag.Format(FormatStyle.Currency);
                            }
                            break;
                        }
                    case TypeCode.String: ViewConfig(item.Name).AsTextBox().MaxLength(200);
                        break;
                    case TypeCode.DBNull:
                    case TypeCode.Byte:
                    case TypeCode.Empty:
                    default: ViewConfig(item.Name).AsTextBox();
                        break;
                }
                if (code == TypeCode.Object)
                {
                    DataConfig(item.Name).Ignore();
                }
                else
                {
                    DataConfig(item.Name);
                }
            }
            if (typeof(EditorEntity).IsAssignableFrom(this.TargetType))
            {
                ViewConfig("CreateBy").AsHidden();
                ViewConfig("CreatebyName").AsTextBox().Hide();
                ViewConfig("CreateDate").AsTextBox().Hide().FormatAsDateTime();

                ViewConfig("LastUpdateBy").AsHidden();
                ViewConfig("LastUpdateByName").AsTextBox().Hide();
                ViewConfig("LastUpdateDate").AsTextBox().Hide().FormatAsDateTime();

                DataConfig("CreateBy").Update();
                DataConfig("CreatebyName").Update();
                DataConfig("CreateDate").Update();
            }
            if (typeof(IImage).IsAssignableFrom(this.TargetType))
            {
                ViewConfig("ImageUrl").AsTextBox().HideInGrid();
                ViewConfig("ImageThumbUrl").AsTextBox().HideInGrid();
            }
            this.DataConfigure();
            this.ViewConfigure();
        }
        Dictionary<string, HtmlTagBase> _HtmlTags = new Dictionary<string, HtmlTagBase>();
        Dictionary<string, PropertyDataInfo> _PorpertyDataConfig = new Dictionary<string, PropertyDataInfo>();
        List<Relation> _DataRelations = new List<Relation>();
        Dictionary<int, string> _Primarykey;
        public Dictionary<string, HtmlTagBase> HtmlTags
        {
            get { return this._HtmlTags; }
            private set { this._HtmlTags = value; }
        }

        public Type TargetType
        {
            get;
            private set;
        }
        public Dictionary<string, PropertyDataInfo> PropertyDataConfig
        {
            get { return this._PorpertyDataConfig; }
            private set { this._PorpertyDataConfig = value; }
        }
        IUser _user;
        public IUser User
        {
            get
            {
                IApplicationContext app = Easy.IOCAdapter.Loader.CreateInstance<IApplicationContext>();
                if (app != null)
                {
                    _user = app.CurrentUser;
                }
                return _user;
            }
        }

        public List<Relation> DataRelations
        {
            get { return this._DataRelations; }
            private set { this._DataRelations = value; }
        }
        public Dictionary<int, string> Primarykey
        {
            get
            {
                if (_Primarykey != null)
                    return _Primarykey;
                else
                {
                    foreach (var item in _PorpertyDataConfig)
                    {
                        if (item.Value.IsPrimaryKey)
                        {
                            if (_Primarykey == null)
                            {
                                _Primarykey = new Dictionary<int, string>();
                            }
                            _Primarykey.Add(item.Value.PrimaryKeyIndex, item.Value.ColumnName);
                        }
                    }
                    return _Primarykey;
                }
            }
        }

        public int PrimarykeyCount
        {
            get;
            set;
        }
        /// <summary>
        /// 数据表
        /// </summary>
        public string Table
        {
            get;
            private set;
        }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据配置 方法[DataConfig][DataTable][DataPrimaryKey]
        /// </summary>
        public abstract void DataConfigure();
        /// <summary>
        /// 视图配置 方法[ViewConfig]
        /// </summary>
        public abstract void ViewConfigure();
        public virtual DataFilter DataAccess(DataFilter filter)
        {
            return filter;
        }
        /// <summary>
        /// 视图配置，界面显示
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected TagsHelper ViewConfig(Expression<Func<T, object>> ex)
        {
            string key = Common.GetLinqExpressionText(ex);
            return new TagsHelper(key, ref _HtmlTags, TargetType, TargetType.GetProperty(key));
        }
        /// <summary>
        /// 视图配置，界面显示
        /// </summary>
        /// <param name="properyt">实体字段名称</param>
        /// <returns></returns>
        protected TagsHelper ViewConfig(string properyt)
        {
            return new TagsHelper(properyt, ref _HtmlTags, TargetType, TargetType.GetProperty(properyt));
        }
        /// <summary>
        /// 主键
        /// </summary>
        /// <summary>
        ///  数据配置，与数据库对应
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected PropertyDataInfoHelper DataConfig(Expression<Func<T, object>> expression)
        {
            string key = Common.GetLinqExpressionText(expression);
            PropertyDataInfo data;
            if (PropertyDataConfig.ContainsKey(key))
                data = PropertyDataConfig[key];
            else
            {
                data = new PropertyDataInfo(key);
                PropertyDataConfig.Add(key, data);
            }
            return new PropertyDataInfoHelper(data, this);
        }
        /// <summary>
        /// 数据配置，与数据库对应
        /// </summary>
        /// <param name="property">实体字段名称</param>
        /// <returns></returns>
        protected PropertyDataInfoHelper DataConfig(string property)
        {
            PropertyDataInfo data;
            if (PropertyDataConfig.ContainsKey(property))
                data = PropertyDataConfig[property];
            else
            {
                data = new PropertyDataInfo(property);
                data.TableAlias = "T0";
                data.ColumnName = property;
                PropertyDataConfig.Add(property, data);
            }
            return new PropertyDataInfoHelper(data, this);
        }
        /// <summary>
        /// 数据表名称
        /// </summary>
        /// <param name="table">表名称</param>
        protected RelationHelper DataTable(string table)
        {
            this.Table = table;
            return new RelationHelper(this._DataRelations);
        }
        /// <summary>
        /// 设置主键，数据表里的字段名
        /// </summary>
        /// <param name="increment">列名</param>
    }

}
