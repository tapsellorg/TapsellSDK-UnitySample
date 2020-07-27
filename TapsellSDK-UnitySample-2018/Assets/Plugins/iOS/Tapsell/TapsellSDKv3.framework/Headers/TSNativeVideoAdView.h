//
//  TSNativeVideoAdView.h
//  TapsellSDKv3
//
//  Created by Tapsell on 11/16/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "TSNativeAdView.h"
#import "TSNativeVideoBundle.h"

@interface TSNativeVideoAdView : TSNativeAdView

@property(nonatomic, strong, readonly) TSNativeVideoBundle* bundle;
@property(nonatomic, readwrite) NSInteger videoViewTag;

-(BOOL) loadAd:(NSObject*)ad;
-(TSNativeVideoBundle*) getBundle;
-(void) fillWithBundle:(TSNativeVideoBundle*)bundle;
-(void) fillVideoView:(NSString*)adId withUrl:(NSString*)videoUrl;
@end
