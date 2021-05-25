#import "AANotificationBridge.h"
#import <AntiAddictionSystem/AAPrivacyPolicyViewController.h>
#import <AntiAddictionSystem/AALoginViewController.h>
#import <AntiAddictionSystem/AALogin.h>
#import <AntiAddictionSystem/AAUserAuthenticationViewController.h>
#import <AntiAddictionSystem/AAPayNumberReport.h>

@interface AANotificationBridge ()<AANotificationDelegate>
@property (nonatomic) AAPrivacyPolicyViewController *privacyPolicyVc;
@property (nonatomic) AALoginViewController *loginVc;
@property (nonatomic) AALogin *loginManager;
@property (nonatomic) AAUserAuthenticationViewController *authVc;
@property (nonatomic) AAPayNumberReport *payReport;

@end

@implementation AANotificationBridge

- (instancetype)initWithNotificationClientReference:(AATypeNotificationClientRef*)aaNotificationClientRef {
    if (self = [super init]) {
        _notificationClient = aaNotificationClientRef;
        _notification = [AANotification shared];
        _notification.delegate = self;
    }
    return self;
}

// 获取当前用户登录状态
// 0: 未知
// 1: 游客
// 2: 正式用户
- (int)getUserLoginStatus {
    int loginStatus = [NSNumber numberWithBool:[self.notification getUserLoginStatus]].intValue;
    return loginStatus;
}

// 获取用户的认证身份
// 0: 未知
// 1：已成年
// 2: 未成年
- (int)getUserAuthenticationIdentity {
    int userAuthStatus = [NSNumber numberWithBool:[self.notification getUserAuthenticationIdentity]].intValue;
    return userAuthStatus;
}

// 展示隐私政策弹框
- (void)showPrivacyPolicyView {
    [self.privacyPolicyVc showPrivacyPolicyViewWithRootViewController:UnityGetGLViewController()];
}

// 展示登录界面
- (void)showLoginViewController {
    [self.loginVc showLoginViewControllerWith:UnityGetGLViewController()];
}

// 展示实名认证界面
// 登录后先检测实名认证状态，如已经实名认证，则不展示此界面
- (void)showUserAuthenticationViewController {
    [self.authVc showUserAuthenticationViewControllerWith:UnityGetGLViewController()];
}

// 使用帐号密码注册
// username: 用户帐号
// password: 用户密码
- (void)loginWithUserName:(NSString *)username
                 password:(NSString *)password {
    [self.loginManager loginWithUserName:username password:password success:^(NSString * _Nonnull zplayID) {
        
    } failure:^(NSError * _Nonnull error) {
        
    }];
}

// 使用第三方平台登录SDK
// token: 如使用第三方登录（如微信），请使用三方登录SDK返回的唯一ID
// otherID: 如果三方登录平台d返回除token之外的ID，请将此ID赋值给此参数
// platformName : 三方平台名称（请联系产品获取）
- (void)loginWithPlatformToken:(NSString *)token
                       otherID:(NSString *)otherID
                  platformName:(NSString *)platformName {
    [self.loginManager loginWithPlatformToken:token otherID:otherID platformName:platformName];
}

// 使用Zplay登录SDK
- (void)loginWithZplayID:(NSString *)zplayID {
    [self.loginManager loginWithZplayID:zplayID];
}

// 注销用户
- (void)loginOut {
    [self.loginManager loginOut];
}

// 支付前检查用户是否被限额（成年人不受限制）
// paynumber: 付费金额，单位分
- (void)checkNumberLimitBeforePayment:(int)payNumber {
    [self.payReport checkNumberLimitBeforePayment:payNumber];
}

// 支付成功后上报玩家支付金额
// payNumber: 付费金额，单位分
- (void)reportNumberAfterPayment:(int)payNumber {
    [self.payReport reportNumberAfterPayment:payNumber];
}

