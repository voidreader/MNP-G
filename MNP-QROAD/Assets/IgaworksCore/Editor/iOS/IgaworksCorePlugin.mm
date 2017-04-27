//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "IgaworksCorePlugin.h"


UIViewController *UnityGetGLViewController();

static IgaworksCorePlugin *_sharedInstance = nil; //To make IgaworksCorePlugin Singleton

@implementation IgaworksCorePlugin

@synthesize callbackHandlerName = _callbackHandlerName;


+ (void)initialize
{
	if (self == [IgaworksCorePlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (IgaworksCorePlugin *)sharedIgaworksCorePlugin
{
	return _sharedInstance;
}

- (id)init
{
	self = [super init];
    
    if (self)
    {
        
    }
	return self;
}



- (void)setIgaworksCoreDelegate
{
    [IgaworksCore shared].delegate = self;
}

- (void)setIgaworksADClientRewardDelegate
{
    [IgaworksCore shared].clientRewardDelegate = self;
    
    NSLog(@"[IgaworksCore shared].clientRewardDelegate : %@", [IgaworksCore shared].clientRewardDelegate);
}


#pragma mark - IgaworksADClientRewardDelegate
- (void)onRewardRequestResult:(BOOL)isSuccess withMessage:(NSString *)message itemName:(NSString *)itemName itemKey:(NSString *)itemKey campaignName:(NSString *)campaignName campaignKey:(NSString *)campaignKey rewardKey:(NSString *)rewardKey quantity:(NSInteger)quantity
{
    NSString *result = [NSString stringWithFormat: @"isSuccess=%d&message=%@&itemName=%@&itemKey=%@&campaignName=%@&campaignKey=%@&rewardKey=%@&quantity=%ld", isSuccess, message, itemName, itemKey, campaignName, campaignKey, rewardKey, (long)quantity];
    
    NSLog(@"result : %@", result);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "OnRewardRequestResult", [result UTF8String]);
}


- (void)onRewardCompleteResult:(BOOL)isSuccess withMessage:(NSString *)message resultCode:(NSInteger)resultCode withCompletedRewardKey:(NSString *)completedRewardKey
{
    NSString *completeResult = [NSString stringWithFormat: @"isSuccess=%d&message=%@&resultCode=%ld&completedRewardKey=%@", isSuccess, message, (long)resultCode, completedRewardKey];
    
    NSLog(@"completeResult : %@", completeResult);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "OnRewardCompleteResult", [completeResult UTF8String]);
}

#pragma mark - IgaworksCoreDelegate
- (void)didSaveConversionKey:(NSInteger)conversionKey subReferralKey:(NSString *)subReferralKey
{
    NSString *conversionInfo = [NSString stringWithFormat: @"%ld,%@", (long)conversionKey, subReferralKey];
    
    NSLog(@"conversionInfo : %@", conversionInfo);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DidSaveConversionKey", [conversionInfo UTF8String]);
}

- (void)didReceiveDeeplink:(NSString *)deepLink
{
    NSLog(@"- (void)didReceiveDeeplink:(NSString *)deepLink : %@", deepLink);
    
    UnitySendMessage([_callbackHandlerName UTF8String], "DidReceiveDeeplink", [deepLink UTF8String]);
}


// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    
	void _SetCallbackHandler(const char* handlerName)
	{
		[[IgaworksCorePlugin sharedIgaworksCorePlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
		NSLog(@"callbackHandlerName: %@", [[IgaworksCorePlugin sharedIgaworksCorePlugin] callbackHandlerName]);
	}
	
    
    void _IgaworksCoreWithAppKey(const char* appKey, const char* hashKey)
    {
        [IgaworksCore igaworksCoreWithAppKey:[NSString stringWithUTF8String:appKey] andHashKey:[NSString stringWithUTF8String:hashKey]];
        
        
//        NSLog(@"appKey : %@, hashKey : %@, isUseIgaworksRewardServer : %d", [NSString stringWithUTF8String:appKey], [NSString stringWithUTF8String:hashKey], isUseIgaworksRewardServer);
        
        // adbrix start
        [IgaworksCore start];
    }
    
    void _SetUseIgaworksRewardServer(bool isUseIgaworksRewardServer)
    {
        NSLog(@"_SetUseIgaworksRewardServer : %d", isUseIgaworksRewardServer);
        
        if (isUseIgaworksRewardServer)
        {
            [IgaworksCore shared].useIgaworksRewardServer = YES;
            
            [[IgaworksCorePlugin sharedIgaworksCorePlugin] setIgaworksADClientRewardDelegate];
        }
    }
    
    
    void _SetLogLevel(IgaworksCoreLogLevel logLevel)
    {
    
        [IgaworksCore setLogLevel:logLevel];
        
    }
    
    void _SetAge(int age)
    {
        [IgaworksCore setAge:age];
    }
    
    void _SetGender(Gender gender)
    {
        [IgaworksCore setGender:gender];
    }
    
    void _SetUserId(const char* userId)
    {
        [IgaworksCore setUserId:[NSString stringWithUTF8String:userId]];
    }
    
    void _setAppleAdvertisingIdentifier(const char* appleAdvertisingIdentifier, bool isAppleAdvertisingTrackingEnabled)
    {
        [IgaworksCore setAppleAdvertisingIdentifier:[NSString stringWithUTF8String:appleAdvertisingIdentifier] isAppleAdvertisingTrackingEnabled:isAppleAdvertisingTrackingEnabled];
    }
    
    void _SetIgaworksCoreDelegate()
    {
        [[IgaworksCorePlugin sharedIgaworksCorePlugin] setIgaworksCoreDelegate];
    }
    
    void _SetReferralUrl(const char* url)
    {
        [IgaworksCore setReferralUrl:[NSURL URLWithString:[NSString stringWithUTF8String:url]]];
    }
    
    void _SetReferralUrlForFacebook(const char* url)
    {
        [IgaworksCore setReferralUrlForFacebook:[NSURL URLWithString:[NSString stringWithUTF8String:url]]];
    }
    
}

@end

