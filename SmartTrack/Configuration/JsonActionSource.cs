﻿using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using SmartTrack.Web.Http.Output;

namespace SmartTrack.Web.Configuration
{
    public class JsonActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(TypePool types)
        {
            yield return ActionCall.For<JsonResponseAction>(a => a.Execute(null));
        }
    }

    public class JsonResponseAction
    {
        public JsonResponse Execute(JsonResponse response)
        {
            return response;
        }
    }
}