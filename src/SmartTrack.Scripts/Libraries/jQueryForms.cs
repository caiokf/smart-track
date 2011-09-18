using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace SmartTrack.Scripts.Libraries
{
    [IgnoreNamespace]
    public delegate void JsonResponseCallback(JsonResponse response);

    [Imported]
    [IgnoreNamespace]
    [ScriptName("Object")]
    public sealed class AjaxSubmitOptions : Record
    {
        public string Url;
        public string Type;
        public string DataType;
        public JsonResponseCallback Success;
        public AjaxErrorCallback Error;

        public AjaxSubmitOptions() { }

        public AjaxSubmitOptions(params object[] nameValuePairs) { }
    }

    [Imported]
    [IgnoreNamespace]
    public sealed class FormsPluginObject : jQueryObject
    {
        private FormsPluginObject() { }

        public jQueryObject AjaxSubmit(AjaxSubmitOptions options)
        {
            return null;
        }
    }
}