// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

global using Matrix = System.Numerics.Matrix4x4;
using System;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using SharpDX;

namespace Microsoft.Xna.Framework;

/// <summary>
/// Extensions for <see cref="Matrix"/>.
/// </summary>
public static class MatrixExtensions
{
    /// <inheritdoc cref="MatrixExtensions" />
    extension (Matrix matrix)
    {
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
                    case 0: return matrix.M11;
                    case 1: return matrix.M12;
                    case 2: return matrix.M13;
                    case 3: return matrix.M14;
                    case 4: return matrix.M21;
                    case 5: return matrix.M22;
                    case 6: return matrix.M23;
                    case 7: return matrix.M24;
                    case 8: return matrix.M31;
                    case 9: return matrix.M32;
                    case 10: return matrix.M33;
                    case 11: return matrix.M34;
                    case 12: return matrix.M41;
                    case 13: return matrix.M42;
                    case 14: return matrix.M43;
                    case 15: return matrix.M44;
                }
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            set
            {
                switch (index)
                {
                    case 0: matrix.M11 = value; break;
                    case 1: matrix.M12 = value; break;
                    case 2: matrix.M13 = value; break;
                    case 3: matrix.M14 = value; break;
                    case 4: matrix.M21 = value; break;
                    case 5: matrix.M22 = value; break;
                    case 6: matrix.M23 = value; break;
                    case 7: matrix.M24 = value; break;
                    case 8: matrix.M31 = value; break;
                    case 9: matrix.M32 = value; break;
                    case 10: matrix.M33 = value; break;
                    case 11: matrix.M34 = value; break;
                    case 12: matrix.M41 = value; break;
                    case 13: matrix.M42 = value; break;
                    case 14: matrix.M43 = value; break;
                    case 15: matrix.M44 = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        /// <summary>
        /// The backward vector formed from the third row M31, M32, M33 elements.
        /// </summary>
        public Vector3 Backward
        {
            get
            {
                return new Vector3(matrix.M31, matrix.M32, matrix.M33);
            }
            set
            {
                matrix.M31 = value.X;
                matrix.M32 = value.Y;
                matrix.M33 = value.Z;
            }
        }

        /// <summary>
        /// The down vector formed from the second row -M21, -M22, -M23 elements.
        /// </summary>
        public Vector3 Down
        {
            get
            {
                return new Vector3(-matrix.M21, -matrix.M22, -matrix.M23);
            }
            set
            {
                matrix.M21 = -value.X;
                matrix.M22 = -value.Y;
                matrix.M23 = -value.Z;
            }
        }

        /// <summary>
        /// The forward vector formed from the third row -M31, -M32, -M33 elements.
        /// </summary>
        public Vector3 Forward
        {
            get
            {
                return new Vector3(-matrix.M31, -matrix.M32, -matrix.M33);
            }
            set
            {
                matrix.M31 = -value.X;
                matrix.M32 = -value.Y;
                matrix.M33 = -value.Z;
            }
        }

        /// <summary>
        /// The left vector formed from the first row -M11, -M12, -M13 elements.
        /// </summary>
        public Vector3 Left
        {
            get
            {
                return new Vector3(-matrix.M11, -matrix.M12, -matrix.M13);
            }
            set
            {
                matrix.M11 = -value.X;
                matrix.M12 = -value.Y;
                matrix.M13 = -value.Z;
            }
        }

        /// <summary>
        /// The right vector formed from the first row M11, M12, M13 elements.
        /// </summary>
        public Vector3 Right
        {
            get
            {
                return new Vector3(matrix.M11, matrix.M12, matrix.M13);
            }
            set
            {
                matrix.M11 = value.X;
                matrix.M12 = value.Y;
                matrix.M13 = value.Z;
            }
        }

        /// <summary>
        /// Position stored in this matrix.
        /// </summary>
        public Vector3 Translation
        {
            get
            {
                return new Vector3(matrix.M41, matrix.M42, matrix.M43);
            }
            set
            {
                matrix.M41 = value.X;
                matrix.M42 = value.Y;
                matrix.M43 = value.Z;
            }
        }

        /// <summary>
        /// The upper vector formed from the second row M21, M22, M23 elements.
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return new Vector3(matrix.M21, matrix.M22, matrix.M23);
            }
            set
            {
                matrix.M21 = value.X;
                matrix.M22 = value.Y;
                matrix.M23 = value.Z;
            }
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains sum of two matrixes.
        /// </summary>
        /// <param name="matrix1">The first matrix to add.</param>
        /// <param name="matrix2">The second matrix to add.</param>
        /// <param name="result">The result of the matrix addition as an output parameter.</param>
        [Obsolete("Use Matrix.Add(Matrix, Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = Matrix.Add(matrix1, matrix2);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <returns>The <see cref="Matrix"/> for spherical billboarding.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 cameraUpVector, Vector3? cameraForwardVector)
        {
            return Matrix.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, cameraForwardVector ?? Vector3.Forward);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> for spherical billboarding that rotates around specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of billboard object. It will rotate around that vector.</param>
        /// <param name="cameraPosition">The camera position.</param>
        /// <param name="cameraUpVector">The camera up vector.</param>
        /// <param name="cameraForwardVector">Optional camera forward vector.</param>
        /// <param name="result">The <see cref="Matrix"/> for spherical billboarding as an output parameter.</param>
        [Obsolete("Use Matrix.CreateBillboard(Vector3, Vector3, Vector3, Vector3?)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
        {
            result = Matrix.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, cameraForwardVector ?? Vector3.Forward);
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
        public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition,
            Vector3 rotateAxis, Nullable<Vector3> cameraForwardVector, Nullable<Vector3> objectForwardVector)
        {
            return Matrix.CreateConstrainedBillboard(
                objectPosition, cameraPosition, rotateAxis,
                cameraForwardVector ?? Vector3.Forward, objectForwardVector ?? Vector3.Up);
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
        [Obsolete("Use Matrix.CreateConstrainedBillboard(Vector3, Vector3, Vector3, Vector3?, Vector3?)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
        {
            result = Matrix.CreateConstrainedBillboard(
                objectPosition, cameraPosition, rotateAxis,
                cameraForwardVector ?? Vector3.Forward, objectForwardVector ?? Vector3.Up);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains the rotation moment around specified axis.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateFromAxisAngle(Vector3, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
        {
            result = Matrix.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> from a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion"><see cref="Quaternion"/> of rotation moment.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateFromQuaternion(Quaternion)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
        {
            result = Matrix.CreateFromQuaternion(quaternion);
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
        [Obsolete("Use Matrix.CreateFromYawPitchRoll(float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
        {
            result = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        /// <summary>
        /// Creates a new viewing <see cref="Matrix"/>.
        /// </summary>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraTarget">Lookup vector of the camera.</param>
        /// <param name="cameraUpVector">The direction of the upper edge of the camera.</param>
        /// <param name="result">The viewing <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateLookAt(Vector3, Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
        {
            result = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for orthographic view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for orthographic view as an output parameter.</param>
        [Obsolete("Use Matrix.CreateOrthographic(float, float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
        {
            result = Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized orthographic view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="zNearPlane">Depth of the near plane.</param>
        /// <param name="zFarPlane">Depth of the far plane.</param>
        /// <returns>The new projection <see cref="Matrix"/> for customized orthographic view.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreateOrthographicOffCenter(Rectangle viewingVolume, float zNearPlane, float zFarPlane)
        {
            return Matrix.CreateOrthographicOffCenter(viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top, zNearPlane, zFarPlane);
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
        [Obsolete("Use Matrix.CreateOrthographicOffCenter(float, float, float, float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
        {
            result = Matrix.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view.
        /// </summary>
        /// <param name="width">Width of the viewing volume.</param>
        /// <param name="height">Height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane, or <see cref="float.PositiveInfinity"/>.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view as an output parameter.</param>
        [Obsolete("Use Matrix.CreatePerspective(float, float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = Matrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for perspective view with field of view.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction in radians.</param>
        /// <param name="aspectRatio">Width divided by height of the viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance of the near plane.</param>
        /// <param name="farPlaneDistance">Distance of the far plane, or <see cref="float.PositiveInfinity"/>.</param>
        /// <param name="result">The new projection <see cref="Matrix"/> for perspective view with FOV as an output parameter.</param>
        [Obsolete("Use Matrix.CreatePerspectiveFieldOfView(float, float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new projection <see cref="Matrix"/> for customized perspective view.
        /// </summary>
        /// <param name="viewingVolume">The viewing volume.</param>
        /// <param name="nearPlaneDistance">Distance to the near plane.</param>
        /// <param name="farPlaneDistance">Distance to the far plane.</param>
        /// <returns>The new <see cref="Matrix"/> for customized perspective view.</returns>
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix CreatePerspectiveOffCenter(Rectangle viewingVolume, float nearPlaneDistance, float farPlaneDistance)
        {
            return Matrix.CreatePerspectiveOffCenter(viewingVolume.Left, viewingVolume.Right, viewingVolume.Bottom, viewingVolume.Top, nearPlaneDistance, farPlaneDistance);
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
        [Obsolete("Use Matrix.CreatePerspectiveOffCenter(float, float, float, float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
        {
            result = Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around X axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around X axis as an output parameter.</param>
        [Obsolete("Use Matrix.CreateRotationX(float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationX(float radians, out Matrix result)
        {
            result = Matrix.CreateRotationX(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Y axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Y axis as an output parameter.</param>
        [Obsolete("Use Matrix.CreateRotationX(float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationY(float radians, out Matrix result)
        {
            result = Matrix.CreateRotationY(radians);
        }

        /// <summary>
        /// Creates a new rotation <see cref="Matrix"/> around Z axis.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <param name="result">The rotation <see cref="Matrix"/> around Z axis as an output parameter.</param>
        [Obsolete("Use Matrix.CreateRotationZ(float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateRotationZ(float radians, out Matrix result)
        {
            result = Matrix.CreateRotationZ(radians);
        }


        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scale">Scale value for all three axises.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateScale(float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(float scale, out Matrix result)
        {
            result = Matrix.CreateScale(scale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xScale">Scale value for X axis.</param>
        /// <param name="yScale">Scale value for Y axis.</param>
        /// <param name="zScale">Scale value for Z axis.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateScale(float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
        {
            result = Matrix.CreateScale(xScale, yScale, zScale);
        }

        /// <summary>
        /// Creates a new scaling <see cref="Matrix"/>.
        /// </summary>
        /// <param name="scales"><see cref="Vector3"/> representing x,y and z scale values.</param>
        /// <param name="result">The scaling <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateScale(Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateScale(ref Vector3 scales, out Matrix result)
        {
            result = Matrix.CreateScale(scales.X, scales.Y, scales.Z);
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that flattens geometry into a specified <see cref="Plane"/> as if casting a shadow from a specified light source. 
        /// </summary>
        /// <param name="lightDirection">A vector specifying the direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <param name="result">A <see cref="Matrix"/> that can be used to flatten geometry onto the specified plane from the specified direction as an output parameter.</param>
        [Obsolete("Use Matrix.CreateShadow(Vector3, Plane)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
        {
            result = Matrix.CreateShadow(lightDirection, plane);
        }


        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">X,Y and Z coordinates of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateTranslation(Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            result = Matrix.CreateTranslation(position.X, position.Y, position.Z);
        }


        /// <summary>
        /// Creates a new translation <see cref="Matrix"/>.
        /// </summary>
        /// <param name="xPosition">X coordinate of translation.</param>
        /// <param name="yPosition">Y coordinate of translation.</param>
        /// <param name="zPosition">Z coordinate of translation.</param>
        /// <param name="result">The translation <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateTranslation(float, float, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
        {
            result = Matrix.CreateTranslation(xPosition, yPosition, zPosition);
        }

        /// <summary>
        /// Creates a new reflection <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">The plane that used for reflection calculation.</param>
        /// <param name="result">The reflection <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateReflection(Plane)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateReflection(ref Plane value, out Matrix result)
        {
            result = Matrix.CreateReflection(value);
        }

        /// <summary>
        /// Creates a new world <see cref="Matrix"/>.
        /// </summary>
        /// <param name="position">The position vector.</param>
        /// <param name="forward">The forward direction vector.</param>
        /// <param name="up">The upward direction vector. Usually <see cref="Vector3Extensions.get_Up"/>.</param>
        /// <param name="result">The world <see cref="Matrix"/> as an output parameter.</param>
        [Obsolete("Use Matrix.CreateWorld(Vector3, Vector3, Vector3)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result)
        {
            result = Matrix.CreateWorld(position, forward, up);
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
            translation = matrix.Translation;

            float xs = (Math.Sign(matrix.M11 * matrix.M12 * matrix.M13 * matrix.M14) < 0) ? -1 : 1;
            float ys = (Math.Sign(matrix.M21 * matrix.M22 * matrix.M23 * matrix.M24) < 0) ? -1 : 1;
            float zs = (Math.Sign(matrix.M31 * matrix.M32 * matrix.M33 * matrix.M34) < 0) ? -1 : 1;

            scale.X = xs * MathF.Sqrt(matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 + matrix.M13 * matrix.M13);
            scale.Y = ys * MathF.Sqrt(matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 + matrix.M23 * matrix.M23);
            scale.Z = zs * MathF.Sqrt(matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 + matrix.M33 * matrix.M33);

            if (scale.X == 0.0 || scale.Y == 0.0 || scale.Z == 0.0)
            {
                rotation = Quaternion.Identity;
                return false;
            }

            Matrix m1 = new Matrix(matrix.M11 / scale.X, matrix.M12 / scale.X, matrix.M13 / scale.X, 0,
                               matrix.M21 / scale.Y, matrix.M22 / scale.Y, matrix.M23 / scale.Y, 0,
                               matrix.M31 / scale.Z, matrix.M32 / scale.Z, matrix.M33 / scale.Z, 0,
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
        [Obsolete("Use Matrix.GetDeterminant()")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Determinant()
        {
            return matrix.GetDeterminant();
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix"/>.</param>
        /// <returns>The result of dividing the matrix.</returns>
        public static unsafe Matrix Divide(Matrix matrix1, Matrix matrix2)
        {
            if (Vector.IsHardwareAccelerated)
            {
                Unsafe.SkipInit(out Matrix result);
                Vector4* m1 = (Vector4*)Unsafe.AsPointer(ref matrix1);
                Vector4* m2 = (Vector4*)Unsafe.AsPointer(ref matrix2);
                Vector4* r = (Vector4*)Unsafe.AsPointer(ref result);

                r[0] = m1[0] / m2[0]; // M11..M14
                r[1] = m1[1] / m2[1]; // M21..M24
                r[2] = m1[2] / m2[2]; // M31..M34
                r[3] = m1[3] / m2[3]; // M41..M44
                return result;
                /*
                ref Vector4 m1_0 = ref Unsafe.As<Matrix, Vector4>(ref matrix1);
                ref Vector4 m1_1 = ref Unsafe.Add(ref m1_0, 1);
                ref Vector4 m1_2 = ref Unsafe.Add(ref m1_0, 2);
                ref Vector4 m1_3 = ref Unsafe.Add(ref m1_0, 3);

                ref Vector4 m2_0 = ref Unsafe.As<Matrix, Vector4>(ref matrix2);
                ref Vector4 m2_1 = ref Unsafe.Add(ref m2_0, 1);
                ref Vector4 m2_2 = ref Unsafe.Add(ref m2_0, 2);
                ref Vector4 m2_3 = ref Unsafe.Add(ref m2_0, 3);

                Unsafe.SkipInit(out result);
                ref Vector4 r_0 = ref Unsafe.As<Matrix, Vector4>(ref result);
                ref Vector4 r_1 = ref Unsafe.Add(ref r_0, 1);
                ref Vector4 r_2 = ref Unsafe.Add(ref r_0, 2);
                ref Vector4 r_3 = ref Unsafe.Add(ref r_0, 3);

                r_0 = m1_0 / m2_0;
                r_1 = m1_1 / m2_1;
                r_2 = m1_2 / m2_2;
                r_3 = m1_3 / m2_3;
                return result;
                */
            }
            else
            {
                matrix1.M11 = matrix1.M11 / matrix2.M11;
                matrix1.M12 = matrix1.M12 / matrix2.M12;
                matrix1.M13 = matrix1.M13 / matrix2.M13;
                matrix1.M14 = matrix1.M14 / matrix2.M14;
                matrix1.M21 = matrix1.M21 / matrix2.M21;
                matrix1.M22 = matrix1.M22 / matrix2.M22;
                matrix1.M23 = matrix1.M23 / matrix2.M23;
                matrix1.M24 = matrix1.M24 / matrix2.M24;
                matrix1.M31 = matrix1.M31 / matrix2.M31;
                matrix1.M32 = matrix1.M32 / matrix2.M32;
                matrix1.M33 = matrix1.M33 / matrix2.M33;
                matrix1.M34 = matrix1.M34 / matrix2.M34;
                matrix1.M41 = matrix1.M41 / matrix2.M41;
                matrix1.M42 = matrix1.M42 / matrix2.M42;
                matrix1.M43 = matrix1.M43 / matrix2.M43;
                matrix1.M44 = matrix1.M44 / matrix2.M44;
                return matrix1;
            }
        }


        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by the elements of another matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Divisor <see cref="Matrix"/>.</param>
        /// <param name="result">The result of dividing the matrix as an output parameter.</param>
        public static unsafe void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            if (Vector.IsHardwareAccelerated)
            {
                Unsafe.SkipInit(out result);
                Vector4* m1 = (Vector4*)Unsafe.AsPointer(ref matrix1);
                Vector4* m2 = (Vector4*)Unsafe.AsPointer(ref matrix2);
                Vector4* r = (Vector4*)Unsafe.AsPointer(ref result);

                r[0] = m1[0] / m2[0]; // M11..M14
                r[1] = m1[1] / m2[1]; // M21..M24
                r[2] = m1[2] / m2[2]; // M31..M34
                r[3] = m1[3] / m2[3]; // M41..M44

                // With one to choice ? unsafe or safe ? Witch is faster ?
                // Use Vector<float> shoold be the best
                /*
                ref Vector4 m1_0 = ref Unsafe.As<Matrix, Vector4>(ref matrix1);
                ref Vector4 m1_1 = ref Unsafe.Add(ref m1_0, 1);
                ref Vector4 m1_2 = ref Unsafe.Add(ref m1_0, 2);
                ref Vector4 m1_3 = ref Unsafe.Add(ref m1_0, 3);

                ref Vector4 m2_0 = ref Unsafe.As<Matrix, Vector4>(ref matrix2);
                ref Vector4 m2_1 = ref Unsafe.Add(ref m2_0, 1);
                ref Vector4 m2_2 = ref Unsafe.Add(ref m2_0, 2);
                ref Vector4 m2_3 = ref Unsafe.Add(ref m2_0, 3);

                Unsafe.SkipInit(out result);
                ref Vector4 r_0 = ref Unsafe.As<Matrix, Vector4>(ref result);
                ref Vector4 r_1 = ref Unsafe.Add(ref r_0, 1);
                ref Vector4 r_2 = ref Unsafe.Add(ref r_0, 2);
                ref Vector4 r_3 = ref Unsafe.Add(ref r_0, 3);

                r_0 = m1_0 / m2_0;
                r_1 = m1_1 / m2_1;
                r_2 = m1_2 / m2_2;
                r_3 = m1_3 / m2_3;
                */
            }
            else
            {
                result.M11 = matrix1.M11 / matrix2.M11;
                result.M12 = matrix1.M12 / matrix2.M12;
                result.M13 = matrix1.M13 / matrix2.M13;
                result.M14 = matrix1.M14 / matrix2.M14;
                result.M21 = matrix1.M21 / matrix2.M21;
                result.M22 = matrix1.M22 / matrix2.M22;
                result.M23 = matrix1.M23 / matrix2.M23;
                result.M24 = matrix1.M24 / matrix2.M24;
                result.M31 = matrix1.M31 / matrix2.M31;
                result.M32 = matrix1.M32 / matrix2.M32;
                result.M33 = matrix1.M33 / matrix2.M33;
                result.M34 = matrix1.M34 / matrix2.M34;
                result.M41 = matrix1.M41 / matrix2.M41;
                result.M42 = matrix1.M42 / matrix2.M42;
                result.M43 = matrix1.M43 / matrix2.M43;
                result.M44 = matrix1.M44 / matrix2.M44;
            }
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static unsafe Matrix Divide(Matrix matrix1, float divider)
        {
            if (Vector.IsHardwareAccelerated)
            {
                Unsafe.SkipInit(out Matrix result);
                Vector4* m1 = (Vector4*)Unsafe.AsPointer(ref matrix1);
                Vector4* r = (Vector4*)Unsafe.AsPointer(ref result);

                float num = 1f / divider;
                r[0] = m1[0] * num; // M11..M14
                r[1] = m1[1] * num; // M21..M24
                r[2] = m1[2] * num; // M31..M34
                r[3] = m1[3] * num; // M41..M44
                return result;
            }
            else
            {
                float num = 1f / divider;
                matrix1.M11 = matrix1.M11 * num;
                matrix1.M12 = matrix1.M12 * num;
                matrix1.M13 = matrix1.M13 * num;
                matrix1.M14 = matrix1.M14 * num;
                matrix1.M21 = matrix1.M21 * num;
                matrix1.M22 = matrix1.M22 * num;
                matrix1.M23 = matrix1.M23 * num;
                matrix1.M24 = matrix1.M24 * num;
                matrix1.M31 = matrix1.M31 * num;
                matrix1.M32 = matrix1.M32 * num;
                matrix1.M33 = matrix1.M33 * num;
                matrix1.M34 = matrix1.M34 * num;
                matrix1.M41 = matrix1.M41 * num;
                matrix1.M42 = matrix1.M42 * num;
                matrix1.M43 = matrix1.M43 * num;
                matrix1.M44 = matrix1.M44 * num;
                return matrix1;
            }
        }

        /// <summary>
        /// Divides the elements of a <see cref="Matrix"/> by a scalar.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a matrix by a scalar.</returns>
        public static unsafe void Divide(Matrix matrix1, float divider, out Matrix result)
        {// TODO: TEST
            if (Vector.IsHardwareAccelerated)
            {
                Unsafe.SkipInit(out result);
                Vector4* m1 = (Vector4*)Unsafe.AsPointer(ref matrix1);
                Vector4* r = (Vector4*)Unsafe.AsPointer(ref result);

                float num = 1f / divider;
                r[0] = m1[0] * num; // M11..M14
                r[1] = m1[1] * num; // M21..M24
                r[2] = m1[2] * num; // M31..M34
                r[3] = m1[3] * num; // M41..M44
            }
            else
            {
                float num = 1f / divider;
                result.M11 = matrix1.M11 * num;
                result.M12 = matrix1.M12 * num;
                result.M13 = matrix1.M13 * num;
                result.M14 = matrix1.M14 * num;
                result.M21 = matrix1.M21 * num;
                result.M22 = matrix1.M22 * num;
                result.M23 = matrix1.M23 * num;
                result.M24 = matrix1.M24 * num;
                result.M31 = matrix1.M31 * num;
                result.M32 = matrix1.M32 * num;
                result.M33 = matrix1.M33 * num;
                result.M34 = matrix1.M34 * num;
                result.M41 = matrix1.M41 * num;
                result.M42 = matrix1.M42 * num;
                result.M43 = matrix1.M43 * num;
                result.M44 = matrix1.M44 * num;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="value">Source <see cref="Matrix"/>.</param>
        /// <returns>The inverted matrix.</returns>
        [Obsolete("Use **bool** Matrix.Invert(Matrix, out Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix Invert(Matrix value)
        {
            Matrix result;
            Invert(ref value, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> which contains inversion of the specified matrix. 
        /// </summary>
        /// <param name="value">Source <see cref="Matrix"/>.</param>
        /// <param name="result">The inverted matrix as output parameter.</param>
        [Obsolete("Use **bool** Matrix.Invert(Matrix, out Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invert(ref Matrix value, out Matrix result)
        {
            if (!Matrix.Invert(value, out result))
                throw new InvalidOperationException("The matrix do not have an invert.");
        }


        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains linear interpolation of the values in specified matrixes.
        /// </summary>
        /// <param name="matrix1">The first <see cref="Matrix"/>.</param>
        /// <param name="matrix2">The second <see cref="Vector2"/>.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified matrixes as an output parameter.</param>
        [Obsolete("Use Matrix.Lerp(Matrix, Matrix, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
        {
            result = Matrix.Lerp(matrix1, matrix2, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of two matrix.
        /// </summary>
        /// <param name="matrix1">Source <see cref="Matrix"/>.</param>
        /// <param name="matrix2">Source <see cref="Matrix"/>.</param>
        /// <param name="result">Result of the matrix multiplication as an output parameter.</param>
        [Obsolete("Use Matrix.Multiply(Matrix, Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result = Matrix.Multiply(matrix1, matrix2);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains a multiplication of <see cref="Matrix"/> and a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="Matrix"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">Result of the matrix multiplication with a scalar as an output parameter.</param>
        [Obsolete("Use Matrix.Multiply(Matrix, float)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Matrix value, float scaleFactor, out Matrix result)
        {
            result = Matrix.Multiply(value, scaleFactor);

        }

        /// <summary>
        /// Copy the values of specified <see cref="Matrix"/> to the float array.
        /// </summary>
        /// <param name="value">The source <see cref="Matrix"/>.</param>
        /// <returns>The array which matrix values will be stored.</returns>
        /// <remarks>
        /// Required for OpenGL 2.0 projection matrix stuff.
        /// </remarks>
        public static float[] ToFloatArray(Matrix value)
        {// TODO: TEST
            float[] matarray = new float[16];
            Unsafe.CopyBlock(ref Unsafe.As<float, byte>(ref matarray[0]),
                ref Unsafe.As<Matrix, byte>(ref value), 16 * sizeof(float));

            //float[] matarray = {
            //                        value.M11, value.M12, value.M13, value.M14,
            //                        value.M21, value.M22, value.M23, value.M24,
            //                        value.M31, value.M32, value.M33, value.M34,
            //                        value.M41, value.M42, value.M43, value.M44
            //                    };
            return matarray;
        }

        /// <summary>
        /// Returns a matrix with the all values negated.
        /// </summary>
        /// <param name="value">Source <see cref="Matrix"/>.</param>
        /// <param name="result">Result of the matrix negation as an output parameter.</param>
        public static void Negate(ref Matrix value, out Matrix result)
        {
            result = Matrix.Negate(value);
        }

        /// <summary>
        /// Creates a new <see cref="Matrix"/> that contains subtraction of one matrix from another.
        /// </summary>
        /// <param name="value1">The first <see cref="Matrix"/>.</param>
        /// <param name="value2">The second <see cref="Matrix"/>.</param>
        /// <param name="result">The result of the matrix subtraction as an output parameter.</param>
        [Obsolete("Use Matrix.Subtract(Matrix, Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Matrix value1, ref Matrix value2, out Matrix result)
        {
            result = Matrix.Subtract(value1, value2);
        }

        /// <summary>
        /// Swap the matrix rows and columns.
        /// </summary>
        /// <param name="value">The matrix for transposing operation.</param>
        /// <param name="result">The new <see cref="Matrix"/> which contains the transposing result as an output parameter.</param>
        [Obsolete("Use Matrix.Transpose(Matrix)")] // TODO: obsolete or for inligne ?
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transpose(ref Matrix value, out Matrix result)
        {
            result = Matrix.Transpose(value);
        }
    }
}
