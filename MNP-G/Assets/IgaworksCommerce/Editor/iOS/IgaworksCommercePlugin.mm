//
//  AdPopcornSDKPlugin.m
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import "IgaworksCommercePlugin.h"


UIViewController *UnityGetGLViewController();

static IgaworksCommercePlugin *_sharedInstance = nil; //To make IgaworksCorePlugin Singleton

@implementation IgaworksCommercePlugin

@synthesize callbackHandlerName = _callbackHandlerName;

+ (void)initialize
{
	if (self == [IgaworksCommercePlugin class])
	{
		_sharedInstance = [[self alloc] init];
	}
}


+ (IgaworksCommercePlugin *)sharedIgaworksCommercePlugin
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



extern "C" {
    void _IgaworksCommercePurchase (const char* orderId, const char* productId, const char* productName, double price, int quantity, const char* currencyString, const char* category)
    {
        NSLog(@"_IgaworksCommercePurchase %s %s %s %f %d %s %s",
            orderId, productId, productName, price, quantity, currencyString, category);

//        NSArray* categories = @[
//            [NSString stringWithUTF8String:category1],
//            [NSString stringWithUTF8String:category2],
//            [NSString stringWithUTF8String:category3],
//            [NSString stringWithUTF8String:category4],
//            [NSString stringWithUTF8String:category5]
//        ];

        [IgaworksCommerce purchase:[NSString stringWithUTF8String:orderId]
            productId:[NSString stringWithUTF8String:productId]
            productName:[NSString stringWithUTF8String:productName]
            price:price
            quantity:quantity
            currencyString:[NSString stringWithUTF8String:currencyString]
            category:[NSString stringWithUTF8String:category]];
    }

    void _IgaworksCommercePurchaseWithJson (const char* jsonString)
    {
        NSLog(@"_IgaworksCommercePurchaseWithJson %s ", jsonString);
        [IgaworksCommerce purchase:[NSString stringWithUTF8String:jsonString]];
    }
    
    void _IgaworksCommercePurchaseList (const char *pArr[], int arrCnt)
    {
        NSMutableArray *pList = [NSMutableArray array];
        if(!pArr || arrCnt <= 0)
        {
            NSLog(@"_IgaworksCommercePurchaseList error! Cnt of purchaseList shold be more than zero!");
            return;
        }

        for(int i = 0; i < arrCnt; i++)
        {
            NSString* purchaseInfo = [NSString stringWithUTF8String:pArr[i]];
            NSLog(@"_IgaworksCommercePurchaseList[%d] %@ ", i, purchaseInfo);
            if (purchaseInfo)
            {
                NSArray* arrPurchaseInfo = [purchaseInfo componentsSeparatedByString:@"&"];
                if(!(arrPurchaseInfo.count == 7))
                {
                    NSLog(@"_IgaworksCommercePurchaseList error! PurchaseInfo datum shold be orderId(String), productId(String), productName(String), price(double), quantity(Int), category(String - ex Game.Console.Ps4");
                    return;
                }
                [pList addObject:[IgaworksCommerce createItemModel:arrPurchaseInfo[0] productId:arrPurchaseInfo[1] productName:arrPurchaseInfo[2] price:[arrPurchaseInfo[3] doubleValue] quantity:[arrPurchaseInfo[4] integerValue] currencyString:arrPurchaseInfo[5] category:arrPurchaseInfo[6]]];
            }
        }
        
        [IgaworksCommerce purchaseList:pList];
 
    }

    const char* _currencyName (int currency)
    {
        NSString* str = [IgaworksCommerce currencyName:currency];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
}

@end