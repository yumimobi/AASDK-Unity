#if UNITY_ANDROID

using System;
using UnityEngine;

using AntiAddictionSDK.Api;
using AntiAddictionSDK.Common;

namespace AntiAddictionSDK.Android
{
    public class AntiAddictionClient : AndroidJavaProxy, IAntiAddictionClient
    {
        private AndroidJavaObject antiAddictionSDK;

        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess = delegate { };
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed = delegate { };
        public event EventHandler<EventArgs> RealNameAuthenticateSuccess = delegate { };
        public event EventHandler<EventArgs> RealNameAuthenticateFailed = delegate { };
        public event EventHandler<EventArgs> NoTimeLeftWithTouristsMode = delegate { };
        public event EventHandler<EventArgs> NoTimeLeftWithNonageMode = delegate { };
        public event EventHandler<EventArgs> RealNameAuthenticateFailedWithForceExit = delegate { };
        public event EventHandler<LeftTimeEventArgs> LeftTimeOfCurrentUserInEverySeconds = delegate { };

        // Android SDK当游戏调用UpdateDataReport接口后返回此回调通知游戏实名认证成功状态
        public event EventHandler<EventArgs> RealNameAuthSuccessStatus = delegate { };
        // Android 联想渠道用户防沉迷实名认证状态
        // 0：未实名认证
        // 1：成年人
        // 2：未成年人
        public event EventHandler<ChannelUserInfoEventArgs> OnCurrentChannelUserInfo = delegate { };
        //Android 调用CheckUserGroupId接口后，会返回当前用户的分组状态（可选）
        // -1 : 没获取到
        // 1 : 新用户
        // 2 : 老用户
        public event EventHandler<GroupIdEventArgs> OnUserGroupSuccessResult = delegate { };


        public AntiAddictionClient() : base(Utils.UnityAntiAddictionListenerClassName)
        {

            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            antiAddictionSDK = new AndroidJavaObject(Utils.AntiAddictionClassName, activity,  this);
        }

        #region IAntiAddictionClient implementation


        public int IsLogined()
        {
            return antiAddictionSDK.Call<int>("isLogined"); 
        }

        public int IsAuthenticated()
        {
            return antiAddictionSDK.Call<int>("isAuthenticated");
        }

        public void ShowRealNameView()
        {
            antiAddictionSDK.Call("showRealNameView");
        }

        public int LeftTimeOfCurrentUser()
        {
            return antiAddictionSDK.Call<int>("leftTimeOfCurrentUser");
        }

        public int AgeGroupOfCurrentUser()
        {
            return antiAddictionSDK.Call<int>("isAdult");
        }

        public void ShowRealNameViewWithForceExit()
        {
            antiAddictionSDK.Call("showForceExitRealNameDialog");
        }

        public void ShowAlertInfoController()
        {
            antiAddictionSDK.Call("showAntiAddictionPromptDialog");
        }

        public void ShowCheckDetailInfoController() {
            antiAddictionSDK.Call("showTimeTipsDialog");
        }

        public void ShowCashLimitedController()
        {
            antiAddictionSDK.Call("checkPay");
        }

        public void SetChannelUserId(string userId)
        {
            antiAddictionSDK.Call("setChannelUserId", userId);
        }


        public void GameOnPause()
        {
            antiAddictionSDK.Call("onPause");
        }

        public void GameOnResume()
        {
            antiAddictionSDK.Call("onResume");
        }

        public void StopTimerInUnity()
        {
            antiAddictionSDK.Call("onPause");
        }

        public void ResumeTimerInUnity()
        {
            antiAddictionSDK.Call("onResume");
        }

        public string GetUserCode()
        {
            return antiAddictionSDK.Call<string>("getUserCode");
        }

        public void SetGroupId(int groupId)
        {
            antiAddictionSDK.Call("setGroupId", groupId);
        }

        public int GetGroupId()
        {
            return antiAddictionSDK.Call<int>("getGroupId");
        }

        public void UpdateDataReport()
        {
            antiAddictionSDK.Call("updateDataReport");
        }

        public void CheckUserGroupId(string zplayId)
        {
            antiAddictionSDK.Call("checkUserGroupId", zplayId);
        }

        #endregion



        #region Callback from UnityAntiAddictionListener

        void onTouristsModeLoginSuccess(string touristsID)
        {
            Debug.Log("-----onTouristsModeLoginSuccess touristsID: " + touristsID);
            if (OnTouristsModeLoginSuccess != null)
            {
                LoginSuccessEventArgs args = new LoginSuccessEventArgs()
                {
                    Message = touristsID
                };
                OnTouristsModeLoginSuccess(this, args);
            }
        }

        void onTouristsModeLoginFailed()
        {
            if (OnTouristsModeLoginFailed != null)
            {
                OnTouristsModeLoginFailed(this, EventArgs.Empty);
            }
        }

    
        void realNameAuthenticateSuccess()
        {
            if (RealNameAuthenticateSuccess != null)
            {
               RealNameAuthenticateSuccess(this, EventArgs.Empty);
            }
        }

        void realNameAuthenticateFailed()
        {
            if (RealNameAuthenticateFailed != null)
            {
                RealNameAuthenticateFailed(this, EventArgs.Empty);
            }
        }

        void noTimeLeftWithTouristsMode()
        {
            if (NoTimeLeftWithTouristsMode != null)
            {
                NoTimeLeftWithTouristsMode(this, EventArgs.Empty);
            }
        }

        void noTimeLeftWithNonageMode()
        {
            if (NoTimeLeftWithNonageMode != null)
            {
                NoTimeLeftWithNonageMode(this, EventArgs.Empty);
            }
        }

        void onCurrentUserInfo(int leftTime, int isAuth, int isAdult)
        {
            Debug.Log("-----onCurrentUserInfo touristsID: " + leftTime);
            if (LeftTimeOfCurrentUserInEverySeconds != null)
            {
                LeftTimeEventArgs args = new LeftTimeEventArgs()
                {
                    LeftTime = leftTime
                };
                LeftTimeOfCurrentUserInEverySeconds(this, args);
            }
        }

        // Android SDK当游戏调用UpdateDataReport接口后返回此回调通知游戏实名认证成功状态
        void realNameAuthSuccessStatus()
        {
            if (RealNameAuthSuccessStatus != null)
            {
                RealNameAuthSuccessStatus(this, EventArgs.Empty);
            }
        }

        // Android 联想渠道用户防沉迷实名认证状态
        // 0：未实名认证
        // 1：成年人
        // 2：未成年人
        void onCurrentChannelUserInfo(int realNameStatus)
        {
            Debug.Log("-----onCurrentChannelUserInfo realNameStatus: " + realNameStatus);
            if (OnCurrentChannelUserInfo != null)
            {
                ChannelUserInfoEventArgs args = new ChannelUserInfoEventArgs()
                {
                    RealNameStatus = realNameStatus
                };
                OnCurrentChannelUserInfo(this, args);
            }
        }

        //Android 调用CheckUserGroupId接口后，会返回当前用户的分组状态（可选）
        // -1 : 没获取到
        // 1 : 新用户
        // 2 : 老用户
        void onUserGroupSuccessResult(int groupId)
        {
            Debug.Log("-----onUserGroupSuccessResult groupId: " + groupId);
            if (OnUserGroupSuccessResult != null)
            {
                GroupIdEventArgs args = new GroupIdEventArgs()
                {
                    GroupId = groupId
                };
                OnUserGroupSuccessResult(this, args);
            }
        }


        #endregion
    }
}

#endif