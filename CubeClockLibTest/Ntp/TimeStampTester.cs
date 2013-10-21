/* ------------------------------------------------------------------------- */
///
/// Ntp/TimeStampTester.cs
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
using NUnit.Framework;

namespace CubeClockLibTest.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeStampTester
    ///
    /// <summary>
    /// TimeStamp クラスのテストをするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class TimeStampTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestConvert
        /// 
        /// <summary>
        /// 現在時刻をいったん NTP タイムスタンプに変換し、再度 DateTime
        /// オブジェクトに変換するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestConvert()
        {
            var now = DateTime.Now;
            var timestamp = CubeClock.Ntp.TimeStamp.ToTimeStamp(now);
            var converted = CubeClock.Ntp.TimeStamp.ToDateTime(timestamp);
            Assert.AreEqual(now.Year, converted.Year);
            Assert.AreEqual(now.Month, converted.Month);
            Assert.AreEqual(now.Day, converted.Day);
            Assert.AreEqual(now.Hour, converted.Hour);
            Assert.AreEqual(now.Minute, converted.Minute);
            Assert.AreEqual(now.Second, converted.Second);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestConvertDetail
        /// 
        /// <summary>
        /// 引数に指定された日時をいったん NTP タイムスタンプに変換し、
        /// 再度 DateTime オブジェクトに変換するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(1970,  1,  1,  0,  0,  0)]
        [TestCase(2000,  1,  1,  0,  0,  0)]
        [TestCase(2036,  2,  7,  6, 28, 16)]
        [TestCase(2104,  1,  1,  0,  0,  0)]
        public void TestConvertDetail(int year, int month, int day, int hour, int minute, int second)
        {
            var datetime = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
            var timestamp = CubeClock.Ntp.TimeStamp.ToTimeStamp(datetime);
            var converted = CubeClock.Ntp.TimeStamp.ToDateTime(timestamp);
            Assert.AreEqual(year, converted.Year);
            Assert.AreEqual(month, converted.Month);
            Assert.AreEqual(day, converted.Day);
            Assert.AreEqual(hour, converted.Hour);
            Assert.AreEqual(minute, converted.Minute);
            Assert.AreEqual(second, converted.Second);
        }
    }
}
