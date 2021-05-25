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
        // 实名认证失败
        AAUserAuthFailCallback userAuthFailCallback,
        // 游客时间已用尽
        AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback,
        // 未成年时间已用尽
        AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    internalManager.loginSuccessCallback = loginSuccessCallback;
    internalManager.loginFailCallback = loginFailCallback;
    internalManager.userAuthSuccessCallback = userAuthSuccessCallback;
    internalManager.userAuthFailCallback = userAuthFailCallback;
    internalManager.noTimeLeftWithTouristsModeCallback = noTimeLeftWithTouristsModeCallback;
    internalManager.noTimeLeftWithNonageModeCallback = noTimeLeftWithNonageModeCallback;
}

int getUserLoginStatus(AATypeRef manager) {    
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getUserLoginStatus];
}

int getUserAuthenticationStatus(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    return [internalManager getUserAuthenticationStatus];
}

void presentRealNameAuthController(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager presentRealNameAuthController];
}

void checkLeftTimeOfCurrentUser(AATypeRef manager) {
    AABridge *internalManager = (__bridge AABridge *)manager;
    [internalManager checkLeftTimeOfCurrentUser];
}

#pragma mark - Other methods
void AARelease(AATypeRef ref) {
    if (ref) {
        AAObjectCache *cache = [AAObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref aa_referenceKey]];
    }
}
