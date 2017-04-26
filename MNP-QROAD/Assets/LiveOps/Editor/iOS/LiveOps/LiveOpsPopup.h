//
//  LiveOpsPopup.h
//  IgaworksAd
//
//  Created by 강기태 on 2015. 2. 27..
//  Copyright (c) 2015년 wonje,song. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>

typedef void (^LiveOpsPopupCompleteCallback)();
typedef void (^LiveOpsPopupLinkCallback)(NSString* popupSpaceKey, NSDictionary* customData);
typedef void (^LiveOpsPopupCloseCallback)(NSString* popupSpaceKey, NSString* popupCampaignName, NSDictionary* customData, NSUInteger remainPopupNum);


@interface LiveOpsPopup : NSObject

+ (void)getPopups:(LiveOpsPopupCompleteCallback)block;
+ (void)showPopups:(NSString*)popupSpaceKey;
+ (void)showPopups:(NSString*)popupSpaceKey withViewController:(UIViewController*)viewCtrler;
+ (void)destroyPopup;
+ (void)destroyAllPopups;

+ (void)setPopupLinkListener:(LiveOpsPopupLinkCallback)block;
+ (void)setPopupCloseListener:(LiveOpsPopupCloseCallback)block;

@end