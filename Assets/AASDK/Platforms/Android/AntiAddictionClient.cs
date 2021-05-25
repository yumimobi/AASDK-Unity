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

        public void GameOnPause()
        {
            antiAddictionSDK.Call("onPause");
        }

        public void GameOnResume()
        {
            antiAddictionSDK.Call("onResume");
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
        #endregion
    }
}

#endif