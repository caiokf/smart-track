﻿using FubuMVC.Validation;

namespace SmartTrack.Web.Http.Behaviors.Validation
{
    public class ValidationPolicy : ValidationConvention
    {
        public ValidationPolicy() : base(call => call.HasInput && call.InputType().Name.Contains("Input")) { }
    }
}