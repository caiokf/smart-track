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