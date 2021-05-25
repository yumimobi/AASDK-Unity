#import <Foundation/Foundation.h>
#import <AAManager/AAManager.h>
#import "AATypes.h"

@interface AABridge : NSObject
- (instancetype)initSDKWithClientReference:(AATypeClientRef*)aaClientRef;

@property(nonatomic, assign) AATypeClientRef *client;
@property(nonatomic, strong) AAManager *manager;
// 登录成功
@property(nonatomic, assign) AALoginSuccessCallback loginSuccessCallback;
// 登录失败
@property(nonatomic, assign) AALoginFailCallback loginFailCallback;
// 实名认证成功
@property(nonatomic, assign) AAUserAuthSuccessCallback userAuthSuccessCallback;
// 实名认证失败
@property(nonatomic, assign) AAUserAuthFailCallback userAuthFailCallback;
// 游客时间已用尽
@property(nonatomic, assign) AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback;
// 未成年时间已用尽
@property(nonatomic, assign) AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback;

// 获取当前用户登录状态
// 0: 未登录
// 1: 已登录
- (int)getUserLoginStatus;
// 获取用户的认证身份
// 0: 未认证
// 1：已认证
- (int)getUserAuthenticationStatus;
// 展示实名认证控制器
- (void)presentRealNameAuthController;
// 查询剩余时间
- (int)checkLeftTimeOfCurrentUser;

@end
