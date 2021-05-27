[toc]
# 1. Get Started

This guide is intended for publishers who want to integrate the Anti-Addiction system.

# 2. Prerequisites

- To deploy to iOS  
  - Xcode 10 or higher  
  - iOS 9.0 or higher  
  - [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)  

- To deploy to Android  
  - Target Android API level 14 or higher  

- [Access to Demo](https://github.com/yumimobi/AASDK-Unity)

## 2.1 Download the AntiAddictionSystem Unity plugin  

The AntiAddictionSystem Unity plugin enables Unity developers to easily serve AntiAddictionSystemSDK on Android and iOS apps without having to write Java or Objective-C code. The plugin provides a C# interface for requesting ads that is used by C# scripts in your Unity project.

Use the links below to download the Unity package for the plugin or to take a look at its code on GitHub.  

[Download the Plugin](https://github.com/yumimobi/AASDK-Unity/releases/download/0.1.13/AASDK.unitypackage)    
[View Source Code](https://github.com/yumimobi/AASDK-Unity)  

## 2.2 Import the AntiAddictionSystem Unity plugin  
Open your project in the Unity editor.   
Select Assets > Import Package > Custom Package and find the AntiAddictionSystem.unitypackage file you downloaded.  

<img src='resources/add_custom_package.png'>

Make sure all of the files are selected and click Import.

<img src='resources/import_custom_package.png'>

## 2.3 Include the AntiAddictionSystem SDK

The AntiAddictionSystem Unity plugin is distributed with the Unity [Play Services Resolver library](https://github.com/googlesamples/unity-jar-resolver). This library is intended for use by any Unity plugin that requires access to Android specific libraries (e.g., AARs) or iOS CocoaPods. It provides Unity plugins the ability to declare dependencies, which are then automatically resolved and copied into your Unity project.

Follow the steps listed below to ensure your project includes the Atmosplay Ads SDK.

### 2.3.1 Deploy to iOS

No additional steps are required to include the AntiAddictionSystem SDK into the Unity project.

After building, open xcworkspace project.

*Note: iOS dependencies are identified using CocoaPods. CocoaPods is run as a post build process step.*  

### 2.3.2 Deploy to Android 

In the Unity editor, select `Assets > Play Services Resolver > Android Resolver > Force Resolve. The Unity Play Services Resolver library will copy the declared dependencies into the `Assets/Plugins/Android` directory of your Unity app.  
<img src='resources/force_resolve.png'>

## 2.4 Config Parametrs
### 2.4.1 Config the iOS parameters
Add the following parameters into the `info.plist`.
You can find the `info.plist` in your Xcode project.

`warning: You can contact PM for all of these IDs .`

```xml
    <key>zgameid</key>
    <string>your game id</string>
    <key>zchannelid</key>
    <string>your channel id</string>
```
### 2.4.2 Config the Android prameters
Modify the prameters in `Assets/Plugins/Android/assets/ZplayConfig.xml` file.
<img src='resources/android-setting.png'>  

`warning: For GameID, ChannelID, Zplay_SDK_KEY parameters in ZplayConfig.xml file, please contact PM for all of these IDs.`

# 3. Integration
## 3.1 Create AntiAddictionSDK object

```csharp
using System;
using UnityEngine;
using AntiAddictionSDK.Api;
using UnityEngine.UI;

public class AASDKDemoScript : MonoBehaviour {
AntiAddictionStytemSDK antiAddictionSDK;

  void Start() 
  {
    antiAddictionSDK = new AntiAddictionStytemSDK();
    antiAddictionSDK.OnTouristsModeLoginSuccess += HandleTouristsModeLoginSuccess;
    antiAddictionSDK.OnTouristsModeLoginFailed += HandleTouristsModeLoginFailed;
    antiAddictionSDK.RealNameAuthenticateSuccess += HandleRealNameAuthenticateSuccess;
    antiAddictionSDK.RealNameAuthenticateFailed += HandleRealNameAuthenticateFailed;
    antiAddictionSDK.NoTimeLeftWithTouristsMode += HandleNoTimeLeftWithTouristsMode;
    antiAddictionSDK.NoTimeLeftWithNonageMode += HandleNoTimeLeftWithNonageMode;
  }
#region AntiAddictionStytemSDK callback handlers
    // Tourist login success event
    // Normaly you should ignore this event
    public void HandleTouristsModeLoginSuccess(object sender, LoginSuccessEventArgs args)
    {
        // get the tourist id
        String touristsID = args.Message;
        print("AntiAddiction---HandleTouristsModeLoginSuccess: " + touristsID);
    }
    // Tourist login failed event
    // Normaly you should ignore this event
    public void HandleTouristsModeLoginFailed(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleTouristsModeLoginFailed");
    }

    // Real name authentication successful
    public void HandleRealNameAuthenticateSuccess(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleRealNameAuthenticateSuccess");
    }
    // Real name authentication failed
    public void HandleRealNameAuthenticateFailed(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleRealNameAuthenticateFailed");
    }

    // This method will be called when the tourist mode runs out of time 
    // Will present the real name authentication controller after 3 second
    public void HandleNoTimeLeftWithTouristsMode(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleNoTimeLeftWithTouristsMode");
    }
    // This method will be called when the nonage mode runs out of time 
    // Will present the alert controller after 3 second
    public void HandleNoTimeLeftWithNonageMode(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleNoTimeLeftWithNonageMode");
    }

#endregion
}
```  

## 3.2 Present authentication controller
The game should provide a real name authentication button. 
Call this method when the user clicks.

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowRealNameView();
}
```  

## 3.3 Other API

<span style="color:rgb(150,0,0);">
<b>Warning:</b> 3.1 and 3.2 is required by Android.
</span>

### 3.3.1 Application will enter background
<span style="color:rgb(255,0,0);">
<b>warning:</b> IS required by android
</span>

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.GameOnPause();
}
```
### 3.3.2 Application will enter foreground
<span style="color:rgb(255,0,0);">
<b>warning:</b> IS required by android
</span>

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.GameOnResume();
}
```
### 3.3.3 Get the status of user authencation
0: No verified
1: Has been verified

```csharp
if (antiAddictionSDK != null)
{
    int authenticatedStatus = antiAddictionSDK.IsAuthenticated();
}
```
### 3.3.4 Get the left time of current user
-1 : no time limited
>0 : left time of current user

```csharp
if (antiAddictionSDK != null)
{
    int leftTimeOfCurrentUser = antiAddictionSDK.LeftTimeOfCurrentUser();
}
```