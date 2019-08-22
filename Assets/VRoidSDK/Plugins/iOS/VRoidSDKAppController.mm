#import "UnityAppController.h"

void UnitySendMessage(const char* objName, const char* methodName, const char* param);

@interface VRoidSDKAppController : UnityAppController
@end

@implementation VRoidSDKAppController
// iOS9以上のスキーム起動処理.
#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 90000

-(BOOL) application:(nonnull UIApplication *)application openURL:(nonnull NSURL *)url options:(nonnull NSDictionary<NSString *,id> *)options {
    NSString* message = [url absoluteString];
    UnitySendMessage("BrowserAuthorize", "OnOpenUrl", [message UTF8String]);
    
    [[NSNotificationCenter defaultCenter] postNotificationName:@"kSafariViewControllerNotification" object:url];
    return YES;
}

#endif

// 通常のスキーム起動処理.
- (BOOL)application:(nonnull UIApplication *)application openURL:(nonnull NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation {
    NSString* message = [url absoluteString];
    UnitySendMessage("BrowserAuthorize", "OnOpenUrl", [message UTF8String]);
    
    [[NSNotificationCenter defaultCenter] postNotificationName:@"kSafariViewControllerNotification" object:url];
    return YES;
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(VRoidSDKAppController)
