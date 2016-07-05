using System;

namespace Easy.Data.ValueProvider
{
    public class CurrentDateProvider : IValueProvider
    {

        public object GenerateValue()
        {
            return DateTime.Now;
        }
    }
}