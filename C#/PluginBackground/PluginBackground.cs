using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using Rainmeter;
using Microsoft.Win32;
using System.IO;

/**
 * Return current background image using the GetString function, which returns non-null.
 * This implies the GetValue function isn't called. We don't take any input parameters 
 * or read any variables from the API.
 */
namespace PluginBackground
{
    //class Measure
    //{
    //    static public implicit operator Measure(IntPtr data)
    //    {
    //        return (Measure)GCHandle.FromIntPtr(data).Target;
    //    }
    //}

    public class Plugin
    {
        const int offsetStart = 24;
        const int offsetEnd = 0x220;

        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            //data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
            //Rainmeter.API api = (Rainmeter.API)rm;
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            //GCHandle.FromIntPtr(data).Free();
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            //Measure measure = (Measure)data;
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            //Measure measure = (Measure)data;

            return 0.0;
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            //Measure measure = (Measure)data;
            try
            {
                var desktop = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
                var value = desktop.GetValue("TranscodedImageCache");
                var bytesAll = (byte[])value;

                int N = offsetEnd - offsetStart;
                var bytesPath = new byte[N];
                Array.Copy(bytesAll, offsetStart, bytesPath, 0, N);
                string path = Encoding.Unicode.GetString(bytesPath).Replace("\0", string.Empty);

                return Marshal.StringToHGlobalUni(path); //returning IntPtr.Zero will result in it not being used
            }
            catch (Exception exc)
            {
                return Marshal.StringToHGlobalUni(exc.ToString());
            }
        }

        //[DllExport]
        //public static void ExecuteBang(IntPtr data, [MarshalAs(UnmanagedType.LPWStr)]String args)
        //{
        //    Measure measure = (Measure)data;
        //}

        //[DllExport]
        //public static IntPtr (IntPtr data, int argc,
        //    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] string[] argv)
        //{
        //    Measure measure = (Measure)data;
        //
        //    return Marshal.StringToHGlobalUni(""); //returning IntPtr.Zero will result in it not being used
        //}
    }
}
