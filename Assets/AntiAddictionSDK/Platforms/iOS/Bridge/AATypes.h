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
// 实名认证失败(用户点击暂不认证)
typedef void (*AAUserAuthFailCallback)(AATypeClientRef *aaClient);
// 实名认证失败(用户点击退出游戏)
typedef void (*AAUserAuthFailWithForceExitCallback)(AATypeClientRef *aaClient);
// 游客时间已用尽
typedef void (*AANoTimeLeftWithTouristsModeCallback)(AATypeClientRef *aaClient);
// 未成年时间已用尽
typedef void (*AANoTimeLeftWithNonageModeCallback)(AATypeClientRef *aaClient);
// 每秒回调一次当前用户剩余时间
typedef void (*AALeftTimeOfCurrentUserCallback)(AATypeClientRef *aaClient, int leftTime);
//查询用户分组，1=新用户，2=老用户,查询失败返回null
typedef void (*AACheckNewUseSuccessCallback)(AATypeClientRef *aaClient, const char *userGroup);
