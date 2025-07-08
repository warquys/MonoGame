// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using nQuaternion = System.Numerics.Quaternion;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// An efficient mathematical representation for three dimensional rotations.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        #region Private Fields

        private static readonly Quaternion _identity = new Quaternion(0, 0, 0, 1);

        #endregion

        #region Public Fields

        [IgnoreDataMember, FieldOffset(0)]
        internal Vector128<float> vvimp;
        [IgnoreDataMember, FieldOffset(0)]
        internal nQuaternion nqimp;

        /// <summary>
        /// The x coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 0)]
        public float X;

        /// <summary>
        /// The y coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 1)]
        public float Y;

        /// <summary>
        /// The z coordinate of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 2)]
        public float Z;

        /// <summary>
        /// The rotation component of this <see cref="Quaternion"/>.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 3)]
        public float W;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(float x, float y, float z, float w)
        {
            Unsafe.SkipInit(out this); // Skip init to not init vvimp and nqimp
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z from <see cref="Vector3"/> and rotation component from a scalar.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public Quaternion(Vector3 value, float w)
        {
            Unsafe.SkipInit(out this);
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion from <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space and the rotation component.</param>
        public Quaternion(Vector4 value)
        {
            Unsafe.SkipInit(out this);
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = value.W;
        }

        internal Quaternion(nQuaternion quaternion)
        {
            Unsafe.SkipInit(out this);
            this.nqimp = quaternion;
        }

        internal Quaternion(Vector128<float> vector64)
        {
            Unsafe.SkipInit(out this);
            this.vvimp = vector64;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a quaternion representing no rotation.
        /// </summary>
        public static Quaternion Identity
        {
            get{ return _identity; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                if (this == Quaternion._identity)
                {
                    return "Identity";
                }

                return string.Concat(
                    this.X.ToString(), " ",
                    this.Y.ToString(), " ",
                    this.Z.ToString(), " ",
                    this.W.ToString()
                );
            }
        }

        #endregion

        #region Public Methods

        #region Add

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
			return quaternion1.nqimp + quaternion2.nqimp;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion addition as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
			result = quaternion1.nqimp + quaternion2.nqimp;
        }

        #endregion

        #region Concatenate

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
        /// <returns>The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
		    return nQuaternion.Concatenate(value1.nqimp, value2.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="Quaternion"/> to concatenate.</param>
        /// <param name="result">The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
            result = nQuaternion.Concatenate(value1.nqimp, value2.nqimp);
        }

        #endregion

        #region Conjugate

        /// <summary>
        /// Transforms this quaternion into its conjugated version.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Conjugate()
		{
			nqimp = nQuaternion.Conjugate(nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <returns>The conjugate version of the specified quaternion.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Conjugate(Quaternion value)
		{
			return nQuaternion.Conjugate(value.nqimp);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <param name="result">The conjugated version of the specified quaternion as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Conjugate(ref Quaternion value, out Quaternion result)
		{
			result = nQuaternion.Conjugate(value.nqimp);
        }

        #endregion

        #region CreateFromAxisAngle

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The new quaternion builded from axis and angle.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
		    return nQuaternion.CreateFromAxisAngle(axis.nvimp, angle);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            result = nQuaternion.CreateFromAxisAngle(axis.nvimp, angle);
        }

        #endregion

        #region CreateFromRotationMatrix

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>A quaternion composed from the rotation part of the matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
		    return nQuaternion.CreateFromRotationMatrix(matrix.nmimp);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            result = nQuaternion.CreateFromRotationMatrix(matrix.nmimp);
        }

        #endregion

        #region CreateFromYawPitchRoll

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <returns>A new quaternion from the concatenated yaw, pitch, and roll angles.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
            return nQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
 		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
		{
            result = nQuaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        #endregion

        #region Divide

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1 / quaternion2;
        }

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of dividing the quaternions as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result = quaternion1 / quaternion2;
        }

        #endregion

        #region Dot

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns>The dot product of two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return nQuaternion.Dot(quaternion1.nqimp, quaternion2.nqimp);
        }

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <param name="result">The dot product of two quaternions as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
        {
            result = nQuaternion.Dot(quaternion1.nqimp, quaternion2.nqimp);
        }

        #endregion

        #region Equals

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Quaternion quaternion)
                return Equals(quaternion);
            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="other">The <see cref="Quaternion"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Quaternion other)
        {
            return nqimp == other.nqimp;
        }

        #endregion

        /// <summary>
        /// Gets the hash code of this <see cref="Quaternion"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Quaternion"/>.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }

        #region Inverse

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The inverse quaternion.</returns>
        public static Quaternion Inverse(Quaternion quaternion)
        {
            return nQuaternion.Inverse(quaternion.nqimp);
        }

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The inverse quaternion as an output parameter.</param>
        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            result = nQuaternion.Inverse(quaternion.nqimp);
        }

        #endregion

        /// <summary>
        /// Returns the magnitude of the quaternion components.
        /// </summary>
        /// <returns>The magnitude of the quaternion components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length()
        {
            return nqimp.Length();
        }

        /// <summary>
        /// Returns the squared magnitude of the quaternion components.
        /// </summary>
        /// <returns>The squared magnitude of the quaternion components.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared()
        {
            return nqimp.LengthSquared();
        }

        #region Lerp

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of linear blending between two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            return nQuaternion.Lerp(quaternion1.nqimp, quaternion2.nqimp, amount);
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            result = nQuaternion.Lerp(quaternion1.nqimp, quaternion2.nqimp, amount);

        }

        #endregion

        #region Slerp

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of spherical linear blending between two quaternions.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            return nQuaternion.Slerp(quaternion1.nqimp, quaternion2.nqimp, amount);
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            result = nQuaternion.Slerp(quaternion1.nqimp, quaternion2.nqimp, amount);
        }

        #endregion

        #region Subtract

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion subtraction.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1 - quaternion2;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains subtraction of one <see cref="Quaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion subtraction as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result = quaternion1 - quaternion2;
        }

        #endregion

        #region Multiply

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion multiplication.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {            
		    return quaternion1 * quaternion2;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the quaternion multiplication with a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
        {
            return quaternion1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of <see cref="Quaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
        {
            result = quaternion1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Quaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion multiplication as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
          result = quaternion1 * quaternion2;
        }

        #endregion

        #region Negate

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static Quaternion Negate(Quaternion quaternion)
        {
            return -quaternion;
        }

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The result of the quaternion negation as an output parameter.</param>
        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result = -quaternion;
        }

        #endregion

        #region Normalize

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        public void Normalize()
        {
		    nqimp = nQuaternion.Normalize(nqimp);
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <returns>The unit length quaternion.</returns>
        public static Quaternion Normalize(Quaternion quaternion)
        {
		    return nQuaternion.Normalize(quaternion.nqimp);
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/>.</param>
        /// <param name="result">The unit length quaternion an output parameter.</param>
        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
		    result = nQuaternion.Normalize(quaternion.nqimp);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Quaternion"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Z:[<see cref="Z"/>] W:[<see cref="W"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Quaternion"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Z:" + Z + " W:" + W + "}";
        }

        /// <summary>
        /// Gets a <see cref="Vector4"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="Vector4"/> representation for this object.</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(vvimp);
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
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Quaternion"/>.
        /// </summary>
        public nQuaternion ToNumerics()
        {
            return nqimp;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Quaternion"/> to a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator Quaternion(nQuaternion value)
        {
            return new Quaternion(value);
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the add sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            quaternion1.nqimp += quaternion2.nqimp;
            return quaternion1;
        }

        /// <summary>
        /// Divides a <see cref="Quaternion"/> by the other <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the div sign.</param>
        /// <param name="quaternion2">Divisor <see cref="Quaternion"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            quaternion1.nqimp /= quaternion2.nqimp;
            return quaternion1;
        }

        /// <summary>
        /// Compares whether two <see cref="Quaternion"/> instances are equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.nqimp == quaternion2.nqimp;
        }

        /// <summary>
        /// Compares whether two <see cref="Quaternion"/> instances are not equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="Quaternion"/> instance on the left of the not equal sign.</param>
        /// <param name="quaternion2"><see cref="Quaternion"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
           return quaternion1.nqimp != quaternion2.nqimp;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Quaternion"/> on the left of the mul sign.</param>
        /// <param name="quaternion2">Source <see cref="Quaternion"/> on the right of the mul sign.</param>
        /// <returns>Result of the quaternions multiplication.</returns>
        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            quaternion1.nqimp *= quaternion2.nqimp;
            return quaternion1;
        }

        /// <summary>
        /// Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the quaternion multiplication with a scalar.</returns>
        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            quaternion1.nqimp *= scaleFactor;
            return quaternion1;
        }

        /// <summary>
        /// Subtracts a <see cref="Quaternion"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="Vector3"/> on the left of the sub sign.</param>
        /// <param name="quaternion2">Source <see cref="Vector3"/> on the right of the sub sign.</param>
        /// <returns>Result of the quaternion subtraction.</returns>
        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            quaternion1.nqimp -= quaternion2.nqimp;
            return quaternion1;
        }

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="Quaternion"/> on the right of the sub sign.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static Quaternion operator -(Quaternion quaternion)
        {
            quaternion.nqimp = -quaternion.nqimp;
            return quaternion;
        }

        #endregion
    }
}
