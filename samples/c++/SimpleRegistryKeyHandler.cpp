#include "stdafx.h"
#include "SimpleRegistryKeyHandler.h"

LONG SimpleRegistryKeyHandler::GetDWORDRegKey(DWORD & nValue, DWORD nDefaultValue)
{
  nValue = nDefaultValue;
  DWORD dwBufferSize(sizeof(DWORD));
  DWORD nResult(0);
  LONG  nError = ::RegQueryValueExW(key, keyName.c_str(), 0, NULL,
                                   reinterpret_cast<LPBYTE>(&nResult), &dwBufferSize);
  if (ERROR_SUCCESS == nError)
  {
    nValue = nResult;
  }
  return nError;
}

SimpleRegistryKeyHandler::SimpleRegistryKeyHandler(wstring appId, wstring appVersion)
  : mAppId(appId)
  , mAppVersion(appVersion)
{
  regPath     = regPath + L"\\" + appId + L"\\" + appVersion + L"\\";
  LONG nError = ::RegOpenKeyEx(HKEY_CURRENT_USER, regPath.c_str(), 0, KEY_READ, &key);
}

SimpleRegistryKeyHandler::~SimpleRegistryKeyHandler()
{
  LONG nError = ::RegCloseKey(key);
}

void SimpleRegistryKeyHandler::setHasBeenChecked(bool aFlag)
{
  DWORD disposition;
  DWORD type  = REG_BINARY;
  BYTE  input = aFlag;

  // DWORD
  LONG nError = ::RegCreateKeyEx(HKEY_CURRENT_USER, regPath.c_str(), 0, NULL,
                                 REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &key, &disposition);
  nError = ::RegSetValueEx(key, keyName.c_str(), NULL, type, &input, disposition);
}

bool SimpleRegistryKeyHandler::hasBeenChecked()
{
  DWORD res;
  GetDWORDRegKey(res, false);
  if (res != 0)
    return true;
  return false;
}
