/* ------------------------------------------------------------------------- */
///
/// Packet.cs
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
using System.Text;

namespace CubeClock.Ntp
{
    /* --------------------------------------------------------------------- */
    ///
    /// Packet
    /// 
    /// <summary>
    /// NTP パケットを表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Packet
    {
        #region Initialization and Termination

        /* ----------------------------------------------------------------- */
        ///
        /// Packet (constructor)
        /// 
        /// <summary>
        /// 送信用の NTP パケットとしてオブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Packet()
        {
            _raw = CreateNewPacket();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Packet (constructor)
        /// 
        /// <summary>
        /// 引数に指定された時刻を用いて、送信用の NTP パケットとして
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Packet(DateTime creation)
        {
            _creation = creation;
            _raw = CreateNewPacket();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Packet (constructor)
        /// 
        /// <summary>
        /// サーバから受信したパケットデータを用いて、オブジェクトを初期化
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Packet(byte[] rawdata)
        {
            _raw = rawdata;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// LeapIndicator
        /// 
        /// <summary>
        /// 閏秒指示子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public LeapIndicator LeapIndicator
        {
            get
            {
                var value = (byte)(_raw[0] >> 6);
                switch (value)
                {
                    case 0: return Ntp.LeapIndicator.NoWarning;
                    case 1: return Ntp.LeapIndicator.LastMinute61;
                    case 2: return Ntp.LeapIndicator.LastMinute59;
                    case 3: return Ntp.LeapIndicator.Alarm;
                    default: break;
                }
                return Ntp.LeapIndicator.Alarm;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        /// 
        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public VersionNumber Version
        {
            get
            {
                var value = (byte)((_raw[0] & 0x38) >> 3);
                switch (value)
                {
                    case 0: return Ntp.VersionNumber.Unknown;
                    case 1: return Ntp.VersionNumber.Version1;
                    case 2: return Ntp.VersionNumber.Version2;
                    case 3: return Ntp.VersionNumber.Version3;
                    case 4: return Ntp.VersionNumber.Version4;
                    default: break;
                }
                return Ntp.VersionNumber.Unknown;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Mode
        /// 
        /// <summary>
        /// 動作モードを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Mode Mode
        {
            get
            {
                var value = (byte)(_raw[0] & 0x7);
                switch (value)
                {
                    case 0: return Ntp.Mode.Unknown;
                    case 1: return Ntp.Mode.SymmetricActive;
                    case 2: return Ntp.Mode.SymmetricPassive;
                    case 3: return Ntp.Mode.Client;
                    case 4: return Ntp.Mode.Server;
                    case 5: return Ntp.Mode.Broadcast;
                    case 6: return Ntp.Mode.Unknown;
                    case 7: return Ntp.Mode.Unknown;
                    default: break;
                }
                return Ntp.Mode.Unknown;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Stratum
        /// 
        /// <summary>
        /// 階層を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Stratum Stratum
        {
            get
            {
                var value = (byte)_raw[1];
                if (value == 0) return Ntp.Stratum.Unspecified;
                else if (value == 1) return Ntp.Stratum.PrimaryReference;
                else if (value <= 15) return Ntp.Stratum.SecondaryReference;
                else return Ntp.Stratum.Reserved;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PollInterval
        /// 
        /// <summary>
        /// ポーリング間隔を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public uint PollInterval
        {
            get
            {
                int value = (sbyte)_raw[2];
                if (value <= 0) return 0;
                else return (uint)Math.Pow(2, value - 1);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Precision
        /// 
        /// <summary>
        /// 時計の精度を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double Precision
        {
            get { return Math.Pow(2, (sbyte)_raw[3]); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RootDelay
        /// 
        /// <summary>
        /// ルート遅延を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double RootDelay
        {
            get
            {
                var value = BitConverter.ToInt32(_raw, 4);
                return FixedPoint.ToDouble(IPAddress.NetworkToHostOrder(value));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RootDispersion
        /// 
        /// <summary>
        /// ルート分散を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public double RootDispersion
        {
            get
            {
                var value = BitConverter.ToInt32(_raw, 8);
                return FixedPoint.ToDouble(IPAddress.NetworkToHostOrder(value));
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReferenceID
        /// 
        /// <summary>
        /// 参照識別子 (Reference Identifier) を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string ReferenceID
        {
            get
            {
                switch (Stratum)
                {
                    case Ntp.Stratum.Unspecified:
                    case Ntp.Stratum.PrimaryReference:
                        return Encoding.ASCII.GetString(_raw, 12, 4).TrimEnd(new char());
                    case Ntp.Stratum.SecondaryReference:
                        return GetSecondaryReferenceID();
                    default:
                        break;
                }
                return string.Empty;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReferenceTimestamp
        /// 
        /// <summary>
        /// 参照タイムスタンプ（一次時刻情報源の最終時計参照時刻）を取得
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime ReferenceTimestamp
        {
            get
            {
                var value = BitConverter.ToInt64(_raw, 16);
                return Timestamp.ToDateTime(IPAddress.NetworkToHostOrder(value)).ToLocalTime();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OriginateTimestamp
        /// 
        /// <summary>
        /// 開始タイムスタンプ（クライアントからサーバへリクエストを発信した
        /// 時刻）を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime OriginateTimestamp
        {
            get
            {
                var value = BitConverter.ToInt64(_raw, 24);
                return Timestamp.ToDateTime(IPAddress.NetworkToHostOrder(value)).ToLocalTime();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReceiveTimestamp
        /// 
        /// <summary>
        /// 受信タイムスタンプ（サーバへリクエストが到着した時刻）を取得
        /// します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime ReceiveTimestamp
        {
            get
            {
                var value = BitConverter.ToInt64(_raw, 32);
                return Timestamp.ToDateTime(IPAddress.NetworkToHostOrder(value)).ToLocalTime();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TransmitTimestamp
        /// 
        /// <summary>
        /// 送信タイムスタンプ（サーバからクライアントに応答が発信された
        /// 時刻）を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime TransmitTimestamp
        {
            get
            {
                var value = BitConverter.ToInt64(_raw, 40);
                return Timestamp.ToDateTime(IPAddress.NetworkToHostOrder(value)).ToLocalTime();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// KeyID
        /// 
        /// <summary>
        /// 鍵識別子を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string KeyID
        {
            get
            {
                if (_raw.Length < 52) return string.Empty;
                else return Encoding.ASCII.GetString(_raw, 48, 4).TrimEnd(new char());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MessageDigest
        /// 
        /// <summary>
        /// メッセージダイジェストを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string MessageDigest
        {
            get
            {
                if (_raw.Length < 68) return string.Empty;
                else return Encoding.ASCII.GetString(_raw, 52, 16).TrimEnd(new char());
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RawData
        /// 
        /// <summary>
        /// 生のパケットデータを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public byte[] RawData
        {
            get { return _raw; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreationTime
        /// 
        /// <summary>
        /// この Packet オブジェクトが生成された時刻を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime CreationTime
        {
            get { return _creation; }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NetworkDelay
        /// 
        /// <summary>
        /// 通信遅延時間 (RTT) を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// クライアントがリクエストを送信した時刻を t_s 、サーバが
        /// クライアントのリクエストを受信した時刻を T_r 、サーバが
        /// レスポンスを送信した時刻を T_s 、クライアントがサーバの
        /// レスポンスを受信した時刻を t_r とすると、通信遅延時間 δ は、
        /// 以下の式で表されます。
        /// 
        /// δ = (t_r - t_s) - (T_s - T_r)
        /// 
        /// これは、パケットの往復時間からサーバの処理時間を引いたものに
        /// 相当します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan NetworkDelay
        {
            get { return (CreationTime - OriginateTimestamp) - (TransmitTimestamp - ReceiveTimestamp); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LocalClockOffset
        /// 
        /// <summary>
        /// NTP サーバーとの差異（遅延時刻）を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// クライアントがリクエストを送信した時刻を t_s 、サーバが
        /// クライアントのリクエストを受信した時刻を T_r 、サーバが
        /// レスポンスを送信した時刻を T_s 、クライアントがサーバの
        /// レスポンスを受信した時刻を t_r とします。
        /// この時、往路と復路の通信時間に差がないと仮定すれば、クライアント
        /// の時計の遅延時間 θ は、以下の式で表される。
        /// 
        /// θ = (T_s + T_r) / 2 - (t_s + t_r) / 2
        ///
        /// これは、サーバ、クライアントの、パケットの送信時刻、受信時刻の
        /// 平均の差を表します。尚、実装上の都合として、以下のように式変形
        /// して計算を行います。
        /// 
        /// θ = ((T_r - t_s) + (T_s - t_r)) / 2
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public TimeSpan LocalClockOffset
        {
            get
            {
                var ticks = ((ReceiveTimestamp - OriginateTimestamp) + (TransmitTimestamp - CreationTime)).Ticks;
                return new TimeSpan(ticks / 2);
            }
        }

        #endregion

        #region Public methods

        /* ----------------------------------------------------------------- */
        ///
        /// IsValid
        /// 
        /// <summary>
        /// NTP パケットとして有効なものであるかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsValid()
        {
            return (_raw != null & _raw.Length >= 48);
        }

        #endregion

        #region Other methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateNewPacket
        /// 
        /// <summary>
        /// 送信用の新しいパケットを生成します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private byte[] CreateNewPacket()
        {
            var dest = new byte[48];
            dest[0] = (byte)(_ClientLeapIndicator | _ClientVersion << 3 | _ClientMode);

            var timestamp = Timestamp.ToTimestamp(CreationTime.ToUniversalTime());
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(timestamp));
            Array.ConstrainedCopy(bytes, 0, dest, 40, 8);

            return dest;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetSecondaryReferenceID
        /// 
        /// <summary>
        /// 階層が 2-15 次参照 (NTP、SNTP サーバを経由して参照している) の
        /// 場合の参照識別子 (Reference Identifier) を取得します。
        /// </summary>
        /// 
        /// <remarks>
        /// TODO: バージョン 4 の場合の変換を実装する。RFC 2030 によると、
        /// NTP バージョン 4の従属的なサーバーでは基準源の最終送信タイム
        /// スタンプの下位 32 ビットになるとの事。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private string GetSecondaryReferenceID()
        {
            switch (Version)
            {
                case VersionNumber.Version3:
                    var ipaddr = new IPAddress(new byte[] { _raw[12], _raw[13], _raw[14], _raw[15] });
                    return ipaddr.ToString();
                case VersionNumber.Version4:
                    return string.Empty;
                default:
                    break;
            }
            return string.Empty;
        }

        #endregion

        #region Variables
        private byte[] _raw = null;
        private DateTime _creation = DateTime.Now;
        #endregion

        #region Constant variables
        private static readonly byte _ClientLeapIndicator = 0x00;
        private static readonly byte _ClientMode = 0x03;
        private static readonly int  _ClientVersion = 3;
        #endregion
    }
}
