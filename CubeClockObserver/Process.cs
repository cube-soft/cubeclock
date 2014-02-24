/* ------------------------------------------------------------------------- */
///
/// Process.cs
///
/// Copyright (c) 2014 CubeSoft, Inc. All rights reserved.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.InteropServices;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// Process
    /// 
    /// <summary>
    /// プロセスに関する補助関数群です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Process
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TryActivate
        /// 
        /// <summary>
        /// 現在のプロセスと同名のプロセスのアクティブ化を試みます。
        /// 失敗した場合、false が返ります。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static bool TryActivate()
        {
            var current = System.Diagnostics.Process.GetCurrentProcess();
            var others = System.Diagnostics.Process.GetProcessesByName(current.ProcessName);

            foreach (var proc in others)
            {
                if (proc.Id == current.Id) continue;
                if (proc.MainModule.FileName.Equals(current.MainModule.FileName))
                {
                    if (Win32Api.IsIconic(proc.MainWindowHandle)) Win32Api.ShowWindowAsync(proc.MainWindowHandle, SW_RESTORE);
                    Win32Api.SetForegroundWindow(proc.MainWindowHandle);
                    return true;
                }
            }
            return false;
        }

        #region Win32 APIs

        internal class Win32Api
        {
            [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
            public static extern bool IsIconic(IntPtr hWnd);
        }

        #endregion

        #region Constant variables
        private const int SW_NORMAL = 1;
        private const int SW_RESTORE = 9;
        #endregion
    }
}
