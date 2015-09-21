#EasyFrameWork
主旨：简单使用，快速开发。
=============
使用本框架开发，从访问数据库，到UI交互都可以快速，轻松完成。如无特别需求，基本可不写代码。

简单示例
------
### 数据库的设置:
### Web.Config:
    <connectionStrings>
        <add name="Easy" connectionString="App_Data\DataBase.mdb" />
    </connectionStrings>
    <appSettings>
        <add key="DataBase" value="Ace"/> //可选值：Ace(Access 2007),Jet(Access 2003),SQL
    </appSettings>
### EntityConfig    
    public override void DataConfigure()
    {
        DataTable("Advertisement");
        DataConfig(m => m.ID).AsIncreasePrimaryKey(); 
    }
### UI的配置，如何呈现，一切随你简单配置。
    public override void ViewConfigure()
    {
        ViewConfig(m => m.ID).AsHidden();
        ViewConfig(m => m.Title).AsTextBox().Required().Order(1).MaxLength(100);
        ViewConfig(m => m.Description).AsMutiLineTextBox().Order(101);
        ViewConfig(m => m.Position).AsDropDownList().DataSource<EnumFlag.ADPosition>();
    }

控制器（Controller）
------
    public class AdvertisementController : BasicController<[Entity], [long], [Service]>
    {
    
    }
不用写一句代码，增，删，改，查，便可轻松完成。

界面（UI）
------
#### 列表：
    @(
        Html.Grid().Name("AdvertisementList")
            .SetAsToolBar("#toolBar")
            .SetColumnTemplate(m => { m.Add(q => q.Title, "<a>{Title}</a>"); })
            .ShowCheckbox(m => m.ID)
            .OrderBy(m=>m.OrderIndex, Easy.Constant.DataEnumerate.OrderType.Descending)
    )
这样，便完成了一个复杂的列表页面，包括检索，分页，自定义排序等。
### 编辑，新建：
    @Html.EditModel(2)
这样，便完成了一个2例N行的自动布局。


EasyFramework使用如此简单，并不代表它只能实现这样简单的东西。简约，但并不简单。

[Apache-2.0](http://opensource.org/licenses/Apache-2.0)
