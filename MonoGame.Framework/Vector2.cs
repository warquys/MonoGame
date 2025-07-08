// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using nVector2 = System.Numerics.Vector2;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Describes a 2D-vector.
    /// </summary>
#if XNADESIGNPROVIDED
    [System.ComponentModel.TypeConverter(typeof(Microsoft.Xna.Framework.Design.Vector2TypeConverter))]
#endif
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Private Fields

        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);
        private static readonly Vector2 unitVector = new Vector2(1f, 1f);
        private static readonly Vector2 unitXVector = new Vector2(1f, 0f);
        private static readonly Vector2 unitYVector = new Vector2(0f, 1f);

        #endregion

        #region Public Fields

        [IgnoreDataMember, FieldOffset(0)]
        internal Vector64<float> vvimp;
        [IgnoreDataMember, FieldOffset(0)]
        internal nVector2 nvimp;

        /// <summary>
        /// The x coordinate of this <see cref="Vector2"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 0)]
        public float X;

        /// <summary>
        /// The y coordinate of this <see cref="Vector2"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 1)]
        public float Y;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 0, 0.
        /// </summary>
        public static Vector2 Zero
        {
            get { return zeroVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 1, 1.
        /// </summary>
        public static Vector2 One
        {
            get { return unitVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 1, 0.
        /// </summary>
        public static Vector2 UnitX
        {
            get { return unitXVector; }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with components 0, 1.
        /// </summary>
        public static Vector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a 2d vector with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public Vector2(float x, float y)
        {
            Unsafe.SkipInit(out this); // Skip init to not init vvimp and nvimp
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public Vector2(float value)
        {
            Unsafe.SkipInit(out this);
            this.X = value;
            this.Y = value;
        }

        internal Vector2(nVector2 vector2)
        {
            Unsafe.SkipInit(out this);
            this.nvimp = vector2;
        }

        internal Vector2(Vector64<float> vector64)
        {
            Unsafe.SkipInit(out this);
            this.vvimp = vector64;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Vector2"/> to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(nVector2 value)
        {
            return new Vector2(value);
        }

        /// <summary>
        /// Inverts values in the specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 value)
        {
            value.nvimp = -value.nvimp;
            return value;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.nvimp += value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Subtracts a <see cref="Vector2"/> from a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the vector subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.nvimp -= value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of two vectors by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Vector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.nvimp *= value2.nvimp;
            return value1; ;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            value.nvimp *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="value">Source <see cref="Vector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            value.nvimp *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.nvimp /= value2.nvimp;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value1, float divider)
        {
            value1.nvimp /= divider;
            return value1;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector2"/> instances are equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector2"/> instance on the left of the equal sign.</param>
        /// <param name="value2"><see cref="Vector2"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.vvimp == value2.vvimp;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector2"/> instances are not equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector2"/> instance on the left of the not equal sign.</param>
        /// <param name="value2"><see cref="Vector2"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return value1.vvimp != value2.vvimp;
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
        public static Vector2 Add(Vector2 value1, Vector2 value2)
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
        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = value1 + value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 2d-triangle.</param>
        /// <param name="value2">The second vector of 2d-triangle.</param>
        /// <param name="value3">The third vector of 2d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
        {
            return new Vector2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 2d-triangle.</param>
        /// <param name="value2">The second vector of 2d-triangle.</param>
        /// <param name="value3">The third vector of 2d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
        {
            return new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> towards positive infinity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ceiling()
        {
            vvimp = Vector64.Ceiling(vvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Ceiling(Vector2 value)
        {
            value.vvimp = Vector64.Ceiling(value.vvimp);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ceiling(ref Vector2 value, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.vvimp = Vector64.Ceiling(value.vvimp);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            return nVector2.Clamp(value1.nvimp, min.nvimp, max.nvimp);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result = nVector2.Clamp(value1.nvimp, min.nvimp, max.nvimp);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            return nVector2.Distance(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = nVector2.Distance(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            return nVector2.DistanceSquared(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = nVector2.DistanceSquared(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            return nVector2.Divide(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = nVector2.Divide(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(Vector2 value1, float divider)
        {
            return nVector2.Divide(value1.nvimp, divider);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            result = nVector2.Divide(value1.nvimp, divider);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return nVector2.Dot(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = nVector2.Dot(value1.nvimp, value2.nvimp);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return Equals((Vector2)obj);
            }

            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="other">The <see cref="Vector2"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2 other)
        {
            return vvimp.Equals(other);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> towards negative infinity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Floor()
        {
            vvimp = Vector64.Floor(vvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Floor(Vector2 value)
        {
            value.vvimp = Vector64.Floor(value.vvimp);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Floor(ref Vector2 value, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.vvimp = Vector64.Floor(value.vvimp);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Vector2"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
        {
            return new Vector2(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount), MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The length of this <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length()
        {
            return nvimp.Length();
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared()
        {
            return nvimp.LengthSquared();
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.Lerp(value1.X, value2.X, amount);
            result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector2.Lerp(Vector2, Vector2, float)"/>.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpPrecise(Vector2 value1, Vector2 value2, float amount)
        {
            return nVector2.Lerp(value1.nvimp, value2.nvimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector2.Lerp(ref Vector2, ref Vector2, float, out Vector2)"/>.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LerpPrecise(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result = nVector2.Lerp(value1.nvimp, value2.nvimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector2"/> with maximal values from the two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return nVector2.Max(value1.nvimp, value1.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector2"/> with maximal values from the two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = nVector2.Max(value1.nvimp, value1.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector2"/> with minimal values from the two vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            return nVector2.Max(value1.nvimp, value1.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector2"/> with minimal values from the two vectors as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = nVector2.Min(value1.nvimp, value1.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 value1, Vector2 value2)
        {
            return value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = value1 * value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a multiplication of <see cref="Vector2"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(Vector2 value1, float scaleFactor)
        {
            return value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a multiplication of <see cref="Vector2"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
        {
            result = value1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Negate(Vector2 value)
        {
            return -value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result = -value;
        }

        /// <summary>
        /// Turns this <see cref="Vector2"/> to a unit vector with the same direction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            this.nvimp = nVector2.Normalize(nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>Unit vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 value)
        {
            return nVector2.Normalize(value.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            result = nVector2.Normalize(value.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector2"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <returns>Reflected vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            return nVector2.Reflect(vector.nvimp, normal.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector2"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            result = nVector2.Reflect(vector.nvimp, normal.nvimp);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> to the nearest integer value.
        /// </summary>
        public void Round()
        {
            X = MathF.Round(X);
            Y = MathF.Round(Y);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        public static Vector2 Round(Vector2 value)
        {
            value.X = MathF.Round(value.X);
            value.Y = MathF.Round(value.Y);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        public static void Round(ref Vector2 value, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathF.Round(value.X);
            result.Y = MathF.Round(value.Y);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            Unsafe.SkipInit(out result);
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            return value1 - value2;
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = value1 - value2;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Vector2"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Vector2"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Gets a <see cref="Point"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Point"/> representation for this object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            return nVector2.Transform(position.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
        {
            result = nVector2.Transform(position.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector2"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            return nVector2.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
        {
            result = nVector2.Transform(value.nvimp, rotation.nqimp);
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(
            Vector2[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector2[] destinationArray,
            int destinationIndex,
            int length)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (int x = 0; x < length; x++)
            {
                var position = sourceArray[sourceIndex + x];
                destinationArray[destinationIndex + x] = Transform(position, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector2"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform
        (
            Vector2[] sourceArray,
            int sourceIndex,
            ref Quaternion rotation,
            Vector2[] destinationArray,
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

            for (int x = 0; x < length; x++)
            {
                var position = sourceArray[sourceIndex + x];
                destinationArray[destinationIndex + x] = Transform(position, rotation);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(
            Vector2[] sourceArray,
            ref Matrix matrix,
            Vector2[] destinationArray)
        {
            Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector2"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform
        (
            Vector2[] sourceArray,
            ref Quaternion rotation,
            Vector2[] destinationArray
        )
        {
            Transform(sourceArray, 0, ref rotation, destinationArray, 0, sourceArray.Length);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector2"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed normal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            return nVector2.TransformNormal(normal.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector2"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed normal as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
        {
            result = nVector2.TransformNormal(normal.nvimp, matrix.nmimp);
        }

        /// <summary>
        /// Apply transformation on normals within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector2"/> should be written.</param>
        /// <param name="length">The number of normals to be transformed.</param>
        public static void TransformNormal
        (
            Vector2[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector2[] destinationArray,
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

            for (int i = 0; i < length; i++)
            {
                var normal = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = TransformNormal(normal, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on all normals within array of <see cref="Vector2"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void TransformNormal
            (
            Vector2[] sourceArray,
            ref Matrix matrix,
            Vector2[] destinationArray
            )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (int i = 0; i < sourceArray.Length; i++)
            {
                var normal = sourceArray[i];

                destinationArray[i] = TransformNormal(normal, matrix);
            }
        }

        /// <summary>
        /// Rotates a vector by the specified number of radians
        /// </summary>
        /// <param name="value">The vector to be rotated.</param>
        /// <param name="radians">The amount to rotate the vector.</param>
        /// <returns>A rotated copy of value.</returns>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public static Vector2 Rotate(Vector2 value, float radians)
        {
            if (System.Numerics.Vector.IsHardwareAccelerated)
            {
                return nVector2.Transform(value.nvimp, System.Numerics.Matrix3x2.CreateRotation(radians));
            }
            else
            {
                float cos = MathF.Cos(radians);
                float sin = MathF.Sin(radians);

                return new Vector2(value.X * cos - value.Y * sin, value.X * sin + value.Y * cos);
            }
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> by the specified number of radians
        /// </summary>
        /// <param name="radians">The amount to rotate this <see cref="Vector2"/>.</param>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public void Rotate(float radians)
        {
            if (System.Numerics.Vector.IsHardwareAccelerated)
            {
                this = nVector2.Transform(nvimp, System.Numerics.Matrix3x2.CreateRotation(radians));
            }
            else
            {
                float cos = MathF.Cos(radians);
                float sin = MathF.Sin(radians);

                float oldx = X;

                X = X * cos - Y * sin;
                Y = oldx * sin + Y * cos;
            }
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> around another <see cref="Vector2"/> representing a location
        /// </summary>
        /// <param name="value">The <see cref="Vector2"/> to be rotated</param>
        /// <param name="origin">The origin location to be rotated around</param>
        /// <param name="radians">The amount to rotate by in radians</param>
        /// <returns>The rotated <see cref="Vector2"/></returns>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public static Vector2 RotateAround(Vector2 value, Vector2 origin, float radians)
        {
            return Rotate(value - origin, radians) + origin;
        }

        /// <summary>
        /// Rotates a <see cref="Vector2"/> around another <see cref="Vector2"/> representing a location
        /// </summary>
        /// <param name="origin">The origin location to be rotated around</param>
        /// <param name="radians">The amount to rotate by in radians</param>
        /// <remarks>
        /// A positive angle and negative angle
        /// would rotate counterclockwise and clockwise,
        /// respectively
        /// </remarks>
        public void RotateAround(Vector2 origin, float radians)
        {
            this -= origin;
            Rotate(radians);
            this += origin;
        }

        /// <summary>
        /// Deconstruction method for <see cref="Vector2"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Vector2"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public nVector2 ToNumerics()
        {
            return nvimp;
        }

        #endregion

    }
}
