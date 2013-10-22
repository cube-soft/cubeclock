/* ------------------------------------------------------------------------- */
///
/// Ntp/TimeSyncTester.cs
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
using System.ComponentModel;
using NUnit.Framework;

namespace CubeClockLibTest.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// TimeSyncTester
    /// 
    /// <summary>
    /// TimeSync クラスのテストを行うためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class TimeSyncTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestRun
        /// 
        /// <summary>
        /// NTP サーバから時刻を取得して同期するテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// Windows Vista 以降、時刻の同期には管理者権限を必要とします。
        /// そのため、時刻を設定する部分の失敗 (Win32Exception) は無視する
        /// 事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRun()
        {
            try
            {
                var sync = new CubeClock.Ntp.TimeSync();
                sync.Run();
            }
            catch (Win32Exception err) { Assert.Ignore(err.ToString()); }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestRunWithPacket
        /// 
        /// <summary>
        /// NTP サーバから時刻を取得して同期するテストを行います。
        /// </summary>
        /// 
        /// <remarks>
        /// Windows Vista 以降、時刻の同期には管理者権限を必要とします。
        /// そのため、時刻を設定する部分の失敗 (Win32Exception) は無視する
        /// 事とします。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestRunWithPacket()
        {
            try
            {
                var client = new CubeClock.Ntp.Client();
                var packet = client.Receive();
                Assert.IsNotNull(packet);
                Assert.IsTrue(packet.IsValid());

                var sync = new CubeClock.Ntp.TimeSync();
                sync.Run(packet);
            }
            catch (Win32Exception err) { Assert.Ignore(err.ToString()); }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }
    }
}
