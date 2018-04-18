//
//  BannerAdView.h
//  TapsellSDKv3
//
//  Created by Tapsell on 10/26/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <UIKit/UIKit.h>

typedef enum BannerType : NSInteger {
    BANNER_320x50=1,
    BANNER_320x100=2,
    BANNER_250x250=3,
    BANNER_300x250=4
} BannerType;

@interface TSBannerAdView : UIWebView <UIWebViewDelegate>
    @property (nonatomic, strong) NSNumber* bannerType;
    @property (nonatomic, strong) NSString* zoneId;
    @property (nonatomic, strong) void (^onRequestFilled)(void);
    @property (nonatomic, strong) void (^onHiddenBannerClicked)(void) ;
    @property (nonatomic, strong) void (^onNoAdAvailable)(void);
-(void) loadAdWithZoneId:(NSString*)zoneId andBannerType:(NSNumber*)bannerType
         onRequestFilled:(void (^_Nullable)()) onRequestFilled
     onHideBannerClicked:(void (^_Nullable)()) onHideBannerClicked
         onNoAdAvailable:(void (^_Nullable)()) onNoAdAvailable;
+ (void) loadAdWithZoneId:(NSString*)zoneId andBannerType:(NSNumber*)bannerType
          andHorizGravity:(NSNumber*)horiz andVertGravity:(NSNumber*)vert;
@end

