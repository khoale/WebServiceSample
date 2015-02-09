namespace WcfDataServiceToolkitAdvanced.Extensions
{
    using System;

    public static class ReflectionExtensions
    {
        public static bool IsImplementGenericType(this Type type, Type genericType)
        {
            return
                type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == genericType;
        }

        public static bool IsConcreteType(this Type type)
        {
            return type.IsClass && !type.IsAbstract && !type.IsInterface;
        }
    }
}