global using Vector2 = System.Numerics.Vector2;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Microsoft.Xna.Framework;

/// <summary>
/// Extensions for <see cref="Vector2"/>.
/// </summary>
public static class Vector2Extensions
{
    /// <inheritdoc cref="Vector2Extensions"/>
    extension(ref Vector2 vector2)
    {
        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and
        /// <paramref name="value2"/>, storing the result of the
        /// addition in <paramref name="result"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <param name="result">The result of the vector addition.</param>
        [Obsolete("Use Vector2.Add(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Add(value1, value2);
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
            // TODO: Benchmark
            var vv1 = value1.AsVector64();
            var vv2 = value2.AsVector64();
            var vv3 = value3.AsVector64();

            return (vv1 + (vv2 - vv1) * amount1 + (vv3 - vv1) * amount2).AsVector2();

            //return new Vector2(
            //    MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
            //    MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
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
            // TODO: Benchmark
            var vv1 = value1.AsVector64();
            var vv2 = value2.AsVector64();
            var vv3 = value3.AsVector64();

            result = (vv1 + (vv2 - vv1) * amount1 + (vv3 - vv1) * amount2).AsVector2();

            //result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            //result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
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
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> towards positive infinity.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ceiling()
        {
           vector2 = Vector64.Ceiling(vector2.AsVector64()).AsVector2();
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Ceiling(Vector2 value)
        {
            return Vector64.Ceiling(value.AsVector64()).AsVector2();
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ceiling(ref Vector2 value, out Vector2 result)
        {
            result = Vector64.Ceiling(value.AsVector64()).AsVector2();
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        [Obsolete("Use Vector2.Clamp(Vector2, Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result = Vector2.Clamp(value1, min, max);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Distance(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = Vector2.Distance(value1, value2);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        [Obsolete("Use Vector2.DistanceSquared(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = Vector2.DistanceSquared(value1, value2);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by the components of another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector2"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Divide(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Divide(value1, value2);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        [Obsolete("Use Vector2.Divide(Vector2, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            result = Vector2.Divide(value1, divider);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Dot(Vector2, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = Vector2.Dot(value1, value2);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> towards negative infinity.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Floor()
        {
            vector2 = Vector64.Floor(vector2.AsVector64()).AsVector2();
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Floor(Vector2 value)
        {
            return Vector64.Floor(value.AsVector64()).AsVector2();
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Floor(ref Vector2 value, out Vector2 result)
        {
            result = Vector64.Floor(value.AsVector64()).AsVector2();
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
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Lerp(Vector2, Vector2, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result = Vector2.Lerp(value1, value2, amount);
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
        [Obsolete("Use Vector2.Lerp(Vector2, Vector2, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpPrecise(Vector2 value1, Vector2 value2, float amount)
        {
            return Vector2.Lerp(value1, value2, amount);

        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Lerp(Vector2, Vector2, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LerpPrecise(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result = Vector2.Lerp(value1, value2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector2"/> with maximal values from the two vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Max(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Max(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector2"/> with minimal values from the two vectors as an output parameter.</param>
        [Obsolete("Use Vector2.Min(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Min(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        [Obsolete("Use Vector2.Multiply(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Multiply(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        [Obsolete("Use Vector2.Negate(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result = Vector2.Negate(value);
        }

        /// <summary>
        /// Turns this <see cref="Vector2"/> to a unit vector with the same direction.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            vector2 = Vector2.Normalize(vector2);
        }

       /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        [Obsolete("Use Vector2.Normalize(Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            result = Vector2.Normalize(value);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector2"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        [Obsolete("Use Vector2.Reflect(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            result = Vector2.Reflect(vector, normal);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector2"/> to the nearest integer value.
        /// </summary>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Round()
        {
#if NET9_0_OR_GREATER
            vector2 = Vector128.Round(vector2.AsVector64()).AsVector2();
#else
            vector2.X = MathF.Round(vector2.X);
            vector2.Y = MathF.Round(vector2.Y);
#endif
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vector2 Round(Vector2 value)
        {
#if NET9_0_OR_GREATER
            return Vector128.Round(value.AsVector64()).AsVector2();
#else
            value.X = MathF.Round(value.X);
            value.Y = MathF.Round(value.Y);
            return value;
#endif
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The rounded <see cref="Vector2"/>.</param>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Round(ref Vector2 value, out Vector2 result)
        {
#if NET9_0_OR_GREATER
            result = Vector128.Round(value.AsVector64()).AsVector2();
#else
            result.X = MathF.Round(value.X);
            result.Y = MathF.Round(value.Y);
#endif
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
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains subtraction of on <see cref="Vector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector2"/>.</param>
        /// <param name="value2">Source <see cref="Vector2"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        [Obsolete("Use Vector2.Subtract(Vector2, Vector2)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = Vector2.Subtract(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        [Obsolete("Use Vector2.Subtract(Vector2, Matrix4x4)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
        {
            result = Vector2.Transform(position, matrix);
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector2"/> as an output parameter.</param>
        [Obsolete("Use Vector2.Subtract(Vector2, Quaternion)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
        {
           result = Vector2.Transform(value, rotation);
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
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            for (int i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Vector2.Transform(value, matrix);
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
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            for (int i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Vector2.Transform(value, rotation);
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
        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            return new Vector2((normal.X * matrix.M11) + (normal.Y * matrix.M21), (normal.X * matrix.M12) + (normal.Y * matrix.M22));
        }

        /// <summary>
        /// Creates a new <see cref="Vector2"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector2"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed normal as an output parameter.</param>
        public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
        {
            result = Vector2.TransformNormal(normal, matrix);
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
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            for (int i = 0; i < length; i++)
            {
                var normal = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Vector2.TransformNormal(normal, matrix);
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
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, sourceArray.Length);

            for (int i = 0; i < sourceArray.Length; i++)
            {
                var normal = sourceArray[i];
                destinationArray[i] = Vector2.TransformNormal(normal, matrix);
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
            if (Vector.IsHardwareAccelerated)
            {
                vector2 = Vector2.Transform(vector2, Matrix.CreateRotationX(radians));
            }
            else
            {
                float cos = MathF.Cos(radians);
                float sin = MathF.Sin(radians);

                float oldx = vector2.X;

                vector2.X = vector2.X * cos - vector2.Y * sin;
                vector2.Y = oldx * sin + vector2.Y * cos;
            }
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
            vector2 -= origin;
            vector2.Rotate(radians);
            vector2 += origin;
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
            if (Vector.IsHardwareAccelerated)
            {
                return Vector2.Transform(value, Matrix.CreateRotationX(radians));
            }
            else
            {
                float cos = MathF.Cos(radians);
                float sin = MathF.Sin(radians);

                return new Vector2(value.X * cos - value.Y * sin, value.X * sin + value.Y * cos);
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
        /// Deconstruction method for <see cref="Vector2"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out float x, out float y)
        {
            x = vector2.X;
            y = vector2.Y;
        }

        /// <summary>Reinterprets a <see cref="Vector2" /> as a new <see cref="Vector64{Single}" />.</summary>
        /// <returns><paramref name="vector2" /> reinterpreted as a new <see cref="Vector64{Single}" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector64<float> AsVector64()
        {
            return Unsafe.As<Vector2, Vector64<float>>(ref vector2);
        }

        /// <summary>
        /// Gets a <see cref="Point"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Point"/> representation for this object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point ToPoint()
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }
    }

    /// <inheritdoc cref="Vector2Extensions"/>
    extension(Vector64<float> vector64)
    {
        /// <summary>Reinterprets a <see cref="Vector128{Single}" /> as a new <see cref="Vector2" />.</summary>
        /// <returns><paramref name="vector64" /> reinterpreted as a new <see cref="Vector2" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 AsVector2()
        {
            return Unsafe.As<Vector64<float>, Vector2>(ref vector64);
        }
    }
}
