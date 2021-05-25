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

    void realNameAuthenticateResult(String isSuccess);

    void noTimeLeftWithTouristsMode();

    void noTimeLeftWithNonageMode();

}
