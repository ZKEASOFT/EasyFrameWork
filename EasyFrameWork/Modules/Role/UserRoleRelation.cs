using System.Linq;
using Easy.IOC;
using Easy.MetaData;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Modules.Role
{
    [DataConfigure(typeof(UserRoleRelationMetaData))]
    public class UserRoleRelation : EditorEntity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string UserID { get; set; }
    }

    class UserRoleRelationMetaData : DataViewMetaData<UserRoleRelation>
    {

        protected override void DataConfigure()
        {
            DataTable("UserRoleRelation");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.Description).AsHidden();
            ViewConfig(m => m.RoleID).AsDropDownList().DataSource(() =>
            {
                return ServiceLocator.Current.GetInstance<IRoleService>()
                    .Get()
                    .ToDictionary(m => m.ID.ToString(), n => n.Title);
            });
        }
    }
}