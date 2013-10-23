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
        /// 少なくとも 1 回 NTP サーバから結果を取得して、その結果をクリア
        /// するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestNormalCase()
        {
            try
            {
                var observer = new CubeClock.Ntp.Observer();
                observer.Client.ReceiveTimeout = TimeSpan.FromSeconds(1);
                Assert.IsNotNull(observer.Client);
                Assert.AreEqual(TimeSpan.FromHours(1), observer.TimeToLive);
                Assert.AreEqual(0, observer.FailedCount);

                observer.Refresh();
                Assert.IsNotNull(observer.LastResult);
                Assert.IsTrue(observer.IsValid);
                Assert.AreEqual(0, observer.FailedCount);
                Assert.IsTrue(Math.Abs(observer.LocalClockOffset.TotalMilliseconds) > 0);

                observer.Clear();
                Assert.IsNull(observer.LastResult);
                Assert.IsFalse(observer.IsValid);
                Assert.AreEqual(0, observer.FailedCount);
            }
            catch (Exception err)
            {
                Assert.Fail(err.ToString());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestReset
        /// 
        /// <summary>
        /// 途中で NTP サーバを変更するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestReset()
        {
            var observer = new CubeClock.Ntp.Observer();
            observer.Client.ReceiveTimeout = TimeSpan.FromSeconds(1);
            Assert.IsNotNull(observer.Client);
            Assert.AreEqual(0, observer.FailedCount); 

            try
            {
                observer.Reset("ntp.ring.gr.jp");
                Assert.IsNotNull(observer.Client);
                Assert.IsNull(observer.LastResult);
                Assert.IsFalse(observer.IsValid);
                Assert.AreEqual(0, observer.FailedCount);
            }
            catch (Exception err) { Assert.Fail(err.ToString()); }

            var previous = observer.Client;
            Assert.IsNotNull(previous);

            try
            {
                observer.Client.ReceiveTimeout = TimeSpan.FromMilliseconds(100);
                observer.Reset("404.not.found.com");
                Assert.Fail("never reached");
            }
            catch (Exception err)
            {
                Assert.Pass(err.ToString());
                Assert.AreEqual(previous, observer.Client);
                Assert.IsTrue(observer.IsValid);
            }
        }
    }
}
