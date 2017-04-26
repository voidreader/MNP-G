//
//  LiveOpsPush.h
//  LiveOps
//
//  Created by 강기태 on 2014. 7. 29..
//  Copyright (c) 2014년 IGAWorks. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>



@interface LiveOpsPushInfo : NSObject

@property (nonatomic,copy) NSDate* sentTime;
@property (nonatomic,copy) NSString* bodyText;
@property (nonatomic,copy) NSString* deepLinkUrl;
@property (nonatomic,copy) id deepLink;

@end



typedef void (^LiveOpsLocalNotificationCallback)(NSInteger Id, NSDate* sentTime, NSString* bodyText, NSDictionary* customData, BOOL isForeGround);
typedef void (^LiveOpsRemoteNotificationCallback)(NSArray* pushInfos, BOOL isForeGround);
typedef void (^LiveOpsRemotePushEnableCallback)(BOOL result);


@interface LiveOpsPush : NSObject

+ (void)initPush;
+ (void)setDeviceToken:(NSData*)deviceToken;
+ (void)setDeviceTokenForANE:(NSString*)deviceToken;

+ (void)handleAllNotificationFromLaunch:(NSDictionary*)launchOptions;
+ (void)handleLocalNotification:(UILocalNotification *)notification;
+ (void)handleRemoteNotification:(NSDictionary *)userInfo fetchHandler:(void (^)(UIBackgroundFetchResult))completionHandler;

+ (void)setLocalNotificationListener:(LiveOpsLocalNotificationCallback)block;
+ (void)setRemoteNotificationListener:(LiveOpsRemoteNotificationCallback)block;

+ (void)setRemotePushEnable:(BOOL)isEnabled;
+ (void)setRemotePushEnable:(BOOL)isEnabled completion:(LiveOpsRemotePushEnableCallback)block;

+ (void)registerLocalPushNotification:(NSInteger)Id date:(NSDate*)date body:(NSString*)bodyText button:(NSString*)buttonText soundName:(NSString*)sound badgeNumber:(NSInteger)badgeNum customPayload:(NSDictionary*)payloadDict;
+ (void)cancelLocalPush:(NSInteger)Id;

@end
