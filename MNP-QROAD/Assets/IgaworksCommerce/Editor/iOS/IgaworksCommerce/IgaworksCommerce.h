//
//  IgaworksCommerce.h
//  IgaworksAd
//
//  Created by 강기태 on 2015. 6. 9..
//  Copyright (c) 2015년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "IgaworksCommerceItem.h"

typedef NS_ENUM(NSInteger, IgaworksCommerceCurrencyType)
{
    IgaworksCommerceCurrencyKRW = 1,
    IgaworksCommerceCurrencyUSD = 2,
    IgaworksCommerceCurrencyJPY = 3,
    IgaworksCommerceCurrencyEUR = 4,
    IgaworksCommerceCurrencyGBP = 5,
    IgaworksCommerceCurrencyCHY = 6,
    IgaworksCommerceCurrencyTWD = 7,
    IgaworksCommerceCurrencyHKD = 8
};

@interface IgaworksCommerce : NSObject

+ (void)purchase:(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories;

+ (void)purchaseList:(NSArray*)orderInfo;

+ (void)purchase:(NSString*)purchaseDataJsonString;

+ (NSString *)currencyName:(NSUInteger)currency;

+ (IgaworksCommerceItem*)createItemModel :(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories;

@end
