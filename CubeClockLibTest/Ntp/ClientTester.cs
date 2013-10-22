﻿/* ------------------------------------------------------------------------- */
///
/// Ntp/ClientTester.cs
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
    /// ClientTester
    ///
    /// <summary>
    /// Client クラスのテストをするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class ClientTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestReceive
        /// 
        /// <summary>
        /// NTP サーバと通信するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestReceive()
        {
            try
            {
                var server = "time.windows.com";
                var client = new CubeClock.Ntp.Client(server);
                Assert.IsNotNullOrEmpty(client.Host.HostName);
                Assert.IsTrue(client.Host.AddressList.Length > 0);
                Assert.AreEqual(123, client.Port);
                Assert.AreEqual(2000, client.ReceiveTimeout);

                // TODO: どんなアサーションを入れれば良いか…？
                var packet = client.Receive();
            }
            catch (Exception err) { Assert.Fail(err.ToString()); }
        }
    }
}