namespace BoxedApp.Models;

public class SettingModel
{
}

public class MongoDBSetting
{
    public string ConnectionUrl { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}
