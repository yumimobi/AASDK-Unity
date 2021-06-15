package com.zplay.antiaddiction.system;

/**
 * Description:
 * <p>
 * Created by lgd on 2019-10-31.
 */
public interface UnityAntiAddictionListener {

    //登录回调
    void onTouristsModeLoginSuccess(String usercode);

    void onTouristsModeLoginFailed();

    void realNameAuthenticateSuccess(String isAdult);

    void realNameAuthenticateFailed();

    void noTimeLeftWithTouristsMode();

    void noTimeLeftWithNonageMode();

    void onClickExitGameButton();

    void onClickTempLeaveButton();

    void onCurrentUserCanPay();

    void onCurrentUserBanPay();

    void onCurrentUserInfo(int leftTime, String isAuth, String isAdult);

}
