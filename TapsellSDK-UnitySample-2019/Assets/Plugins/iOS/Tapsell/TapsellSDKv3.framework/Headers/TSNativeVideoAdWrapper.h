//
//  TSNativeVideoAdWrapper.h
//  TapsellSDKv3
//
//  Created by Tapsell on 12/14/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TSNativeAdWrapper.h"

@interface TSNativeVideoAdWrapper : TSNativeAdWrapper
-(instancetype) initWithAdId:(NSString*) adId
                    andTitle:(NSString*) title
              andDescription:(NSString*) htmlDescription
                  andLogoUrl:(NSString*) logoUrl
         andCallToActionText:(NSString*) callToActionText
         andVideoUrl:(NSString*) videoUrl;

@property(nonatomic, strong, readonly) NSString* videoUrl;
@end
