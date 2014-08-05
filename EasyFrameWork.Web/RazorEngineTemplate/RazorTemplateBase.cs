using System;
using System.Text;

namespace Easy.Web.RazorEngineTemplate
{
    public abstract class RazorTemplateBase<T>
    {
        public T Model { get; set; }

        public StringBuilder Buffer { get; set; }

        protected RazorTemplateBase()
        {
            Buffer = new StringBuilder();
        }

        public abstract void Execute();

        public virtual void Write(object value)
        {
            WriteLiteral(value);
        }

        public virtual void WriteLiteral(object value)
        {
            Buffer.Append(value);
        }

        public virtual void WriteAttribute(string name, Tuple<String, int> attrStart, Tuple<String, int> attrEnd,
            Tuple<Tuple<string, int>, Tuple<Object, int>, bool> value)
        {
            WriteLiteral(attrStart.Item1);
            WriteLiteral(value.Item2.Item1);
            WriteLiteral(attrEnd.Item1);
        }

    }
}
