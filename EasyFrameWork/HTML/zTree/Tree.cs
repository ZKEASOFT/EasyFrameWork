using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.HTML.zTree
{
    
    public class Tree<T> where T : class
    {
        IEnumerable<T> DataSource;
        Setting setting = new Setting();
        Func<T, string> valueProperty;
        Func<T, string> parentProperty;
        Func<T, string> textProperty;
        Node root;
        string name;
        public Tree()
        {
            this.name = Guid.NewGuid().ToString().Replace("-", "");
            this.InitSetting();
        }
        public Tree(string name)
        {
            this.name = name;
            this.InitSetting();
        }
        public void InitSetting()
        {
            this.setting.callback = new Setting.CallBack();
            this.setting.check = new Setting.Check();
        }
        #region 公用方法
        public Tree<T> Name(string name)
        {
            this.name = name;
            return this;
        }
        public Tree<T> Source(IEnumerable<T> source)
        {
            this.DataSource = source;
            if (this.setting.async == null)
            {
                this.setting.async = new Setting.Async();
                this.setting.async.enable = false;
            }
            return this;
        }

        public virtual Tree<T> Source(string url)
        {
            if (this.setting.async == null)
            {
                this.setting.async = new Setting.Async();
            }
            this.setting.async.enable = true;
            this.setting.async.url = url;
            this.setting.async.autoParam.Add("id");
            return this;
        }

        public Tree<T> Value(Expression<Func<T, string>> expression)
        {
            valueProperty = expression.Compile();
            return this;
        }
        public Tree<T> Parent(Expression<Func<T, string>> expression)
        {
            parentProperty = expression.Compile();
            return this;
        }
        public Tree<T> Text(Expression<Func<T, string>> expression)
        {
            textProperty = expression.Compile();
            return this;
        }
        public Tree<T> ShowCheckBox()
        {
            if (this.setting.check == null)
            {
                this.setting.check = new Setting.Check();
            }
            this.setting.check.chkStyle = "checkbox";
            this.setting.check.enable = true;
            this.setting.check.radioType = "level";
            return this;
        }
        public Tree<T> Root(string text)
        {
            this.root = new Node() { isParent = true, name = text, open = true, id = "0", pId = "0" };
            return this;
        }
        #endregion

        #region 事件
        /// <summary>
        ///单击事件 function(event, treeId, treeNode, clickFlag)
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public Tree<T> OnNodeClick(string function)
        {
            if (this.setting.callback == null)
            {
                this.setting.callback = new Setting.CallBack();
            }
            this.setting.callback.onClick = string.Format("({0})", function);
            return this;
        }
        #endregion
        /// <summary>
        /// 转为树结点
        /// </summary>
        /// <param name="isParent">是否有子级</param>
        /// <returns></returns>
        public List<Node> ToNode(bool isParent)
        {
            List<Node> result = new List<Node>();
            if (this.root != null)
            {
                result.Add(root);
            }
            InitNode(result, isParent);
            return result;
        }
        public string ToNodeJson(bool isParent)
        {
            List<Node> nodes = ToNode(isParent);
            return Newtonsoft.Json.JsonConvert.SerializeObject(nodes);
        }
        public override string ToString()
        {
            setting.data = new Setting.Data();
            setting.data.simpleData = new Setting.SimpleData();
            setting.data.simpleData.enable = true;
            string settingStr = string.Format("var Setting{0}={1}; for (var item in Setting{0}.callback) {{ if (Setting{0}.callback[item]){{Setting{0}.callback[item] = eval(Setting{0}.callback[item]);}}}};", this.name, Newtonsoft.Json.JsonConvert.SerializeObject(setting));
            List<Node> result = new List<Node>();
            if (this.root != null)
            {
                result.Add(root);
            }
            if (this.DataSource != null)
            {
                InitNode(result, false);
            }
            if (result.Count > 0)
            {
                string nodeStr = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return string.Format("<ul id='{0}' class='ztree'></ul><script type='text/javascript'>$(document).ready(function(){{{3}$.fn.zTree.init($('#{0}'), {1}, {2});}});</script>", this.name, string.Format("Setting{0}", this.name), nodeStr, settingStr);
            }
            else
            {
                return string.Format("<ul id='{0}' class='ztree'></ul><script type='text/javascript'>$(document).ready(function(){{{2}$.fn.zTree.init($('#{0}'), {1});}});</script>", this.name, string.Format("Setting{0}", this.name), settingStr);
            }
        }

        private void InitNode(List<Node> result, bool parent)
        {
            if (result == null)
            {
                result = new List<Node>();
            }
            foreach (var item in DataSource)
            {
                Node node = new Node();
                if (valueProperty != null)
                {
                    node.id = valueProperty.Invoke(item);
                }
                else throw new Exception("未设置Node的Value属性，请调用Value方法");
                if (parentProperty != null)
                {
                    node.pId = parentProperty.Invoke(item);
                }
                else throw new Exception("未设置Node的Parent属性，请调用Parent方法");
                if (textProperty != null)
                {
                    node.name = textProperty.Invoke(item);
                }
                else throw new Exception("未设置Node的Text属性，请调用Text方法");
                node.isParent = parent;
                result.Add(node);
            }
        }
    }
}
