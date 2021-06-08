#import "AABridge.h"

@interface AABridge ()<AAManagerDelegate>

@end

@implementation AABridge
- (instancetype)initSDKWithClientReference:(AATypeClientRef*)aaClientRef {
    if (self = [super init]) {
        _client = aaClientRef;
        _manager = [AAManager shared];
        _manager.delegate = self;
    }
    return self;
}

// 获取当前用户登录状态
// 0: 未登录
// 1: 已登录
- (int)getUserLoginStatus {
    int loginStatus = [NSNumber numberWithBool:[self.manager isLogined]].intValue;
    return loginStatus;
}

// 获取用户的认证身份
// 0: 未认证
// 1：已认证
- (int)getUserAuthenticationStatus {
    int authStatus = [NSNumber numberWithBool:[self.manager isAuthenticated]].intValue;
    return authStatus;
}

// 获取用户年龄状态
// 0: 未登录
// 1: 成年人
// 2: 未成年人
- (int)getUserAgeGroup {
    AAAgeGroup ageGroup = [self.manager isAdult];
    if (ageGroup == adult) {
        return 1;
    }
    if (ageGroup == nonage) {
        return 2;
    }
    return 0;
}

// 展示实名认证控制器
- (void)presentRealNameAuthController {
    [self.manager presentRealNameAuthenticationControllerWithRootViewController:UnityGetGLViewController()];
}

// 展示实名认证控制器（用户可点击退出游戏）
- (void)presentRealNameAuthControllerWithForceExit {
    [self.manager presentForceExitRealNameAuthControllerWithRootViewController:UnityGetGLViewController()];
}

// 展示已在线时长提示界面，游戏主动调用
- (void)presentAlertInfoController {
    [self.manager presentAlertInfoControllerWithRootViewController:UnityGetGLViewController()];
}

// 展示查看详情界面
- (void)presentCheckDetailInfoController {
    [self.manager presentDetailInfoControllerWithRootViewController:UnityGetGLViewController()];
}

// 展示消费限制窗口
- (void)presentCashLimitedController {
    [self.manager presentCashLimitedControllerWith:UnityGetGLViewController()];
}

// 查询剩余时间
- (int)checkLeftTimeOfCurrentUser {
    return [self.manager leftTimeOfCurrentUser];
}

// 暂停计时器
- (void)stopTimerInUnity {
    [self.manager stopTimer];
}

// 恢复计时器
- (void)resumeTimerInUnity {
    [self.manager resumeTimer];
}

#pragma mark - AAManagerDelegate
/// 游客登录结果
/// @param touristsID 游客ID 有值则为登录成功，否则登录失败
- (void)touristsModeLoginResult:(nullable NSString *)touristsID {
    if (touristsID.length && self.loginSuccessCallback) {
        self.loginSuccessCallback(self.client, [touristsID cStringUsingEncoding:NSUTF8StringEncoding]);
    } else if (self.loginFailCallback) {
        self.loginFailCallback(self.client);
    }
}

/// 实名认证成功
- (void)realNameAuthSuccess {
    if (self.userAuthSuccessCallback) {
        self.userAuthSuccessCallback(self.client);
    }
}

/// 用户在实名认证界面点击暂不认证
- (void)clickTempLeaveButtonOnRealNameAuthController {
    if (self.userAuthFailCallback) {
        self.userAuthFailCallback(self.client);
    }
}

/// 用户在实名认证界面点击退出游戏
- (void)clickForceExitButtonOnRealNameAuthController {
    if (self.userAuthFailWithForceExitCallback) {
        self.userAuthFailWithForceExitCallback(self.client);
    }
}

/// 游客时长已用尽
/// 收到此回调后，请将当前控制器传入SDK，用于展示实名认证界面
- (UIViewController *)noTimeLeftWithTouristsMode {
    if (self.noTimeLeftWithTouristsModeCallback) {
        self.noTimeLeftWithTouristsModeCallback(self.client);
    }
    return UnityGetGLViewController();
}

/// 未成年时长已用尽
/// 收到此回调后，请将当前控制器传入SDK，用于展示未成年时长已用尽弹窗
- (UIViewController *)noTimeLeftWithNonageMode {
    if (self.noTimeLeftWithNonageModeCallback) {
        self.noTimeLeftWithNonageModeCallback(self.client);
    }
    return UnityGetGLViewController();
}

/// 当前用户剩余时间，每秒回调一次
/// @param leftTime 剩余时间， -1 为无限制
/// @param isAuth 是否已经实名认证
/// @param ageGroup 是否成年人
- (void)currentUserInfo:(int)leftTime isAuthenticated:(BOOL)isAuth ageGroup:(AAAgeGroup)ageGroup {
    if (self.leftTimeOfCurrentUserCallback) {
        self.leftTimeOfCurrentUserCallback(self.client, leftTime);
    }
}

@end
