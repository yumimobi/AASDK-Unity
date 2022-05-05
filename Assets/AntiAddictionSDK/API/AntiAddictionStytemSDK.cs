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

            client.RealNameAuthSuccessStatus += (SpriteRenderer, args) =>
            {
                if (RealNameAuthSuccessStatus != null)
                {
                    RealNameAuthSuccessStatus(this, args);
                }
            };

            client.OnCurrentChannelUserInfo += (SpriteRenderer, args) =>
            {
                if (OnCurrentChannelUserInfo != null)
                {
                    OnCurrentChannelUserInfo(this, args);
                }
            };

            client.OnUserGroupSuccessResult += (SpriteRenderer, args) =>
            {
                if (OnUserGroupSuccessResult != null)
                {
                    OnUserGroupSuccessResult(this, args);
                }
            };

            client.OnClickExitGameButton += (SpriteRenderer, args) =>
            {
                if (OnClickExitGameButton != null)
                {
                    OnClickExitGameButton(this, args);
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

        // Android SDK当游戏调用UpdateDataReport接口后返回此回调通知游戏实名认证成功状态
        public event EventHandler<EventArgs> RealNameAuthSuccessStatus;
        // Android 联想渠道用户防沉迷实名认证状态
        // 0：未实名认证
        // 1：成年人
        // 2：未成年人
        public event EventHandler<ChannelUserInfoEventArgs> OnCurrentChannelUserInfo;
        //Android 调用CheckUserGroupId接口后，会返回当前用户的分组状态（可选）
        // -1 : 没获取到
        // 1 : 新用户
        // 2 : 老用户
        public event EventHandler<GroupIdEventArgs> OnUserGroupSuccessResult;

        //用户点击实名认证界面退出游戏按钮时回调
        public event EventHandler<EventArgs> OnClickExitGameButton;


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

        // Android 获取UserCode
        public string GetUserCode()
        {
            return client.GetUserCode();
        }

        // Android 设置用户GroupId
        public void SetGroupId(int groupId)
        {
            client.SetGroupId(groupId);
        }

        // Android 获取用户GroupId
        public int GetGroupId()
        {
            return client.GetGroupId();
        }

        // Android更新用户数据接口
        public void UpdateDataReport()
        {
            client.UpdateDataReport();
        }

        // Android获取当前用户的分组id，调用此方法后，会通过 event EventHandler<GroupIdEventArgs> OnUserGroupSuccessResult接口返回用户的分组信息
        // zplayId:之前用户系统的zplayId，没有可以传""
        public void CheckUserGroupId(string zplayId)
        {
            client.CheckUserGroupId(zplayId);
        }

    }
}