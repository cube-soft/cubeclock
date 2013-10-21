/* ------------------------------------------------------------------------- */
///
/// Ntp/PacketTester.cs
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
    /// TimestampTester
    ///
    /// <summary>
    /// Packet クラスのテストをするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    public class PacketTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestCreatePacket
        /// 
        /// <summary>
        /// 送信用の NTP パケットを生成するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestCreatePacket() {
            var packet = new CubeClock.Ntp.Packet();
            Assert.IsTrue(packet.IsValid());
            Assert.AreEqual(CubeClock.Ntp.LeapIndicator.NoWarning, packet.LeapIndicator);
            Assert.AreEqual(3, packet.Version);
            Assert.AreEqual(CubeClock.Ntp.Mode.Client, packet.Mode);
            Assert.AreEqual(CubeClock.Ntp.Stratum.Unspecified, packet.Stratum);
            Assert.AreEqual(0, packet.PollInterval);
            Assert.AreEqual(1.0, packet.Precision);
            Assert.AreEqual(0.0, packet.RootDelay);
            Assert.AreEqual(0.0, packet.RootDispersion);
            Assert.IsNullOrEmpty(packet.ReferenceID);
            Assert.IsNullOrEmpty(packet.KeyID);
            Assert.IsNullOrEmpty(packet.MessageDigest);
            Assert.AreEqual(packet.CreationTime.Year,   packet.TransmitTimestamp.Year);
            Assert.AreEqual(packet.CreationTime.Month,  packet.TransmitTimestamp.Month);
            Assert.AreEqual(packet.CreationTime.Day,    packet.TransmitTimestamp.Day);
            Assert.AreEqual(packet.CreationTime.Hour,   packet.TransmitTimestamp.Hour);
            Assert.AreEqual(packet.CreationTime.Minute, packet.TransmitTimestamp.Minute);
            Assert.AreEqual(packet.CreationTime.Second, packet.TransmitTimestamp.Second);
        }
    }
}
