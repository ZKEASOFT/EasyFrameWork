using Easy.Web.Resource;
using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.Page
{
    public class ViewPage<TModel> : WebViewPage<TModel>
    {
        public const string PartScriptKey = "ViewDataKey_PartScript";
        public const string PartStyleKey = "ViewDataKey_PartStyle";

        public IHtmlString ScriptAtHead()
        {
            return GetResource(PartScriptKey, ResourcePosition.Head);
        }

        public IHtmlString ScriptAtFoot()
        {
            return GetResource(PartScriptKey, ResourcePosition.Foot);
        }

        public IHtmlString StyleAtHead()
        {
            return GetResource(PartStyleKey, ResourcePosition.Head);
        }

        public IHtmlString StyleAtFoot()
        {
            return GetResource(PartStyleKey, ResourcePosition.Foot);
        }

        private IHtmlString GetResource(string key, ResourcePosition position)
        {
            var writer = new HtmlStringWriter();
            switch (key)
            {
                case PartScriptKey:
                    ResourceManager.ScriptSource.Where(m => m.Value.Required && m.Value.Position == position)
                        .Each(m => m.Value.Each(r => writer.WriteLine(r.ToSource(this, this.Context))));break;
                case PartStyleKey:
                    ResourceManager.StyleSource.Where(m => m.Value.Required && m.Value.Position == position)
                        .Each(m => m.Value.Each(r => writer.WriteLine(r.ToSource(this, this.Context))));break;
            }
            if (ViewData.ContainsKey(key))
            {
                var source = ViewData[key] as ResourceCollection;
                if (source != null)
                {
                    var headSource = source.Where(m => m.Position == position);
                    foreach (ResourceEntity item in headSource)
                    {
                        writer.WriteLine(item.ToSource(this, this.Context));
                    }
                }
            }
            return writer;
        }

        private ScriptRegister _script;
        public ScriptRegister Script
        {
            get
            {
                return _script ?? (_script = new ScriptRegister(this, appendResourceAction));
            }
        }

        private StyleRegister _style;
        public StyleRegister Style
        {
            get
            {
                return _style ?? (_style = new StyleRegister(this, appendResourceAction));
            }
        }
        private void appendResourceAction(ResourceEntity resource, string key)
        {
            List<ResourceEntity> source = null;
            if (ViewData.ContainsKey(key))
            {
                source = ViewData[key] as ResourceCollection;
            }
            else
            {
                source = new ResourceCollection();
            }
            source.Add(resource);
            ViewData[key] = source;
        }



        public override void Execute()
        {
           
        }
    }

    public class ViewPage : ViewPage<dynamic>
    {
        public ViewPage()
        {

        }
    }

}