#pragma mark - notification delegate
// 隐私弹框已经展示
- (void)privacyPolicyViewControllerHasBeenShown {
    if(self.privacyPolicyViewControllerHasBeenShownCallback) {
        self.privacyPolicyViewControllerHasBeenShownCallback(self.notificationClient);
    }
}
// 用户同意隐私政策
- (void)userAgreesToPrivacyPolicy {
    if(self.userAgreesToPrivacyPolicyCallback) {
        self.userAgreesToPrivacyPolicyCallback(self.notificationClient);
    }
}
// 开始展示用户登录界面
- (void)loginViewControllerHasBeenShown {
    if(self.loginViewControllerHasBeenShownCallback) {
        self.loginViewControllerHasBeenShownCallback(self.notificationClient);
    }
}
// 登录界面消失
- (void)loginViewControllerHasBeenDismissed {
    if(self.loginViewControllerHasBeenDismissedCallback) {
        self.loginViewControllerHasBeenDismissedCallback(self.notificationClient);
    }
}
// 登录成功
- (void)loginSuccessWith:(NSString *)zplayKey {
    if(self.loginSuccessCallback) {
        self.loginSuccessCallback(self.notificationClient, [zplayKey cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}
// 登录失败
- (void)loginFail {
    if(self.loginFailCallback) {
        self.loginFailCallback(self.notificationClient);
    }
}
// 注销登录
- (void)loginOutSuccessfull {
    if(self.loginOutSuccessfullCallback) {
        self.loginOutSuccessfullCallback(self.notificationClient);
    }
}
// 实名认证界面已经展示
- (void)userAuthVcHasBeenShown {
    if(self.userAuthVcHasBeenShownCallback) {
        self.userAuthVcHasBeenShownCallback(self.notificationClient);
    }
}
// 实名认证成功
- (void)userAuthSuccessWithRemainTime:(NSNumber *)remainTime {
    if(self.userAuthSuccessCallback) {
        self.userAuthSuccessCallback(self.notificationClient);
    }
}
// warning vc已展示
- (void)warningVcHasBeenShown {
    if(self.warningVcHasBeenShownCallback) {
        self.warningVcHasBeenShownCallback(self.notificationClient);
    }
}
// 用户支付失败时，在提示界面点击登录
- (void)userClickLoginButtonInPaymentWarningVc {
    if(self.userClickLoginButtonInPaymentWarningVcCallback) {
        self.userClickLoginButtonInPaymentWarningVcCallback(self.notificationClient);
    }
}
// 用户游戏时长不足时，在提示界面点击登录
- (void)userClickLoginButtonInNoTimeLeftWarningVc {
    if(self.userClickLoginButtonInNoTimeLeftWarningVcCallback) {
        self.userClickLoginButtonInNoTimeLeftWarningVcCallback(self.notificationClient);
    }
}
// 用户在提示界面点击退出游戏
- (void)userClickLoginOutButton {
    if(self.userClickLoginOutButtonCallback) {
        self.userClickLoginOutButtonCallback(self.notificationClient);
    }
}
// 用户在提示界面点击确定
- (void)userClickConfirmButton {
    if(self.userClickConfirmButtonCallback) {
        self.userClickConfirmButtonCallback(self.notificationClient);
    }
}
// 不可支付
- (void)paymentIsRestricted {
    if(self.paymentIsRestrictedCallback) {
        self.paymentIsRestrictedCallback(self.notificationClient);
    }
}
// 可以支付
- (void)paymentUnlimited {
    if(self.paymentUnlimitedCallback) {
        self.paymentUnlimitedCallback(self.notificationClient);
    }
}

#pragma mark - lazy load
- (AAPrivacyPolicyViewController *)privacyPolicyVc {
    if (!_privacyPolicyVc) {
        _privacyPolicyVc = [[AAPrivacyPolicyViewController alloc] init];
    }
    return _privacyPolicyVc;
}

- (AALoginViewController *)loginVc {
    if (!_loginVc) {
        _loginVc = [[AALoginViewController alloc] init];
    }
    return _loginVc;
}

- (AALogin *)loginManager {
    if (!_loginManager) {
        _loginManager = [[AALogin alloc] init];
    }
    return _loginManager;
}

- (AAUserAuthenticationViewController *)authVc {
    if (!_authVc) {
        _authVc = [[AAUserAuthenticationViewController alloc] init];
    }
    return _authVc;
}

- (AAPayNumberReport *)payReport {
    if (!_payReport) {
        _payReport = [[AAPayNumberReport alloc] init];
    }
    return _payReport;
}

@end
