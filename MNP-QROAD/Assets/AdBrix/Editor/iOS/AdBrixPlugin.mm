//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "AdBrixPlugin.h"


UIViewController *UnityGetGLViewController();

static AdBrixPlugin *_sharedInstance = nil; //To make AdBrixPlugin Singleton

@implementation AdBrixPlugin



+ (void)initialize
{
	if (self == [AdBrixPlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (AdBrixPlugin *)sharedAdBrixPlugin
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






// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
	
    
    void _FirstTimeExperience(const char* name)
    {
        NSLog(@"_FirstTimeExperience : %s", name);
        
        [AdBrix firstTimeExperience:[NSString stringWithUTF8String:name]];
    }
    
    void _FirstTimeExperienceWithParam(const char* name, const char* param)
    {
        [AdBrix firstTimeExperience:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    void _Retention(const char* name)
    {
        [AdBrix retention:[NSString stringWithUTF8String:name]];
    }
    
    void _RetentionWithParam(const char* name, const char* param)
    {
        [AdBrix retention:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    void _Buy(const char* name)
    {
        [AdBrix buy:[NSString stringWithUTF8String:name]];
    }
    
    void _BuyWithParam(const char* name, const char* param)
    {
        [AdBrix buy:[NSString stringWithUTF8String:name] param:[NSString stringWithUTF8String:param]];
    }
    
    
    void _ShowViralCPINotice()
    {
        NSLog(@"AdBrixPlugin : _ShowViralCPINotice");
      
        [AdBrix showViralCPINotice:UnityGetGLViewController()];
    }
    
    void _SetCustomCohort(AdBrixCustomCohortType customCohortType, const char* filterName)
    {
        [AdBrix setCustomCohort:customCohortType filterName:[NSString stringWithUTF8String:filterName]];
    }
    
    void _CrossPromotionShowAD(const char* activityName)
    {
        [CrossPromotion showAD:[NSString stringWithUTF8String:activityName] parentViewController:UnityGetGLViewController()];
    }
    
    void _AdBrixPurchase (const char* orderId, const char* productId, const char* productName, double price, int quantity, const char* currencyString, const char* category)
    {
        NSLog(@"_AdBrixPurchase %s %s %s %f %d %s %s",
              orderId, productId, productName, price, quantity, currencyString, category);
        if(!orderId)orderId = "";
        if(!productId)productId = "";
        if(!productName)productName = "";
        if(!currencyString)currencyString = "";
        if(!category)category = "";
        
        //        NSArray* categories = @[
        //            [NSString stringWithUTF8String:category1],
        //            [NSString stringWithUTF8String:category2],
        //            [NSString stringWithUTF8String:category3],
        //            [NSString stringWithUTF8String:category4],
        //            [NSString stringWithUTF8String:category5]
        //        ];
        
        [AdBrix purchase:[NSString stringWithUTF8String:orderId]
                         productId:[NSString stringWithUTF8String:productId]
                       productName:[NSString stringWithUTF8String:productName]
                             price:price
                          quantity:quantity
                    currencyString:[NSString stringWithUTF8String:currencyString]
                          category:[NSString stringWithUTF8String:category]];
    }
    
    void _AdBrixPurchaseWithJson (const char* jsonString)
    {
        NSLog(@"_AdBrixPurchaseWithJson %s ", jsonString);
        [AdBrix purchase:[NSString stringWithUTF8String:jsonString]];
    }
    
    void _AdBrixPurchaseList (const char *pArr[], int arrCnt)
    {
        NSMutableArray *pList = [NSMutableArray array];
        if(!pArr || arrCnt <= 0)
        {
            NSLog(@"_AdBrixPurchaseList error! Cnt of purchaseList shold be more than zero!");
            return;
        }
        
        for(int i = 0; i < arrCnt; i++)
        {
            NSString* purchaseInfo = [NSString stringWithUTF8String:pArr[i]];
            NSLog(@"_AdBrixPurchaseList[%d] %@ ", i, purchaseInfo);
            if (purchaseInfo)
            {
                NSArray* arrPurchaseInfo = [purchaseInfo componentsSeparatedByString:@"&"];
                if(!(arrPurchaseInfo.count == 7))
                {
                    NSLog(@"_AdBrixPurchaseList error! PurchaseInfo datum shold be orderId(String), productId(String), productName(String), price(double), quantity(Int), category(String - ex Game.Console.Ps4");
                    return;
                }
                [pList addObject:[AdBrix createItemModel:arrPurchaseInfo[0] productId:arrPurchaseInfo[1] productName:arrPurchaseInfo[2] price:[arrPurchaseInfo[3] doubleValue] quantity:[arrPurchaseInfo[4] integerValue] currencyString:arrPurchaseInfo[5] category:arrPurchaseInfo[6]]];
            }
        }
        
        [AdBrix purchaseList:pList];
        
    }
    
    const char* _AdBrixCurrencyName (int currency)
    {
        NSString* str = [AdBrix currencyName:currency];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }

}

@end

