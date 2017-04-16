//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "LiveOpsPlugin.h"


UIViewController *UnityGetGLViewController();

static LiveOpsPlugin *_sharedInstance = nil; //To make IgaworksCorePlugin Singleton

@implementation LiveOpsPlugin

@synthesize callbackHandlerName = _callbackHandlerName;


+ (void)initialize
{
	if (self == [LiveOpsPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (LiveOpsPlugin *)sharedLiveOpsPlugin
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







#pragma mark - LiveOpsPopup
- (void)liveOpsPopupGetPopupsResponded
{
    UnitySendMessage([_callbackHandlerName UTF8String], "LiveOpsPopupGetPopupsResponded", "");
  
}


- (void)liveOpsPopupSetPopupLinkListenerCalled:(NSString *)popupSpaceKey customDataString:(NSString *)customDataString
{
    NSString *resultMessage = [NSString stringWithFormat: @"%@,%@", popupSpaceKey, customDataString];
    NSLog(@"resultMessage : %@", resultMessage);
  
    UnitySendMessage([_callbackHandlerName UTF8String], "LiveOpsPopupSetPopupLinkListenerCalled", [resultMessage UTF8String]);
}


- (void)liveOpsPopupSetPopupCloseListenerCalled:(NSString *)popupSpaceKey popupCampaignName:(NSString *)campaignName customDataString:(NSString *)customDataString remainPopupNumString:(NSString *)remainPopupNumStr
{
    NSString *resultMessage = [NSString stringWithFormat: @"%@,%@,%@,%@", popupSpaceKey, campaignName, customDataString, remainPopupNumStr];
    NSLog(@"resultMessage : %@", resultMessage);
  
    UnitySendMessage([_callbackHandlerName UTF8String], "LiveOpsPopupSetPopupCloseListenerCalled", [resultMessage UTF8String]);
}



extern "C" {
    
    void _LiveOpsSetCallbackHandler(const char* handlerName)
    {
        [[LiveOpsPlugin sharedLiveOpsPlugin] setCallbackHandlerName:[NSString stringWithUTF8String:handlerName]];
        NSLog(@"callbackHandlerName: %@", [[LiveOpsPlugin sharedLiveOpsPlugin] callbackHandlerName]);
    }

    void _LiveOpsInitPush()
    {
        [LiveOpsPush initPush];
    }
    
    void _LiveOpsSetRemotePushEnable(bool isEnabled)
    {
        [LiveOpsPush setRemotePushEnable:isEnabled];
        
        NSLog(@"_LiveOpsSetRemotePushEnable : %d", isEnabled);
    }
    
    void _LiveOpsRegisterLocalPushNotification(int intId, const char* date, const char* body, const char* button, const char* sound, int badgeNumber, const char* customPayload)
    {
        
        NSDate *toDate = nil;
        if(date != NULL)
        {
            NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
            [dateFormatter setDateFormat:@"yyyyMMddHHmmss"];
            
            toDate = [dateFormatter dateFromString:[NSString stringWithUTF8String:date]];
        }
        
        id customPayloadDict = nil;
        if(customPayload != NULL)
        {
            NSData *data = [[NSString stringWithUTF8String:customPayload] dataUsingEncoding:NSUTF8StringEncoding];
            NSError *error = nil;
            customPayloadDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&error];
        }
        
        
        NSLog(@"date : %@, customPayloadDict : %@", toDate, customPayloadDict);
        
        [LiveOpsPush registerLocalPushNotification:intId date:toDate body:body != NULL ? [NSString stringWithUTF8String:body] : nil button:button != NULL ? [NSString stringWithUTF8String:button] : nil soundName:sound != NULL ? [NSString stringWithUTF8String:sound] : nil badgeNumber:badgeNumber customPayload:customPayloadDict];
    }
    
    void _LiveOpsCancelLocalPush(int intId)
    {
        [LiveOpsPush cancelLocalPush:intId];
    }
    

    void _LiveOpsSetTargetingNumberData(int obj, const char* key)
    {
        NSLog(@"void _LiveOpsSetTargetingNumberData(int obj, const char* key) : obj : %d, key : %s", obj, key);
        
        [LiveOpsUser setTargetingData:[NSNumber numberWithInt:obj] withKey:[NSString stringWithUTF8String:key]];
    }
    
    void _LiveOpsSetTargetingStringData(const char* obj, const char* key)
    {
        if (obj != NULL)
        {
            [LiveOpsUser setTargetingData:[NSString stringWithUTF8String:obj] withKey:[NSString stringWithUTF8String:key]];
        }
        else
        {
            [LiveOpsUser setTargetingData:nil withKey:[NSString stringWithUTF8String:key]];
        }
    }
  
    void _LiveOpsFlush()
    {
        NSLog(@"void _LiveOpsFlush()");
        
        [LiveOpsUser flush];
    }
    
    void _LiveOpsPopupGetPopups()
    {        
        NSLog(@"_LiveOpsPopupGetPopups()");
      
        [LiveOpsPopup getPopups:^() {
            NSLog(@"getPopups responded");
          
          [[LiveOpsPlugin sharedLiveOpsPlugin] liveOpsPopupGetPopupsResponded];
        }];
    }
  
    void _LiveOpsPopupShowPopups(const char* popupSpaceKey)
    {
        NSLog(@"popupSpaceKey : %s", popupSpaceKey);
        [LiveOpsPopup showPopups: [NSString stringWithUTF8String:popupSpaceKey]];
    }
    
    void _LiveOpsPopupDestroyPopup()
    {
        NSLog(@"void _LiveOpsPopupDestroyPopup()");
        [LiveOpsPopup destroyPopup];
    }
    
    void _LiveOpsPopupDestroyAllPopups()
    {
        NSLog(@"void _LiveOpsPopupDestroyAllPopups()");
        [LiveOpsPopup destroyAllPopups];
    }
  
    void _LiveOpsSetPopupLinkListener()
    {
        [LiveOpsPopup setPopupLinkListener:^(NSString *popupSpaceKey, NSDictionary *customData) {
          NSLog(@"popupLink listener called\n\
              popupSpaceKey : %@, deep link json: %@", popupSpaceKey, customData);
          
          
          NSString *customDataString = @"";
          if (customData != nil) {
          
              customDataString = [NSString stringWithFormat:@"%@", customData];
          }
          
          [[LiveOpsPlugin sharedLiveOpsPlugin] liveOpsPopupSetPopupLinkListenerCalled:popupSpaceKey customDataString:customDataString];
        
      }];
    }

    void _LiveOpsSetPopupCloseListener()
    {
        [LiveOpsPopup setPopupCloseListener:^(NSString* popupSpaceKey, NSString* popupCampaignName, NSDictionary* customData, NSUInteger remainPopupNum) {
          NSLog(@"popupClose listener called\n\
              popupSpaceKey : %@, popupCampaignName: %@, deep link json: %@, remainPopupNum: %lu", popupSpaceKey, popupCampaignName, customData, (unsigned long)remainPopupNum);
          
          
          NSString *customDataString = @"";
          if (customData != nil) {
              customDataString = [NSString stringWithFormat:@"%@", customData];
          }

          NSString* remainPopupNumStr = [NSString stringWithFormat:@"%lu", (unsigned long)remainPopupNum];

          [[LiveOpsPlugin sharedLiveOpsPlugin] liveOpsPopupSetPopupCloseListenerCalled:popupSpaceKey popupCampaignName:popupCampaignName customDataString:customDataString remainPopupNumString:remainPopupNumStr];
        
      }];
    }
}

@end