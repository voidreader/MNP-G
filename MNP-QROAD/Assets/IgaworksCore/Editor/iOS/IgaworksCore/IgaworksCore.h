//
//  IgaworksCore.h
//  IgaworksCore
//
//  Created by wonje,song on 2015. 5. 20..
//  Copyright (c) 2015년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>


@protocol IgaworksADClientRewardDelegate;

@protocol IgaworksCoreDelegate;


typedef enum _gender
{
    IgaworksCoreGenderMale = 2,
    IgaworksCoreGenderFemale = 1
} Gender;

typedef enum _IgaworksCoreLogLevel
{
    /*! only info logging  */
    IgaworksCoreLogInfo,
    /*! info, debug logging  */
    IgaworksCoreLogDebug,
    /*! all logging */
    IgaworksCoreLogTrace
} IgaworksCoreLogLevel;

@interface IgaworksCore : NSObject




@property (nonatomic, unsafe_unretained) id<IgaworksADClientRewardDelegate> clientRewardDelegate;

@property (nonatomic, unsafe_unretained) id<IgaworksCoreDelegate> delegate;

// igaworks에서 제공하는 reward server를 사용할것인지 여부.
@property (nonatomic, unsafe_unretained) BOOL useIgaworksRewardServer;



/*!
 @abstract
 초기화. init한다. Singleton method
 
 @discussion
 발급 받은 appkey로 connect한다.
 
 @param appkey              app. 등록 후, IGAWorks로부터 발급된 키.
 @param hashkey             app. 등록 후 발급된 키.
 @param isUseIgaworksRewardServer    igaworks에서 제공하는 reward server를 사용할것인지 여부.
 */


+ (void)igaworksCoreWithAppKey:(NSString *)appKey andHashKey:(NSString *)hashKey andIsUseIgaworksRewardServer:(BOOL)isUseIgaworksRewardServer NS_DEPRECATED_IOS(2.0, 2.0.5, "Use -igaworksCoreWithAppKey:andHashKey:");
//HOIIL XCODE6
//+ (void)igaworksCoreWithAppKey:(NSString *)appKey andHashKey:(NSString *)hashKey andIsUseIgaworksRewardServer:(BOOL)isUseIgaworksRewardServer;
/*!
 @abstract
 초기화. init한다. Singleton method
 
 @discussion
 발급 받은 appkey로 connect한다.
 
 @param appkey              app. 등록 후, IGAWorks로부터 발급된 키.
 @param hashkey             app. 등록 후 발급된 키.
 */
+ (void)igaworksCoreWithAppKey:(NSString *)appKey andHashKey:(NSString *)hashKey;



/*!
 @abstract
 singleton IgaworksAD 객체를 반환한다.
 */
+ (IgaworksCore *)shared;


/*!
 @abstract
 로그를 level를 설정한다.
 
 @discussion
 보고자 하는 로그 level을 info, debug, trace으로 설정한다.
 
 @param LogLevel            log level
 */
+ (void)setLogLevel:(IgaworksCoreLogLevel)logLevel;

/*!
 @abstract
 사용자의 나이 정보를 전송하고자 할때 호출한다.
 
 @param age              age.
 */
+ (void)setAge:(int)age;

/*!
 @abstract
 사용자의 성별 정보를 전송하고자 할때 호출한다.
 
 @param gender              gender.
 */
+ (void)setGender:(Gender)gender;

/*!
 @abstract
 사용자의 user id를 전송하고자 할때 호출한다.
 
 @param userId              user id.
 */
+ (void)setUserId:(NSString *)userId;


/*!
 @abstract
 Apple AdvertisingIdentifier를 전송한다.
 
 @param appleAdvertisingIdentifier              Apple AdvertisingIdentifier.
 
 @discussion
 광고 목적이 아닌 경우, Apple AdvertisingIdentifier를 사용하면, app reject 사유가 되기 때문에, iAd(AdSupport.framework), AdPopcornOfferwall, AdPopcornDA 등의 광고 기능을 사용하지 않는다면 값을 전송하지 않는다.
 IgaworksCore에서는 전달받은 Apple AdvertisingIdentifier 값이 없는경우, identifierForVendor 값을 identifier로 사용한다.
 */
+ (void)setAppleAdvertisingIdentifier:(NSString *)appleAdvertisingIdentifier isAppleAdvertisingTrackingEnabled:(BOOL)isAppleAdvertisingTrackingEnabled;

/*!
 @abstract
 App.이 최초 실행될때 시작되었음을 서버로 전송하기 위해 호출한다.
 한번만 호출한다.
 
 @discussion
 AppDelegate의 - application:didFinishLaunchingWithOptions: 메소드에서 IgaworksCore -igaworksCoreWithAppKey:andHashKey:andIsUseIgaworksRewardServer: 메소드를 호출하는 경우에는 start 메소드를 호출하지 않는다.
 */
+ (void)start;


/*!
 @abstract
 url scheme를 통해 open된 url을 전달한다.
 
 @discussion

 */
+ (void)passOpenURL:(NSURL *)URL;

+ (void)setReferralUrl:(NSURL *)URL __attribute__((deprecated("use -setReferralUrlForFacebook: instead")));
+ (void)setReferralUrlForFacebook:(NSURL *)URL;
@end

@protocol IgaworksADClientRewardDelegate <NSObject>

@optional

/*!
 @abstract
 사용자에게 지급할 아이템이 있을때 호출된다.
 
 @discussion
 사용자에게 아이템을 지급하고, 지급이 완료되면 didGiveRewardItemWithRewardKey 메소드를 호출하여 지급 완료 확정 처리를 한다.
 */
- (void)onRewardRequestResult:(BOOL)isSuccess withMessage:(NSString *)message itemName:(NSString *)itemName itemKey:(NSString *)itemKey campaignName:(NSString *)campaignName campaignKey:(NSString *)campaignKey rewardKey:(NSString *)rewardKey quantity:(NSInteger)quantity;


/*!
 @abstract
 사용자에게 지급할 아이템이 있을때 호출된다. 아이템 리스트를 전달한다.
 
 @discussion
 사용자에게 아이템을 지급하고, 지급이 완료되면 didGiveRewardItemWithRewardKey 메소드를 호출하여 지급 완료 확정 처리를 한다.
 */
- (void)onRewardRequestResult:(BOOL)isSuccess withMessage:(NSString *)message items:(NSArray *)items;

/*!
 @abstract
 Reward 지급 확정 처리 콜백 메소드.
 
 @discussion
 didGiveRewardItemWithRewardKey 메소드에서 reward 지급 처리를 완료한 뒤에 IGAWorks에 요청한 결과가 이 곳으로 리턴된다. isSuccess가 YES가 리턴되어야 최종 reward 지급이 완료된다.
 */
- (void)onRewardCompleteResult:(BOOL)isSuccess withMessage:(NSString *)message resultCode:(NSInteger)resultCode withCompletedRewardKey:(NSString *)completedRewardKey;

@end


@protocol IgaworksCoreDelegate <NSObject>

@optional

/*!
 @abstract
 conversion key, referral key를 전달한다.
*/
- (void)didSaveConversionKey:(NSInteger)conversionKey subReferralKey:(NSString *)subReferralKey;

/*!
 @abstract
 deep link를 전달한다.
 */
- (void)didReceiveDeeplink:(NSString *)deepLink;

@end
