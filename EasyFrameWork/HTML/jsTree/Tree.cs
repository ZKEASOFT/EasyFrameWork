using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Easy.Extend;

namespace Easy.HTML.jsTree
{

    public class Tree<T> where T : class
    {
        IEnumerable<T> DataSource;
        string _sourceUrl = "";
        Func<T, string> valueProperty;
        Func<T, string> parentProperty;
        Func<T, string> textProperty;
        Dictionary<string, string> _events = new Dictionary<string, string>();
        Dictionary<string, string> _plugs = new Dictionary<string, string>();
        List<Node> nodes;
        string _rootId;
        string name;
        public Tree()
        {
            this.name = Guid.NewGuid().ToString("N");
        }
        public Tree(string name)
        {
            this.name = name;
        }

        #region 公用方法

        public Tree<T> Source(IEnumerable<T> source)
        {
            this.DataSource = source;
            return this;
        }

        public virtual Tree<T> Source(string url)
        {
            _sourceUrl = url;
            return this;
        }


        public Tree<T> ShowCheckBox()
        {

            return this;
        }

        #endregion
        public Tree<T> On(string events, string fun)
        {
            if (_events.ContainsKey(events))
            {
                _events[events] = fun;
            }
            else
            {
                _events.Add(events, fun);
            }
            return this;
        }
        public Tree<T> Id(Expression<Func<T, string>> value)
        {
            valueProperty = value.Compile();
            return this;
        }
        public Tree<T> Text(Expression<Func<T, string>> text)
        {
            textProperty = text.Compile();
            return this;
        }
        public Tree<T> Parent(Expression<Func<T, string>> parent)
        {
            parentProperty = parent.Compile();
            return this;
        }

        public Tree<T> RootId(string rootId)
        {
            _rootId = rootId;
            return this;
        }

        public List<Node> ToNode(Expression<Func<T, string>> value, Expression<Func<T, string>> text, Expression<Func<T, string>> parent, string rootId)
        {
            valueProperty = value.Compile();
            parentProperty = parent.Compile();
            textProperty = text.Compile();
            _rootId = rootId;
            InitDode();
            return nodes;
        }

        public override string ToString()
        {
            InitDode();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<div id=\"{0}\"></div><script type='text/javascript'>$(function(){{ $('#{0}')", this.name);
            foreach (var item in _events)
            {
                builder.AppendFormat(".on('{0}',{1})", item.Key, item.Value);
            }
            string source = "{'url' : '" + _sourceUrl + "','data' : function (node) {return { 'id' : node.id };}}";
            if (nodes != null && nodes.Count > 0)
            {
                source = Newtonsoft.Json.JsonConvert.SerializeObject(nodes);
            }
            builder.AppendFormat(".jstree({{'core':{{data:{0}}}}});", source);
            builder.Append("});</script>");
            return builder.ToString();
        }
        private void InitDode()
        {
            if (nodes == null && DataSource != null)
            {
                nodes = new List<Node>();
                DataSource
               .Where(m => parentProperty(m) == _rootId)
               .Each(m =>
               {
                   nodes.Add(InitNode(m));
               });
            }
        }
        private Node InitNode(T data)
        {
            Node node = new Node();
            node.id = valueProperty(data);
            node.text = textProperty(data);
            node.state = new State { opened = true };
            node.children = new List<Node>();
            DataSource.Where(m => parentProperty(m) == node.id).Each(m => node.children.Add(InitNode(m)));
            return node;
        }
    }
}
