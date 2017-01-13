# AdvancedAnalytics SDK

Is an SDK which sends data about your application to our servers. You can then see Launches count and Active users count information in the Launch report from your Analytics account.

## How to

### C++
* Include header in project.
* Link or load Advanced Analytics library.
* Get your app id from AdvancedInstaller or from [InstallerAnalytics](https://installeranalytics.com)
* Replace the default app id and version with your own.
* Use AA_Enable (true / false) to enable tracking of your app.
* Use AA_Start and AA_Stop, to signal the start and stop of your application.

### C#
* Load the dll (see c# sample for example)
* Get your app id from AdvancedInstaller or from [InstallerAnalytics](https://installeranalytics.com)
* Replace the default app id and version with your own.
* Use AA_Enable (true / false) to enable tracking of your app.
* Use AA_Start and AA_Stop, to signal the start and stop of your application.

### Java
* Load the dll (see java sample for example)
* Get your app id from AdvancedInstaller or from [InstallerAnalytics](https://installeranalytics.com)
* Replace the default app id and version with your own.
* Use AA_Enable (true / false) to enable tracking of your app.
* Use AA_Start and AA_Stop, to signal the start and stop of your application.

## Requirements

* Installer Analytics Account with a valid application. When the application is added the app id is generated.
* Visual C++ Runtime