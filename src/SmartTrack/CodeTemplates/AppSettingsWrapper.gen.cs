using System.Configuration;
using System.ComponentModel;

public enum BuildConfigurations 
{
	[Description("Debug")]
	Debug,

	[Description("Deploy")]
	Deploy,

	[Description("Release")]
	Release,

	[Description("Tests.Unit")]
	TestsUnit
}

public interface IAppSettings 
{
    string Environment { get; }
    string MONGOHQ_URL { get; }
}

public class AppSettingsWrapper : IAppSettings
{
    public virtual string Environment { get { return ConfigurationManager.AppSettings["Environment"]; } }
    public virtual string MONGOHQ_URL { get { return ConfigurationManager.AppSettings["MONGOHQ_URL"]; } }
}