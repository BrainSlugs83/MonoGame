// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Contains commonly used precalculated values and mathematical operations.
    /// </summary>
    public static class MathHelper
    {
    	/// <summary>
        /// Represents the mathematical constant e(2.71828175).
        /// </summary>
        public const float E = (float)Math.E;
        
        /// <summary>
        /// Represents the log base ten of e(0.4342945).
        /// </summary>
        public const float Log10E = 0.4342945f;
        
        /// <summary>
        /// Represents the log base two of e(1.442695).
        /// </summary>
        public const float Log2E = 1.442695f;
        
        /// <summary>
        /// Represents the value of pi(3.14159274).
        /// </summary>
        public const float Pi = (float)Math.PI;
        
        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);
        
        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);
        
        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPi = (float)(Math.PI * 2.0);
        
        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
        {
            return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
        }

	/// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="value1">The first position in the interpolation.</param>
        /// <param name="value2">The second position in the interpolation.</param>
        /// <param name="value3">The third position in the interpolation.</param>
        /// <param name="value4">The fourth position in the interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
        {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precission
            double amountSquared = amount * amount;
            double amountCubed = amountSquared * amount;
            return (float)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

 	/// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            value = (value < min) ? min : value;

            // There's no check to see if min > max.
            return value;
        }
        
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        { 
            value = (value > max) ? max : value; 
            value = (value < min) ? min : value; 
            return value;
        }
        
        /// <summary>
        /// Calculates the absolute value of the difference of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>Distance between the two values.</returns>
        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }
        
        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        /// <param name="value1">Source position.</param>
        /// <param name="tangent1">Source tangent.</param>
        /// <param name="value2">Source position.</param>
        /// <param name="tangent2">Source tangent.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of the Hermite spline interpolation.</returns>
        public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            // All transformed to double not to lose precission
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (amount == 0f)
                result = value1;
            else if (amount == 1f)
                result = value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }
        
        
        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
        /// <returns>Interpolated value.</returns> 
        /// <remarks>This method performs the linear interpolation based on the following formula.
        /// <c>value1 + (value2 - value1) * amount</c>
        /// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
        /// </remarks>
        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

	/// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The greater value.</returns>
        public static int Max(int value1, int value2)
        {
            return value1 > value2 ? value1 : value2;
        }
        
        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        /// <summary>
        /// Returns the lesser of two values.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <returns>The lesser value.</returns>
        public static int Min(int value1, int value2)
        {
            return value1 < value2 ? value1 : value2;
        }
        
        /// <summary>
        /// Interpolates between two values using a cubic equation.
        /// </summary>
        /// <param name="value1">Source value.</param>
        /// <param name="value2">Source value.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Interpolated value.</returns>
        public static float SmoothStep(float value1, float value2, float amount)
        {
            // It is expected that 0 < amount < 1
            // If amount < 0, return value1
            // If amount > 1, return value2
            float result = MathHelper.Clamp(amount, 0f, 1f);
            result = MathHelper.Hermite(value1, 0f, value2, 0f, result);

            return result;
        }
        
        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = 180 / pi
        /// </remarks>
        public static float ToDegrees(float radians)
        { 
            return (float)(radians * 57.295779513082320876798154814105);
        }
        
        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        /// <remarks>
        /// This method uses double precission internally,
        /// though it returns single float
        /// Factor = pi / 180
        /// </remarks>
        public static float ToRadians(float degrees)
        { 
            return (float)(degrees * 0.017453292519943295769236907684886);
        }
	 
        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
	public static float WrapAngle(float angle)
	{
            angle = (float)Math.IEEERemainder((double)angle, 6.2831854820251465);
	    if (angle <= -3.14159274f)
	    {
		angle += 6.28318548f;
	    }
	    else
	    {
		if (angle > 3.14159274f)
		{
		   angle -= 6.28318548f;
		}
	    }
	    return angle;
	}

 	/// <summary>
        /// Determines if value is powered by two.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <returns><c>true</c> if <c>value</c> is powered by two; otherwise <c>false</c>.</returns>
	public static bool IsPowerOfTwo(int value)
	{
	     return (value > 0) && ((value & (value - 1)) == 0);
	}

        /// <summary>
        /// Class for generating random numbers consistantly when using Mono or Microsoft compilers
        /// </summary>
        public class Random
        {
            #region Static methods

            public static long GenerateSeed() 
            {
                byte[] guid = new Guid().ToByteArray();
                return (long)(BitConverter.ToUInt64(guid, 0) ^ BitConverter.ToUInt64(guid, 7));
            }

            #endregion

            #region Private Fields

            private ulong seed;
            private ulong lastValue;

            #endregion

            #region Properties

            public long Seed 
            { 
                get 
                { 
                    return (long)this.seed; 
                }
                set 
                {
                    this.seed = (ulong)value;
                }
            }

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the random number generator using a new GUID as a default seed value
            /// </summary>
            public Random() : this((ulong)GenerateSeed())
            {
            }

            /// <summary>
            /// Initializes a new instance of the random number generator using the specified seed
            /// </summary>
            /// <param name="seed">Seed to be used when computing new random numbers</param>
            public Random(ulong seed)
            {
                this.seed = seed;
                this.lastValue = seed;
            }

            #endregion

            #region Public methods

            int Next() 
            {
                throw new NotImplementedException();
            }

            #endregion

            #region Private methods

            private ulong XORShift64Star()
            {
                lastValue ^= lastValue >> 12;
                lastValue ^= lastValue << 25;
                lastValue ^= lastValue >> 27;
                return lastValue * ulong.MaxValue;
            }

            private ulong Remap(ulong value, ulong oldMin, ulong oldMax, ulong newMin, ulong newMax) 
            {
                // Check for zero range
                if (oldMin == oldMax || newMin == newMax)
                    throw new ArithmeticException("Zero range used in MathHelper.Random.Remap()");

                // Check for reversed input
                if (Min(oldMin, oldMax) != oldMin)
                    throw new ArgumentException("Output range maximum is reversed with minimum", "oldMax");
                else if (Min(newMin, newMax) != newMin)
                    throw new ArgumentException("Output range maximum is reversed with minimum", "newMax");

                // Compute the new value and return it
                return (((value - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
            }

            #region Integer values

            private byte UInt64ToInt8(ulong uint64) 
            {
                // Map the uint64 onto a range from 0 (same as byte.MinValue) to byte.MaxValue
                return (byte)Remap(uint64, 0UL, ulong.MaxValue, 0UL, (ulong)(byte.MaxValue));
            }

            private short UInt64ToInt16(ulong uint64) 
            {
                // Map the uint64 onto a range from 0 to short.MaxValue * 2, then remap it to short.MinValue-short.MaxValue
                    // because the Remap function returns an unsigned long integer
                return (short)((long)(Remap(uint64, 0UL, ulong.MaxValue, 0UL, (((ulong)(short.MaxValue)) * 2))) + (long)short.MinValue);
            }

            private int UInt64ToInt32(ulong uint64) 
            {
                // Map the uint64 onto a range from 0 to int.MaxValue * 2, then remap it to int.MinValue-int.MaxValue
                    // because the Remap function returns an unsigned long integer
                return (int)((long)(Remap(uint64, 0UL, ulong.MaxValue, 0UL, (((ulong)(int.MaxValue)) * 2))) + (long)int.MinValue);
            }

            private long UInt64ToInt64(ulong uint64) 
            {
                // Map the uint64 onto a range from 0 to long.MaxValue * 2, then remap it to long.MinValue-long.MaxValue
                    // because the Remap function returns an unsigned long integer
                return (long)(Remap(uint64, 0UL, ulong.MaxValue, 0UL, (((ulong)(long.MaxValue)) * 2))) + (long)long.MinValue;
            }

            #endregion

            #region Floating point values

            private float UInt64ToFloat(ulong uint64)
            {
                return (float)UInt64ToDouble(uint64);
            }

            private double UInt64ToDouble(ulong uint64) 
            {
                return Convert.ToDouble(UInt64ToInt64(uint64));
            }

            #endregion

            #endregion
        }
    }
}
