//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <IgaworksCommerce/IgaworksCommerce.h>



@interface IgaworksCommercePlugin : NSObject


@property (nonatomic, copy) NSString *callbackHandlerName;

+ (IgaworksCommercePlugin *)sharedIgaworksCommercePlugin;


@end
