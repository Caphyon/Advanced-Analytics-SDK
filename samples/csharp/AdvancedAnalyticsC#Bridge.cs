using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

/**
 * Class that loads the native dll which contains the data sending logic and
 * calls the proper methods based on the process type (x86 vs x64) via
 * dynamic linking.
 *  
 */

namespace Caphyon
{
    class AdvancedAnalytics
    {
        private IntPtr dllHandle = IntPtr.Zero;

        /**
         * Private helper class for dll loading. 
         */
        #region Dll loader helper class.
        private class DllLoader
        {
            /**
             * Load the library provided by libname in memory. The library should be in
             * the same folder as the application binaries.
             */
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr LoadLibrary(string aLibname);

            /**
             * Releases the handles to the loaded library.
             */
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern bool FreeLibrary(IntPtr hModule);

            /**
             * Retrieve the address of a specific function.
             */
            [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        }
        #endregion

        /**
         * LaunchAnalyticsSDK functions declarations found in the native library.
         */
        #region Native function declarations.
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AA_Start_Native(
            [param: MarshalAs(UnmanagedType.LPTStr)]
            string aTrackingCode,
            [param: MarshalAs(UnmanagedType.LPTStr)]
            string aApplicationVersion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AA_Enable_Native(
            [param: MarshalAs(UnmanagedType.Bool)]
            bool aEnabledFlag);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool AA_IsEnabled_Native();
            [return: MarshalAs(UnmanagedType.Bool)]

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AA_Stop_Native();
        #endregion

        #region Sdk implementation
        /**
         * Start advanced analytics tracking.
         * This method must be called before any other calls to the SDK.
         *
         * @param aTrackingCode       - a valid tracking code generated from https://installeranalytics.com/
         * @param aApplicationVersion - the current version of your application.
         *        NOTE! Make sure to keep this parameter in sync with the version of your install package
         *        because this version will appear in the generated reports.
         *
         *
         * NOTE! The tracking client is disabled by default!
         * In order to start sending data to the server, the AA_Enable method must be called at least once
         * with a value of TRUE.
         */
        public void Start(string aTrackingCode, string aApplicationVersion)
        {
            string x86DllName = "AdvancedAnalytics.dll";
            string x64DllName = "AdvancedAnalytics_x64.dll";

            if (IntPtr.Size == 4)
                this.dllHandle = DllLoader.LoadLibrary(x86DllName);
            else
                this.dllHandle = DllLoader.LoadLibrary(x64DllName);

#if DEBUG
            string libName = IntPtr.Size == 4 ? x86DllName : x64DllName;
            if (this.dllHandle == IntPtr.Zero)
                Console.Write("Cannot load " + libName + "! Make sure that the dll is in the same folder as the executable.");
#endif

            if (this.dllHandle != IntPtr.Zero)
            {
                IntPtr startHandle = DllLoader.GetProcAddress(this.dllHandle, "AA_Start");
                AA_Start_Native AA_Start_NativeDelegate = (AA_Start_Native)Marshal.GetDelegateForFunctionPointer
                                                    (startHandle, typeof(AA_Start_Native));
                AA_Start_NativeDelegate(aTrackingCode, aApplicationVersion);
            }
        }

        /**
         * Stop advanced analytics tracking.
         * After this method is called, any further calls to the SDK will not work.
         */
        public void Stop()
        {
            if (this.dllHandle != IntPtr.Zero)
            {
                IntPtr stopHandle = DllLoader.GetProcAddress(this.dllHandle, "AA_Stop");
                AA_Stop_Native AA_Stop_NativeDelegate = (AA_Stop_Native)Marshal.GetDelegateForFunctionPointer
                                                    (stopHandle, typeof(AA_Stop_Native));
                AA_Stop_NativeDelegate();
                DllLoader.FreeLibrary(this.dllHandle);
            }
        }

        /**
         * Enable/disable data sending to server.
         * The SDK will persist the selected option and on the next application start,
         * it will pick up the previously selected option.
         * 
         * @param aEnable - true for enabling, false for disabling.
         */
        public void Enable(bool aEnable)
        {
            if (this.dllHandle != IntPtr.Zero)
            {
                IntPtr enableHandle = DllLoader.GetProcAddress(this.dllHandle, "AA_Enable");
                AA_Enable_Native AA_Enable_NativeDelegate = (AA_Enable_Native)Marshal.GetDelegateForFunctionPointer
                                                    (enableHandle, typeof(AA_Enable_Native));
                AA_Enable_NativeDelegate(aEnable);
            }
        }

        /**
         * Checks if data sending is enabled.
         */
        public bool IsEnabled()
        {
            if (this.dllHandle != IntPtr.Zero)
            {
                IntPtr isEnabledHandle = DllLoader.GetProcAddress(this.dllHandle, "AA_IsEnabled");
                AA_IsEnabled_Native AA_IsEnabled_NativeDelegate = (AA_IsEnabled_Native)Marshal.GetDelegateForFunctionPointer
                                                    (isEnabledHandle, typeof(AA_IsEnabled_Native));
                return AA_IsEnabled_NativeDelegate();
            }
            return false;
        }
        #endregion
    }
}
