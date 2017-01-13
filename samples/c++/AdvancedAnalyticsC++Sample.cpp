// AdvancedAnalyticsC++Sample.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../../sdk/AdvancedAnalyticsSDK.h"
#include "SimpleRegistryKeyHandler.h"

class Program
{
public:
  static void Run() { ::MessageBox(NULL, L"The application is running.", L"Sample", MB_OK); }
};

int main()
{
  // app id and app version
  wstring appId      = L"5846b40946c9112c443e66ff";
  wstring appVersion = L"1.0.0";

  // initialize the service.
  AA_Start(appId.c_str(), appVersion.c_str());

  /**
   * Check to see if tracking is enabled
   * A good user experience is to ask just once and persist the setting.
   */
  SimpleRegistryKeyHandler regHandler(appId, appVersion);

  if (regHandler.hasBeenChecked() == false)
  {
    if (::MessageBox(NULL, L"Do you want to enable tracking?", L"Sample", MB_YESNO) == IDYES)
      AA_Enable(true);
    else
      AA_Enable(false);
    regHandler.setHasBeenChecked(true);
  }

  // Run the application
  Program::Run();

  // When the app needs to close, stop the tracking service.
  AA_Stop();
  return 0;
}
