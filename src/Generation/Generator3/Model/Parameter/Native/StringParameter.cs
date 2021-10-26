﻿namespace Generator3.Model.Native
{
    public class StringParameter : Parameter
    {
        public override string NullableTypeName => Model.AnyType.AsT0 switch
        {
            // Marshal as a UTF-8 encoded string
            GirModel.Utf8String s => GetNullableTypeName(s, "[MarshalAs(UnmanagedType.LPUTF8Str)] "),
            
            // Marshal as a null-terminated array of ANSI characters
            // TODO: This is likely incorrect:
            //  - GObject introspection specifies that Windows should use
            //    UTF-8 and Unix should use ANSI. Does using ANSI for
            //    everything cause problems here?
            GirModel.PlatformString p => GetNullableTypeName(p, "[MarshalAs(UnmanagedType.LPStr)] "),
            
            _ => GetNullableTypeName(Model.AnyType.AsT0)
        };

        public override string Direction => Model.GetDirection(
            @in: ParameterDirection.In,
            @out: ParameterDirection.Out,
            @outCallerAllocates: ParameterDirection.Ref,
            @inout: ParameterDirection.Ref
        );

        protected internal StringParameter(GirModel.Parameter parameter) : base(parameter)
        {
            parameter.AnyType.VerifyType<GirModel.String>();
        }

        private string GetNullableTypeName(GirModel.Type type, string attribute = "") => attribute + type.GetName() + GetDefaultNullable();
    }
}
