using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace AdvancedAnalyticsCSharpSample
{ 
    class Program
    {
        static void Main(string[] args)
        {
            String appId = "5846b40946c9112c443e66ff";
            String appVersion = "1.0.0";

            /**
             * Initialize the service
             */
            Caphyon.AdvancedAnalytics lAdvancedAnalytics = new Caphyon.AdvancedAnalytics();
            lAdvancedAnalytics.Start(appId, appVersion);

            /**
             * Check to see if tracking is enabled.
             * This must be done once at startup. The user option needs to persist.
             */
            String keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Caphyon\\LaunchAnalytics\\" 
                             + appId + "\\" + appVersion;
            bool regValue = false;

            try {
                regValue = (string)Registry.GetValue(keyName, "C# Sample", false) == "True" ? true : false;
            } catch(Exception) {
                Console.Write("There is no registry yet. Assuming false.");
            }

            if (regValue == false)
            { 
                DialogResult res = MessageBox.Show("Do you want to enable tracking?",
                        "LaunchAnalytics", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                    lAdvancedAnalytics.Enable(true);
                else
                    lAdvancedAnalytics.Enable(false);
                Registry.SetValue(keyName, "C# Sample", true);
            }

            // Run your app
            RunApp();

            // When the app needs to close, call AA_Stop function.
            lAdvancedAnalytics.Stop();
        }

        static void RunApp()
        {
            MessageBox.Show("The app is running.");
        }
    }
}
