namespace Kobra.Model.Config;

public class DbConfig : ConfigBase
{

    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

}
