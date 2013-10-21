/* ------------------------------------------------------------------------- */
///
/// TimeStamp.cs
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
    /// TimeStamp
    ///
    /// <summary>
    /// NTP タイムスタンプと DateTime タイムオブジェクトの相互変換機能を
    /// 提供するためのクラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// NTP Timestamp Format (as described in RFC 2030)
    ///                         1                   2                   3
    ///     0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// |                           Seconds                             |
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// |                  Seconds Fraction (0-padded)                  |
    /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class TimeStamp
    {
        /* ----------------------------------------------------------------- */
        ///
        /// ToDateTime
        /// 
        /// <summary>
        /// NTP タイムスタンプから DateTime オブジェクトへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static DateTime ToDateTime(Int64 timestamp)
        {
            var seconds  = (UInt32)(timestamp >> 32);
            var fraction = (UInt32)(timestamp & UInt32.MaxValue);

            var milliseconds = (Int64)seconds * 1000 + (fraction * 1000) / _CompensatingRate32;
            var origin = ((seconds & _ConpensatingRate31) == 0) ? _MidTerm : _InitialTerm;
            
            return origin + TimeSpan.FromMilliseconds(milliseconds);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ToTimeStamp
        /// 
        /// <summary>
        /// DateTime オブジェクトから NTP タイムスタンプへ変換します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static Int64 ToTimeStamp(DateTime datetime)
        {
            var origin = (_MidTerm <= datetime) ? _MidTerm : _InitialTerm;
            var tick = (datetime - origin).TotalMilliseconds;

            var seconds  = (UInt32)((datetime - origin).TotalSeconds);
            var fraction = (UInt32)((tick % 1000) * _CompensatingRate32 / 1000);

            return (Int64)(((UInt64)seconds << 32) | fraction);
        }

        #region Constant variables
        private static readonly Int64  _CompensatingRate32 = 0x100000000L;
        private static readonly UInt32 _ConpensatingRate31 =  0x80000000u;
        private static readonly DateTime _InitialTerm = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime _MidTerm = _InitialTerm.AddSeconds(UInt32.MaxValue);
        #endregion
    }
}
