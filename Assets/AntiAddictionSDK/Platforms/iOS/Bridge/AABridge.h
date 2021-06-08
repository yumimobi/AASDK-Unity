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
// 实名认证失败(用户点击暂不认证)
@property(nonatomic, assign) AAUserAuthFailCallback userAuthFailCallback;
// 实名认证失败(用户点击退出游戏)
@property(nonatomic, assign) AAUserAuthFailWithForceExitCallback userAuthFailWithForceExitCallback;
// 游客时间已用尽
@property(nonatomic, assign) AANoTimeLeftWithTouristsModeCallback noTimeLeftWithTouristsModeCallback;
// 未成年时间已用尽
@property(nonatomic, assign) AANoTimeLeftWithNonageModeCallback noTimeLeftWithNonageModeCallback;
// 每秒回调一次剩余时间
@property(nonatomic, assign) AALeftTimeOfCurrentUserCallback leftTimeOfCurrentUserCallback;

// 获取当前用户登录状态
// 0: 未登录
// 1: 已登录
- (int)getUserLoginStatus;

// 获取用户的认证身份
// 0: 未认证
// 1：已认证
- (int)getUserAuthenticationStatus;

// 获取用户年龄状态
// 0: 未登录
// 1: 成年人
// 2: 未成年人
- (int)getUserAgeGroup;

// 展示实名认证控制器（用户可点击暂不认证）
- (void)presentRealNameAuthController;

// 展示实名认证控制器（用户可点击退出游戏）
- (void)presentRealNameAuthControllerWithForceExit;

// 展示已在线时长提示界面，游戏主动调用
- (void)presentAlertInfoController;

// 展示查看详情界面
- (void)presentCheckDetailInfoController;

// 展示消费限制窗口
- (void)presentCashLimitedController;

// 查询剩余时间
- (int)checkLeftTimeOfCurrentUser;

// 暂停计时器
- (void)stopTimerInUnity;

// 恢复计时器
- (void)resumeTimerInUnity;

@end
