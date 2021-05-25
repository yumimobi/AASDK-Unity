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
    int loginStaus = [NSNumber numberWithBool:[self.manager isLogined]].intValue;
    return loginStatus;
}

// 获取用户的认证身份
// 0: 未认证
// 1：已认证
- (int)getUserAuthenticationStatus {
    int authStatus = [NSNumber numberWithBool:[self.manager isAuthenticated]].intValue;
    return authStatus;
}

// 展示实名认证控制器
- (void)presentRealNameAuthController {
    [self.manager presentRealNameAuthenticationController];
}

// 查询剩余时间
- (int)checkLeftTimeOfCurrentUser {
    return [self.manager leftTimeOfCurrentUser];
}

#pragma mark - AAManagerDelegate
// 游客登录结果
// touristsID 有值则为登录成功，否则登录失败
- (void)touristsModeLoginResult:(nullable NSString *)touristsID {
    if (touristsID.length && self.loginSuccessCallback) {
        self.loginSuccessCallback(self.client, [touristsID cStringUsingEncoding:NSUTF8StringEncoding]);
    } else if (self.loginFailCallback) {
        self.loginFailCallback(self.client);
    }
}

// 实名认证结果
- (void)realNameAuthenticateResult:(bool)success {
    if (success && self.userAuthSuccessCallback) {
        self.userAuthSuccessCallback(self.client);
    } else if (self.userAuthFailCallback) {
        self.userAuthFailCallback(self.client);
    }
}

// 游客时长已用尽(1h/15 days)
// 收到此回调 3s 后，会展示实名认证界面
// 游戏请在收到回调 3s 内处理未尽事宜
- (void)noTimeLeftWithTouristsMode {
    if (self.noTimeLeftWithTouristsModeCallback) {
        self.noTimeLeftWithTouristsModeCallback(self.client);
    }
}

// 未成年时长已用尽
// 收到此回调 3s 后，会展示未成年时长已用尽弹窗
// 游戏请在收到回调 3s 内处理未尽事宜
- (void)noTimeLeftWithNonageMode {
    if (self.noTimeLeftWithNonageModeCallback) {
        self.noTimeLeftWithNonageModeCallback(self.client);
    }
}

@end
