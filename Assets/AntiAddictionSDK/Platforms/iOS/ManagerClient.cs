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
        internal delegate void AANoTimeLeftWithTouristsModeCallback(IntPtr managerClient);
        internal delegate void AANoTimeLeftWithNonageModeCallback(IntPtr managerClient);
#endregion
        // 登录回调
        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed;

        //实名认证回调
        public event EventHandler<EventArgs> RealNameAuthenticateSuccess;
        public event EventHandler<EventArgs> RealNameAuthenticateFailed;
        
        public event EventHandler<EventArgs> NoTimeLeftWithTouristsMode;
        public event EventHandler<EventArgs> NoTimeLeftWithNonageMode;

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
                noTimeLeftWithTouristsModeCallback,
                noTimeLeftWithNonageModeCallback
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

        public void ShowRealNameView()
        {
            Externs.presentRealNameAuthController(managerPtr);
        }

        public int LeftTimeOfCurrentUser()
        {
            return Externs.checkLeftTimeOfCurrentUser(managerPtr);
        }

        public void GameOnPause()
        {
        }

        public void GameOnResume()
        {
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
        
        private static ManagerClient IntPtrToManagerClient(IntPtr managerClient)
        {
            GCHandle handle = (GCHandle)managerClient;

            return handle.Target as ManagerClient;
        }

#endregion
    }
}
#endif