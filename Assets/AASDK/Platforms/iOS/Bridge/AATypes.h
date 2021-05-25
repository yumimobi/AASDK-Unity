typedef const void *AATypeRef;

#pragma mark - AANotification
typedef const void *AATypeNotificationClientRef;
typedef const void *AATypeNotificationRef;
#pragma mark - AANotification callback
// 隐私弹框已经展示
typedef void (*AAPrivacyPolicyViewControllerHasBeenShownCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 用户同意隐私政策
typedef void (*AAUserAgreesToPrivacyPolicyCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 开始展示用户登录界面
typedef void (*AALoginViewControllerHasBeenShownCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 登录界面消失
typedef void (*AALoginViewControllerHasBeenDismissedCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 登录成功
typedef void (*AALoginSuccessCallback)(AATypeNotificationClientRef *aaNotificationClient, const char *zplayKey);
// 登录失败
typedef void (*AALoginFailCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 注销登录
typedef void (*AALoginOutSuccessfullCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 实名认证界面已经展示
typedef void (*AAUserAuthVcHasBeenShownCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 实名认证成功
typedef void (*AAUserAuthSuccessCallback)(AATypeNotificationClientRef *aaNotificationClient);
// warning vc已展示
typedef void (*AAWarningVcHasBeenShownCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 用户支付失败时，在提示界面点击登录
typedef void (*AAUserClickLoginButtonInPaymentWarningVcCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 用户游戏时长不足时，在提示界面点击登录
typedef void (*AAUserClickLoginButtonInNoTimeLeftWarningVcCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 用户在提示界面点击退出游戏
typedef void (*AAUserClickLoginOutButtonCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 用户在提示界面点击确定
typedef void (*AAUserClickConfirmButtonCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 不可支付
typedef void (*AAPaymentIsRestrictedCallback)(AATypeNotificationClientRef *aaNotificationClient);
// 可以支付
typedef void (*AAPaymentUnlimitedCallback)(AATypeNotificationClientRef *aaNotificationClient);
