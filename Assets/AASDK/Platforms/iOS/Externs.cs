#if UNITY_IOS

using System;
using System.Runtime.InteropServices;

namespace AntiAddictionSDK.iOS
{
    class Externs
    {
        #region Common externs
        [DllImport("__Internal")]
        internal static extern IntPtr AARelease(IntPtr obj);
        #endregion

        #region AANotification externs
        [DllImport("__Internal")]
        internal static extern IntPtr AACreateNotification(IntPtr notificationClient);
        [DllImport("__Internal")]
        internal static extern void AASetNotificationCallbacks(
            IntPtr notification,
            NotificationClient.AAPrivacyPolicyViewControllerHasBeenShownCallback privacyPolicyViewControllerHasBeenShownCallback,
            NotificationClient.AAUserAgreesToPrivacyPolicyCallback userAgreesToPrivacyPolicyCallback,
            NotificationClient.AALoginViewControllerHasBeenShownCallback loginViewControllerHasBeenShownCallback,
            NotificationClient.AALoginViewControllerHasBeenDismissedCallback loginViewControllerHasBeenDismissedCallback,
            NotificationClient.AALoginSuccessCallback loginSuccessCallback,
            NotificationClient.AALoginFailCallback loginFailCallback,
            NotificationClient.AAUserAuthVcHasBeenShownCallback userAuthVcHasBeenShownCallback,
            NotificationClient.AAUserAuthSuccessCallback userAuthSuccessCallback,
            NotificationClient.AAWarningVcHasBeenShownCallback warningVcHasBeenShownCallback,
            NotificationClient.AAUserClickLoginButtonInPaymentWarningVcCallback userClickLoginButtonInPaymentWarningVcCallback,
            NotificationClient.AAUserClickLoginButtonInNoTimeLeftWarningVcCallback userClickLoginButtonInNoTimeLeftWarningVcCallback,
            NotificationClient.AAUserClickLoginOutButtonCallback userClickLoginOutButtonCallback,
            NotificationClient.AAUserClickConfirmButtonCallback userClickConfirmButtonCallback,
            NotificationClient.AALoginOutSuccessfullCallback loginOutSuccessfullCallback,
            NotificationClient.AAPaymentIsRestrictedCallback paymentIsRestrictedCallback,
            NotificationClient.AAPaymentUnlimitedCallback paymentUnlimitedCallback
        );
        
        [DllImport("__Internal")]
        internal static extern int getUserLoginStatus(IntPtr notification);
        
        [DllImport("__Internal")]
        internal static extern int getUserAuthenticationIdentity(IntPtr notification);

        [DllImport("__Internal")]
        internal static extern void showPrivacyPolicyView(IntPtr notification);
        
        [DllImport("__Internal")]
        internal static extern void showLoginViewController(IntPtr notification);

        [DllImport("__Internal")]
        internal static extern void showUserAuthenticationViewController(IntPtr notification);

        [DllImport("__Internal")]
        internal static extern void loginWithUserNameAndPassword(IntPtr notification, string userName, string password);

        [DllImport("__Internal")]
        internal static extern void loginWithThirdPartyPlatform(IntPtr notification, string token, string otherId, string platformName);

        [DllImport("__Internal")]
        internal static extern void loginWithZplayID(IntPtr notification, string zplayID);

        [DllImport("__Internal")]
        internal static extern void loginOut(IntPtr notification);

        [DllImport("__Internal")]
        internal static extern void checkNumberLimitBeforePayment(IntPtr notification, int payNumber);

        [DllImport("__Internal")]
        internal static extern void reportNumberAfterPayment(IntPtr notification, int payNumber);
        #endregion
    }
}
#endif