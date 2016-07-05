using System;

namespace Easy.Data.ValueProvider
{
    public interface IValueProvider
    {
        object GenerateValue();
    }
}