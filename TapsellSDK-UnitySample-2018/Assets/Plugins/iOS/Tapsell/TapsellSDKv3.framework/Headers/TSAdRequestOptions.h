//
//  TSAdRequestOptions.h
//  TapsellSDKv3
//
//  Created by Tapsell on 5/13/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#ifndef TSAdRequestOptions_h
#define TSAdRequestOptions_h

#import <Foundation/Foundation.h>

typedef enum CacheType : NSInteger {
    CacheTypeCached=1,
    CacheTypeStreamed=2
} CacheType;

@interface TSAdRequestOptions : NSObject

- (void)setCacheType:(CacheType) cacheType;

- (NSNumber* ) getCacheTypeNumber;

- (CacheType) getCacheType;

// serialization
-(NSArray<NSString*> *) keys;
-(BOOL) isValidValue: (id) value;

@property (nonatomic, strong, readonly) NSNumber* mCacheType;

@end


#endif /* TSAdRequestOptions_h */
