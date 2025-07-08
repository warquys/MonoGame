// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using nVector4 = System.Numerics.Vector4;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Describes a 4D-vector.
    /// </summary>
#if XNADESIGNPROVIDED
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Xna.Framework.Design.Vector4TypeConverter))]
#endif
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Vector4 : IEquatable<Vector4>
    {
        #region Private Fields

        private static readonly Vector4 zero = new Vector4();
        private static readonly Vector4 one = new Vector4(1f, 1f, 1f, 1f);
        private static readonly Vector4 unitX = new Vector4(1f, 0f, 0f, 0f);
        private static readonly Vector4 unitY = new Vector4(0f, 1f, 0f, 0f);
        private static readonly Vector4 unitZ = new Vector4(0f, 0f, 1f, 0f);
        private static readonly Vector4 unitW = new Vector4(0f, 0f, 0f, 1f);

        #endregion

        #region Public Fields

        [IgnoreDataMember, FieldOffset(0)]
        internal Vector128<float> vvimp;
        [IgnoreDataMember, FieldOffset(0)]
        internal nVector4 nvimp;

        /// <summary>
        /// The x coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 0)]
        public float X;

        /// <summary>
        /// The y coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 1)]
        public float Y;

        /// <summary>
        /// The z coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 2)]
        public float Z;

        /// <summary>
        /// The w coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 4)]
        public float W;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 0, 0.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 1, 1, 1, 1.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 1, 0, 0, 0.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 1, 0, 0.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 1, 0.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 0, 1.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString(), "  ",
                    this.Z.ToString(), "  ",
                    this.W.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a 3d vector with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 4d-space.</param>
        /// <param name="y">The y coordinate in 4d-space.</param>
        /// <param name="z">The z coordinate in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(float x, float y, float z, float w)
        {
            Unsafe.SkipInit(out this); // Skip init to not init vvimp and nvimp
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a 3d vector with X and Z from <see cref="Vector2"/> and Z and W from the scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates in 4d-space.</param>
        /// <param name="z">The z coordinate in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(Vector2 value, float z, float w)
        {
            Unsafe.SkipInit(out this);
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a 3d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(Vector3 value, float w)
        {
            Unsafe.SkipInit(out this);
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z and W set to the same value.
        /// </summary>
        /// <param name="value">The x, y, z and w coordinates in 4d-space.</param>
        public Vector4(float value)
        {
            Unsafe.SkipInit(out this);
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        internal Vector4(nVector4 vector4)
        {
            Unsafe.SkipInit(out this);
            this.nvimp = vector4;
        }

        internal Vector4(Vector128<float> vector128)
        {
            Unsafe.SkipInit(out this);
            this.vvimp = vector128;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Add(Vector4 value1, Vector4 value2)
        {
            return value1 + value2;
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and
        /// <paramref name="value2"/>, storing the result of the
        /// addition in <paramref name="result"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <param name="result">The result of the vector addition.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = value1 + value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 4d-triangle.</param>
        /// <param name="value2">The second vector of 4d-triangle.</param>
        /// <param name="value3">The third vector of 4d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
        {
            return new Vector4(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 4d-triangle.</param>
        /// <param name="value2">The second vector of 4d-triangle.</param>
        /// <param name="value3">The third vector of 4d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
            result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
            result.W = MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
        {
            return new Vector4(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
            result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
            result.W = MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> towards positive infinity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ceiling()
        {
            vvimp = Vector128.Ceiling(vvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Ceiling(Vector4 value)
        {
            value.vvimp = Vector128.Ceiling(value.vvimp);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ceiling(ref Vector4 value, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.vvimp = Vector128.Ceiling(value.vvimp);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
        {
            return nVector4.Clamp(value1.nvimp, min.nvimp, max.nvimp);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clamp(ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            result = nVector4.Clamp(value1.nvimp, min.nvimp, max.nvimp);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector4 value1, Vector4 value2)
        {
            return nVector4.Distance(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            result = nVector4.Distance(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector4 value1, Vector4 value2)
        {
              return nVector4.DistanceSquared(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            result = nVector4.DistanceSquared(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(Vector4 value1, Vector4 value2)
        {
            return value1 / value2;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(Vector4 value1, float divider)
        {
            return value1 / divider;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector4 value1, float divider, out Vector4 result)
        {
            result = value1 / divider;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = value1 / value2;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector4 value1, Vector4 value2)
        {
            return nVector4.Dot(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            result = nVector4.Dot(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector4 vector4)
                return Equals(vector4);

            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="other">The <see cref="Vector4"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4 other)
        {
            return nvimp == other.nvimp;
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> towards negative infinity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Floor()
        {
            vvimp = Vector128.Floor(vvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Floor(Vector4 value)
        {
            value.vvimp = Vector128.Floor(value.vvimp);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Floor(ref Vector4 value, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.vvimp = Vector128.Floor(value.vvimp);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Vector4"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = W.GetHashCode();
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
        {
            return new Vector4(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                               MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                               MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount),
                               MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, float amount, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>The length of this <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length()
        {
            return nvimp.Length();
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared()
        {
            return nvimp.LengthSquared();
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
        {
            return new Vector4(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount),
                MathHelper.Lerp(value1.W, value2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.Lerp(value1.X, value2.X, amount);
            result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
            result.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
            result.W = MathHelper.Lerp(value1.W, value2.W, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector4.Lerp(Vector4, Vector4, float)"/>.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 LerpPrecise(Vector4 value1, Vector4 value2, float amount)
        {
            return nVector4.Lerp(value1.nvimp, value2.nvimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector4.Lerp(ref Vector4, ref Vector4, float, out Vector4)"/>.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LerpPrecise(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
        {
            result = nVector4.Lerp(value1.nvimp, value2.nvimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4"/> with maximal values from the two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Max(Vector4 value1, Vector4 value2)
        {
            return nVector4.Max(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector4"/> with maximal values from the two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = nVector4.Max(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4"/> with minimal values from the two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Min(Vector4 value1, Vector4 value2)
        {
            return nVector4.Min(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector4"/> with minimal values from the two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = nVector4.Min(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(Vector4 value1, Vector4 value2)
        {
            return value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(Vector4 value1, float scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector4 value1, float scaleFactor, out Vector4 result)
        {
            result = value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Negate(Vector4 value)
        {
            return -value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Vector4 value, out Vector4 result)
        {
            result = -value;
        }

        /// <summary>
        /// Turns this <see cref="Vector4"/> to a unit vector with the same direction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            nvimp = nVector4.Normalize(nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>Unit vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Normalize(Vector4 value)
        {
           return nVector4.Normalize(value.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector4 value, out Vector4 result)
        {
            result = nVector4.Normalize(value.nvimp);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> to the nearest integer value.
        /// </summary>
        public void Round()
        {
            X = MathF.Round(X);
            Y = MathF.Round(Y);
            Z = MathF.Round(Z);
            W = MathF.Round(W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        public static Vector4 Round(Vector4 value)
        {
            value.X = MathF.Round(value.X);
            value.Y = MathF.Round(value.Y);
            value.Z = MathF.Round(value.Z);
            value.W = MathF.Round(value.W);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        public static void Round(ref Vector4 value, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathF.Round(value.X);
            result.Y = MathF.Round(value.Y);
            result.Z = MathF.Round(value.Z);
            result.W = MathF.Round(value.W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
        {
            return new Vector4(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount),
                MathHelper.SmoothStep(value1.W, value2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
            result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
            result.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Subtract(Vector4 value1, Vector4 value2)
        {
            return value1 - value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result = value1 - value2;
        }

        #region Transform

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector2 value, Matrix matrix)
        {
            return nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector2 value, Quaternion rotation)
        {
            return nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector3 value, Matrix matrix)
        {
            return nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector3 value, Quaternion rotation)
        {
            return nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector4 value, Matrix matrix)
        {
            return nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Transform(Vector4 value, Quaternion rotation)
        {
            return nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 value, ref Matrix matrix, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector3 value, ref Matrix matrix, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector4 value, ref Matrix matrix, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector4 value, ref Quaternion rotation, out Vector4 result)
        {
            result = nVector4.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform
        (
            Vector4[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector4[] destinationArray,
            int destinationIndex,
            int length
        )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = nVector4.Transform(value.nvimp, matrix.nmimp);
            }
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(
            Vector4[] sourceArray,
            int sourceIndex,
            ref Quaternion rotation,
            Vector4[] destinationArray,
            int destinationIndex,
            int length
            )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = nVector4.Transform(value.nvimp, rotation.nqimp);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = nVector4.Transform(value.nvimp, matrix.nmimp);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector4[] sourceArray, ref Quaternion rotation, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = nVector4.Transform(value.nvimp, rotation.nqimp);
            }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Vector4"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Z:[<see cref="Z"/>] W:[<see cref="W"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Vector4"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Z:" + Z + " W:" + W + "}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="Vector4"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Vector4"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public nVector4 ToNumerics()
        {
            return this.nvimp;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Vector4"/> to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector4(nVector4 value)
        {
            return new Vector4(value);
        }

        /// <summary>
        /// Inverts values in the specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(Vector4 value)
        {
            value.nvimp = -value.nvimp;
            return value;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector4"/> instances are equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector4"/> instance on the left of the equal sign.</param>
        /// <param name="value2"><see cref="Vector4"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4 value1, Vector4 value2)
        {
            return value1.nvimp == value2.nvimp;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector4"/> instances are not equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector4"/> instance on the left of the not equal sign.</param>
        /// <param name="value2"><see cref="Vector4"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4 value1, Vector4 value2)
        {
            return value1.nvimp != value2.nvimp;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator +(Vector4 value1, Vector4 value2)
        {
            value1.nvimp += value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Subtracts a <see cref="Vector4"/> from a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the sub sign.</param>
        /// <returns>Result of the vector subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator -(Vector4 value1, Vector4 value2)
        {
            value1.nvimp -= value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of two vectors by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(Vector4 value1, Vector4 value2)
        {
            value1.nvimp *= value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(Vector4 value, float scaleFactor)
        {
            value.nvimp *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="value">Source <see cref="Vector4"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator *(float scaleFactor, Vector4 value)
        {
            value.nvimp *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the div sign.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator /(Vector4 value1, Vector4 value2)
        {
            value1.nvimp /= value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 operator /(Vector4 value1, float divider)
        {
            value1.nvimp /= divider;
            return value1;
        }

        #endregion
    }
}
