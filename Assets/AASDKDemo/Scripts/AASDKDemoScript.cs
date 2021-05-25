using System;
using UnityEngine;
using AntiAddictionSDK.Api;
using AntiAddictionSDK.Common;
using AntiAddictionSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AASDKDemoScript: MonoBehaviour
{
    AntiAddictionStytemSDK antiAddictionSDK;
    public Text statusText;
    // Start is called before the first frame update
    void Start()
    {
        antiAddictionSDK = new AntiAddictionStytemSDK();
        antiAddictionSDK.OnTouristsModeLoginSuccess += HandleTouristsModeLoginSuccess;
        antiAddictionSDK.OnTouristsModeLoginFailed += HandleTouristsModeLoginFailed;
        antiAddictionSDK.RealNameAuthenticateResult += HandleRealNameAuthenticateResult;
        antiAddictionSDK.NoTimeLeftWithTouristsMode += HandleNoTimeLeftWithTouristsMode;
        antiAddictionSDK.NoTimeLeftWithNonageMode += HandleNoTimeLeftWithNonageMode;
        
    }

    // 获取当前用户游客登录状态
    // 0: 已登录
    // 1: 未登录
    public void IsLogined()
    {
        statusText.text = "IsLogined";
        if (antiAddictionSDK != null)
        {
            statusText.text = antiAddictionSDK.IsLogined()+"";
        }
    }

    // 获取用户实名认证状态
    // 0: 未实名认证
    // 1：已实名认证
    public void IsAuthenticated()
    {
        statusText.text = "IsAuthenticated";
        if (antiAddictionSDK != null)
        {
            statusText.text = antiAddictionSDK.IsAuthenticated()+"";
        }
    }

    // 展示实名认证弹窗，如果游戏主界面增加实名认证按钮，用户点击后，可调用此接口显示实名认证界面
    public void ShowRealNameView()
    {
        statusText.text = "ShowRealNameView";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ShowRealNameView();
        }
    }

    // 获取当前用户剩余可玩时长
    //如果为-1，表示当前用户为成年人账号，将不受防沉迷限制
    //如果为大于0的数，返回的为当前用户的剩余可玩时长，单位秒
    public void LeftTimeOfCurrentUser()
    {
        statusText.text = "LeftTimeOfCurrentUser";
        if (antiAddictionSDK != null)
        {
            statusText.text = antiAddictionSDK.LeftTimeOfCurrentUser()+"";
        }
    }

     private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //游戏退到后台时调用
            statusText.text = "GameOnPause";
            if (antiAddictionSDK != null)
            {
                antiAddictionSDK.GameOnPause();
            }
        }
        else {
            //游戏恢复前台时调用
            statusText.text = "GameOnResume";
            if (antiAddictionSDK != null)
            {
                antiAddictionSDK.GameOnResume();
            }
        }
    }



    #region AntiAddiction callback handlers

    public void HandleTouristsModeLoginSuccess(object sender, LoginSuccessEventArgs args)
    {
        String touristsID = args.Message;
        print("AntiAddiction---HandleTouristsModeLoginSuccess: " + touristsID);
        statusText.text = "HandleTouristsModeLoginSuccess: " + touristsID;
    }

    public void HandleTouristsModeLoginFailed(object sender, EventArgs args)
    {
        statusText.text = "HandleTouristsModeLoginFailed";
        print("AntiAddiction---HandleTouristsModeLoginFailed");
    }


    public void HandleRealNameAuthenticateResult(object sender, RealNameAuthenticateEventArgs args)
    {
        String isRealNameStatus = args.Message;
        statusText.text = "HandleRealNameAuthenticateResult：" + isRealNameStatus;
        print("AntiAddiction---HandleRealNameAuthenticateResult");
    }


    public void HandleNoTimeLeftWithTouristsMode(object sender, EventArgs args)
    {
        statusText.text = "HandleNoTimeLeftWithTouristsMode";
        print("AntiAddiction---HandleNoTimeLeftWithTouristsMode");
    }

    public void HandleNoTimeLeftWithNonageMode(object sender, EventArgs args)
    {
        statusText.text = "HandleNoTimeLeftWithNonageMode";
        print("AntiAddiction---HandleNoTimeLeftWithNonageMode");
    }

    #endregion

}
