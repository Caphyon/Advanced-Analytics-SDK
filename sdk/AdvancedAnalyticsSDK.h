/*
 * Copyright 2002 - 2016 Caphyon LTD. All rights reserved.
 *
 * mailto: eng@caphyon.com
 * http://www.caphyon.com
 *
 */
#ifndef _ADVANCED_ANALYTICS_H_
#define _ADVANCED_ANALYTICS_H_

/**
 * Public api for advanced analytics.
 */
//----------------------------------------------------------------------------

/**
 * Start advanced analtyics tracking.
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
void __stdcall AA_Start(const wchar_t * aTrackingCode, const wchar_t * aApplicationVersion);

/**
 * Stop advanced analytics tracking.
 * After this method is called, any further calls to the SDK will not work.
 */
void __stdcall AA_Stop();

/**
 * Enable/disable data sending to server.
 * The SDK will persist the selected option and on the next application start,
 * it will pick up the previously selected option.
 */
void __stdcall AA_Enable(bool);

/**
 * Check if data sending is enabled.
 */
bool __stdcall AA_IsEnabled();

#endif  //_ADVANCED_ANALYTICS_H_
