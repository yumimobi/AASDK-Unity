package com.zplay.antiaddiction.system;

import android.app.Activity;
import android.text.TextUtils;
import android.util.Log;

import com.android.antiaddiction.callback.AntiAddictionCallback;
import com.android.antiaddiction.callback.UserGroupCallback;
import com.android.antiaddiction.system.AntiAddictionSystemSDK;
import com.android.antiaddiction.utils.enumbean.AgeGroup;


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
                    public void realNameAuthenticateSuccess() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "realNameSuccess isAdult :");
                                    listener.realNameAuthenticateSuccess();
                                }
                            });
                        }
                    }

                    @Override
                    public void realNameAuthenticateFailed() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "realNameSuccess Failed");
                                    listener.realNameAuthenticateFailed();
                                }
                            });
                        }
                    }

                    @Override
                    public void realNameAuthSuccessStatus() {
                        //实名认证成功的状态回调，当调用 4.6.3 更新防沉迷数据接口，并且服务端返回已经实名认证后才会返回本回调
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onCurrentChannelUserInfo");
                                    listener.realNameAuthSuccessStatus();
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

                    @Override
                    public void onCurrentUserInfo(final long leftTime, final boolean isAuth, final AgeGroup ageGroup) {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onCurrentUserInfo:");
                                    int iLeftTime = Integer.valueOf(leftTime + "");
                                    int isAuthed = isAuth ? 1 : 0;
                                    listener.onCurrentUserInfo(iLeftTime, isAuthed, ageGroup.getType());
                                }
                            });
                        }
                    }

                    @Override
                    public void onClickExitGameButton() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onClickExitGameButton:");
                                    listener.onClickExitGameButton();
                                }
                            });
                        }
                    }

                    @Override
                    public void onClickTempLeaveButton() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onClickTempLeaveButton:");
                                    listener.onClickTempLeaveButton();
                                }
                            });
                        }
                    }

                    @Override
                    public void onCurrentUserCanPay() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onCurrentUserCanPay");
                                    listener.onCurrentUserCanPay();
                                }
                            });
                        }
                    }

                    @Override
                    public void onCurrentUserBanPay() {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onCurrentUserBanPay");
                                    listener.onCurrentUserBanPay();
                                }
                            });
                        }
                    }

                    @Override
                    public void onCurrentChannelUserInfo(final AgeGroup ageGroup) {
                        if (listener != null && activity != null) {
                            activity.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    Log.i(TAG, "onCurrentChannelUserInfo");
                                    listener.onCurrentChannelUserInfo(ageGroup.getType());
                                }
                            });
                        }
                    }
                });


            }
        });

    }

    // 防沉迷提示弹窗界面
    // 游戏可主动调用
    // 游戏进入主界面之后，调用下面的接口，给未实名或者未成年人介绍实名认证规范
    public void showAntiAddictionPromptDialog() {
        Log.i(TAG, "showAntiAddictionPromptDialog");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (AntiAddictionSystemSDK.isLogined(activity)) {
                    AntiAddictionSystemSDK.showAlertInfoDialog(activity);
                }
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

    // 实名认证界面
    // 游戏可主动调用
    // 显示带退出游戏按钮的实名认证界面
    public void showForceExitRealNameDialog() {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                AntiAddictionSystemSDK.showForceExitRealNameDialog(activity);
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


    public int isAdult() {
        Log.i(TAG, "isAdult");
        // unknown: 未实名认证
        // adult: 成年
        // nonage: 未成年
        return AntiAddictionSystemSDK.isAdult(activity).getType();
    }

    //显示剩余游戏时长说明弹窗
    public void showTimeTipsDialog() {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.i(TAG, "showTimeTipsDialog");
                AntiAddictionSystemSDK.showTimeTipsDialog(activity);
            }
        });
    }


    public int leftTimeOfCurrentUser() {
        Log.i(TAG, "leftTimeOfCurrentUser");
        return Integer.valueOf(AntiAddictionSystemSDK.leftTimeOfCurrentUser(activity) + "");
    }

    public void setChannelUserId(final String userId) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.i(TAG, "setChannelUserId");
                AntiAddictionSystemSDK.setChannelUserId(activity, userId);
            }
        });
    }

    //检测当前用户是否能购买计费
    public void checkPay() {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.i(TAG, "checkPay");
                AntiAddictionSystemSDK.checkCurrentUserPay(activity);
            }
        });
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

    //****************用户分组相关接口*******************
    //获取用户id
    public String getUserCode() {
        Log.i(TAG, "getUserCode");
        return AntiAddictionSystemSDK.getUserCode(activity);
    }

    public void setGroupId(final int groupId) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.i(TAG, "setGroupId");
                // 1: 新用户 // 2：老用户
                AntiAddictionSystemSDK.setGroupId(activity, groupId);
            }
        });
    }

    public int getGroupId() {
        Log.i(TAG, "getGroupId");
        // -1: 游戏未设置 // 1: 新用户 // 2：老用户
        return AntiAddictionSystemSDK.getGroupId(activity);
    }

    public void updateDataReport() {
        //游戏调用此接口，SDK会马上去服务端获取当前用户的数据
        AntiAddictionSystemSDK.updateDataReport();
    }

    public void checkUserGroupId(final String zplayId) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.i(TAG, "checkUserGroupId");
                /** 参数说明
                 * Activity
                 * zplayId:之前用户系统的zplayId，没有可以传""
                 * UserGroupCallback:回调
                 */
                AntiAddictionSystemSDK.checkUserGroupId(activity, zplayId, new UserGroupCallback() {
                    @Override
                    public void onUserGroupSuccessResult(String userGroup) {
                        //获取UserGroup(-1:没获取到,1:新用户,2:老用户)
                        if (listener != null) {
                            if (TextUtils.equals(userGroup, "-1")) {
                                listener.onUserGroupSuccessResult(-1);
                            } else if (TextUtils.equals(userGroup, "1")) {
                                listener.onUserGroupSuccessResult(1);
                            } else if (TextUtils.equals(userGroup, "2")) {
                                listener.onUserGroupSuccessResult(2);
                            }
                        }
                    }
                });
            }
        });
    }

}
