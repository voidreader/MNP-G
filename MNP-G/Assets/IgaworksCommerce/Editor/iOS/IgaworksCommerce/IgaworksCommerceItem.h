//
//  IgaworksCommerceItem.h
//  IgaworksCommerceLib
//
//  Created by igaworks on 2016. 7. 25..
//  Copyright © 2016년 igaworks. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface IgaworksCommerceItem : NSObject

- (IgaworksCommerceItem*)create :(NSString*)orderId productId:(NSString*)productId productName:(NSString*)productName price:(double)price quantity:(NSUInteger)quantity currencyString:(NSString *)currencyString category:(NSString*)categories;

- (NSString *)getOrderId;
- (NSString *)getProductId;
- (double)getPrice;
- (NSString *)getCurrencyString;
- (NSUInteger)getQuantity;
- (NSString *)getProductName;
- (NSString *)getCategories;

@end
