//
//  TapsellExtraPlatformsController.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/30/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TapsellExtraPlatformsController_h
#define TapsellExtraPlatformsController_h

#import <Foundation/Foundation.h>
#import "TapsellAd.h"

@interface TapsellExtraPlatformsController : NSObject

+ (TapsellExtraPlatformsController *_Nonnull)sharedInstance;

+ (void) onAdAvailable:(TapsellAd* _Nullable) ad;

+ (void) onNoAdAvailable:(NSString* _Nullable)zoneId;

+ (void) onError:(NSString* _Nullable)error zoneId:(NSString* _Nullable)zoneId;

+ (void) onExpiring:(TapsellAd* _Nullable)ad;

+ (void) onOpened:(TapsellAd* _Nullable)ad;

+ (void) onClosed:(TapsellAd* _Nullable)ad;

+ (void)initializeWithAppKey:(NSString* _Nonnull)appKey;

+ (BOOL)isDebugMode;

+ (void)setDebugMode:(BOOL)debugMode;

+ (NSString*_Nullable) getAppUserId;

+ (void)setAppUserId:(NSString*_Nullable)appUserId;


+ (void)requestAdForZone:(NSString*_Nonnull)zoneId
                isCached:(BOOL)isCached;

- (void)cacheNewAd:(TapsellAd* _Nullable) ad;

- (void)removeCachedAd:(TapsellAd* _Nullable) ad;

- (void)showAd:(NSString* _Nonnull)adId
   withOptions:(TSAdShowOptions* _Nullable)showOptions;

+ (NSString* _Nullable)getVersion;

- (void) setPlatformControllerOnAdAvailable: (void (^_Nullable)(TapsellAd * _Nullable ad)) onAdAvailable
                            onNoAdAvailable:(void (^_Nullable)(NSString* _Nullable zoneId)) onNoAdAvailable
                                    onError:(void (^_Nullable)(NSString* _Nullable error, NSString* _Nullable zoneId)) onError
                                 onExpiring:(void (^_Nullable)(TapsellAd * _Nullable ad)) onExpiring
                                   onOpened:(void (^_Nullable)(TapsellAd * _Nullable ad)) onOpened
                                   onClosed:(void (^_Nullable)(TapsellAd * _Nullable ad)) onClosed;

+ (void)setAdShowFinishedCallback: (void (^_Nullable)(TapsellAd * _Nullable ad, BOOL completed)) onAdShowFinished;

@end


#endif /* TapsellExtraPlatformsController_h */
