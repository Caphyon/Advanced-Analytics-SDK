#ifndef _SIMPLE_REGISTRY_KEY_HANDLER_ANALYTICS_H_
#define _SIMPLE_REGISTRY_KEY_HANDLER_ANALYTICS_H_

/**
* Class that manages some simple registry operations.
*/
class SimpleRegistryKeyHandler
{
  static unsigned const SIZE    = 2;
  wstring               regPath = L"SOFTWARE\\Caphyon\\LaunchAnalytics";
  wstring               keyName = L"C++ Sample";
  wstring               mAppId;
  wstring               mAppVersion;
  HKEY                  key;

  LONG GetDWORDRegKey(DWORD & nValue, DWORD nDefaultValue);

public:
  SimpleRegistryKeyHandler(wstring appId, wstring appVersion);
  ~SimpleRegistryKeyHandler();
  void setHasBeenChecked(bool aFlag);
  bool hasBeenChecked();
};

#endif _SIMPLE_REGISTRY_KEY_HANDLER_ANALYTICS_H_
