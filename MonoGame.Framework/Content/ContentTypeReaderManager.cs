// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MonoGame.Framework.Utilities;

namespace Microsoft.Xna.Framework.Content
{
    /// <summary>
    /// Defines a manager that constructs and keeps track of <see cref="ContentTypeReader"/> objects.
    /// </summary>
    public sealed class ContentTypeReaderManager
    {
        private static readonly object _locker;

        private static readonly Dictionary<Type, ContentTypeReader> _contentReadersCache;

        private Dictionary<Type, ContentTypeReader> _contentReaders;

        private static readonly string _assemblyName;

        private static readonly bool _isRunningOnNetCore = typeof(object).Assembly.GetName().Name == "System.Private.CoreLib";

        static ContentTypeReaderManager()
        {
            _locker = new object();
            _contentReadersCache = new Dictionary<Type, ContentTypeReader>(255);
            _assemblyName = ReflectionHelpers.GetAssembly(typeof(ContentTypeReaderManager)).FullName;

            // cache the most common type readers to avoid the most common AOT/trimming related issues
            // (especially on MonoGame standard content types)
            AddTypeCreator(typeof(AlphaTestEffectReader).FullName, () => new AlphaTestEffectReader());
            AddTypeCreator(typeof(ArrayReader<int>).FullName, () => new ArrayReader<int>());
            AddTypeCreator(typeof(ArrayReader<float>).FullName, () => new ArrayReader<float>());
            AddTypeCreator(typeof(ArrayReader<char>).FullName, () => new ArrayReader<char>());
            AddTypeCreator(typeof(ArrayReader<string>).FullName, () => new ArrayReader<string>());
            AddTypeCreator(typeof(ArrayReader<Point>).FullName, () => new ArrayReader<Point>());
            AddTypeCreator(typeof(ArrayReader<Vector2>).FullName, () => new ArrayReader<Vector2>());
            AddTypeCreator(typeof(ArrayReader<Vector3>).FullName, () => new ArrayReader<Vector3>());
            AddTypeCreator(typeof(ArrayReader<Matrix>).FullName, () => new ArrayReader<Matrix>());
            AddTypeCreator(typeof(ArrayReader<Rectangle>).FullName, () => new ArrayReader<Rectangle>());
            AddTypeCreator(typeof(ArrayReader<Color>).FullName, () => new ArrayReader<Color>());
            AddTypeCreator(typeof(ArrayReader<StringReader>).FullName, () => new ArrayReader<StringReader>());
            AddTypeCreator(typeof(BasicEffectReader).FullName, () => new BasicEffectReader());
            AddTypeCreator(typeof(BooleanReader).FullName, () => new BooleanReader());
            AddTypeCreator(typeof(BoundingBoxReader).FullName, () => new BoundingBoxReader());
            AddTypeCreator(typeof(BoundingFrustumReader).FullName, () => new BoundingFrustumReader());
            AddTypeCreator(typeof(BoundingSphereReader).FullName, () => new BoundingSphereReader());
            AddTypeCreator(typeof(ByteReader).FullName, () => new ByteReader());
            AddTypeCreator(typeof(CharReader).FullName, () => new CharReader());
            AddTypeCreator(typeof(ColorReader).FullName, () => new ColorReader());
            AddTypeCreator(typeof(CurveReader).FullName, () => new CurveReader());
            AddTypeCreator(typeof(DateTimeReader).FullName, () => new DateTimeReader());
            AddTypeCreator(typeof(DecimalReader).FullName, () => new DecimalReader());
            // DictionaryReader<TKey, TValue>
            AddTypeCreator(typeof(DoubleReader).FullName, () => new DoubleReader());
            AddTypeCreator(typeof(DualTextureEffectReader).FullName, () => new DualTextureEffectReader());
            AddTypeCreator(typeof(EffectMaterialReader).FullName, () => new EffectMaterialReader());
            AddTypeCreator(typeof(EffectReader).FullName + ", " + _assemblyName, () => new EffectReader());
            AddTypeCreator(typeof(EnumReader<Graphics.SpriteEffects>).FullName, () => new EnumReader<Graphics.SpriteEffects>());
            AddTypeCreator(typeof(EnumReader<Graphics.Blend>).FullName, () => new EnumReader<Graphics.Blend>());
            AddTypeCreator(typeof(EnvironmentMapEffectReader).FullName, () => new EnvironmentMapEffectReader());
            AddTypeCreator(typeof(ExternalReferenceReader).FullName + ", " + _assemblyName, () => new ExternalReferenceReader());
            AddTypeCreator(typeof(IndexBufferReader).FullName + ", " + _assemblyName, () => new IndexBufferReader());
            AddTypeCreator(typeof(Int16Reader).FullName, () => new Int16Reader());
            AddTypeCreator(typeof(Int32Reader).FullName, () => new Int32Reader());
            AddTypeCreator(typeof(Int64Reader).FullName, () => new Int64Reader());
            AddTypeCreator(typeof(ListReader<int>).FullName, () => new ListReader<int>());
            AddTypeCreator(typeof(ListReader<float>).FullName, () => new ListReader<float>());
            AddTypeCreator(typeof(ListReader<char>).FullName, () => new ListReader<char>());
            AddTypeCreator(typeof(ListReader<string>).FullName, () => new ListReader<string>());
            AddTypeCreator(typeof(ListReader<Point>).FullName, () => new ListReader<Point>());
            AddTypeCreator(typeof(ListReader<Vector2>).FullName, () => new ListReader<Vector2>());
            AddTypeCreator(typeof(ListReader<Vector3>).FullName, () => new ListReader<Vector3>());
            AddTypeCreator(typeof(ListReader<Matrix>).FullName, () => new ListReader<Matrix>());
            AddTypeCreator(typeof(ListReader<Rectangle>).FullName, () => new ListReader<Rectangle>());
            AddTypeCreator(typeof(ListReader<Color>).FullName, () => new ListReader<Color>());
            AddTypeCreator(typeof(ListReader<StringReader>).FullName, () => new ListReader<StringReader>());
            AddTypeCreator(typeof(MatrixReader).FullName, () => new MatrixReader());
            AddTypeCreator(typeof(ModelReader).FullName, () => new ModelReader());
            // MultiArrayReader<T>
            AddTypeCreator(typeof(NullableReader<Rectangle>).FullName, () => new NullableReader<Rectangle>());
            AddTypeCreator(typeof(PlaneReader).FullName, () => new PlaneReader());
            AddTypeCreator(typeof(PointReader).FullName, () => new PointReader());
            AddTypeCreator(typeof(QuaternionReader).FullName, () => new QuaternionReader());
            AddTypeCreator(typeof(RayReader).FullName, () => new RayReader());
            AddTypeCreator(typeof(RectangleReader).FullName, () => new RectangleReader());
            // ReflectiveReader<T>
            AddTypeCreator(typeof(SByteReader).FullName, () => new SByteReader());
            AddTypeCreator(typeof(SingleReader).FullName, () => new SingleReader());
            AddTypeCreator(typeof(SkinnedEffectReader).FullName, () => new SkinnedEffectReader());
            AddTypeCreator(typeof(SongReader).FullName, () => new SongReader());
            AddTypeCreator(typeof(SoundEffectReader).FullName, () => new SoundEffectReader());
            AddTypeCreator(typeof(SpriteFontReader).FullName + ", " + _assemblyName, () => new SpriteFontReader());
            AddTypeCreator(typeof(StringReader).FullName, () => new StringReader());
            AddTypeCreator(typeof(Texture2DReader).FullName, () => new Texture2DReader());
            AddTypeCreator(typeof(Texture3DReader).FullName, () => new Texture3DReader());
            AddTypeCreator(typeof(TextureCubeReader).FullName, () => new TextureCubeReader());
            AddTypeCreator(typeof(TextureReader).FullName, () => new TextureReader());
            AddTypeCreator(typeof(TimeSpanReader).FullName, () => new TimeSpanReader());
            AddTypeCreator(typeof(UInt16Reader).FullName, () => new UInt16Reader());
            AddTypeCreator(typeof(UInt32Reader).FullName, () => new UInt32Reader());
            AddTypeCreator(typeof(UInt64Reader).FullName, () => new UInt64Reader());
            AddTypeCreator(typeof(Vector2Reader).FullName, () => new Vector2Reader());
            AddTypeCreator(typeof(Vector3Reader).FullName, () => new Vector3Reader());
            AddTypeCreator(typeof(Vector4Reader).FullName, () => new Vector4Reader());
            AddTypeCreator(typeof(VertexBufferReader).FullName, () => new VertexBufferReader());
            AddTypeCreator(typeof(VertexDeclarationReader).FullName, () => new VertexDeclarationReader());
            AddTypeCreator(typeof(VideoReader).FullName, () =>  new VideoReader());
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ContentReader"/> class initialized for the specified type.
        /// </summary>
        /// <param name="targetType">The type the <see cref="ContentReader"/> will handle.</param>
        /// <returns>
        /// The <see cref="ContentReader"/> created by this method if a content reader of the specified type has been
        /// registered with this content manager; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="targetType"/> parameter is null.</exception>
        public ContentTypeReader GetTypeReader(Type targetType)
        {
            if (targetType.IsArray && targetType.GetArrayRank() > 1)
                targetType = typeof(Array);

            ContentTypeReader reader;
            if (_contentReaders.TryGetValue(targetType, out reader))
                return reader;

            return null;
        }

        internal ContentTypeReader[] LoadAssetReaders(ContentReader reader)
        {
            // The first content byte i read tells me the number of content readers in this XNB file
            var numberOfReaders = reader.Read7BitEncodedInt();
            var contentReaders = new ContentTypeReader[numberOfReaders];
            var needsInitialize = new BitArray(numberOfReaders);
            _contentReaders = new Dictionary<Type, ContentTypeReader>(numberOfReaders);

            // Lock until we're done allocating and initializing any new
            // content type readers...  this ensures we can load content
            // from multiple threads and still cache the readers.
            lock (_locker)
            {
                // For each reader in the file, we read out the length of the string which contains the type of the reader,
                // then we read out the string. Finally we instantiate an instance of that reader using reflection
                for (var i = 0; i < numberOfReaders; i++)
                {
                    // This string tells us what reader we need to decode the following data
                    string originalReaderTypeString = reader.ReadString();
                    originalReaderTypeString = MirgrationHelper.Migrate(originalReaderTypeString);

                    Func<ContentTypeReader> readerFunc;
                    if (typeCreators.TryGetValue(originalReaderTypeString, out readerFunc))
                    {
                        contentReaders[i] = readerFunc();
                        needsInitialize[i] = true;
                    }
                    else
                    {
                        // Need to resolve namespace differences
                        string readerTypeString = originalReaderTypeString;

                        readerTypeString = PrepareType(readerTypeString);

                        Type l_readerType = null;
                        try
                        {
                            // This might fail in AOT context and we need to properly warn the user on what to do if it happens
#pragma warning disable IL2057
                            l_readerType = Type.GetType(readerTypeString);
#pragma warning restore IL2057
                        }
                        catch (NotSupportedException e)
                        {
                            // This will not trigger on recent NativeAOT versions, it will crash later on GetDefaultConstructor() with a native access violation
                            // but we keep this catch block for backward compatibility with older NativeAOT
                            throw new NotSupportedException("It seems that you are using PublishAot and trying to load assets with a reflection-based serializer (which is not natively supported). To work around this error, call ContentTypeReaderManager.AddTypeCreator() in your Game constructor with the following type: " + originalReaderTypeString);
                        }

                        if (l_readerType != null)
                        {
                            ContentTypeReader typeReader;
                            if (!_contentReadersCache.TryGetValue(l_readerType, out typeReader))
                            {
                                try
                                {
                                    typeReader = l_readerType.GetDefaultConstructor().Invoke(null) as ContentTypeReader;
                                }
                                catch (TargetInvocationException ex)
                                {
                                    // If you are getting here, the Mono runtime is most likely not able to JIT the type.
                                    // In particular, MonoTouch needs help instantiating types that are only defined in strings in Xnb files.
                                    throw new InvalidOperationException(
                                        "Failed to get default constructor for ContentTypeReader. To work around, add a creation function to ContentTypeReaderManager.AddTypeCreator() " +
                                        "with the following failed type string: " + originalReaderTypeString, ex);
                                }
                                // Catching non-CLS compliant exceptions to catch native exceptions like Access Violations on NativeAOT
                                catch
                                {
                                    throw new NotSupportedException("It seems that you are using PublishAot and trying to load assets with a reflection-based serializer (which is not natively supported). To work around this error, call ContentTypeReaderManager.AddTypeCreator() in your Game constructor with the following type: " + originalReaderTypeString);
                                }

                                needsInitialize[i] = true;

                                _contentReadersCache.Add(l_readerType, typeReader);
                            }

                            contentReaders[i] = typeReader;
                        }
                        else
                            throw new ContentLoadException(
                                    "Could not find ContentTypeReader Type. Please ensure the name of the Assembly that contains the Type matches the assembly in the full type name: " +
                                    originalReaderTypeString + " (" + readerTypeString + "). " +
                                    " If you are using trimming, PublishAOT, or targeting mobile platforms, you should call ContentTypeReaderManager.AddTypeCreator() on that reader type somewhere in your code.");
                    }

                    var targetType = contentReaders[i].TargetType;
                    if (targetType != null)
                        if (!_contentReaders.ContainsKey(targetType))
                            _contentReaders.Add(targetType, contentReaders[i]);

                    // I think the next 4 bytes refer to the "Version" of the type reader,
                    // although it always seems to be zero
                    reader.ReadInt32();
                }

                // Initialize any new readers.
                for (var i = 0; i < contentReaders.Length; i++)
                {
                    if (needsInitialize.Get(i))
                        contentReaders[i].Initialize(this);
                }

            } // lock (_locker)

            return contentReaders;
        }

        /// <summary>
        /// Removes the Version, Culture, and PublicKeyToken from a fully-qualified type name string.
        /// </summary>
        /// <remarks>
        /// Supports multiple generic types (e.g. Dictionary&lt;TKey,TValue&gt;) and nested generic types (e.g. List&lt;List&lt;int&gt;&gt;).
        /// </remarks>
        /// <param name="type">A string containing the fully-qualified type name to prepare.</param>
        /// <returns>A new string with the Version, Culture and PublicKeyToken removed.</returns>
        public static string PrepareType(string type)
        {
            //Needed to support nested types
            int count = type.Split(new[] { "[[" }, StringSplitOptions.None).Length - 1;

            string preparedType = type;

            for (int i = 0; i < count; i++)
            {
                preparedType = Regex.Replace(preparedType, @"\[(.+?), Version=.+?\]", "[$1]");
            }

            //Handle non generic types
            if (preparedType.Contains("PublicKeyToken"))
                preparedType = Regex.Replace(preparedType, @"(.+?), Version=.+?$", "$1");

            preparedType = preparedType.Replace(", Microsoft.Xna.Framework.Graphics", string.Format(", {0}", _assemblyName));
            preparedType = preparedType.Replace(", Microsoft.Xna.Framework.Video", string.Format(", {0}", _assemblyName));
            preparedType = preparedType.Replace(", Microsoft.Xna.Framework", string.Format(", {0}", _assemblyName));

            if (_isRunningOnNetCore)
                preparedType = preparedType.Replace("mscorlib", "System.Private.CoreLib");
            else
                preparedType = preparedType.Replace("System.Private.CoreLib", "mscorlib");

            return preparedType;
        }

        // Static map of type names to creation functions. Required as iOS requires all types at compile time
        private static Dictionary<string, Func<ContentTypeReader>> typeCreators = new Dictionary<string, Func<ContentTypeReader>>();

        /// <summary>
        /// Registers a function to create a <see cref="ContentTypeReader"/> instance used to read an object of the
        /// type specified.
        /// Call this method to register content readers that may fail loading when trimming or PublishAot are used.
        /// </summary>
        /// <param name='typeString'>A string containing the fully-qualified type name of the object type.</param>
        /// <param name='createFunction'>The function responsible for creating an instance of the <see cref="ContentTypeReader"/> class.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="typeString"/> parameter is null or an empty string.</exception>
        public static void AddTypeCreator(string typeString, Func<ContentTypeReader> createFunction)
        {
            if (!typeCreators.ContainsKey(typeString))
                typeCreators.Add(typeString, createFunction);
        }

        /// <summary>
        /// Clears all content type creators that were registered with <see cref="AddTypeCreator(string, Func{ContentTypeReader})"/>
        /// </summary>
        public static void ClearTypeCreators()
        {
            typeCreators.Clear();
        }

        // HACK: Migration solution, XNA numeric get remove for System Numeric
        private static class MirgrationHelper
        {
            private static Regex removedOldType = new Regex("""
                    Microsoft.Xna.Framework.(?<type>Vector2|Vector3|Vector4|Matrix|Quaternion)
                    (?<module>,\s(?:Microsoft.Xna.Framework|MonoGame.Framework))
                    (?<version>,\sVersion=\d+(?:.\d+)*)
                    (?<culture>,\sCulture=neutral)?
                    (?<token>,\sPublicKeyToken=[a-z0-9]+)?
                    """, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

            private static string module = null;
            private static string version = null;
            private static string cultureSet = null;
            private static string token = null;

            static MirgrationHelper()
            {
                Regex infoExtractor = new Regex("""
                    System.Numerics.Vector2
                    (?<module>,\s(?:System.Private.CoreLib|mscorlib|System.Numerics.Vectors))?
                    (?<version>,\sVersion=\d+(?:.\d+)*)?
                    (?<culture>,\sCulture=neutral)?
                    (?<token>,\sPublicKeyToken=[a-z0-9]+)?
                    """, RegexOptions.IgnorePatternWhitespace);

                var match = infoExtractor.Match(typeof(ListReader<Vector2>).FullName);
                if (!match.Success)
                    throw new Exception("Failed to extract information from Vector2 type for migration hack.");

                if (match.Groups["module"].Success)
                    module = match.Groups["namesapce"].Value;
                if (match.Groups["version"].Success)
                    version = match.Groups["version"].Value;
                if (match.Groups["culture"].Success)
                    cultureSet = match.Groups["culture"].Value;
                if (match.Groups["token"].Success)
                    token = match.Groups["token"].Value;
            }

            public static string Migrate(string type)
            {
                return removedOldType.Replace(type, static (Match match) =>
                {
                    string remplacement = match.Groups["type"].Value switch
                    {
                        "Vector2" => typeof(Vector2).FullName,
                        "Vector3" => typeof(Vector3).FullName,
                        "Vector4" => typeof(Vector4).FullName,
                        "Matrix" => typeof(Matrix).FullName,
                        "Quaternion" => typeof(Quaternion).FullName,
                        _ => throw new NotSupportedException($"Unsupported type in migration hack: {match.Groups["type"].Value}")
                    };
                    if (module != null && match.Groups["module"].Success)
                        remplacement += module;
                    if (version != null && match.Groups["version"].Success)
                        remplacement += version;
                    if (cultureSet != null && match.Groups["culture"].Success)
                        remplacement += cultureSet;
                    if (token != null && match.Groups["token"].Success)
                        remplacement += token;

                    return remplacement;
                });
            }
        }

    }
}
