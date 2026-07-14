global using Quaternion = System.Numerics.Quaternion;
using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework;


/// <summary>
/// Extensions for <see cref="Vector2"/>.
/// </summary>
public static class QuaternionExtensions
{
    /// <inheritdoc cref="QuaternionExtensions"/>
    extension(ref Quaternion quaternion)
    {
        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="value1">Source <see cref="Quaternion"/>.</param>
        /// <param name="value2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion addition as an output parameter.</param>
        [Obsolete("Use Quaternion.Add(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result = Quaternion.Add(value1, value2);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="result">The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation as an output parameter.</param>
        [Obsolete("Use Quaternion.Concatenate(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result = Quaternion.Concatenate(value1, value2);
        }

        /// <summary>
        /// Transforms this quaternion into its conjugated version.
        /// </summary>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Conjugate()
        {
            quaternion = Quaternion.Conjugate(quaternion);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <param name="result">The conjugated version of the specified quaternion as an output parameter.</param>
        [Obsolete("Use Quaternion.Conjugate(Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result = Quaternion.Conjugate(value);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
        [Obsolete("Use Quaternion.CreateFromAxisAngle(Vector3, float)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            result = Quaternion.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
        [Obsolete("Use Quaternion.CreateFromRotationMatrix(Matrix)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            result = Quaternion.CreateFromRotationMatrix(matrix);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
        [Obsolete("Use Quaternion.CreateFromYawPitchRoll(float, float, float)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            result = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Quaternion"/>.</param>
        /// <param name="value2">Divisor <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of dividing the quaternions as an output parameter.</param>
        [Obsolete("Use Quaternion.Divide(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            result = Quaternion.Divide(value1, value2);
        }

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="value1">The first quaternion.</param>
        /// <param name="value2">The second quaternion.</param>
        /// <param name="result">The dot product of two quaternions as an output parameter.</param>
        [Obsolete("Use Quaternion.Dot(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Quaternion value1, ref Quaternion value2, out float result)
        {
            result = Quaternion.Dot(value1, value2);
        }

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="value">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The inverse quaternion as an output parameter.</param>
        [Obsolete("Use Quaternion.Inverse(Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Inverse(ref Quaternion value, out Quaternion result)
        {
            result = Quaternion.Inverse(value);
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="value1">Source <see cref="Quaternion"/>.</param>
        /// <param name="value2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="value1"/> and 1 <paramref name="value2"/>.</param>
        /// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
        [Obsolete("Use Quaternion.Lerp(Quaternion, Quaternion, float)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Quaternion value1, ref Quaternion value2, float amount, out Quaternion result)
        {
            result = Quaternion.Lerp(value1, value2, amount);
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="value1">Source <see cref="Quaternion"/>.</param>
        /// <param name="value2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="value1"/> and 1 <paramref name="value2"/>.</param>
        /// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
        [Obsolete("Use Quaternion.Slerp(Quaternion, Quaternion, float)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Slerp(ref Quaternion value1, ref Quaternion value2, float amount, out Quaternion result)
        {
            result = Quaternion.Slerp(value1, value2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion subtraction as an output parameter.</param>
        [Obsolete("Use Quaternion.Subtract(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result = Quaternion.Subtract(quaternion1, quaternion2);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
        [Obsolete("Use Quaternion.Multiply(Quaternion, float)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
        {
            result = Quaternion.Multiply(quaternion1, scaleFactor);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion multiplication as an output parameter.</param>
        [Obsolete("Use Quaternion.Multiply(Quaternion, Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
           result = Quaternion.Multiply(quaternion1, quaternion2);
        }

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="value">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion negation as an output parameter.</param>
        [Obsolete("Use Quaternion.Negate(Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Quaternion value, out Quaternion result)
        {
            result = Quaternion.Negate(value);
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        public void Normalize()
        {
            quaternion = Quaternion.Normalize(quaternion);
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="value">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The unit length quaternion an output parameter.</param>
        [Obsolete("Use Quaternion.Normalize(Quaternion)")]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Quaternion value, out Quaternion result)
        {
            result = Quaternion.Normalize(value);
        }

        /// <summary>
        /// Gets a <see cref="Vector4"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector4"/> representation for this object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4 ToVector4()
        {
            return Unsafe.As<Quaternion, Vector4>(ref quaternion);
        }

        /// <summary>
        /// Deconstruction method for <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = quaternion.X;
            y = quaternion.Y;
            z = quaternion.Z;
            w = quaternion.W;
        }

        /// <summary>
        /// Constructs a quaternion from <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The x, y, z coordinates in 3d-space and the rotation component.</param>
        public static Quaternion FromVector4(Vector4 vector)
        {
            return Unsafe.As<Vector4, Quaternion>(ref vector);
        }
    }
}
