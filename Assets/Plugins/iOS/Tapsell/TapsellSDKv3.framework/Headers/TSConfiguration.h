//
//  TSConfiguration.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/13/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TSConfiguration_h
#define TSConfiguration_h

#import <Foundation/Foundation.h>

@interface TSConfiguration : NSObject


- (void)setDebugMode:(BOOL) debugMode;
- (void)setAppUserId:(NSString* )appUserId;

- (BOOL) isDebugMode;
- (NSString* ) appUserId;


/* boolean to send request or not */
@property (nonatomic, strong, readonly) NSNumber* mDebugMode;
@property (nonatomic, strong, readonly) NSString* mAppUserId;


@end

#endif /* TSConfiguration_h */
