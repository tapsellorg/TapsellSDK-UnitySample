//
//  TapsellVAST.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/22/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TapsellVAST_h
#define TapsellVAST_h

#import <Foundation/Foundation.h>

typedef enum VastVersion : NSInteger {
    VAST2=2,
    VAST3=3
} VastVersion;

typedef enum PrerollType : NSInteger {
    PrerollTypeShort=1,
    PrerollTypeLong=2
} PrerollType;

@interface TapsellVAST : NSObject

+(NSString*) getVastUrlForZone:(NSString*)zoneId withType:(PrerollType)prerollType ofVastVersion:(VastVersion)vastVersion;

@end


#endif /* TapsellVAST_h */
