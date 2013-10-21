/* ------------------------------------------------------------------------- */
///
/// Parameter.cs
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
    /// LeapIndicator
    /// 
    /// <summary>
    /// 閏秒指示子 (LI: Leap Indicator) の状態を定義した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum LeapIndicator : uint
    {
        NoWarning = 0,  // 0 - No warning
        LastMinute61,   // 1 - Last minute has 61 seconds
        LastMinute59,   // 2 - Last minute has 59 seconds
        Alarm           // 3 - Alarm condition (clock not synchronized)
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Mode
    /// 
    /// <summary>
    /// 動作モードの状態を定義した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Mode : uint
    {
        Unknown = 0,        // 0, 6, 7 - Reserved
        SymmetricActive,    // 1 - Symmetric active
        SymmetricPassive,   // 2 - Symmetric pasive
        Client,             // 3 - Client
        Server,             // 4 - Server
        Broadcast,          // 5 - Broadcast
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Stratum
    /// 
    /// <summary>
    /// 階層の状態を定義した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum Stratum
    {
        Unspecified,            // 0 - unspecified or unavailable
        PrimaryReference,       // 1 - primary reference (e.g. radio-clock)
        SecondaryReference,     // 2-15 - secondary reference (via NTP or SNTP)
        Reserved                // 16-255 - reserved
    }
}
