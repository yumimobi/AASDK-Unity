#if UNITY_IOS
using System;
using AASDK.Common;
using AASDK.Api;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AASDK.iOS
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
        internal delegate void AANoTimeLeftWithTouristsModeCallback(IntPtr managerClient, string zplayID);
        internal delegate void AANoTimeLeftWithNonageModeCallback(IntPtr managerClient);
#endregion
        // 登录回调
        public event EventHandler<LoginSuccessEventArgs> OnTouristsModeLoginSuccess;
        public event EventHandler<EventArgs> OnTouristsModeLoginFailed;

        //实名认证回调
        public event EventHandler<EventArgs> RealNameAuthenticateResult;
        public event EventHandler<EventArgs> OnUserAuthFail;
        
        public event EventHandler<EventArgs> OnNoTimeLeftWithTouristMode;
        public event EventHandler<EventArgs> OnNoTimeLeftWithNonageMode;

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
        public int GetUserLoginStatus()
        {
            return Externs.getUserLoginStatus(managerPtr);
        }
        
        public int GetUserAuthenticationIdentity()
        {
            return Externs.getUserAuthenticationStatus(managerPtr);
        }

        public void ShowPrivacyPolicyView()
        {
            Externs.presentRealNameAuthController(managerPtr);
        }

        public void ShowLoginViewController()
        {
            Externs.checkLeftTimeOfCurrentUser(managerPtr);
        }
#endregion

#region Notification callback methods
        [MonoPInvokeCallback(typeof(AAPrivacyPolicyViewControllerHasBeenShownCallback))]
        private static void privacyPolicyViewControllerHasBeenShownCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnPrivacyPolicyShown != null)
            {
                client.OnPrivacyPolicyShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAgreesToPrivacyPolicyCallback))]
        private static void userAgreesToPrivacyPolicyCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserAgreesToPrivacyPolicy != null)
            {
                client.OnUserAgreesToPrivacyPolicy(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginViewControllerHasBeenShownCallback))]
        private static void loginViewControllerHasBeenShownCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnLoginHasBeenShown != null)
            {
                client.OnLoginHasBeenShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginViewControllerHasBeenDismissedCallback))]
        private static void loginViewControllerHasBeenDismissedCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnLoginHasBeenDismissed != null)
            {
                client.OnLoginHasBeenDismissed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginSuccessCallback))]
        private static void loginSuccessCallback(IntPtr notificationClient, string zplayID)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnLoginSuccess != null)
            {
                LoginSuccessEventArgs args = new LoginSuccessEventArgs()
                {
                    Message = zplayID
                };
                client.OnLoginSuccess(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginFailCallback))]
        private static void loginFailCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnLoginFail != null)
            {
                client.OnLoginFail(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAuthVcHasBeenShownCallback))]
        private static void userAuthVcHasBeenShownCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserAuthVcHasBeenShown != null)
            {
                client.OnUserAuthVcHasBeenShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserAuthSuccessCallback))]
        private static void userAuthSuccessCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserAuthSuccess != null)
            {
                client.OnUserAuthSuccess(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAWarningVcHasBeenShownCallback))]
        private static void warningVcHasBeenShownCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnWarningHasBeenShown != null)
            {
                client.OnWarningHasBeenShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserClickLoginButtonInPaymentWarningVcCallback))]
        private static void userClickLoginButtonInPaymentWarningVcCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserClickLoginButtonInPayment != null)
            {
                client.OnUserClickLoginButtonInPayment(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserClickLoginButtonInNoTimeLeftWarningVcCallback))]
        private static void userClickLoginButtonInNoTimeLeftWarningVcCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserClickLoginButtonInNoTimeLeft != null)
            {
                client.OnUserClickLoginButtonInNoTimeLeft(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserClickLoginOutButtonCallback))]
        private static void userClickLoginOutButtonCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserClickQuitButton != null)
            {
                client.OnUserClickQuitButton(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAUserClickConfirmButtonCallback))]
        private static void userClickConfirmButtonCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnUserClickConfirmButton != null)
            {
                client.OnUserClickConfirmButton(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AALoginOutSuccessfullCallback))]
        private static void loginOutSuccessfullCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnLogoutCallback != null)
            {
                client.OnLogoutCallback(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAPaymentIsRestrictedCallback))]
        private static void paymentIsRestrictedCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnCanPay != null)
            {
                client.OnCanPay(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AAPaymentUnlimitedCallback))]
        private static void paymentUnlimitedCallback(IntPtr notificationClient)
        {
            NotificationClient client = IntPtrToNotifiactionClient(notificationClient);
            if (client.OnProhibitPay != null)
            {
                client.OnProhibitPay(client, EventArgs.Empty);
            }
        }
        
        private static NotificationClient IntPtrToNotifiactionClient(IntPtr notificationClient)
        {
            GCHandle handle = (GCHandle)notificationClient;

            return handle.Target as NotificationClient;
        }

#endregion
    }
}
#endif