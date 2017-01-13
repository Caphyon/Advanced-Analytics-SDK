package com.caphyon;

public class AdvancedAnalytics {
	
	private static class DllLoader {
		public static void load() {
			String path = System.getProperty("user.dir");
			System.load(path + "/../../sdk/x64/AdvancedAnalytics_x64.dll");
			System.load(path + "/../../sdk/x64/AdvancedAnalyticsJavaBridge.dll");			
		}
	}
	
	/**
	 * Native function declarations.
	 */
	static native void AA_Start(String aTrackingCode, String aAppVersion);
	static native void AA_Stop();
	static native void AA_Enable(boolean aEnableFlag);
	static native boolean AA_IsEnabled();
    
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
	public static void Start(String aTrackingCode, String aApplicationVersion) {
		DllLoader.load();
		AA_Start(aTrackingCode, aApplicationVersion);
	}
	
    /**
     * Stop advanced analytics tracking.
     * After this method is called, any further calls to the SDK will not work.
     */
	public static void Stop() {
		AA_Stop();
	}
	
    /**
     * Checks if data sending is enabled.
     */
	public static boolean IsEnabled() {		
		return AA_IsEnabled();
	}
	
    /**
     * Enable/disable data sending to server.
     * The SDK will persist the selected option and on the next application start,
     * it will pick up the previously selected option.
     * 
     * @param aEnable - true for enabling, false for disabling.
     */
	public static void Enable(Boolean aEnable) {	
		AA_Enable(aEnable);
	}
}
