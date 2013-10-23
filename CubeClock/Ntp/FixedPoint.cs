/* ------------------------------------------------------------------------- */
///
/// FixedPoint.cs
/// 
/// Copyright (c) 2013 CubeSoft, Inc. All rights reserved.
///
/// MIT License
///
/// Permission is hereby granted, free of charge, to any person obtaining a
/// copy of this software and associated documentation files (the "Software"),
/// to deal in the Software without restriction, including without limitation
/// the rights to use, copy, modify, merge, publish, distribute, sublicense,
/// and/or sell copies of the Software, and to permit persons to whom the
/// Software is furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included
/// in all copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
/// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
/// DEALINGS IN THE SOFTWARE.
///
/* ------------------------------------------------------------------------- */
using System;

namespace CubeClock.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// Timestamp
    ///
    /// <summary>
    /// 符号付き 32bit 固定小数点数から double への変換機能を提供するための
    /// クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class FixedPoint
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToDouble
        /// 
        /// <summary>
        /// 符号付き 32bit 固定小数点数から double へ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static double ToDouble(Int32 value)
        {
            var number = (Int16)(value >> 16);
            var fraction = (UInt16)(value & Int16.MaxValue);
            return number + fraction / _CompensatingRate16;
        }

        #region Constant variables
        private static readonly double _CompensatingRate16 = 0x10000d;
        #endregion
    }
}
