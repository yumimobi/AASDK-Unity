#if UNITY_ANDROID
namespace AntiAddictionSDK.Android
{
    internal static class Utils
    {
        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

        public const string BundleClassName = "android.os.Bundle";
        public const string DateClassName = "java.util.Date";
        public const string AntiAddictionClassName = "com.zplay.antiaddiction.system.AntiAddictionSDK";
        public const string UnityAntiAddictionListenerClassName = "com.zplay.antiaddiction.system.UnityAntiAddictionListener";
    }
}
#endif