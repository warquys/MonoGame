using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Design;

#if XNADESIGNPROVIDED
/// <summary>
/// Use to add <see cref="TypeConverterAttribute"/> to <see cref="Vector2"/>, <see cref="Vector3"/>, and <see cref="Vector4"/> types.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal class AddAttributeModuleInitializer
{
#pragma warning disable CA2255 // I do not find any other way to add type converters on prexisting type.
    [ModuleInitializer]
#pragma warning restore CA2255
    internal static void M1()
    {
        if (!IsTypeConverterAdded(typeof(Vector2), typeof(Vector2TypeConverter)))
        {
            TypeDescriptor.AddAttributes(typeof(Vector2), new Attribute[]
            {
                new TypeConverterAttribute(typeof(Vector2TypeConverter))
            });
        }
        if (!IsTypeConverterAdded(typeof(Vector3), typeof(Vector3TypeConverter)))
        {
            TypeDescriptor.AddAttributes(typeof(Vector3), new Attribute[]
            {
                new TypeConverterAttribute(typeof(Vector3TypeConverter))
            });
        }

        if (!IsTypeConverterAdded(typeof(Vector4), typeof(Vector4TypeConverter)))
        {
            TypeDescriptor.AddAttributes(typeof(Vector4), new Attribute[]
            {
                new TypeConverterAttribute(typeof(Vector4TypeConverter))
            });
        }

    }

    private static bool IsTypeConverterAdded(Type type, Type converterType)
    {
        return TypeDescriptor.GetAttributes(type)
            .OfType<TypeConverterAttribute>()
            .Any(a => a.ConverterTypeName == converterType.AssemblyQualifiedName);
    }
}
#endif
