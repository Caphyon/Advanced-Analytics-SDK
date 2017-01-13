package com.caphyon;
import java.util.prefs.*;

import javax.swing.JOptionPane;

public class Program {
	
	public static void main(String[] args) {
		
		String appId = "5846b40946c9112c443e66ff";
		String appVersion = "1.0.0";
		String PATH = "launchanalytics/" + appId + "/" + appVersion;
		String KEY = "javasample";	

		// initialize the service.
		AdvancedAnalytics.Start(appId, appVersion);
		
		// check user settings.
		Preferences prefs = Preferences.userRoot().node(PATH);
		boolean hasbeenset = prefs.getBoolean(KEY, false);

		if (!hasbeenset) 
		{
			// check if the sdk is enabled
			if (AdvancedAnalytics.IsEnabled() == false)
			{
				// ask user if he wants to allow tracking
				int dialogResult = JOptionPane.showConfirmDialog(null, "Do you enable tracking?");
				if (dialogResult == JOptionPane.YES_OPTION) 
					// enable tracking, this option will persist 
					AdvancedAnalytics.Enable(true);
			}
			// save the state
			prefs.put(KEY, "true");
		}

		// run the application
		RunApp();
		
		// when the app finishes, stop the service.
		AdvancedAnalytics.Stop();
	}
	
	public static void RunApp() {
		JOptionPane.showMessageDialog(null, "The app is running!");
	}

}
