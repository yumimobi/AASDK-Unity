using AntiAddictionSDK.Api;
using AntiAddictionSDK.Common;
using UnityEngine;

namespace AntiAddictionSDK
{
    public class AntiAddictionClientFactory
    {

        public static IAntiAddictionClient BuildAntiAddictionClient()
        {
#if UNITY_ANDROID
            return new Android.AntiAddictionClient();
#elif UNITY_IPHONE
            return new iOS.ManagerClient();
#else
            return new DummyClient();
#endif
        }


    }
}