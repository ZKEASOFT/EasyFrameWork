using Easy.IOC;

namespace Easy.Models
{
    public interface IUser : IEntity
    {
        string UserID { get; set; }
        string NickName { get; set; }
    }
}
