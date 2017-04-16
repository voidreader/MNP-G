//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <IgaworksCore/IgaworksCore.h>



@interface IgaworksCorePlugin : NSObject <IgaworksCoreDelegate, IgaworksADClientRewardDelegate>


@property (nonatomic, copy) NSString *callbackHandlerName;


+ (IgaworksCorePlugin *)sharedIgaworksCorePlugin;

- (void)setIgaworksCoreDelegate;
- (void)setIgaworksADClientRewardDelegate;


@end
