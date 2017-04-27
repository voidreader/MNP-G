//
//  LiveOpsUser.h
//  LiveOps
//
//  Created by 강기태 on 2014. 8. 5..
//  Copyright (c) 2014년 IGAWorks. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef void (^LiveOpsUserLoginWorkCallback)(NSString* userObjectId);

@interface LiveOpsUser : NSObject

+ (void)setLoginCompleteCallback:(LiveOpsUserLoginWorkCallback)block;

+ (NSString*)getUserId;
+ (NSString*)getObjectId;

+ (void)setTargetingData:(id)obj withKey:(NSString*)key;
+ (id)getTargetingDataWithKey:(NSString*)key;

+ (void)flush;

@end