using System.Configuration;

public interface IAppSettings 
{
    string Configuration { get; }
    string Environment { get; }
    string MONGOHQ_URL { get; }
}

public class AppSettingsWrapper : IAppSettings
{
    public virtual string Configuration { get { return ConfigurationManager.AppSettings["Configuration"]; } }
    public virtual string Environment { get { return ConfigurationManager.AppSettings["Environment"]; } }
    public virtual string MONGOHQ_URL { get { return ConfigurationManager.AppSettings["MONGOHQ_URL"]; } }
}