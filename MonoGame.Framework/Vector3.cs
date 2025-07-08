global using Vector3 = System.Numerics.Vector3;
using System;
using System.Runtime.Intrinsics;

namespace Microsoft.Xna.Framework;

// TODO: for method with need of benchmark if accepted, use Vector128.IsHardwareAccelerated

/// <summary>
/// Extensions for <see cref="Vector3"/>.
/// </summary>
public static class Vector3Extensions
{
    private static readonly Vector3 up = new Vector3(0f, 1f, 0f);
    private static readonly Vector3 down = new Vector3(0f, -1f, 0f);
    private static readonly Vector3 right = new Vector3(1f, 0f, 0f);
    private static readonly Vector3 left = new Vector3(-1f, 0f, 0f);
    private static readonly Vector3 forward = new Vector3(0f, 0f, -1f);
    private static readonly Vector3 backward = new Vector3(0f, 0f, 1f);

    /// <inheritdoc cref="Vector3Extensions"/>
    extension(ref Vector3 vector3)
    {
        /// <summary>
        /// Returns a <see cref="Vector3"/> with components 0, 1, 0.
        /// </summary>
        public static Vector3 Up => up;

        /// <summary>
        /// Returns a <see cref="Vector3"/> with components 0, -1, 0.
        /// </summary>
        public static Vector3 Down => down;

        /// <summary>
        /// Returns a <see cref="Vector3"/> with components 1, 0, 0.
        /// </summary>
        public static Vector3 Right => right;

        /// <summary>
        /// Returns a <see cref="Vector3"/> with components -1, 0, 0.
        /// </summary>
        public static Vector3 Left => left;

        /// <summary>
        /// Returns a <see cref="Vector3"/> with components 0, 0, -1.
        /// </summary>
        public static Vector3 Forward => forward;

