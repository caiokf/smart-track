using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SmartTrack.Scripts
{
    [IgnoreNamespace]
    public sealed class JsonResponse : Record
    {
        [PreserveCase] public bool Success;
        [PreserveCase] public string Message;
        [PreserveCase] public object Data; 
        [PreserveCase] public List<ValidationError> Errors;
    }

    [IgnoreNamespace] [Imported]
    public sealed class ValidationError : Record
    {
        public string Message;
        public string Field;
    }
}