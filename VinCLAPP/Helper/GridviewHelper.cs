using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VinCLAPP.Helper
{
    public class GridviewHelper
    {
        #region Hide checkbox Helper

        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam,
                                                 ref TVITEM lParam);

        /// <summary>
        ///     Hides the checkbox for the specified node on a TreeView control.
        /// </summary>
        public static void HideCheckBox(TreeView tvw, TreeNode node)
        {
            var tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)] public readonly string lpszText;
            public readonly int cchTextMax;
            public readonly int iImage;
            public readonly int iSelectedImage;
            public readonly int cChildren;
            public readonly IntPtr lParam;
        }

        #endregion
    }
}