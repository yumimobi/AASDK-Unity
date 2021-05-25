#import <Foundation/Foundation.h>
#import <AntiAddictionSystem/AANotification.h>
#import "AATypes.h"

@interface AANotificationBridge : NSObject

- (instancetype)initWithNotificationClientReference:(AATypeNotificationClientRef*)aaNotificationClientRef;

@property(nonatomic, assign) AATypeNotificationClientRef *notificationClient;
@property(nonatomic, strong) AANotification *notification;
// 隐私弹框已经展示
@property(nonatomic, assign) AAPrivacyPolicyViewControllerHasBeenShownCallback privacyPolicyViewControllerHasBeenShownCallback;
// 用户同意隐私政策
@property(nonatomic, assign) AAUserAgreesToPrivacyPolicyCallback userAgreesToPrivacyPolicyCallback;
// 开始展示用户登录界面
@property(nonatomic, assign) AALoginViewControllerHasBeenShownCallback loginViewControllerHasBeenShownCallback;
// 登录界面消失
@property(nonatomic, assign) AALoginViewControllerHasBeenDismissedCallback loginViewControllerHasBeenDismissedCallback;
// 登录成功
@property(nonatomic, assign) AALoginSuccessCallback loginSuccessCallback;
// 登录失败
@property(nonatomic, assign) AALoginFailCallback loginFailCallback;
// 注销登录
@property(nonatomic, assign) AALoginOutSuccessfullCallback loginOutSuccessfullCallback;
// 实名认证界面已经展示
@property(nonatomic, assign) AAUserAuthVcHasBeenShownCallback userAuthVcHasBeenShownCallback;
// 实名认证成功
@property(nonatomic, assign) AAUserAuthSuccessCallback userAuthSuccessCallback;
// warning vc已展示
@property(nonatomic, assign) AAWarningVcHasBeenShownCallback warningVcHasBeenShownCallback;
// 用户支付失败时，在提示界面点击登录
@property(nonatomic, assign) AAUserClickLoginButtonInPaymentWarningVcCallback userClickLoginButtonInPaymentWarningVcCallback;
// 用户游戏时长不足时，在提示界面点击登录
@property(nonatomic, assign) AAUserClickLoginButtonInNoTimeLeftWarningVcCallback userClickLoginButtonInNoTimeLeftWarningVcCallback;
// 用户在提示界面点击退出游戏
@property(nonatomic, assign) AAUserClickLoginOutButtonCallback userClickLoginOutButtonCallback;
// 用户在提示界面点击确定
@property(nonatomic, assign) AAUserClickConfirmButtonCallback userClickConfirmButtonCallback;
// 不可支付
@property(nonatomic, assign) AAPaymentIsRestrictedCallback paymentIsRestrictedCallback;
// 可以支付
@property(nonatomic, assign) AAPaymentUnlimitedCallback paymentUnlimitedCallback;

// 获取当前用户登录状态
// 0: 未知
// 1: 游客
// 2: 正式用户
- (int)getUserLoginStatus;

// 获取用户的认证身份
// 0: 未知
// 1：已成年
// 2: 未成年
- (int)getUserAuthenticationIdentity;

// 展示隐私政策弹框
- (void)showPrivacyPolicyView;

// 展示登录界面
- (void)showLoginViewController;

// 展示实名认证界面
// 登录后先检测实名认证状态，如已经实名认证，则不展示此界面
- (void)showUserAuthenticationViewController;

// 使用帐号密码注册
// username: 用户帐号
// password: 用户密码
- (void)loginWithUserName:(NSString *)username
                 password:(NSString *)password;

// 使用第三方平台登录SDK
// token: 如使用第三方登录（如微信），请使用三方登录SDK返回的唯一ID
// otherID: 如果三方登录平台d返回除token之外的ID，请将此ID赋值给此参数
// platformName : 三方平台名称（请联系产品获取）
- (void)loginWithPlatformToken:(NSString *)token
                       otherID:(NSString *)otherID
                  platformName:(NSString *)platformName;

// 使用Zplay登录SDK
- (void)loginWithZplayID:(NSString *)zplayID;

// 注销用户
- (void)loginOut;

// 支付前检查用户是否被限额（成年人不受限制）
// paynumber: 付费金额，单位分
- (void)checkNumberLimitBeforePayment:(int)payNumber;

// 支付成功后上报玩家支付金额
// payNumber: 付费金额，单位分
- (void)reportNumberAfterPayment:(int)payNumber;

@end
