using System.Web.Mvc;
using Spark;
using Spark.Web.Mvc;

namespace SmartTrack.Web.Configuration
{
    public class SparkConfiguration : SparkSettings
    {
        public SparkConfiguration()
        {
            SetAutomaticEncoding(true);

            this.AddNamespace("System")
                .AddNamespace("System.Collections.Generic")
                .AddNamespace("System.Linq")
                .AddNamespace("System.Web.Mvc")
                .AddNamespace("System.Web.Mvc.Html")
                .AddNamespace("Microsoft.Web.Mvc");

            this.AddAssembly("Microsoft.Web.Mvc")
                .AddAssembly("Spark.Web.Mvc")
                .AddAssembly("System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")
                .AddAssembly("System.Web.Routing, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
        }
    }
}