        /// <summary>
        /// Returns a <see cref="Vector3"/> with components 0, 0, 1.
        /// </summary>
        public static Vector3 Backward => backward;

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and
        /// <paramref name="value2"/>, storing the result of the
        /// addition in <paramref name="result"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <param name="result">The result of the vector addition.</param>
        [Obsolete("Use Vector3.Add(Vector3, Vector3)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result = Vector3.Add(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 3d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 3d-triangle.</param>
        /// <param name="value2">The second vector of 3d-triangle.</param>
        /// <param name="value3">The third vector of 3d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 3d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 3d-triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
        {
            // TODO: Benchmark
            var vv1 = value1.AsVector128();
            var vv2 = value2.AsVector128();
            var vv3 = value3.AsVector128();
            return (vv1 + (vv2 - vv1) * amount1 + (vv3 - vv1) * amount2).AsVector3();

            //return new Vector3(
            //    MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
            //    MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
            //    MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 3d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 3d-triangle.</param>
        /// <param name="value2">The second vector of 3d-triangle.</param>
        /// <param name="value3">The third vector of 3d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 3d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 3d-triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1, float amount2, out Vector3 result)
        {
            // TODO: Benchmark
            var vv1 = value1.AsVector128();
            var vv2 = value2.AsVector128();
            var vv3 = value3.AsVector128();
            result = (vv1 + (vv2 - vv1) * amount1 + (vv3 - vv1) * amount2).AsVector3();

            //result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            //result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
            //result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
        {
            return new Vector3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, float amount, out Vector3 result)
        {
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
            result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector3"/> towards positive infinity.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Ceiling()
        {
            vector3 = Vector128.Ceiling(vector3.AsVector128()).AsVector3();

            // TODO: All runtime can do this ? Benchmark
            // X = MathF.Ceiling(X);
            // Y = MathF.Ceiling(Y);
            // ²Z = MathF.Ceiling(Z);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <returns>The rounded <see cref="Vector3"/>.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Ceiling(Vector3 value)
        {
            return Vector128.Ceiling(value.AsVector128()).AsVector3();

            // TODO: All runtime can do this ? Benchmark
            // value.X = MathF.Ceiling(value.X);
            // value.Y = MathF.Ceiling(value.Y);
            // value.Z = MathF.Ceiling(value.Z);
            // return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The rounded <see cref="Vector3"/>.</param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ceiling(ref Vector3 value, out Vector3 result)
        {
            result = Vector128.Ceiling(value.AsVector128()).AsVector3();

            // TODO: All runtime can do this ? Benchmark
            // result.X = MathF.Ceiling(value.X);
            // result.Y = MathF.Ceiling(value.Y);
            // result.Z = MathF.Ceiling(value.Z);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        [Obsolete("Use Vector3.Clamp(Vector3, Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
        {
            result = Vector3.Clamp(value1, min, max);
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <param name="result">The cross product of two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Cross(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
        {
            result = Vector3.Cross(vector1, vector2);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Distance(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            result = Vector3.Distance(value1, value2);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.DistanceSquared(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            result = Vector3.DistanceSquared(value1, value2);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector3"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        [Obsolete("Use Vector3.Divide(Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector3 value1, float divider, out Vector3 result)
        {
            result = Vector3.Divide(value1, divider);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector3"/> by the components of another <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector3"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Divide(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result = Vector3.Divide(value1, value2);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Dot(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            result = Vector3.Dot(value1, value2);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector3"/> towards negative infinity.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Floor()
        {
            // TODO: benchmark
            vector3 = Vector128.Floor(vector3.AsVector128()).AsVector3();
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <returns>The rounded <see cref="Vector3"/>.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Floor(Vector3 value)
        {
            // TODO: benchmark
            return Vector128.Floor(value.AsVector128()).AsVector3();
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The rounded <see cref="Vector3"/>.</param>
        public static void Floor(ref Vector3 value, out Vector3 result)
        {
            // TODO: benchmark
            result = Vector128.Floor(value.AsVector128()).AsVector3();
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
        {
            return new Vector3(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                               MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                               MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, float amount, out Vector3 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Lerp(Vector3, Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
        {
            result = Vector3.Lerp(value1, value2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        [Obsolete("Use Vector3.Lerp(Vector3, Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 LerpPrecise(Vector3 value1, Vector3 value2, float amount)
        {
            return Vector3.Lerp(value1, value2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="MathHelper.LerpPrecise"/> on MathHelper for the interpolation.
        /// See remarks section of <see cref="MathHelper.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Lerp(Vector3, Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LerpPrecise(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
        {
            result = Vector3.Lerp(value1, value2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector3"/> with maximal values from the two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Max(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result = Vector3.Max(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector3"/> with minimal values from the two vectors as an output parameter.</param>
        [Obsolete("Use Vector3.Min(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result = Vector3.Min(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a multiplication of <see cref="Vector3"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
        [Obsolete("Use Vector3.Multiply(Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector3 value1, float scaleFactor, out Vector3 result)
        {
            result = Vector3.Multiply(value1, scaleFactor);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="value2">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        [Obsolete("Use Vector3.Multiply(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result =  Vector3.Multiply(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        [Obsolete("Use Vector3.Negate(Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Vector3 value, out Vector3 result)
        {
            result = Vector3.Negate(value);
        }

        /// <summary>
        /// Turns this <see cref="Vector3"/> to a unit vector with the same direction.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize() => vector3 = Vector3.Normalize(vector3);

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        [Obsolete("Use Vector3.Normalize(Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector3 value, out Vector3 result)
        {
            result = Vector3.Normalize(value);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector3"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <returns>Reflected vector.</returns>
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            // I is the original array
            // N is the normal of the incident plane
            // R = I - (2 * N * ( DotProduct[ I,N] ))
            // Vector3 reflectedVector;
            // inline the dotProduct here instead of calling method
            var vv1 = vector.AsVector128();
            var vvN = normal.AsVector128();
            float dotProduct = Vector3.Dot(vector, normal);

            // reflectedVector.X = vector.X - (2.0f * normal.X) * dotProduct;
            // reflectedVector.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
            // reflectedVector.Z = vector.Z - (2.0f * normal.Z) * dotProduct;
            return (vv1 - (2.0f * vvN) * dotProduct).AsVector3();
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="Vector3"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            // I is the original array
            // N is the normal of the incident plane
            // R = I - (2 * N * ( DotProduct[ I,N] ))

            // inline the dotProduct here instead of calling method
            var vv1 = vector.AsVector128();
            var vvN = normal.AsVector128();
            float dotProduct = Vector3.Dot(vector, normal);
            result = (vv1 - (2.0f * vvN) * dotProduct).AsVector3();

            // result.X = vector.X - (2.0f * normal.X) * dotProduct;
            // result.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
            // result.Z = vector.Z - (2.0f * normal.Z) * dotProduct;
        }

        /// <summary>
        /// Round the members of this <see cref="Vector3"/> towards the nearest integer value.
        /// </summary>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void Round()
        {
#if NET9_0_OR_GREATER
            vector3 = Vector128.Round(vector3.AsVector128()).AsVector3();
#else
            vector3.X = MathF.Round(vector3.X);
            vector3.Y = MathF.Round(vector3.Y);
            vector3.Z = MathF.Round(vector3.Z);
#endif
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <returns>The rounded <see cref="Vector3"/>.</returns>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Vector3 Round(Vector3 value)
        {
#if NET9_0_OR_GREATER
            retrun Vector128.Round(value.AsVector128()).AsVector3();
#else
            value.X = MathF.Round(value.X);
            value.Y = MathF.Round(value.Y);
            value.Z = MathF.Round(value.Z);
            return value;
#endif
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The rounded <see cref="Vector3"/>.</param>
#if NET9_0_OR_GREATER
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void Round(ref Vector3 value, out Vector3 result)
        {
#if NET9_0_OR_GREATER
            result = Vector128.Round(value.AsVector128()).AsVector3();
#else
            result.X = MathF.Round(value.X);
            result.Y = MathF.Round(value.Y);
            result.Z = MathF.Round(value.Z);
#endif
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="value2">Source <see cref="Vector3"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
        {
            return new Vector3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="value2">Source <see cref="Vector3"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
        {
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
            result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains subtraction of on <see cref="Vector3"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector3"/>.</param>
        /// <param name="value2">Source <see cref="Vector3"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        [Obsolete("Use Vector3.Subtract(Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="Vector3"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector3"/> as an output parameter.</param>
        [Obsolete("Use Vector3.Transform(Vector3, Matrix3x3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result)
        {
            result = Vector3.Transform(position, matrix);
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector3"/> as an output parameter.</param>
        [Obsolete("Use Vector3.Transform(Vector3, Quaternion)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result)
        {
            result = Vector3.Transform(value, rotation);
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector3"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < length; i++)
            {
                var position = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Vector3.Transform(position, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector3"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector3[] destinationArray, int destinationIndex, int length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < length; i++)
            {
                var position = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Vector3.Transform(position, rotation);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector3"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceArray.Length);

            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var position = sourceArray[i];
                destinationArray[i] = Vector3.Transform(position, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector3"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector3[] sourceArray, ref Quaternion rotation, Vector3[] destinationArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceArray.Length);
           
            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var position = sourceArray[i];
                destinationArray[i] = Vector3.Transform(position, rotation);
            }
        }

        /// <summary>
        /// Creates a new <see cref="Vector3"/> that contains a transformation of the specified normal by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="Vector3"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed normal as an output parameter.</param>
        [Obsolete("Use Vector3.Min(Vector3, Matrix4x4)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
        {
            result = Vector3.TransformNormal(normal, matrix);
        }

        /// <summary>
        /// Apply transformation on normals within array of <see cref="Vector3"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector3"/> should be written.</param>
        /// <param name="length">The number of normals to be transformed.</param>
        public static void TransformNormal(Vector3[] sourceArray,
         int sourceIndex,
         ref Matrix matrix,
         Vector3[] destinationArray,
         int destinationIndex,
         int length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationIndex);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceIndex + length);
            ArgumentOutOfRangeException.ThrowIfLessThan(destinationArray.Length, destinationIndex + length);

            for (int x = 0; x < length; x++)
            {
                var normal = sourceArray[sourceIndex + x];
                destinationArray[destinationIndex + x] = Vector3.TransformNormal(normal, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on all normals within array of <see cref="Vector3"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void TransformNormal(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfLessThan(sourceArray.Length, sourceArray.Length);

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var normal = sourceArray[i];
                destinationArray[i] = Vector3.TransformNormal(normal, matrix);
            }
        }

        /// <summary>
        /// Deconstruction method for <see cref="Vector3"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = vector3.X;
            y = vector3.Y;
            z = vector3.Z;
        }
    }
}
