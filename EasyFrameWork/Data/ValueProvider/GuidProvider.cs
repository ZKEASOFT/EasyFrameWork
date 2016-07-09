using System;

namespace Easy.Data.ValueProvider
{
    public class GuidProvider : IValueProvider
    {

        public object GenerateValue()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}