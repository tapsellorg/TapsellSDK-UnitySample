//
//  TSNativeBannerAdWrapper.h
//  TapsellSDKv3
//
//  Created by Tapsell on 11/29/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TSNativeAdWrapper.h"

@interface TSNativeBannerAdWrapper : TSNativeAdWrapper

-(instancetype) initWithAdId:(NSString*) adId
                    andTitle:(NSString*) title
              andDescription:(NSString*) htmlDescription
                  andLogoUrl:(NSString*) logoUrl
         andCallToActionText:(NSString*) callToActionText
         andProtraitImageUrl:(NSString*) portriatImageUrl
        andLandscapeImageUrl:(NSString*) landscapeImageUrl;

@property(nonatomic, strong, readonly) NSString* portriatImageUrl;
@property(nonatomic, strong, readonly) NSString* landscapeImageUrl;
@end
