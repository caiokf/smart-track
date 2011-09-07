using FubuMVC.Core;

namespace SmartTrack.Web.Utils.Extensions
{
    public static class SparkExtensions
    {
         public static void UseSpark<T>(this T registry) where T : FubuRegistry
         {
             FubuMVC.Spark.FubuRegistryExtensions.UseSpark(registry);
         }
    }
}