#if UNITY_IOS
using System;
using AntiAddictionSDK.Common;
using AntiAddictionSDK.Api;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AntiAddictionSDK.iOS
{
    public class ManagerClient : IAntiAddictionClient
    {
        private IntPtr managerPtr;
        private IntPtr managerClientPtr;

#region callback types
        internal delegate void AALoginSuccessCallback(IntPtr managerClient, string zplayID);
        internal delegate void AALoginFailCallback(IntPtr managerClient);
        internal delegate void AAUserAuthSuccessCallback(IntPtr managerClient);
        internal delegate void AAUserAuthFailCallback(IntPtr managerClient);
        internal delegate void AAUserAuthFailWithForceExitCallback(IntPtr managerClient);
        internal delegate void AANoTimeLeftWithTouristsModeCallback(IntPtr managerClient);
        internal delegate void AANoTimeLeftWithNonageModeCallback(IntPtr managerClient);
        internal delegate void AALeftTimeOfCurrentUserCallback(IntPtr managerClient, int leftTime);
#endregion
        // 登录回调
        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed;

        //实名认证回调
        public event EventHandler<EventArgs> RealNameAuthenticateSuccess;
        public event EventHandler<EventArgs> RealNameAuthenticateFailed;
        public event EventHandler<EventArgs> RealNameAuthenticateFailedWithForceExit;
        
        public event EventHandler<EventArgs> NoTimeLeftWithTouristsMode;
        public event EventHandler<EventArgs> NoTimeLeftWithNonageMode;
        public event EventHandler<LeftTimeEventArgs> LeftTimeOfCurrentUserInEverySeconds;

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

        //用户点击实名认证界面退出游戏按钮时回调
        public event EventHandler<EventArgs> OnClickExitGameButton = delegate { };

        public ManagerClient()
        {
            managerClientPtr = (IntPtr)GCHandle.Alloc(this);
            managerPtr = Externs.AACreateManager(managerClientPtr);

            Externs.AASetManagerCallbacks(
                managerPtr,
                loginSuccessCallback,
                loginFailCallback,
                userAuthSuccessCallback,
                userAuthFailCallback,
                userAuthFailWithForceExitCallback,
                noTimeLeftWithTouristsModeCallback,
                noTimeLeftWithNonageModeCallback,
                leftTimeOfCurrentUserInEverySecondsCallback
            );
        }

        private IntPtr ManagerPtr
        {
            get
            {
                return ManagerPtr;
            }

            set
            {
                Externs.AARelease(ManagerPtr);
                ManagerPtr = value;
            }
        }

#region IAntiAddictionClient implement 
        public int IsLogined()
        {
            return Externs.getUserLoginStatus(managerPtr);
        }
        
        public int IsAuthenticated()
        {
            return Externs.getUserAuthenticationStatus(managerPtr);
        }

        public int AgeGroupOfCurrentUser() 
        {
            return Externs.getUserAgeGroup(managerPtr);
        }

        public void ShowRealNameView()
        {
            Externs.presentRealNameAuthController(managerPtr);
        }

        public void ShowRealNameViewWithForceExit()
        {
            Externs.presentRealNameAuthControllerWithForceExit(managerPtr);
        }

        public void ShowAlertInfoController()
        {
            Externs.presentAlertInfoController(managerPtr);
        }

        public void ShowCheckDetailInfoController()
        {
            Externs.presentCheckDetailInfoController(managerPtr);
        }

        public void ShowCashLimitedController()
        {
            Externs.presentCashLimitedController(managerPtr);
        }

        public int LeftTimeOfCurrentUser()
        {
            return Externs.checkLeftTimeOfCurrentUser(managerPtr);
        }

        public void SetChannelUserId(string userId)
        {
        }

        public void GameOnPause()
        {
        }

        public void GameOnResume()
        {
        }

        public string GetUserCode()
        {
            return "";
        }

        public void SetGroupId(int groupId)
        {
        }

        public int GetGroupId()
        {
            return 0;
        }

        public void UpdateDataReport()
        {
        }

        public void CheckUserGroupId(string zplayId)
        {
        }

        public void StopTimerInUnity()
        {
            Externs.stopTimerInUnity(managerPtr);
        }

        public void ResumeTimerInUnity()
        {
            Externs.resumeTimerInUnity(managerPtr);
        }
#endregion

#region Notification callback methods
        [MonoPInvokeCallback(typeof(AALoginSuccessCallback))]
        private static void loginSuccessCallback(IntPtr managerClient, string zplayID)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.OnTouristsModeLoginSuccess != null)
            {
                LoginSuccessEventArgs args = new LoginSuccessEventArgs()
                {
                    Message = zplayID
                };
                client.OnTouristsModeLoginSuccess(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginFailCallback))]
        private static void loginFailCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.OnTouristsModeLoginFailed != null)
            {
                client.OnTouristsModeLoginFailed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAuthSuccessCallback))]
        private static void userAuthSuccessCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.RealNameAuthenticateSuccess != null)
            {
                client.RealNameAuthenticateSuccess(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAuthFailCallback))]
        private static void userAuthFailCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.RealNameAuthenticateFailed != null)
            {
                client.RealNameAuthenticateFailed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAuthFailWithForceExitCallback))]
        private static void userAuthFailWithForceExitCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.RealNameAuthenticateFailedWithForceExit != null)
            {
                client.RealNameAuthenticateFailedWithForceExit(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AANoTimeLeftWithTouristsModeCallback))]
        private static void noTimeLeftWithTouristsModeCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.NoTimeLeftWithTouristsMode != null)
            {
                client.NoTimeLeftWithTouristsMode(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AANoTimeLeftWithNonageModeCallback))]
        private static void noTimeLeftWithNonageModeCallback(IntPtr managerClient)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            if (client.NoTimeLeftWithNonageMode != null)
            {
                client.NoTimeLeftWithNonageMode(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AALeftTimeOfCurrentUserCallback))]
        private static void leftTimeOfCurrentUserInEverySecondsCallback(IntPtr managerClient, int leftTime)
        {
            ManagerClient client = IntPtrToManagerClient(managerClient);
            LeftTimeEventArgs args = new LeftTimeEventArgs()
                {
                    LeftTime = leftTime
                };
            if (client.LeftTimeOfCurrentUserInEverySeconds != null)
            {
                client.LeftTimeOfCurrentUserInEverySeconds(client, args);
            }
        }
        
        private static ManagerClient IntPtrToManagerClient(IntPtr managerClient)
        {
            GCHandle handle = (GCHandle)managerClient;

            return handle.Target as ManagerClient;
        }

#endregion
    }
}
#endif