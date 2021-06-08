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
        antiAddictionSDK.RealNameAuthenticateSuccess += HandleRealNameAuthenticateSuccess;
        antiAddictionSDK.RealNameAuthenticateFailed += HandleRealNameAuthenticateFailed;
        antiAddictionSDK.RealNameAuthenticateFailedWithForceExit += HandleRealNameAuthenticateFailedWithForceExit;
        antiAddictionSDK.NoTimeLeftWithTouristsMode += HandleNoTimeLeftWithTouristsMode;
        antiAddictionSDK.NoTimeLeftWithNonageMode += HandleNoTimeLeftWithNonageMode;
        antiAddictionSDK.LeftTimeOfCurrentUserInEverySeconds += HandleLeftTimeOfCurrentUserInEverySeconds;
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

    // 获取当前用户年龄段
    // 0 未认证
    // 1 成年人
    // 2 未成年人
    public void GetUserAgeGroup()
    {
        statusText.text = "GetUserAgeGroup";
        if (antiAddictionSDK != null)
        {
            statusText.text = antiAddictionSDK.AgeGroupOfCurrentUser()+"";
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

    // 展示实名认证界面（退出游戏）
    public void ShowRealNameViewWithForceExit()
    {
        statusText.text = "ShowRealNameViewWithForceExit";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ShowRealNameViewWithForceExit();
        }
    }

    // 展示已在线时长提示界面
    public void ShowAlertInfoController()
    {
        statusText.text = "ShowAlertInfoController";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ShowAlertInfoController();
        }
    }

    // 展示查看详情界面
    public void ShowCheckDetailInfoController()
    {
        statusText.text = "ShowCheckDetailInfoController";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ShowCheckDetailInfoController();
        }
    }

    // 展示消费限制窗口
    public void ShowCashLimitedController()
    {
        statusText.text = "ShowCashLimitedController";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ShowCashLimitedController();
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

    // 暂停计时器
    public void StopTimerInUnity()
    {
        statusText.text = "StopTimerInUnity";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.StopTimerInUnity();
        }
    }

    // 恢复计时器
    public void ResumeTimerInUnity()
    {
        statusText.text = "ResumeTimerInUnity";
        if (antiAddictionSDK != null)
        {
            antiAddictionSDK.ResumeTimerInUnity();
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


    public void HandleRealNameAuthenticateSuccess(object sender, EventArgs args)
    {
        statusText.text = "HandleRealNameAuthenticateSuccess：";
        print("AntiAddiction---HandleRealNameAuthenticateSuccess");
    }

    public void HandleRealNameAuthenticateFailed(object sender, EventArgs args)
    {
        statusText.text = "HandleRealNameAuthenticateFailed：";
        print("AntiAddiction---HandleRealNameAuthenticateFailed");
    }

    public void HandleRealNameAuthenticateFailedWithForceExit(object sender, EventArgs args)
    {
        statusText.text = "HandleRealNameAuthenticateFailedWithForceExit";
        print("AntiAddiction---HandleRealNameAuthenticateFailedWithForceExit");
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

    public void HandleLeftTimeOfCurrentUserInEverySeconds(object sender, LeftTimeEventArgs args)
    {
        int leftTime = args.LeftTime;
        print("AntiAddiction---HandleTouristsModeLoginSuccess: " + leftTime);
        statusText.text = "HandleTouristsModeLoginSuccess: " + leftTime;
    }
    #endregion

}
