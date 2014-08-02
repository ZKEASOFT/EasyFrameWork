using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter
{
    public class ImplType
    {
        public ImplType(Type type)
        {
            this.TargeType = type;
        }
        public ImplType(Type type, bool oneInstance)
        {
            this.TargeType = type;
            this.OneInstance = oneInstance;
        }
        public Type TargeType { get; set; }
        public bool OneInstance { get; set; }
        private object _Instance;
        public object Instance {
            get
            {
                if (OneInstance && _Instance != null)
                {
                    return _Instance;
                }
                var instance= Loader.BuildInstance(TargeType);
                if (OneInstance)
                {
                    _Instance = instance;
                }
                return instance;
            }
        }
        public override bool Equals(object obj)
        {
            var targetObj = obj as ImplType;
            if (targetObj == null)
            {
                return false;
            }
            return targetObj.TargeType.FullName == this.TargeType.FullName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ImplTypeCollection : Collection<ImplType>
    {

    }
}
