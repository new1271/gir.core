﻿using System.Linq;

namespace Generator3.Renderer
{
    internal class NativeCallbackUnit : Renderer<Model.NativeCallbackUnit>
    {
        public string Render(Model.NativeCallbackUnit model)
        {
            return $@"
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace { model.NamespaceName }
{{
    // AUTOGENERATED FILE - DO NOT MODIFY

    public delegate {model.ReturnType.Render()} {model.Name}({model.Parameters.Select(Parameter.Render).Join(", ")});
}}";
        }
    }
}
