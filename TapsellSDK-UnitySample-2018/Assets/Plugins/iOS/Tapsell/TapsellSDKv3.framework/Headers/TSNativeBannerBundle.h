//
//  TSNativeBannerBundle.h
//  TapsellSDKv3
//
//  Created by Tapsell on 12/13/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//
#import <Foundation/Foundation.h>

@interface TSNativeBannerBundle : NSObject
-(instancetype)initWithAd:(NSObject*) ad
              andTitleTag:(NSInteger) titleTag
        andDescriptionTag:(NSInteger) htmlDescriptionTag
               andLogoTag:(NSInteger) logoTag
   andCallToActionTextTag:(NSInteger) callToActionTag
          andMainImageTag:(NSInteger) mainImageTag;
@property(nonatomic, readwrite) NSInteger titleLabelTag;
@property(nonatomic, readwrite) NSInteger descriptionLabelTag;
@property(nonatomic, readwrite) NSInteger logoImageTag;
@property(nonatomic, readwrite) NSInteger mainImageTag;
@property(nonatomic, readwrite) NSInteger callToActionButtonTag;
-(NSObject*) getAd;
@end
