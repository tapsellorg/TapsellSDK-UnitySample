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

@interface TapsellAd : NSObject

-(void)setAd:(id _Nullable )ad;
-(void)setVideoFilePath:(NSString*_Nullable)videoFilePath;
-(void)setAdRequestOptions:(TSAdRequestOptions*_Nullable)requestOptions;
-(void)setZoneId:(NSString*_Nullable)zoneId;
-(void)setCacheTime:(NSNumber*_Nullable)cacheTime;
-(void)showWithOptions:(TSAdShowOptions*_Nullable)showOptions
     andOpenedCallback:(void (^_Nullable)(TapsellAd * _Nullable ad)) onOpened
     andClosedCallback:(void (^_Nullable)(TapsellAd * _Nullable ad)) onClosed;

-(NSNumber*_Nullable) getCacheTime;
-(NSString*_Nullable) getZoneId;
-(BOOL) isValid;
-(BOOL) isVideoAd;
-(BOOL) isRewardedAd;
-(BOOL) isBannerAd;
-(BOOL) isExpired;
-(NSString*_Nullable)getVideoFilePath;
-(BOOL)isAdShown;
-(NSString*_Nullable) getId;
-(TSAdRequestOptions*_Nullable)getRequestOptions;

@property (nonatomic, strong, readonly) NSNumber* _Nullable cacheTime;
@property (nonatomic, strong, readonly) NSString* _Nullable zoneId;
@property (nonatomic, strong, readonly) NSNumber* _Nullable isShown;
@property (nonatomic, strong, readonly) NSString* _Nullable videoFilePath;
@property (nonatomic, strong, readonly) TSAdRequestOptions* _Nullable requestOptions;

// serialization
-(NSArray<NSString*> *_Nullable) keys;
-(BOOL) isValidValue: (id _Nullable ) value;

@end

#endif /* TapsellAd_h */
