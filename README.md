[See the English Guide](https://github.com/yumimobi/AASDK-Unity/blob/master/Anti-Addiction-Unity.md)

# 入门指南

本指南适用于希望通过 Unity 接入中宣部防沉迷功能的发布商。  


## 前提条件  

- 在 iOS 上部署  
  - Xcode 10 或更高版本  
  - iOS 9.0 或更高版本  
  - [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)  

- 在 Android 上部署
  - 定位到 Android API 级别 14 或更高级别  

- [Demo](https://github.com/yumimobi/AASDK-Unity)

## 下载防沉迷 Unity 插件  

借助防沉迷 Unity 插件，Unity 开发者无需编写 Java 或 Objective-C 代码，即可轻松地在 Android 和 iOS 应用上实现防沉迷的功能。  
该插件提供了一个 C# 界面，用于 Unity 项目中 C# 脚本使用防沉迷功能。

请通过如下链接下载该插件的 Unity 软件包，或在 GitHub 上查看其代码。

[下载插件](https://github.com/yumimobi/AASDK-Unity/releases/download/1.0.1/AASDK.unitypackage)  
[查看源代码](https://github.com/yumimobi/AASDK-Unity) 

### 导入防沉迷 Unity 插件

在 Unity 编辑器中打开您的项目，然后依次选择 Assets > Import Package > Custom Package，并找到您下载的 AntiAddictionSystem.unitypackage 文件。  

<img src='resources/add_custom_package.png'>

确保选择所有文件，然后点击 Import。

<img src='resources/import_custom_package.png'>  

### 加入防沉迷 SDK  

防沉迷 Unity 插件随 Unity Play [服务解析器库](https://github.com/googlesamples/unity-jar-resolver)一起发布。 此库旨在供需要访问 Android 特定库（例如 AAR）或 iOS CocoaPods 的所有 Unity 插件使用。它为 Unity 插件提供了声明依赖项的功能，然后依赖项会被自动解析并复制到 Unity 项目中。

请按照下列步骤操作，确保您的项目包含防沉迷 SDK。 

##### 1. 部署到 iOS   

您无需执行其他步骤即可将防沉迷 SDK 加入 Unity 项目中。  
*注意：iOS 依赖项的标识是通过 CocoaPods 完成的，而 CocoaPods 是构建过程完成后的一个运行步骤。*  

##### 2. 部署到 Android  

在 Unity 编辑器中，依次选择 Assets > External Dependency Manager > Android Resolver > Resolve。  
Unity Play 服务解析器库会将声明的依赖项复制到 Unity 应用的 Assets/Plugins/Android 目录中。  

*注意：防沉迷 Unity 插件依赖项位于 Assets/AntiAddictionSDK/Editor/AntiAddictionSDKDependencies.xml 中*  

<img src='resources/force_resolve.png'>  

### 配置防沉迷SDK参数

#### 配置 iOS 参数
请在info.plist中添加如下参数。
参数获取请联系产品。
```xml
    <key>zgameid</key>
    <string>your game id</string>
    <key>zchannelid</key>
    <string>your channel id</string>
    <key>zplayKey</key>
    <string>your zplayKey</string>
```

#### 配置 Android 参数

请修改Assets/Plugins/Android/assets/ZplayConfig.xml文件中的参数
<img src='resources/android-setting.png'>  

`提示：ZplayConfig.xml文件中的GameID，ChannelID 参数，请联系掌游产品获取`


# 快速接入

### 1. 创建 AntiAddictionSDK

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
    antiAddictionSDK.RealNameAuthenticateFailedWithForceExit += HandleRealNameAuthenticateFailedWithForceExit;
    antiAddictionSDK.NoTimeLeftWithTouristsMode += HandleNoTimeLeftWithTouristsMode;
    antiAddictionSDK.NoTimeLeftWithNonageMode += HandleNoTimeLeftWithNonageMode;
    antiAddictionSDK.LeftTimeOfCurrentUserInEverySeconds += HandleLeftTimeOfCurrentUserInEverySeconds;
  }
#region AntiAddictionStytemSDK callback handlers
    // 游客登录回调
    // 游客登录成功回调
    public void HandleTouristsModeLoginSuccess(object sender, LoginSuccessEventArgs args)
    {
        //获取游客id
        String touristsID = args.Message;
        print("AntiAddiction---HandleTouristsModeLoginSuccess: " + touristsID);
    }
    // 游客登录失败回调
    public void HandleTouristsModeLoginFailed(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleTouristsModeLoginFailed");
    }

    // 实名认证回调
    // 实名认证成功回调
    public void HandleRealNameAuthenticateSuccess(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleRealNameAuthenticateSuccess");
    }
    // 用户在实名认证界面点击暂不认证
    public void HandleRealNameAuthenticateFailed(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleRealNameAuthenticateFailed");
    }

    // 用户在实名认证界面点击退出游戏
    public void HandleRealNameAuthenticateFailedWithForceExit(object sender, EventArgs args)
    {
        statusText.text = "HandleRealNameAuthenticateFailedWithForceExit";
        print("AntiAddiction---HandleRealNameAuthenticateFailedWithForceExit");
    }

     // 游客账号可玩时长已结束回调
    public void HandleNoTimeLeftWithTouristsMode(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleNoTimeLeftWithTouristsMode");
    }
    // 未成年人可玩时长已结束回调
    public void HandleNoTimeLeftWithNonageMode(object sender, EventArgs args)
    {
        print("AntiAddiction---HandleNoTimeLeftWithNonageMode");
    }
    // 每秒回调当前用户剩余时间一次
    public void HandleLeftTimeOfCurrentUserInEverySeconds(object sender, LeftTimeEventArgs args)
    {
        int leftTime = args.LeftTime;
        print("AntiAddiction---HandleTouristsModeLoginSuccess: " + leftTime);
        statusText.text = "HandleTouristsModeLoginSuccess: " + leftTime;
    }

#endregion
}
```  

### 2. 展示实名认证界面接口（暂不认证）
如果App在主界面有提供给用户实名认证的按钮，当用户点击实名认证按钮后，可调用下面的方法展示实名认证界面

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowRealNameView();
}
```  

### 3. 展示实名认证界面接口（退出游戏）
使用场景:  
如果用户点击退出游戏，开发者需要在- (void)clickForceExitButtonOnRealNameAuthController;此回调中展示实名认证获取奖励界面（此界面由开发者自己实现），此界面提供两个交互按钮。  
退出游戏按钮：点击此按钮退出游戏。  
实名认证按钮：点击此按钮再次展示SDK提供的实名认证界面。  
warning： 此时计时器暂停，开发者需要在认证成功回调中重启计时器- (void)resumeTimer;  
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowRealNameViewWithForceExit();
}
```  

### 4. 展示用户剩余时长提示界面  
每次进入游戏时调用。（成年人除外）  
请在登录成功后调用。  
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowAlertInfoController();
}
```

### 5. 展示查看详情界面
显示防沉迷规则
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowCheckDetailInfoController();
}
```

### 6. 展示消费限制窗口
成年人无需展示
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ShowCashLimitedController();
}
```

## 7 设置渠道UserId接口(Android 华为，联想渠道必选)
为兼容华为，联想渠道登录支付SDK进行实名认证兼容，游戏需要在华为联想登录之后，调用下面的接口设置渠道UserId。

注意: 华为，联想渠道防沉迷只有在调用下面的接口之后才会启动防沉迷的功能，否则将不会启动防沉迷SDK的任何功能
```java
//设置渠道UserId接口
//userId:华为，联想渠道登录SDK返回的用户Id
if (antiAddictionSDK != null)
{
    antiAddictionSDK.SetChannelUserId(userId);
}
```

### 8. 其他接口
`注：在Android中，8.1及8.2为必须调用接口，8.3及8.4为非必须调用接口；在iOS中，以下接口均为非必须调用接口`

##### 8.1. 游戏退到后台接口（iOS无需调用）
此接口仅适用于Android，iOS平台无需调用。

当Android用户按home键，将游戏退出到后台时，请务必调用下面的接口

<span style="color:rgb(255,0,0);">
<b>重要提示：</b> 游戏退到后台接口必须调用，不调用会导致防沉迷SDK计算游戏时长错误
</span>

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.GameOnPause();
}
```

##### 8.2. 游戏恢复前台接口（iOS无需调用）
此接口仅适用于Android，iOS平台无需调用。

当Android用户将游戏恢复到前台时，请务必调用下面的接口

<span style="color:rgb(255,0,0);">
<b>重要提示：</b> 游戏恢复前台接口必须调用，不调用会导致防沉迷SDK计算游戏时长错误
</span>

```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.GameOnResume();
}
```

##### 8.3. 获取用户实名认证状态
若需要获取当前用户的实名认证状态，可调用下面的接口进行获取

 0: 未实名认证
 
 1：已实名认证

```csharp
if (antiAddictionSDK != null)
{
    int authenticatedStatus = antiAddictionSDK.IsAuthenticated();
}
```

##### 8.4. 获取当前用户剩余可玩时长
若需要获取当前用户的剩余可玩时长，可调用下面的接口进行获取

如果为-1，表示当前用户为成年人账号，将不受防沉迷限制

如果为大于0的数，返回的为当前用户的剩余可玩时长，单位秒

```csharp
if (antiAddictionSDK != null)
{
    int leftTimeOfCurrentUser = antiAddictionSDK.LeftTimeOfCurrentUser();
}
```

##### 8.5 获取当前用户年龄区间
0: 未认证  
1: 成年  
2: 未成年  
```csharp
if (antiAddictionSDK != null)
{
    statusText.text = antiAddictionSDK.AgeGroupOfCurrentUser()+"";
}
```

##### 8.6 停止计时器
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.StopTimerInUnity();
}
```
##### 8.7 恢复计时器
```csharp
if (antiAddictionSDK != null)
{
    antiAddictionSDK.ResumeTimerInUnity();
}
```
