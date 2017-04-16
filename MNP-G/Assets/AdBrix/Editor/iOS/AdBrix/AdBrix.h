//
//  AdBrix.h
//  AdBrixLib
//
//  Created by wonje,song on 2015. 5. 21..
//  Copyright (c) 2015년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <UIKit/UIKit.h>

#import "AdBrixItem.h"

typedef NS_ENUM(NSInteger, AdBrixCustomCohortType)
{
    AdBrixCustomCohort_1 = 1,
    AdBrixCustomCohort_2 = 2,
    AdBrixCustomCohort_3 = 3
};

typedef NS_ENUM(NSInteger, AdBrixCurrencyType)
{
    AdBrixCurrencyKRW = 1,
    AdBrixCurrencyUSD = 2,
    AdBrixCurrencyJPY = 3,
    AdBrixCurrencyEUR = 4,
    AdBrixCurrencyGBP = 5,
    AdBrixCurrencyCHY = 6,
    AdBrixCurrencyTWD = 7,
    AdBrixCurrencyHKD = 8
};

@interface AdBrix : NSObject

/*!
 @abstract
 singleton AdBrix 객체를 반환한다.
 */
+ (AdBrix *)shared;

/*!
 @abstract
 first time experience의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 */
+ (void)firstTimeExperience:(NSString *)activityName;


/*!
 @abstract
 first time experience의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 @param param                     parameter.
 */
+ (void)firstTimeExperience:(NSString *)activityName param:(NSString *)param;

/*!
 @abstract
 retension의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 */
+ (void)retention:(NSString *)activityName;

/*!
 @abstract
 retension의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 @param param                     parameter.
 */
+ (void)retention:(NSString *)activityName param:(NSString *)param;

/*!
 @abstract
 buy의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 */
+ (void)buy:(NSString *)activityName  __attribute__((deprecated("use -purchase: instead")));

/*!
 @abstract
 buy의 Activity에 해당할때 호출한다.
 
 @param activityName              activity name.
 @param param                     parameter.
 */
+ (void)buy:(NSString *)activityName param:(NSString *)param  __attribute__((deprecated("use -purchase: instead")));


+ (void)showViralCPINotice:(UIViewController *)viewController;

/*!
 @abstract
 cohort 분석시 호출한다.
 
 @param customCohortType          cohort type : AdBrixCustomCohortType
 @param filterName                filter Name
 */
+ (void)setCustomCohort:(AdBrixCustomCohortType)customCohortType filterName:(NSString *)filterName;


#pragma mark - Commerce

+ (void)purchase:(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories;

+ (void)purchaseList:(NSArray*)orderInfo;

+ (void)purchase:(NSString*)purchaseDataJsonString __attribute__((deprecated("use -other purchase api: instead")));

+ (NSString *)currencyName:(NSUInteger)currency;

+ (AdBrixItem*)createItemModel :(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories __attribute__((deprecated("use -PurchaseItemModel: instead")));
    
+ (AdBrixItem*)PurchaseItemModel :(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories;


@end
