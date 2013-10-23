/* ------------------------------------------------------------------------- */
///
/// Ntp/ObserverTester.cs
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

namespace CubeClockTest.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// ObserverTester
    ///
    /// <summary>
    /// Observer クラスのテストをするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ObserverTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestRefresh
        /// 
        /// <summary>
        /// 少なくとも 1 回、NTP サーバから結果を取得するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRefresh()
        {
            try
            {
                var observer = new CubeClock.Ntp.Observer();
                Assert.IsNotNull(observer.Client);
                Assert.AreEqual(60 * 60 * 1000, observer.TimeToLive);
                Assert.IsNull(observer.LastResult);
                Assert.IsFalse(observer.IsValid);
                Assert.AreEqual(0, observer.LocalClockOffset.TotalMilliseconds);
                observer.Refresh();
                Assert.IsNotNull(observer.LastResult);
                Assert.IsTrue(observer.IsValid);
                Assert.IsTrue(Math.Abs(observer.LocalClockOffset.TotalMilliseconds) > 0);
            }
            catch (Exception err)
            {
                Assert.Fail(err.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRefresh
        /// 
        /// <summary>
        /// 途中で NTP サーバを変更するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestReset()
        {
            try
            {
                var observer = new CubeClock.Ntp.Observer();
                Assert.IsNotNull(observer.Client);
                Assert.IsNull(observer.LastResult);
                Assert.IsFalse(observer.IsValid);

                observer.Reset("ntp.nict.jp");
                Assert.IsNotNull(observer.Client);
                Assert.IsNotNull(observer.LastResult);
                Assert.IsTrue(observer.IsValid);
            }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }
    }
}
