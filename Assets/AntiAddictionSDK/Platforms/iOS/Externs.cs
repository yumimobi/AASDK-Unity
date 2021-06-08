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

        #region AAManager externs
        [DllImport("__Internal")]
        internal static extern IntPtr AACreateManager(IntPtr managerClient);
        [DllImport("__Internal")]
        internal static extern void AASetManagerCallbacks(
            IntPtr manager,
            ManagerClient.AALoginSuccessCallback loginSuccessCallback,
            ManagerClient.AALoginFailCallback loginFailCallback,
            ManagerClient.AAUserAuthSuccessCallback userAuthSuccessCallback,
            ManagerClient.AAUserAuthFailCallback userAuthFailCallback,
            ManagerClient.AAUserAuthFailWithForceExitCallback userAuthFailWithForceExitCallback,
            ManagerClient.AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback,
            ManagerClient.AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback,
            ManagerClient.AALeftTimeOfCurrentUserCallback leftTimeOfCurrentUserCallback
        );
        
        [DllImport("__Internal")]
        internal static extern int getUserLoginStatus(IntPtr manager);
        
        [DllImport("__Internal")]
        internal static extern int getUserAuthenticationStatus(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern int getUserAgeGroup(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentRealNameAuthController(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentRealNameAuthControllerWithForceExit(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentAlertInfoController(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentCheckDetailInfoController(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentCashLimitedController(IntPtr manager);
        
        [DllImport("__Internal")]
        internal static extern int checkLeftTimeOfCurrentUser(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void stopTimerInUnity(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void resumeTimerInUnity(IntPtr manager);
        #endregion
    }
}
#endif