// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

global using Plane = System.Numerics.Plane;
using System;

namespace Microsoft.Xna.Framework;

internal class PlaneHelper
{
    /// <summary>
    /// Returns a value indicating what side (positive/negative) of a plane a point is
    /// </summary>
    /// <param name="point">The point to check with</param>
    /// <param name="plane">The plane to check against</param>
    /// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
    public static float ClassifyPoint(ref Vector3 point, ref Plane plane)
    {
        return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
    }

    /// <summary>
    /// Returns the perpendicular distance from a point to a plane
    /// </summary>
    /// <param name="point">The point to check</param>
    /// <param name="plane">The place to check</param>
    /// <returns>The perpendicular distance from the point to the plane</returns>
    public static float PerpendicularDistance(ref Vector3 point, ref Plane plane)
    {
        // dist = (ax + by + cz + d) / sqrt(a*a + b*b + c*c)
        return (float)Math.Abs((plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z)
                                / Math.Sqrt(plane.Normal.X * plane.Normal.X + plane.Normal.Y * plane.Normal.Y + plane.Normal.Z * plane.Normal.Z));
    }
}

/// <summary>
/// Extensions for <see cref="Plane"/>.
/// </summary>
public static class PlaneExtensions
{
    /// <inheritdoc cref="PlaneExtensions"/>
    extension(ref Plane plane)
    {
        /// <summary>
        /// Get the dot product of a <see cref="Vector4"/> with this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector4"/> to calculate the dot product with.</param>
        /// <returns>The dot product of the specified <see cref="Vector4"/> and this <see cref="Plane"/>.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(Vector4 value)
        {
            return Plane.Dot(plane, value);
        }

        /// <summary>
        /// Get the dot product of a <see cref="Vector4"/> with this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector4"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="Vector4"/> and this <see cref="Plane"/>.
        /// </param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dot(ref Vector4 value, out float result)
        {
            result = Plane.Dot(plane, value);
        }

        /// <summary>
        /// Get the dot product of a <see cref="Vector3"/> with
        /// the <see cref="Plane.Normal"/> vector of this <see cref="Plane"/>
        /// plus the <see cref="Plane.D"/> value of this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> to calculate the dot product with.</param>
        /// <returns>
        /// The dot product of the specified <see cref="Vector3"/> and the normal of this <see cref="Plane"/>
        /// plus the <see cref="Plane.D"/> value of this <see cref="Plane"/>.
        /// </returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotCoordinate(Vector3 value)
        {
            return Plane.DotCoordinate(plane, value);
        }

        /// <summary>
        /// Get the dot product of a <see cref="Vector3"/> with
        /// the <see cref="Plane.Normal"/> vector of this <see cref="Plane"/>
        /// plus the <see cref="Plane.D"/> value of this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="Vector3"/> and the normal of this <see cref="Plane"/>
        /// plus the <see cref="Plane.D"/> value of this <see cref="Plane"/>.
        /// </param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result = Plane.DotCoordinate(plane, value);
        }

        /// <summary>
        /// Get the dot product of a <see cref="Vector3"/> with
        /// the <see cref="Plane.Normal"/> vector of this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> to calculate the dot product with.</param>
        /// <returns>
        /// The dot product of the specified <see cref="Vector3"/> and the normal of this <see cref="Plane"/>.
        /// </returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotNormal(Vector3 value)
        {
            return Plane.DotNormal(plane, value);
        }

        /// <summary>
        /// Get the dot product of a <see cref="Vector3"/> with
        /// the <see cref="Plane.Normal"/> vector of this <see cref="Plane"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="Vector3"/> and the normal of this <see cref="Plane"/>.
        /// </param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DotNormal(ref Vector3 value, out float result)
        {
            result = Plane.DotNormal(plane, value);
        }

        /// <summary>
        /// Transforms a normalized plane by a matrix.
        /// </summary>
        /// <param name="value">The normalized plane to transform.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <param name="result">The transformed plane.</param>
        [Obsolete("Use Plane.Transform(Plane, Matrix)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Plane value, ref Matrix matrix, out Plane result)
        {
            result = Plane.Transform(value, matrix);
        }

        /// <summary>
        /// Transforms a normalized plane by a quaternion rotation.
        /// </summary>
        /// <param name="value">The normalized plane to transform.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="result">The transformed plane.</param>
        [Obsolete("Use Plane.Transform(Plane, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transform(ref Plane value, ref Quaternion rotation, out Plane result)
        {
            result = Plane.Transform(value, rotation);
        }

        /// <summary>
        /// Get a normalized version of the specified plane.
        /// </summary>
        /// <param name="value">The <see cref="Plane"/> to normalize.</param>
        /// <param name="result">A normalized version of the specified <see cref="Plane"/>.</param>
        [Obsolete("Use Plane.Normalize(Plane)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Plane value, out Plane result)
        {
            result = Plane.Normalize(value);
        }

        /// <summary>
        /// Check if this <see cref="Plane"/> intersects a <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="BoundingBox"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="Plane"/> with the specified <see cref="BoundingBox"/>.
        /// </returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            return box.Intersects(plane);
        }

        /// <summary>
        /// Check if this <see cref="Plane"/> intersects a <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="BoundingBox"/> to test for intersection.</param>
        /// <param name="result">
        /// The type of intersection of this <see cref="Plane"/> with the specified <see cref="BoundingBox"/>.
        /// </param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            box.Intersects(ref plane, out result);
        }

        /// <summary>
        /// Check if this <see cref="Plane"/> intersects a <see cref="BoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The <see cref="BoundingFrustum"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="Plane"/> with the specified <see cref="BoundingFrustum"/>.
        /// </returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            return frustum.Intersects(plane);
        }

        /// <summary>
        /// Check if this <see cref="Plane"/> intersects a <see cref="BoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="BoundingSphere"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="Plane"/> with the specified <see cref="BoundingSphere"/>.
        /// </returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            return sphere.Intersects(plane);
        }

        /// <summary>
        /// Check if this <see cref="Plane"/> intersects a <see cref="BoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="BoundingSphere"/> to test for intersection.</param>
        /// <param name="result">
        /// The type of intersection of this <see cref="Plane"/> with the specified <see cref="BoundingSphere"/>.
        /// </param>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            sphere.Intersects(ref plane, out result);
        }

        internal PlaneIntersectionType Intersects(ref Vector3 point)
        {
            float distance = Plane.DotCoordinate(plane, point);

            if (distance > 0)
                return PlaneIntersectionType.Front;

            if (distance < 0)
                return PlaneIntersectionType.Back;

            return PlaneIntersectionType.Intersecting;
        }

        /// <summary>
        /// Deconstruction method for <see cref="Plane"/>.
        /// </summary>
        /// <param name="normal"></param>
        /// <param name="d"></param>
        public void Deconstruct(out Vector3 normal, out float d)
        {
            normal = plane.Normal;
            d = plane.D;
        }
    }
}
