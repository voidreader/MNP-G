//
//  LiveOpsQuery.h
//  LiveOps
//
//  Created by 강기태 on 2014. 7. 9..
//  Copyright (c) 2014년 IGAWorks. All rights reserved.
//

#import <Foundation/Foundation.h>


typedef void (^FindInBackgroundBlock)(NSArray* list, NSError* error);
//typedef void (^GetInBackgroundBlock)(LiveOpsObject* obj, NSError* error);

@class LiveOpsObject;

@interface LiveOpsQuery : NSObject

@property (nonatomic,copy) NSString* collectionName;
@property (nonatomic) NSUInteger listNumber;
@property (nonatomic) NSUInteger skipNumber;
@property (nonatomic,copy) NSString* orderByCol;
@property (nonatomic) NSMutableDictionary* whereJsonObj;

+ (instancetype)QueryWithCollectionName:(NSString*)name;
+ (instancetype)QueryWithObject:(LiveOpsObject*)object;
+ (instancetype)QueryAboutUser;

- (instancetype)initWithCollectionName:(NSString*)name;

- (void)orderDescending:(NSString*)columnName;
- (void)orderAscending:(NSString*)columnName;
- (void)orderDescendingByCreateTime;
- (void)orderDescendingByUpdateTime;
- (void)orderAscendingByCreateTime;
- (void)orderAscendingByUpdateTime;

- (void)setLimit:(NSUInteger)numberOfResult;
- (void)setSkip:(NSUInteger)numberOfSkip;

- (void)whereEqualTo:(id)obj columnName:(NSString*)name;
- (void)whereNotEqualTo:(id)obj columnName:(NSString*)name;
- (void)whereContains:(id)obj columnName:(NSString*)name;
- (void)whereGreaterThan:(id)obj columnName:(NSString*)name;
- (void)whereGreaterThanOrEqualTo:(id)obj columeName:(NSString*)name;
- (void)whereLessThan:(id)obj columnName:(NSString*)name;
- (void)whereLessThanOrEqualTo:(id)obj columeName:(NSString*)name;
- (void)whereExists:(NSString*)columnName;
- (void)whereDoesNotExist:(NSString*)columnName;

- (void)findInBackground:(FindInBackgroundBlock)callback;
//- (void)getInBackground:(GetInBackgroundBlock)callback;

@end
