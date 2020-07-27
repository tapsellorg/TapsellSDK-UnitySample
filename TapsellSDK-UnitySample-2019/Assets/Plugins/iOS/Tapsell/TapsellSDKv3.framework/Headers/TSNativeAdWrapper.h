//
//  TSNativeAdWrapper.h
//  TapsellSDKv3
//
//  Created by Tapsell on 11/29/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface TSNativeAdWrapper : NSObject

-(instancetype) initWithAdId:(NSString*) adId
                    andTitle:(NSString*) title
              andDescription:(NSString*) htmlDescription
                  andLogoUrl:(NSString*) logoUrl
         andCallToActionText:(NSString*) callToActionText;

@property (nonatomic, strong, readonly) NSString* adId;
@property (nonatomic, strong, readonly) NSString* title;
@property (nonatomic, strong, readonly) NSString* htmlDescription;
@property (nonatomic, strong, readonly) NSString* logoUrl;
@property (nonatomic, strong, readonly) NSString* callToActionText;
@end
