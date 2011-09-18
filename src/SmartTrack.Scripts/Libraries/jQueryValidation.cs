using System;
using System.Html;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace SmartTrack.Scripts.Libraries
{
    [IgnoreNamespace] [Imported]
    public delegate void ErrorPlacementCallback(jQueryObject error, jQueryObject element);
    
    [IgnoreNamespace] [Imported]
    public delegate bool SubmitHandlerCallback(Element form);
    
    [Imported]
    [IgnoreNamespace]
    [ScriptName("Object")]
    public sealed class ValidateOptions : Record
    {
        public string ErrorElement;
        public ErrorPlacementCallback ErrorPlacement;
        public SubmitHandlerCallback SubmitHandler;

        public ValidateOptions() { }

        public ValidateOptions(params object[] nameValuePairs) { }
    }

    [Imported]
    [IgnoreNamespace]
    public sealed class JQueryValidationPlugin : jQueryObject
    {
        private JQueryValidationPlugin() { }
        
        public bool Valid()
        {
            return false;
        }

        public jQueryObject Validate(ValidateOptions options)
        {
            return null;
        }
    }
}