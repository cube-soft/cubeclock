/* ------------------------------------------------------------------------- */
///
/// Client.cs
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
using System.Net;
using System.Net.Sockets;

namespace CubeClock.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// Client
    ///
    /// <summary>
    /// NTP でサーバと通信するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Client
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// Client (constructor)
        /// 
        /// <summary>
        /// 引数に指定されたホスト名、または IP アドレスを用いてオブジェクト
        /// を初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Client(string host_or_ipaddr, int port = 123)
        {
            _host = Dns.GetHostEntry(host_or_ipaddr);
            _endpoint = new IPEndPoint(_host.AddressList[0], port);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Client (constructor)
        /// 
        /// <summary>
        /// 引数に IP アドレスを用いてオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Client(IPAddress ipaddr, int port = 123) : this(ipaddr.ToString(), port) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Receive
        /// 
        /// <summary>
        /// NTP サーバと通信を行い、NTP パケットを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Packet Receive()
        {
            var socket = CreateSocket();
            SendTo(socket);
            return ReceiveFrom(socket);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Host
        /// 
        /// <summary>
        /// 通信する NTP サーバのホスト情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IPHostEntry Host
        {
            get { return _host; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Port
        /// 
        /// <summary>
        /// 通信する NTP サーバのポート番号を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Port
        {
            get { return _endpoint.Port; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReceiveTimeout
        /// 
        /// <summary>
        /// NTP パケット受信時のタイムアウト時間 (ms) を取得、または設定
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int ReceiveTimeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateSocket
        /// 
        /// <summary>
        /// 新しい UDP ソケットを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Socket CreateSocket()
        {
            var dest = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            dest.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, _timeout);
            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// SendTo
        /// 
        /// <summary>
        /// NTP サーバへパケットを送信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void SendTo(Socket socket)
        {
            var packet = new Ntp.Packet();
            socket.SendTo(packet.RawData, _endpoint);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReceiveFrom
        /// 
        /// <summary>
        /// NTP サーバからパケットを受信します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private Ntp.Packet ReceiveFrom(Socket socket)
        {
            byte[] raw = new byte[48 + (32 + 128) / 8];
            EndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            var bytes = socket.ReceiveFrom(raw, ref endpoint);
            if (bytes < 48) throw new ArgumentException("too few packet");
            return new Ntp.Packet(raw);
        }

        #endregion

        #region Variables
        private IPHostEntry _host = null;
        private IPEndPoint _endpoint = null;
        private int _timeout = 2000;
        #endregion
    }
}
