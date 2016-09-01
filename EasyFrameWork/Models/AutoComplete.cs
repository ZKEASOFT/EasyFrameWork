using System;
using Easy.IOC;

namespace Easy.Models
{
    [Serializable]
    public class AutoComplete : IEntity
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
