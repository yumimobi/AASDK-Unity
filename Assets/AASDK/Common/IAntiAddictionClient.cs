using System;
using AntiAddictionSDK.Api;
using UnityEngine;

namespace AntiAddictionSDK.Common
{
    public interface IAntiAddictionClient 
    {

        //登录回调
        // 登录成功
        event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        // 登录失败
        event EventHandler<EventArgs> OnTouristsModeLoginFailed;
        // 实名认证回调
        event EventHandler<RealNameAuthenticateEventArgs> RealNameAuthenticateResult;
        // 游客时长已用完
        event EventHandler<EventArgs> NoTimeLeftWithTouristsMode;
        // 未成年人时长已用完
        event EventHandler<EventArgs> NoTimeLeftWithNonageMode;


        // 获取当前用户游客登录状态
        // 0: 已登录
        // 1: 未登录
        int IsLogined();
        // 获取用户实名认证状态
        // 0: 未实名认证
        // 1：已实名认证
        int IsAuthenticated();

        // 展示实名认证弹窗，如果游戏主界面增加实名认证按钮，用户点击后，可调用此接口显示实名认证界面
        void ShowRealNameView();

        // 获取当前用户剩余可玩时长，如果为-1，表示当前用户为成年人账号，将不受防沉迷限制,如果为大于0的数，返回的为当前用户的剩余可玩时长，单位秒
        int LeftTimeOfCurrentUser();

        //游戏退到后台时调用
        void GameOnPause();
        //游戏恢复到前台时调用
        void GameOnResume();
    }
}