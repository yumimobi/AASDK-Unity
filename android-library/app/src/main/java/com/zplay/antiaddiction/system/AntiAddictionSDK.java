package com.zplay.antiaddiction.system;

import android.app.Activity;
import android.util.Log;

import com.android.antiaddiction.callback.AntiAddictionCallback;
import com.android.antiaddiction.system.AntiAddictionSystemSDK;


/**
 * Description:
 * <p>
 * Created by lgd on 2019-10-31.
 */
public class AntiAddictionSDK {
    private static final String TAG = "AntiAddictionSDK";


    private final Activity activity;
    private final UnityAntiAddictionListener listener;

    public AntiAddictionSDK(Activity activity, UnityAntiAddictionListener listener) {
        this.activity = activity;
        this.listener = listener;
        init();
    }

    //初始化接口
    public void init() {
        Log.i(TAG, "init");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {

                AntiAddictionSystemSDK.init(activity, new AntiAddictionCallback() {
                    @Override
                    public void onTouristsModeLoginSuccess(final String touristsID) {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onTouristsModeLoginSuccess usercode:" + touristsID);
                                    listener.onTouristsModeLoginSuccess(touristsID);
                                }
                            });
                        }
                    }

                    @Override
                    public void onTouristsModeLoginFailed() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onTouristsModeLoginFailed :");
                                    listener.onTouristsModeLoginFailed();
                                }
                            });
                        }
                    }

                    @Override
                    public void realNameAuthenticateResult(final boolean isSuccess) {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "realNameAuthenticateResult :" + isSuccess);
                                    if (isSuccess) {
                                        listener.realNameAuthenticateSuccess();
                                    } else {
                                        listener.realNameAuthenticateFailed();
                                    }
                                }
                            });
                        }
                    }

                    @Override
                    public void noTimeLeftWithTouristsMode() {
                        // 游客时长已用尽(1h/15 days)
                        // 收到此回调 3s 后，会展示游客时长已用尽弹窗
                        // 游戏请在收到回调 3s 内处理未尽事宜
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "noTimeLeftWithTouristsMode:");
                                    listener.noTimeLeftWithTouristsMode();
                                }
                            });
                        }
                    }

                    @Override
                    public void noTimeLeftWithNonageMode() {
                        // 未成年时长已用尽(2h/1 day)
                        // 收到此回调 3s 后，会展示未成年时长已用尽弹窗
                        // 游戏请在收到回调 3s 内处理未尽事宜
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "noTimeLeftWithNonageMode:");
                                    listener.noTimeLeftWithNonageMode();
                                }
                            });
                        }
                    }
                });


            }
        });

    }

    // 实名认证界面
    // 游戏可主动调用
    // 如果游戏无主动调用，游客游戏时长用尽后由SDK主动触发
    public void showRealNameView() {
        Log.i(TAG, "showRealNameDialog");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                AntiAddictionSystemSDK.showRealNameDialog(activity);
            }
        });
    }

    public int isLogined() {
        Log.i(TAG, "isLogined");
        if (AntiAddictionSystemSDK.isLogined(activity)) {
            return 1;
        } else {
            return 0;
        }
    }

    public int isAuthenticated() {
        Log.i(TAG, "isAuthenticated");
        if (AntiAddictionSystemSDK.isAuthenticated(activity)) {
            return 1;
        } else {
            return 0;
        }
    }

    public int leftTimeOfCurrentUser() {
        Log.i(TAG, "leftTimeOfCurrentUser");
        return Integer.valueOf(AntiAddictionSystemSDK.leftTimeOfCurrentUser(activity) + "");
    }


    public void onPause() {
        Log.i(TAG, "onPause");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                AntiAddictionSystemSDK.onPause();
            }
        });
    }

    public void onResume() {
        Log.i(TAG, "onResume");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                AntiAddictionSystemSDK.onResume();
            }
        });
    }


}
