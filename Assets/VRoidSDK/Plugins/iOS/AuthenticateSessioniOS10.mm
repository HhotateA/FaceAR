#import <Foundation/Foundation.h>
#include "IAuthenticateSession.h"
#include <functional>

@implementation SafariViewControllerDelegate

- (void) safariViewControllerDidFinish:(SFSafariViewController *)controller
{
    self.OnSafariClosed();
}

- (void) onSafariLoginSuccess:(NSObject*) url
{
    self.OnSafariAuthorized();
}

@end

AuthenticateiOS10::AuthenticateiOS10(NSURL* url)
{
    delegator = [[SafariViewControllerDelegate alloc] init];
    delegator.OnSafariClosed = [] {
        UnitySendMessage("BrowserAuthorize", "OnCancelAuthorize", "Authorize Error");
    };
    delegator.OnSafariAuthorized = [this] {
        [safariVC dismissViewControllerAnimated:YES completion:^{
            [[NSNotificationCenter defaultCenter] removeObserver:delegator];
        }];
    };
    safariVC = [[SFSafariViewController alloc] initWithURL:url];
    safariVC.delegate = delegator;
}

AuthenticateiOS10::~AuthenticateiOS10()
{
    delegator = nil;
    safariVC = nil;
}

void AuthenticateiOS10::start()
{
    UIViewController* uvc = UnityGetGLViewController();
    [[NSNotificationCenter defaultCenter] addObserver:delegator selector:@selector(onSafariLoginSuccess:) name:@"kSafariViewControllerNotification" object:nil];
    [uvc presentViewController:safariVC animated:YES completion:nil];
}
