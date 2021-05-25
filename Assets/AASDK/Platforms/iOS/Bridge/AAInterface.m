#import "AATypes.h"
#import "AAObjectCache.h"
#import "AANotificationBridge.h"

static NSString *AAStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }
#pragma mark - AANotification method

AATypeNotificationRef AACreateNotification(AATypeNotificationClientRef *notificationClient) {
    AANotificationBridge *notification = [[AANotificationBridge alloc] initWithNotificationClientReference:notificationClient];
    AAObjectCache *cache = [AAObjectCache sharedInstance];
    [cache.references setObject:notification forKey:[notification aa_referenceKey]];
    return (__bridge AATypeNotificationRef)notification;
}

void AASetNotificationCallbacks(
        AATypeNotificationClientRef notification,
        // 隐私弹框已经展示
        AAPrivacyPolicyViewControllerHasBeenShownCallback privacyPolicyViewControllerHasBeenShownCallback,
        // 用户同意隐私政策
        AAUserAgreesToPrivacyPolicyCallback userAgreesToPrivacyPolicyCallback,
        // 开始展示用户登录界面
        AALoginViewControllerHasBeenShownCallback loginViewControllerHasBeenShownCallback,
        // 登录界面消失
        AALoginViewControllerHasBeenDismissedCallback loginViewControllerHasBeenDismissedCallback,
        // 登录成功
        AALoginSuccessCallback loginSuccessCallback,
        // 登录失败
        AALoginFailCallback loginFailCallback,
        // 实名认证界面已经展示
        AAUserAuthVcHasBeenShownCallback userAuthVcHasBeenShownCallback,
        // 实名认证成功
        AAUserAuthSuccessCallback userAuthSuccessCallback,
        // warning vc已展示
        AAWarningVcHasBeenShownCallback warningVcHasBeenShownCallback,
        // 用户支付失败时，在提示界面点击登录
        AAUserClickLoginButtonInPaymentWarningVcCallback userClickLoginButtonInPaymentWarningVcCallback,
        // 用户游戏时长不足时，在提示界面点击登录
        AAUserClickLoginButtonInNoTimeLeftWarningVcCallback userClickLoginButtonInNoTimeLeftWarningVcCallback,
        // 用户在提示界面点击退出游戏
        AAUserClickLoginOutButtonCallback userClickLoginOutButtonCallback,
        // 用户在提示界面点击确定
        AAUserClickConfirmButtonCallback userClickConfirmButtonCallback,
        // 注销用户
        AALoginOutSuccessfullCallback loginOutSuccessfullCallback,
        // 不可支付
        AAPaymentIsRestrictedCallback paymentIsRestrictedCallback,
        // 可以支付
        AAPaymentUnlimitedCallback paymentUnlimitedCallback) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    internalNotification.privacyPolicyViewControllerHasBeenShownCallback = privacyPolicyViewControllerHasBeenShownCallback;
    internalNotification.userAgreesToPrivacyPolicyCallback = userAgreesToPrivacyPolicyCallback;
    internalNotification.loginViewControllerHasBeenShownCallback = loginViewControllerHasBeenShownCallback;
    internalNotification.loginViewControllerHasBeenDismissedCallback = loginViewControllerHasBeenDismissedCallback;
    internalNotification.loginSuccessCallback = loginSuccessCallback;
    internalNotification.loginFailCallback = loginFailCallback;
    internalNotification.userAuthVcHasBeenShownCallback = userAuthVcHasBeenShownCallback;
    internalNotification.userAuthSuccessCallback = userAuthSuccessCallback;
    internalNotification.warningVcHasBeenShownCallback = warningVcHasBeenShownCallback;
    internalNotification.userClickLoginButtonInPaymentWarningVcCallback = userClickLoginButtonInPaymentWarningVcCallback;
    internalNotification.userClickLoginButtonInNoTimeLeftWarningVcCallback = userClickLoginButtonInNoTimeLeftWarningVcCallback;
    internalNotification.userClickLoginOutButtonCallback = userClickLoginOutButtonCallback;
    internalNotification.userClickConfirmButtonCallback = userClickConfirmButtonCallback;
    internalNotification.loginOutSuccessfullCallback = loginOutSuccessfullCallback;
    internalNotification.paymentIsRestrictedCallback = paymentIsRestrictedCallback;
    internalNotification.paymentIsRestrictedCallback = paymentIsRestrictedCallback;
}

int getUserLoginStatus(AATypeNotificationRef notification) {    
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    return [internalNotification getUserLoginStatus];
}

int getUserAuthenticationIdentity(AATypeNotificationRef notification) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    return [internalNotification getUserAuthenticationIdentity];
}

void showPrivacyPolicyView(AATypeNotificationRef notification) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification showPrivacyPolicyView];
}

void showLoginViewController(AATypeNotificationRef notification) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification showLoginViewController];
}

void showUserAuthenticationViewController(AATypeNotificationRef notification) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification showUserAuthenticationViewController];
}

void loginWithUserNameAndPassword(AATypeNotificationRef notification, const char *userName, const char *password) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification loginWithUserName:AAStringFromUTF8String(userName) password:AAStringFromUTF8String(password)];
}

void loginWithThirdPartyPlatform(AATypeNotificationRef notification, const char *token, const char *otherID, const char *platformName) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification loginWithPlatformToken:AAStringFromUTF8String(token) otherID:AAStringFromUTF8String(otherID) platformName:AAStringFromUTF8String(platformName)];
}

void loginWithZplayID(AATypeNotificationRef notification, const char *zplayID) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification loginWithZplayID:AAStringFromUTF8String(zplayID)];
}

void loginOut(AATypeNotificationRef notification) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification loginOut];
}

void checkNumberLimitBeforePayment(AATypeNotificationRef notification, int payNumber) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification checkNumberLimitBeforePayment:payNumber];
}

void reportNumberAfterPayment(AATypeNotificationRef notification, int payNumber) {
    AANotificationBridge *internalNotification = (__bridge AANotificationBridge *)notification;
    [internalNotification reportNumberAfterPayment:payNumber];
}

#pragma mark - Other methods
void AARelease(AATypeRef ref) {
    if (ref) {
        AAObjectCache *cache = [AAObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref aa_referenceKey]];
    }
}
