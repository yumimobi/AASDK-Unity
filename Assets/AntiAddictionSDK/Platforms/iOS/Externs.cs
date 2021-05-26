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
            ManagerClient.AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback,
            ManagerClient.AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback
        );
        
        [DllImport("__Internal")]
        internal static extern int getUserLoginStatus(IntPtr manager);
        
        [DllImport("__Internal")]
        internal static extern int getUserAuthenticationStatus(IntPtr manager);

        [DllImport("__Internal")]
        internal static extern void presentRealNameAuthController(IntPtr manager);
        
        [DllImport("__Internal")]
        internal static extern int checkLeftTimeOfCurrentUser(IntPtr manager);
        #endregion
    }
}
#endif