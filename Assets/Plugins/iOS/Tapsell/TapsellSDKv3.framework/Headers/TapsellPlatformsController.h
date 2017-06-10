//
//  TapsellPlatformsController.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/30/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TapsellPlatformsController_h
#define TapsellPlatformsController_h

#import "TapsellAd.h"

@interface TapsellPlatformsController : NSObject

-(void) onAdAvailable:(TapsellAd* _Nullable)ad;

-(void) onNoAdAvailable:(NSString* _Nullable)zoneId;

-(void) onError:(NSString* _Nullable)error zoneId:(NSString* _Nullable)zoneId;

-(void) onExpiring:(TapsellAd* _Nullable)ad;

@end


#endif /* TapsellPlatformsController_h */
