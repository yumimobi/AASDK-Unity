typedef const void *AATypeRef;

#pragma mark - AAManager
typedef const void *AATypeClientRef;
typedef const void *AATypeRef;

#pragma mark - callback
// 登录成功
typedef void (*AALoginSuccessCallback)(AATypeClientRef *aaClient, const char *touristID);
// 登录失败
typedef void (*AALoginFailCallback)(AATypeClientRef *aaClient);
// 实名认证成功
typedef void (*AAUserAuthSuccessCallback)(AATypeClientRef *aaClient);
// 实名认证失败
typedef void (*AAUserAuthFailCallback)(AATypeClientRef *aaClient);
// 游客时间已用尽
typedef void (*AANoTimeLeftWithTouristsModeCallback)(AATypeClientRef *aaClient);
// 未成年时间已用尽
typedef void (*AANoTimeLeftWithNonageModeCallback)(AATypeClientRef *aaClient);
