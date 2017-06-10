//
//  TapsellAd.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/13/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TapsellAd_h
#define TapsellAd_h

#import <Foundation/Foundation.h>
#import "TSAdRequestOptions.h"
#import "TSAdShowOptions.h"
#import "TSWebViewController.h"

@interface TapsellAd : NSObject

-(void)setAd:(id)ad;
-(void)setVideoFilePath:(NSString*)videoFilePath;
-(void)setAdRequestOptions:(TSAdRequestOptions*)requestOptions;
-(void)setZoneId:(NSString*)zoneId;
-(void)setCacheTime:(NSNumber*)cacheTime;
-(void)showWithOptions:(TSAdShowOptions* )showOptions;

-(NSNumber*) getCacheTime;
-(NSString*) getZoneId;
-(BOOL) isValid;
-(BOOL) isVideoAd;
-(BOOL) isRewardedAd;
-(BOOL) isBannerAd;
-(BOOL) isExpired;
-(NSString*)getVideoFilePath;
-(BOOL)isAdShown;
-(NSString*) getId;
-(TSAdRequestOptions*)getRequestOptions;

@property (nonatomic, strong, readonly) NSNumber* cacheTime;
@property (nonatomic, strong, readonly) NSString* zoneId;
@property (nonatomic, strong, readonly) NSNumber* isShown;
@property (nonatomic, strong, readonly) NSString* videoFilePath;
@property (nonatomic, strong, readonly) TSAdRequestOptions* requestOptions;

// serialization
-(NSArray<NSString*> *) keys;
-(BOOL) isValidValue: (id) value;

@end

#endif /* TapsellAd_h */
