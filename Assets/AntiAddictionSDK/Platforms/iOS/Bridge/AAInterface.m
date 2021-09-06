#import "AATypes.h"
#import "AAObjectCache.h"
#import "AABridge.h"

static NSString *AAStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

#pragma mark - method
AATypeRef AACreateManager(AATypeClientRef *client) {
    AABridge *manager = [[AABridge alloc] initSDKWithClientReference:client];
    AAObjectCache *cache = [AAObjectCache sharedInstance];
    [cache.references setObject:manager forKey:[manager aa_referenceKey]];
    return (__bridge AATypeRef)manager;
}

void AASetManagerCallbacks(
        AATypeClientRef manager,
        // 登录成功
        AALoginSuccessCallback loginSuccessCallback,
        // 登录失败
        AALoginFailCallback loginFailCallback,
        // 实名认证成功
        AAUserAuthSuccessCallback userAuthSuccessCallback,
        // 实名认证失败(用户点击暂不认证)
        AAUserAuthFailCallback userAuthFailCallback,
        // 实名认证失败(用户点击退出游戏)
        AAUserAuthFailWithForceExitCallback userAuthFailWithForceExitCallback,
        // 游客时间已用尽
        AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback,
        // 未成年时间已用尽
        AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback,
        // 每秒回调一次当前用户剩余时间
        AALeftTimeOfCurrentUserCallback leftTimeOfCurrentUserCallback,
        //查询用户分组，1=新用户，2=老用户,查询失败返回null
        AACheckNewUseSuccessCallback checkNewUseSuccessCallback) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    internalManager.loginSuccessCallback = loginSuccessCallback;
    internalManager.loginFailCallback = loginFailCallback;
    internalManager.userAuthSuccessCallback = userAuthSuccessCallback;
    internalManager.userAuthFailWithForceExitCallback = userAuthFailWithForceExitCallback;
    internalManager.userAuthFailCallback = userAuthFailCallback;
    internalManager.noTimeLeftWithTouristsModeCallback = noTimeLeftWithTouristsModeCallback;
    internalManager.noTimeLeftWithNonageModeCallback = noTimeLeftWithNonageModeCallback;
    internalManager.leftTimeOfCurrentUserCallback = leftTimeOfCurrentUserCallback;
    internalManager.checkNewUseSuccessCallback = checkNewUseSuccessCallback;}

int getUserLoginStatus(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getUserLoginStatus];
}

int getUserAuthenticationStatus(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getUserAuthenticationStatus];
}

int getUserAgeGroup(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getUserAgeGroup];
}

void presentRealNameAuthController(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentRealNameAuthController];
}

void presentRealNameAuthControllerWithForceExit(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentRealNameAuthControllerWithForceExit];
}

void presentAlertInfoController(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentAlertInfoController];
}

void presentCheckDetailInfoController(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentCheckDetailInfoController];
}

void presentCashLimitedController(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentCashLimitedController];
}

void checkLeftTimeOfCurrentUser(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager checkLeftTimeOfCurrentUser];
}

void stopTimerInUnity(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager stopTimerInUnity];
}

void resumeTimerInUnity(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager resumeTimerInUnity];
}

void checkNewUserInUnity(AATypeRef manager,const char zplayId){
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager checkNewUserInUnity:zplayId];
}

int getOldUserInUnity(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getOldUserInUnity];
}

void updateDataReportInUnity(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager updateDataReportInUnity];
}

#pragma mark - Other methods
void AARelease(AATypeRef ref) {
    if (ref) {
        AAObjectCache *cache = [AAObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref aa_referenceKey]];
    }
}
