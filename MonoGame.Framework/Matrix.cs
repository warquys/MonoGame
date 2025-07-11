// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using nMatrix = System.Numerics.Matrix4x4;

namespace Microsoft.Xna.Framework
{ 
    /// <summary>
    /// Represents the right-handed 4x4 floating point matrix, which can store translation, scale and rotation information.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct Matrix : IEquatable<Matrix>
    {
        #region Public Constructors

        /// <summary>
        /// Constructs a matrix.
        /// </summary>
        /// <param name="m11">A first row and first column value.</param>
        /// <param name="m12">A first row and second column value.</param>
        /// <param name="m13">A first row and third column value.</param>
        /// <param name="m14">A first row and fourth column value.</param>
        /// <param name="m21">A second row and first column value.</param>
        /// <param name="m22">A second row and second column value.</param>
        /// <param name="m23">A second row and third column value.</param>
        /// <param name="m24">A second row and fourth column value.</param>
        /// <param name="m31">A third row and first column value.</param>
        /// <param name="m32">A third row and second column value.</param>
        /// <param name="m33">A third row and third column value.</param>
        /// <param name="m34">A third row and fourth column value.</param>
        /// <param name="m41">A fourth row and first column value.</param>
        /// <param name="m42">A fourth row and second column value.</param>
        /// <param name="m43">A fourth row and third column value.</param>
        /// <param name="m44">A fourth row and fourth column value.</param>
        public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31,
                      float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            Unsafe.SkipInit(out this); // Skip init to not init vvimp and nmimp
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        /// <summary>
        /// Constructs a matrix.
        /// </summary>
        /// <param name="row1">A first row of the created matrix.</param>
        /// <param name="row2">A second row of the created matrix.</param>
        /// <param name="row3">A third row of the created matrix.</param>
        /// <param name="row4">A fourth row of the created matrix.</param>
        public Matrix(Vector4 row1, Vector4 row2, Vector4 row3, Vector4 row4)
        {
            Unsafe.SkipInit(out this);
            this.M11 = row1.X;
            this.M12 = row1.Y;
            this.M13 = row1.Z;
            this.M14 = row1.W;
            this.M21 = row2.X;
            this.M22 = row2.Y;
            this.M23 = row2.Z;
            this.M24 = row2.W;
            this.M31 = row3.X;
            this.M32 = row3.Y;
            this.M33 = row3.Z;
            this.M34 = row3.W;
            this.M41 = row4.X;
            this.M42 = row4.Y;
            this.M43 = row4.Z;
            this.M44 = row4.W;
        }

        internal Matrix(nMatrix quaternion)
        {
            Unsafe.SkipInit(out this);
            this.nmimp = quaternion;
        }

        internal Matrix(Vector128<float> vector1, Vector128<float> vector2,
            Vector128<float> vector3, Vector128<float> vector4)
        {
            Unsafe.SkipInit(out this);
            this.vvimp1 = vector1;
            this.vvimp2 = vector2;
            this.vvimp3 = vector3;
            this.vvimp4 = vector4;
        }
        #endregion

        #region Public Fields

        [IgnoreDataMember, FieldOffset(sizeof(float) * 4 * 0)]
        internal Vector128<float> vvimp1;
        [IgnoreDataMember, FieldOffset(sizeof(float) * 4 * 1)]
        internal Vector128<float> vvimp2;
        [IgnoreDataMember, FieldOffset(sizeof(float) * 4 * 2)]
        internal Vector128<float> vvimp3;
        [IgnoreDataMember, FieldOffset(sizeof(float) * 4 * 3)]
        internal Vector128<float> vvimp4;

        [IgnoreDataMember, FieldOffset(0)]
        internal nMatrix nmimp;

        /// <summary>
        /// A first row and first column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 0)]
        public float M11;

        /// <summary>
        /// A first row and second column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 1)]
        public float M12;

        /// <summary>
        /// A first row and third column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 2)]
        public float M13;

        /// <summary>
        /// A first row and fourth column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 3)]
        public float M14;

        /// <summary>
        /// A second row and first column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 4)]
        public float M21;

        /// <summary>
        /// A second row and second column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 5)]
        public float M22;

        /// <summary>
        /// A second row and third column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 6)]
        public float M23;

        /// <summary>
        /// A second row and fourth column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 7)]
        public float M24;

        /// <summary>
        /// A third row and first column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 8)]
        public float M31;

        /// <summary>
        /// A third row and second column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 9)]
        public float M32;

        /// <summary>
        /// A third row and third column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 10)]
        public float M33;

        /// <summary>
        /// A third row and fourth column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 11)]
        public float M34;

        /// <summary>
        /// A fourth row and first column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 12)]
        public float M41;

        /// <summary>
        /// A fourth row and second column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 13)]
        public float M42;

        /// <summary>
        /// A fourth row and third column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 14)]
        public float M43;

        /// <summary>
        /// A fourth row and fourth column value.
        /// </summary>
        [DataMember, FieldOffset(sizeof(float) * 15)]
        public float M44;

        #endregion

        #region Indexers

        /// <summary>
        /// Get or set the matrix element at the given index, indexed in row major order.
        /// </summary>
        /// <param name="index">The linearized, zero-based index of the matrix element.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the index is less than <code>0</code> or larger than <code>15</code>.
        /// </exception>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return M11;
                    case 1: return M12;
                    case 2: return M13;
                    case 3: return M14;
                    case 4: return M21;
                    case 5: return M22;
                    case 6: return M23;
                    case 7: return M24;
                    case 8: return M31;
                    case 9: return M32;
                    case 10: return M33;
                    case 11: return M34;
                    case 12: return M41;
                    case 13: return M42;
                    case 14: return M43;
                    case 15: return M44;
                }
                throw new ArgumentOutOfRangeException();
            }

            set
            {
                switch (index)
                {
                    case 0: M11 = value; break;
                    case 1: M12 = value; break;
                    case 2: M13 = value; break;
                    case 3: M14 = value; break;
                    case 4: M21 = value; break;
                    case 5: M22 = value; break;
                    case 6: M23 = value; break;
                    case 7: M24 = value; break;
                    case 8: M31 = value; break;
                    case 9: M32 = value; break;
                    case 10: M33 = value; break;
                    case 11: M34 = value; break;
                    case 12: M41 = value; break;
                    case 13: M42 = value; break;
                    case 14: M43 = value; break;
                    case 15: M44 = value; break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Get or set the value at the specified row and column (indices are zero-based).
        /// </summary>
        /// <param name="row">The row of the element.</param>
        /// <param name="column">The column of the element.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the row or column is less than <code>0</code> or larger than <code>3</code>.
        /// </exception>
        public float this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return nmimp[row, column];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                nmimp[row, column] = value;
            }
        }

        #endregion

        #region Private Members
        private static Matrix identity = new Matrix(1f, 0f, 0f, 0f, 
		                                            0f, 1f, 0f, 0f, 
		                                            0f, 0f, 1f, 0f, 
		                                            0f, 0f, 0f, 1f);
        #endregion

        #region Public Properties

        /// <summary>
        /// The backward vector formed from the third row M31, M32, M33 elements.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                return new Vector3(this.M31, this.M32, this.M33);
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        /// <summary>
        /// The down vector formed from the second row -M21, -M22, -M23 elements.
        /// </summary>
        public Vector3 Down
        {
            get
            {
                return new Vector3(-this.M21, -this.M22, -this.M23);
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        /// <summary>
        /// The forward vector formed from the third row -M31, -M32, -M33 elements.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                return new Vector3(-this.M31, -this.M32, -this.M33);
            }
            set
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        /// <summary>
        /// Returns the identity matrix.
        /// </summary>
        public static Matrix Identity
        {
            get { return identity; }
        }

        /// <summary>
        /// The left vector formed from the first row -M11, -M12, -M13 elements.
        /// </summary>
        public Vector3 Left
        {
            get
            {
                return new Vector3(-this.M11, -this.M12, -this.M13);
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        /// <summary>
        /// The right vector formed from the first row M11, M12, M13 elements.
        /// </summary>
        public Vector3 Right
        {
            get
            {
                return new Vector3(this.M11, this.M12, this.M13);
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        /// <summary>
        /// Position stored in this matrix.
        /// </summary>
        public Vector3 Translation
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Vector3(nmimp.Translation);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                nmimp.Translation = value.nvimp;
            }
        }

        /// <summary>
        /// The upper vector formed from the second row M21, M22, M23 elements.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return new Vector3(this.M21, this.M22, this.M23);
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains sum of two matrixes.
        /// </summary>
        /// <param name="matrix1">The first matrix to add.</param>
        /// <param name="matrix2">The second matrix to add.</param>
        /// <returns>The result of the matrix addition.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            return matrix1 + matrix2;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains sum of two matrixes.
        /// </summary>
        /// <param name="matrix1">The first matrix to add.</param>
        /// <param name="matrix2">The second matrix to add.</param>
        /// <param name="result">The result of the matrix addition as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = matrix1 + matrix2;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <returns>The <see cref="Matrix"/> for spherical billboarding.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 cameraUpVector, Nullable<Vector3> cameraForwardVector)
        {
            Vector3 forwardVector = cameraForwardVector ?? Vector3.Forward;
            return nMatrix.CreateBillboard(objectPosition.nvimp, cameraPosition.nvimp,
                cameraUpVector.nvimp, forwardVector.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="result">The <see cref="Matrix"/> for spherical billboarding as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
        {
            Vector3 forwardVector = cameraForwardVector ?? Vector3.Forward;
            result = nMatrix.CreateBillboard(objectPosition.nvimp, cameraPosition.nvimp,
                cameraUpVector.nvimp, forwardVector.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for cylindrical billboarding that rotates around specified axis.
        /// </summary>
        /// <param name="objectPosition">Object position the billboard will rotate around.</param>
        /// <param name="cameraPosition">Camera position.</param>
        /// <param name="rotateAxis">Axis of billboard for rotation.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="objectForwardVector">Optional object forward vector.</param>
        /// <returns>The <see cref="Matrix"/> for cylindrical billboarding.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 rotateAxis, Nullable<Vector3> cameraForwardVector, Nullable<Vector3> objectForwardVector)
        {
            return nMatrix.CreateConstrainedBillboard(
                objectPosition.nvimp, cameraPosition.nvimp, rotateAxis.nvimp,
                cameraForwardVector.HasValue ? cameraForwardVector.Value.nvimp : Vector3.Forward.nvimp,
                objectForwardVector.HasValue ? objectForwardVector.Value.nvimp : Vector3.Forward.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for cylindrical billboarding that rotates around specified axis.
        /// </summary>
        /// <param name="objectPosition">Object position the billboard will rotate around.</param>
        /// <param name="cameraPosition">Camera position.</param>
        /// <param name="rotateAxis">Axis of billboard for rotation.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="objectForwardVector">Optional object forward vector.</param>
        /// <param name="result">The <see cref="Matrix"/> for cylindrical billboarding as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
        {
            result = nMatrix.CreateConstrainedBillboard(
                objectPosition.nvimp, cameraPosition.nvimp, rotateAxis.nvimp,
                cameraForwardVector.HasValue ? cameraForwardVector.Value.nvimp : Vector3.Forward.nvimp,
                objectForwardVector.HasValue ? objectForwardVector.Value.nvimp : Vector3.Forward.nvimp);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
        {
            return nMatrix.CreateFromAxisAngle(axis.nvimp, angle);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
        {
            result = nMatrix.CreateFromAxisAngle(axis.nvimp, angle);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion"/> of rotation moment.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            return nMatrix.CreateFromQuaternion(quaternion.nqimp);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion"/> of rotation moment.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
        {
            result = nMatrix.CreateFromQuaternion(quaternion.nqimp);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from the specified yaw, pitch and roll values.
        /// </summary>
        /// <param name="yaw">The yaw rotation value in radians.</param>
        /// <param name="pitch">The pitch rotation value in radians.</param>
        /// <param name="roll">The roll rotation value in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/>.</returns>
        /// <remarks>For more information about yaw, pitch and roll visit http://en.wikipedia.org/wiki/Euler_angles.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			return nMatrix.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from the specified yaw, pitch and roll values.
        /// </summary>
        /// <param name="yaw">The yaw rotation value in radians.</param>
        /// <param name="pitch">The pitch rotation value in radians.</param>
        /// <param name="roll">The roll rotation value in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        /// <remarks>For more information about yaw, pitch and roll visit http://en.wikipedia.org/wiki/Euler_angles.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
		{
            result = nMatrix.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        /// <summary>
        /// Creates a new viewing <see cref="Matrix"/>.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <returns>The viewing <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            return nMatrix.CreateLookAt(cameraPosition.nvimp, cameraTarget.nvimp, cameraUpVector.nvimp);
        }

        /// <summary>
        /// Creates a new viewing <see cref="Matrix"/>.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <param name="result">The viewing <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
        {
            result = nMatrix.CreateLookAt(cameraPosition.nvimp, cameraTarget.nvimp, cameraUpVector.nvimp);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for orthographic view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
        {
            return nMatrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for orthographic view as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
        {
            result = nMatrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for customized orthographic view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
        {
			return nMatrix.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for customized orthographic view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateOrthographicOffCenter(Rectangle viewingVolume, float zNearPlane, float zFarPlane)
        {
            return nMatrix.CreateOrthographicOffCenter(
                viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top,
                zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for customized orthographic view as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
        {
			result = nMatrix.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for perspective view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            return nMatrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane, or <see cref="float.PositiveInfinity"/>.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = nMatrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view with field of view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane, or <see cref="float.PositiveInfinity"/>.</param>
        /// <returns>The new projection <see cref="Matrix"/> for perspective view with FOV.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            return nMatrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view with field of view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance of the near plane.</param>
        /// <param name="farPlaneDistance">Distance of the far plane, or <see cref="float.PositiveInfinity"/>.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view with FOV as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = nMatrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new <see cref="Matrix"/> for customized perspective view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            return nMatrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new <see cref="Matrix"/> for customized perspective view.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreatePerspectiveOffCenter(Rectangle viewingVolume, float nearPlaneDistance, float farPlaneDistance)
        {
            return nMatrix.CreatePerspectiveOffCenter(
                viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top,
                nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="left">Lower x-value at the near plane.</param>
        /// <param name="right">Upper x-value at the near plane.</param>
        /// <param name="bottom">Lower y-coordinate at the near plane.</param>
        /// <param name="top">Upper y-value at the near plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <param name="result">The new <see cref="Matrix"/> for customized perspective view as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = nMatrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around X axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateRotationX(float radians)
        {
            return nMatrix.CreateRotationX(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around X axis as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationX(float radians, out Matrix result)
        {
            result = nMatrix.CreateRotationX(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around Y axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateRotationY(float radians)
        {
            return nMatrix.CreateRotationY(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Y axis as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationY(float radians, out Matrix result)
        {
            result = nMatrix.CreateRotationY(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>The rotation <see cref="Matrix"/> around Z axis.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateRotationZ(float radians)
        {
            return nMatrix.CreateRotationZ(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Z axis as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationZ(float radians, out Matrix result)
        {
            result = nMatrix.CreateRotationZ(radians);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scale">Scale value for all three axises.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(float scale)
        {
            return CreateScale(scale, scale, scale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scale">Scale value for all three axises.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(float scale, out Matrix result)
        {
            result = CreateScale(scale, scale, scale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xScale">Scale value for X axis.</param>
        /// <param name="yScale">Scale value for Y axis.</param>
        /// <param name="zScale">Scale value for Z axis.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(float xScale, float yScale, float zScale)
        {
            return nMatrix.CreateScale(xScale, yScale, zScale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xScale">Scale value for X axis.</param>
        /// <param name="yScale">Scale value for Y axis.</param>
        /// <param name="zScale">Scale value for Z axis.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
        {
			result = nMatrix.CreateScale(xScale, yScale, zScale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scales"><see cref="Vector3"/> representing x,y and z scale values.</param>
        /// <returns>The scaling <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateScale(Vector3 scales)
        {
            return nMatrix.CreateScale(scales.nvimp);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scales"><see cref="Vector3"/> representing x,y and z scale values.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(ref Vector3 scales, out Matrix result)
        {
            result = nMatrix.CreateScale(scales.nvimp);
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that flattens geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source. 
        /// </summary>
        /// <param name="lightDirection">A vector specifying the direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <returns>A <see cref="Matrix"/> that can be used to flatten geometry onto the specified plane from the specified direction. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateShadow(Vector3 lightDirection, Plane plane)
        {
            return nMatrix.CreateShadow(lightDirection.nvimp, plane.ToNumerics());
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that flattens geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source. 
        /// </summary>
        /// <param name="lightDirection">A vector specifying the direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <param name="result">A <see cref="Matrix"/> that can be used to flatten geometry onto the specified plane from the specified direction as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
        {
            result = nMatrix.CreateShadow(lightDirection.nvimp, plane.ToNumerics());
        }
        
        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xPosition">X coordinate of translation.</param>
        /// <param name="yPosition">Y coordinate of translation.</param>
        /// <param name="zPosition">Z coordinate of translation.</param>
        /// <returns>The translation <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
        {
            return nMatrix.CreateTranslation(xPosition, yPosition, zPosition);
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">X,Y and Z coordinates of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            result = nMatrix.CreateTranslation(position.nvimp);
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">X,Y and Z coordinates of translation.</param>
        /// <returns>The translation <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateTranslation(Vector3 position)
        {
			return nMatrix.CreateTranslation(position.nvimp);
        }

        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xPosition">X coordinate of translation.</param>
        /// <param name="yPosition">Y coordinate of translation.</param>
        /// <param name="zPosition">Z coordinate of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
        {
            result = nMatrix.CreateTranslation(xPosition, yPosition, zPosition);
        }
        
        /// <summary>
        /// Creates a new reflection <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The plane that used for reflection calculation.</param>
        /// <returns>The reflection <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateReflection(Plane value)
        {
            return nMatrix.CreateReflection(value.ToNumerics());
        }

        /// <summary>
        /// Creates a new reflection <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The plane that used for reflection calculation.</param>
        /// <param name="result">The reflection <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateReflection(ref Plane value, out Matrix result)
        {
            result = nMatrix.CreateReflection(value.ToNumerics());
        }

        /// <summary>
        /// Creates a new world <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">The position vector.</param>
        /// <param name="forward">The forward direction vector.</param>
        /// <param name="up">The upward direction vector. Usually <see cref="Vector3.Up"/>.</param>
        /// <returns>The world <see cref="Matrix"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            return nMatrix.CreateWorld(position.nvimp, forward.nvimp, up.nvimp);
        }

        /// <summary>
        /// Creates a new world <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">The position vector.</param>
        /// <param name="forward">The forward direction vector.</param>
        /// <param name="up">The upward direction vector. Usually <see cref="Vector3.Up"/>.</param>
        /// <param name="result">The world <see cref="Matrix"/> as an output parameter.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result)
        {
            result = nMatrix.CreateWorld(position.nvimp, forward.nvimp, up.nvimp);
        }

        /// <summary>
        /// Decomposes this matrix to translation, rotation and scale elements. Returns <c>true</c> if matrix can be decomposed; <c>false</c> otherwise.
        /// </summary>
        /// <param name="scale">Scale vector as an output parameter.</param>
        /// <param name="rotation">Rotation quaternion as an output parameter.</param>
        /// <param name="translation">Translation vector as an output parameter.</param>
        /// <returns><c>true</c> if matrix can be decomposed; <c>false</c> otherwise.</returns>
        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation = nmimp.Translation;

            float xs = (Math.Sign(M11 * M12 * M13 * M14) < 0) ? -1 : 1;
            float ys = (Math.Sign(M21 * M22 * M23 * M24) < 0) ? -1 : 1;
            float zs = (Math.Sign(M31 * M32 * M33 * M34) < 0) ? -1 : 1;

            Unsafe.SkipInit(out scale);
            scale.X = xs * MathF.Sqrt(this.M11 * this.M11 + this.M12 * this.M12 + this.M13 * this.M13);
            scale.Y = ys * MathF.Sqrt(this.M21 * this.M21 + this.M22 * this.M22 + this.M23 * this.M23);
            scale.Z = zs * MathF.Sqrt(this.M31 * this.M31 + this.M32 * this.M32 + this.M33 * this.M33);

            if (scale.X == 0.0 || scale.Y == 0.0 || scale.Z == 0.0)
            {
                rotation = Quaternion.Identity;
                return false;
            }

            Matrix m1 = new Matrix(this.M11 / scale.X, M12 / scale.X, M13 / scale.X, 0,
                                   this.M21 / scale.Y, M22 / scale.Y, M23 / scale.Y, 0,
                                   this.M31 / scale.Z, M32 / scale.Z, M33 / scale.Z, 0,
                                   0, 0, 0, 1);

            rotation = Quaternion.CreateFromRotationMatrix(m1);
            return true;
        }	

		/// <summary>
        /// Returns a determinant of this <see cref="Matrix"/>.
        /// </summary>
        /// <returns>Determinant of this <see cref="Matrix"/></returns>
        /// <remarks>See more about determinant here - http://en.wikipedia.org/wiki/Determinant.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Determinant()
        {
            return nmimp.GetDeterminant();
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix"/>.</param>
        /// <returns>The result of dividing the matrix.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix Divide(Matrix matrix1, Matrix matrix2)
        {
            return matrix1 / matrix2;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix"/>.</param>
        /// <param name="result">The result of dividing the matrix as an output parameter.</param>
        public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = matrix1 / matrix2;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static Matrix Divide(Matrix matrix1, float divider)
        {
		    return matrix1 / divider;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a matrix by a scalar as an output parameter.</param>
        public static void Divide(ref Matrix matrix1, float divider, out Matrix result)
        {
            result = matrix1 / divider;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Matrix"/> without any tolerance.
        /// </summary>
        /// <param name="other">The <see cref="Matrix"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Matrix other)
        {
            return nmimp == other.nmimp;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/> without any tolerance.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
		    if (obj is Matrix matrix)
		    {
		        return this.Equals(matrix);
		    }

		    return false;
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Matrix"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Matrix"/>.</returns>
        public override int GetHashCode()
        {
            return (((((((((((((((this.M11.GetHashCode() + this.M12.GetHashCode()) + this.M13.GetHashCode()) + this.M14.GetHashCode()) + this.M21.GetHashCode()) + this.M22.GetHashCode()) + this.M23.GetHashCode()) + this.M24.GetHashCode()) + this.M31.GetHashCode()) + this.M32.GetHashCode()) + this.M33.GetHashCode()) + this.M34.GetHashCode()) + this.M41.GetHashCode()) + this.M42.GetHashCode()) + this.M43.GetHashCode()) + this.M44.GetHashCode());
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <returns>The inverted matrix.</returns>
        public static Matrix Invert(Matrix matrix)
        {
            if (nMatrix.Invert(matrix.nmimp, out var invert))
                return invert;
            return invert;
            //throw new InvalidOperationException("Matrix is not invertible.");
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="result">The inverted matrix as output parameter.</param>
        public static void Invert(ref Matrix matrix, out Matrix result)
        {
            Unsafe.SkipInit(out result);
            if (nMatrix.Invert(matrix.nmimp, out result.nmimp))
                return;
            //throw new InvalidOperationException("Matrix is not invertible.");
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix"/>.</param>
        /// <param name="matrix2">The second <see cref="Vector2"/>.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>>The result of linear interpolation of the specified matrixes.</returns>
        public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
        {
		    return nMatrix.Lerp(matrix1.nmimp, matrix2.nmimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix"/>.</param>
        /// <param name="matrix2">The second <see cref="Vector2"/>.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified matrixes as an output parameter.</param>
        public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
        {
            result = nMatrix.Lerp(matrix1.nmimp, matrix2.nmimp, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of two matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/>.</param>
        /// <returns>Result of the matrix multiplication.</returns>
        public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
			return matrix1 * matrix2;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of two matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/>.</param>
        /// <param name="result">Result of the matrix multiplication as an output parameter.</param>
        public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = matrix1 * matrix2;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of <see cref="Matrix"/> and a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix Multiply(Matrix matrix1, float scaleFactor)
        {
            return matrix1 * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of <see cref="Matrix"/> and a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">Result of the matrix multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Matrix matrix1, float scaleFactor, out Matrix result)
        {
            result = matrix1 * scaleFactor;
        }

        /// <summary>
        /// Copy the values of specified <see cref="Matrix"/> to the float array.
        /// </summary>
        /// <param name="matrix">The source <see cref="Matrix"/>.</param>
        /// <returns>The array which matrix values will be stored.</returns>
        /// <remarks>
        /// Required for OpenGL 2.0 projection matrix stuff.
        /// </remarks>
        public static float[] ToFloatArray(Matrix matrix)
        {
            float[] matarray = new float[16];
            Unsafe.CopyBlock(ref Unsafe.As<float, byte>(ref matarray[0]),
                             ref Unsafe.As<Matrix, byte>(ref matrix),
                             16 * sizeof(float));
            return matarray;
        }

        /// <summary>
        /// Returns a matrix with the all values negated.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <returns>Result of the matrix negation.</returns>
        public static Matrix Negate(Matrix matrix)
        {
		    return -matrix;
        }

        /// <summary>
        /// Returns a matrix with the all values negated.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/>.</param>
        /// <param name="result">Result of the matrix negation as an output parameter.</param>
        public static void Negate(ref Matrix matrix, out Matrix result)
        {
            result = -matrix;
        }

        /// <summary>
        /// Converts a <see cref="System.Numerics.Matrix4x4"/> to a <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator Matrix(nMatrix value)
        {
            return new Matrix(value);
        }

        /// <summary>
        /// Adds two matrixes.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the add sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/> on the right of the add sign.</param>
        /// <returns>Sum of the matrixes.</returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            matrix1.nmimp += matrix2.nmimp;
            return matrix1;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the div sign.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the matrixes.</returns>
        public static Matrix operator /(Matrix matrix1, Matrix matrix2)
        {
		    matrix1.vvimp1 /= matrix2.vvimp1;
            matrix1.vvimp2 /= matrix2.vvimp2;
            matrix1.vvimp3 /= matrix2.vvimp3;
            matrix1.vvimp4 /= matrix2.vvimp4;
            return matrix1;
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static Matrix operator /(Matrix matrix, float divider)
        {
		    float num = 1f / divider;
            matrix.nmimp *= num;
		    return matrix;
        }

        /// <summary>
        /// Compares whether two <see cref="Matrix"/> instances are equal without any tolerance.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the equal sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/> on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.nmimp == matrix2.nmimp;
        }

        /// <summary>
        /// Compares whether two <see cref="Matrix"/> instances are not equal without any tolerance.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the not equal sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/> on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.nmimp != matrix2.nmimp;
        }

        /// <summary>
        /// Multiplies two matrixes.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the mul sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/> on the right of the mul sign.</param>
        /// <returns>Result of the matrix multiplication.</returns>
        /// <remarks>
        /// Using matrix multiplication algorithm - see http://en.wikipedia.org/wiki/Matrix_multiplication.
        /// </remarks>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            matrix1.nmimp *= matrix2.nmimp;
            return matrix1;
        }

        /// <summary>
        /// Multiplies the elements of matrix by a scalar.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix operator *(Matrix matrix, float scaleFactor)
        {
		    matrix.nmimp *= scaleFactor;
            return matrix;
        }

        /// <summary>
        /// Multiplies the elements of matrix by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="matrix">Source <see cref="Matrix"/> on the right of the mul sign.</param>
        /// <returns>Result of the matrix multiplication with a scalar.</returns>
        public static Matrix operator *(float scaleFactor, Matrix matrix)
        {
		    matrix.nmimp *= scaleFactor;
            return matrix;
        }

        /// <summary>
        /// Subtracts the values of one <see cref="Matrix"/> from another <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/> on the left of the sub sign.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/> on the right of the sub sign.</param>
        /// <returns>Result of the matrix subtraction.</returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
		    matrix1.nmimp -= matrix2.nmimp;
            return matrix1;
        }

        /// <summary>
        /// Inverts values in the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">Source <see cref="Matrix"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static Matrix operator -(Matrix matrix)
        {
		    matrix.nmimp = -matrix.nmimp;
            return matrix;
        }
        
        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains subtraction of one matrix from another.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix"/>.</param>
        /// <param name="matrix2">The second <see cref="Matrix"/>.</param>
        /// <returns>The result of the matrix subtraction.</returns>
        public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
        {
		    return matrix1 - matrix2;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains subtraction of one matrix from another.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix"/>.</param>
        /// <param name="matrix2">The second <see cref="Matrix"/>.</param>
        /// <param name="result">The result of the matrix subtraction as an output parameter.</param>
        public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = matrix1 - matrix2;
        }

        internal string DebugDisplayString
        {
            get
            {
                if (this == Identity)
                {
                    return "Identity";
                }

                return string.Concat(
                     "( ", this.M11.ToString(), "  ", this.M12.ToString(), "  ", this.M13.ToString(), "  ", this.M14.ToString(), " )  \r\n",
                     "( ", this.M21.ToString(), "  ", this.M22.ToString(), "  ", this.M23.ToString(), "  ", this.M24.ToString(), " )  \r\n",
                     "( ", this.M31.ToString(), "  ", this.M32.ToString(), "  ", this.M33.ToString(), "  ", this.M34.ToString(), " )  \r\n",
                     "( ", this.M41.ToString(), "  ", this.M42.ToString(), "  ", this.M43.ToString(), "  ", this.M44.ToString(), " )");
            }
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Matrix"/> in the format:
        /// {M11:[<see cref="M11"/>] M12:[<see cref="M12"/>] M13:[<see cref="M13"/>] M14:[<see cref="M14"/>]}
        /// {M21:[<see cref="M21"/>] M12:[<see cref="M22"/>] M13:[<see cref="M23"/>] M14:[<see cref="M24"/>]}
        /// {M31:[<see cref="M31"/>] M32:[<see cref="M32"/>] M33:[<see cref="M33"/>] M34:[<see cref="M34"/>]}
        /// {M41:[<see cref="M41"/>] M42:[<see cref="M42"/>] M43:[<see cref="M43"/>] M44:[<see cref="M44"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Matrix"/>.</returns>
        public override string ToString()
        {
            return "{M11:" + M11 + " M12:" + M12 + " M13:" + M13 + " M14:" + M14 + "}"
                + " {M21:" + M21 + " M22:" + M22 + " M23:" + M23 + " M24:" + M24 + "}"
                + " {M31:" + M31 + " M32:" + M32 + " M33:" + M33 + " M34:" + M34 + "}"
                + " {M41:" + M41 + " M42:" + M42 + " M43:" + M43 + " M44:" + M44 + "}";
        }

        /// <summary>
        /// Swap the matrix rows and columns.
        /// </summary>
        /// <param name="matrix">The matrix for transposing operation.</param>
        /// <returns>The new <see cref="Matrix"/> which contains the transposing result.</returns>
        public static Matrix Transpose(Matrix matrix)
        {
            return nMatrix.Transpose(matrix.nmimp);
        }

        /// <summary>
        /// Swap the matrix rows and columns.
        /// </summary>
        /// <param name="matrix">The matrix for transposing operation.</param>
        /// <param name="result">The new <see cref="Matrix"/> which contains the transposing result as an output parameter.</param>
        public static void Transpose(ref Matrix matrix, out Matrix result)
        {
            result = nMatrix.Transpose(matrix.nmimp);
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Matrix4x4"/>.
        /// </summary>
        public nMatrix ToNumerics()
        {
            return nmimp;
        }

        #endregion
    }
}
