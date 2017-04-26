//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <LiveOps/LiveOps.h>



@interface LiveOpsPlugin : NSObject


@property (nonatomic, copy) NSString *callbackHandlerName;


+ (LiveOpsPlugin *)sharedLiveOpsPlugin;

- (void)liveOpsPopupGetPopupsResponded;
- (void)liveOpsPopupSetPopupLinkListenerCalled:(NSString *)popupSpaceKey customDataString:(NSString *)customDataString;
- (void)liveOpsPopupSetPopupCloseListenerCalled:(NSString *)popupSpaceKey popupCampaignName:(NSString *)campaignName customDataString:(NSString *)customDataString remainPopupNumString:(NSString *)remainPopupNumStr;


@end
