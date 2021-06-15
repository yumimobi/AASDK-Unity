using System;
using AntiAddictionSDK.Common;
using UnityEngine;

namespace AntiAddictionSDK.Api
{
    public class AntiAddictionStytemSDK
    {
        static readonly object objLock = new object();

        IAntiAddictionClient client;

        // Creates AntiAddictionSDK instance.
        public AntiAddictionStytemSDK()
        {
            client = AntiAddictionClientFactory.BuildAntiAddictionClient();

            client.OnTouristsModeLoginSuccess += (sender, args) =>
            {
                if (OnTouristsModeLoginSuccess != null)
                {
                    OnTouristsModeLoginSuccess(this, args);
                }
            };

            client.OnTouristsModeLoginFailed += (sender, args) =>
            {
                if (OnTouristsModeLoginFailed != null)
                {
                    OnTouristsModeLoginFailed(this, args);
                }
            };

            client.RealNameAuthenticateSuccess += (sender, args) =>
            {
                if(RealNameAuthenticateSuccess != null)
                {
                    RealNameAuthenticateSuccess(this, args);
                }
            };

            client.RealNameAuthenticateFailed += (sender, args) =>
            {
                if (RealNameAuthenticateFailed != null)
                {
                    RealNameAuthenticateFailed(this, args);
                }
            };

            client.RealNameAuthenticateFailedWithForceExit += (sender, args) =>
            {
                if (RealNameAuthenticateFailedWithForceExit != null) {
                    RealNameAuthenticateFailedWithForceExit(this, args);
                }
            };

            client.NoTimeLeftWithTouristsMode += (sender, args) =>
            {
                if (NoTimeLeftWithTouristsMode != null)
                {
                    NoTimeLeftWithTouristsMode(this, args);
                }
            };

            client.NoTimeLeftWithNonageMode += (sender, args) =>
            {
                if (NoTimeLeftWithNonageMode != null)
                {
                    NoTimeLeftWithNonageMode(this, args);
                }
            };

            client.LeftTimeOfCurrentUserInEverySeconds += (SpriteRenderer, args) =>
            {
                if (LeftTimeOfCurrentUserInEverySeconds != null)
                {
                    LeftTimeOfCurrentUserInEverySeconds(this, args);
                }
            };
        }


        // 登录回调
        // 登录成功
        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        // 登录失败
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed;
        // 实名认证回调
        // 实名认证成功
        public event EventHandler<EventArgs> RealNameAuthenticateSuccess;
        // 实名认证失败(用户点击暂不认证)
        public event EventHandler<EventArgs> RealNameAuthenticateFailed;
        // 实名认证失败（用户点击退出游戏）
        public event EventHandler<EventArgs> RealNameAuthenticateFailedWithForceExit;
        // 游客时长已用完
        public event EventHandler<EventArgs> NoTimeLeftWithTouristsMode;
        // 未成年人时长已用完
        public event EventHandler<EventArgs> NoTimeLeftWithNonageMode;
        // 当前用户剩余时间（每秒回调一次）
        public event EventHandler<LeftTimeEventArgs> LeftTimeOfCurrentUserInEverySeconds;

     
        // 获取当前用户游客登录状态
        // 0: 已登录
        // 1: 未登录
        public int IsLogined()
        {
            return client.IsLogined();
        }

        // 获取用户实名认证状态
        // 0: 未实名认证
        // 1：已实名认证
        public int IsAuthenticated()
        {
            return client.IsAuthenticated();
        }

        // 获取当前用户年龄段
        // 0 未认证
        // 1 成年人
        // 2 未成年人
        public int AgeGroupOfCurrentUser()
        {
            return client.AgeGroupOfCurrentUser();
        }

        // 展示实名认证弹窗，如果游戏主界面增加实名认证按钮，用户点击后，可调用此接口显示实名认证界面
        public void ShowRealNameView()
        {
            client.ShowRealNameView();
        }

        // 展示实名认证界面（退出游戏）
        public void ShowRealNameViewWithForceExit()
        {
            client.ShowRealNameViewWithForceExit();
        }

        // 展示在线时长提示界面（成年人无需展示）
        public void ShowAlertInfoController()
        {
            client.ShowAlertInfoController();
        }

        // 展示查看详情界面
        public void ShowCheckDetailInfoController()
        {
            client.ShowCheckDetailInfoController();
        }

        // 展示消费限制窗口（成年人无限制）
        public void ShowCashLimitedController()
        {
            client.ShowCashLimitedController();
        }

        // 获取当前用户剩余可玩时长
        // 如果为-1，表示当前用户为成年人账号，将不受防沉迷限制
        // 如果为大于0的数，返回的为当前用户的剩余可玩时长，单位秒
        public int LeftTimeOfCurrentUser()
        {
            return client.LeftTimeOfCurrentUser();
        }

        //设置Android渠道userId（Android 华为，联想渠道需要调用）
        //为兼容华为,联想渠道的防沉迷SDK，当游戏调用华为，联想渠道SDK登录成功，并且获取到了账号Id之后，请调用下面的接口
        public void SetChannelUserId(string userId) {
            client.SetChannelUserId(userId);
        }

        //游戏退到后台时调用
        public void GameOnPause()
        {
            client.GameOnPause();
        }

        //游戏恢复到前台时调用
        public void GameOnResume()
        {
            client.GameOnResume();
        }

        public void StopTimerInUnity() 
        {
            client.StopTimerInUnity();
        }

        public void ResumeTimerInUnity()
        {
            client.ResumeTimerInUnity();
        }
    }
}