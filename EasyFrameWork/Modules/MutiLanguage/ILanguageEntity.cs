using System;
namespace Easy.Modules.MutiLanguage
{
    public interface ILanguageEntity
    {
        int LanID { get; set; }
        string LanKey { get; set; }
        string LanType { get; set; }
        string LanValue { get; set; }
        string Module { get; set; }
    }
}
