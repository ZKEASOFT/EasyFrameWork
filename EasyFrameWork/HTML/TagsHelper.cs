using Easy.HTML.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easy.HTML
{

    public class TagsHelper
    {
        private readonly string _key;
        private readonly Dictionary<string, HtmlTagBase> _attributes;
        private readonly Type _modelType;
        private readonly Type _dataType;
        private readonly PropertyInfo _targetType;
        public TagsHelper(string Key, ref Dictionary<string, HtmlTagBase> Attributes, Type modelType, PropertyInfo targetType)
        {
            _key = Key;
            _attributes = Attributes;
            _targetType = targetType;
            _modelType = modelType;
            _dataType = targetType.PropertyType.IsGenericType ? targetType.PropertyType.GetGenericArguments()[0] : targetType.PropertyType;
        }
        /// <summary>
        /// 基本输入框
        /// </summary>
        /// <returns></returns>
        public TextBoxHtmlTag AsTextBox()
        {
            TextBoxHtmlTag tag = new TextBoxHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 基本输入框
        /// </summary>
        /// <returns></returns>
        public MutiLineTextBoxHtmlTag AsMutiLineTextBox()
        {
            MutiLineTextBoxHtmlTag tag = new MutiLineTextBoxHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        public DropDownListHtmlTag AsDropDownList()
        {
            DropDownListHtmlTag tag = new DropDownListHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public FileHtmlTag AsFileUp()
        {
            FileHtmlTag tag = new FileHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 多选项
        /// </summary>
        /// <returns></returns>
        public MutiSelectHtmlTag AsMutiSelect()
        {
            MutiSelectHtmlTag tag = new MutiSelectHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 密码输入框
        /// </summary>
        /// <returns></returns>
        public PassWordHtmlTag AsPassWord()
        {
            PassWordHtmlTag tag = new PassWordHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 隐藏框
        /// </summary>
        /// <returns></returns>
        public HiddenHtmlTag AsHidden()
        {
            HiddenHtmlTag tag = new HiddenHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        /// <summary>
        /// 勾选框
        /// </summary>
        /// <returns></returns>
        public CheckBoxHtmlTag AsCheckBox()
        {
            CheckBoxHtmlTag tag = new CheckBoxHtmlTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
        public CollectionAreaTag AsCollectionArea()
        {
            CollectionAreaTag tag = new CollectionAreaTag(_modelType, _key);
            if (_attributes.ContainsKey(this._key))
            {
                _attributes.Remove(this._key);
            }
            tag.DataType = _dataType;
            _attributes.Add(this._key, tag);
            return tag;
        }
    }
}
