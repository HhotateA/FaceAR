#ifndef IAuthenticateSession_h
#define IAuthenticateSession_h

#import <AuthenticationServices/AuthenticationServices.h>
#import <SafariServices/SafariServices.h>
#include <functional>

class IAuthenticateSession
{
public:
    virtual ~IAuthenticateSession()
    {
        
    }
    virtual void start() = 0;
};

@interface SafariViewControllerDelegate : NSObject <SFSafariViewControllerDelegate>
@property (nonatomic) std::function<void()> OnSafariClosed;
@property (nonatomic) std::function<void()> OnSafariAuthorized;
@end

class AuthenticateiOS10 : public IAuthenticateSession
{
public:
    AuthenticateiOS10(NSURL* url);
    virtual ~AuthenticateiOS10();
    virtual void start();
private:
    SafariViewControllerDelegate* delegator;
    SFSafariViewController* safariVC;
};

class AuthenticateiOS11 : public IAuthenticateSession
{
public:
    API_AVAILABLE(ios(11.0))
    AuthenticateiOS11(NSURL* url, NSString* urlSchema);
    
    API_AVAILABLE(ios(11.0))
    virtual ~AuthenticateiOS11();
    
    API_AVAILABLE(ios(11.0))
    virtual void start();
private:
    API_AVAILABLE(ios(11.0)) SFAuthenticationSession* session;
};

#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 120000

class AuthenticateiOS12 : public IAuthenticateSession
{
public:
    API_AVAILABLE(ios(12.0))
    AuthenticateiOS12(NSURL* url, NSString* urlScheme);
    
    API_AVAILABLE(ios(12.0))
    virtual ~AuthenticateiOS12();
    
    API_AVAILABLE(ios(12.0))
    virtual void start();
private:
    API_AVAILABLE(ios(12.0)) ASWebAuthenticationSession* session;
};

#endif

#endif
