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

            client.RealNameAuthenticateResult += (sender, args) =>
            {
                if(RealNameAuthenticateResult != null)
                {
                    RealNameAuthenticateResult(this, args);
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

        }


        // 登录回调
        // 登录成功
        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        // 登录失败
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed;
        // 实名认证回调
        public event EventHandler<RealNameAuthenticateEventArgs> RealNameAuthenticateResult;
        // 游客时长已用完
        public event EventHandler<EventArgs> NoTimeLeftWithTouristsMode;
        //未成年人时长已用完
        public event EventHandler<EventArgs> NoTimeLeftWithNonageMode;

     

     
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

        // 展示实名认证弹窗，如果游戏主界面增加实名认证按钮，用户点击后，可调用此接口显示实名认证界面
        public void ShowRealNameView()
        {
            client.ShowRealNameView();
        }

        // 获取当前用户剩余可玩时长
        // 如果为-1，表示当前用户为成年人账号，将不受防沉迷限制
        // 如果为大于0的数，返回的为当前用户的剩余可玩时长，单位秒
        public int LeftTimeOfCurrentUser()
        {
            return client.LeftTimeOfCurrentUser();
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


    }
}