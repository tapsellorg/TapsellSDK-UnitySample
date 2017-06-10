//
//  Tapsell.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/13/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef Tapsell_h
#define Tapsell_h

#import <Foundation/Foundation.h>
#import "TSConfiguration.h"
#import "TSAdRequestOptions.h"
#import "TapsellAd.h"

@interface Tapsell : NSObject

+ (void)initializeWithAppKey:(NSString* )appKey;

+ (void)initializeWithAppKey:(NSString* )appKey
                   andConfig:(TSConfiguration *)config;

+ (BOOL)isDebugMode;

+ (void)setDebugMode:(BOOL)debugMode;

+ (NSString*) getAppUserId;

+ (void)setAppUserId:(NSString*)appUserId;

+ (void)requestAdForZone:(NSString*)zoneId
           onAdAvailable: (void (^)(TapsellAd * ad)) onAdAvailable
         onNoAdAvailable:(void (^)()) onNoAdAvailable
                 onError:(void (^)(NSString* error)) onError
              onExpiring:(void (^)(TapsellAd * ad)) onExpiring;


+ (void)requestAdForZone:(NSString*)zoneId
              andOptions:(TSAdRequestOptions *)options
           onAdAvailable: (void (^)(TapsellAd * ad)) onAdAvailable
         onNoAdAvailable:(void (^)()) onNoAdAvailable
                 onError:(void (^)(NSString* error)) onError
              onExpiring:(void (^)(TapsellAd * ad)) onExpiring;

+ (void)setAdShowFinishedCallback: (void (^)(TapsellAd * ad, BOOL completed)) onAdShowFinished;

+ (NSString* )getVersion;

@end


#endif /* Tapsell_h */
