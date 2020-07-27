#import <TapsellSDKv3/TapsellSDKv3.h>
#import "UnityAppController.h"
#import <Foundation/Foundation.h>


// Converts NSString to C style string by way of copy (Mono will free it)
#define TS_MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

// Converts C style string to NSString
#define TS_GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

// Converts C style string to NSString as long as it isnt empty
#define TS_GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

#define TS_NSSTRING_OR_EMPTY(x)                        (x ? x : @"")
#define TS_NSDICTIONARY_OR_EMPTY(x)                    (x ? x : @{})
#define TS_IS_STRING_SET(x)                            (x && ![x isEqualToString:@""])

void UnitySendMessage(const char *className, const char *methodName, const char *param);

void TSSafeUnitySendMessage(const char *methodName, const char *param) {
    if (methodName == NULL) {
        methodName = "";
    }
    if (param == NULL) {
        param = "";
    }
    UnitySendMessage("TapsellManager", methodName, param);
}

@interface TapsellUnityController : NSObject

- (void) initializeWithAppKey:(NSString* )appkey;
- (void) setAppUserId:(NSString* )appUserId;
- (NSString*)getAppUserId;
- (void) setDebugMode:(NSString*)debugMode;
- (NSString*) isDebugMode;
- (void) requestAdForZone:(NSString* )zoneId isCached:(NSString*)isCached;
- (NSString*) getVersion;
- (void) showAd:(NSString*)adId backDisabled:(NSString*)backDisabled rotationMode:(NSNumber*)rotationMode showDialog:(NSString*)showDialog;

@end

@implementation TapsellUnityController

+ (TapsellUnityController*) sharedInstance {
    static TapsellUnityController * instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[TapsellUnityController alloc] init];
        [[TapsellExtraPlatformsController sharedInstance]
         setPlatformControllerOnAdAvailable:^(TapsellAd * _Nullable ad) {
             NSString* result = [NSString stringWithFormat:@"{\"adId\":\"%@\",\"zoneId\":\"%@\"}",[ad getId],ad.zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyAdAvailable"),TS_MakeStringCopy(result));
         } onNoAdAvailable:^(NSString * _Nullable zoneId) {
             NSString* result = [NSString stringWithFormat:@"{\"zoneId\":\"%@\"}",zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyNoAdAvailable"),TS_MakeStringCopy(result));
         } onError:^(NSString * _Nullable error, NSString * _Nullable zoneId) {
             NSString* result = [NSString stringWithFormat:@"{\"error\":\"%@\",\"zoneId\":\"%@\"}",error,zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyError"),TS_MakeStringCopy(result));
         } onExpiring:^(TapsellAd * _Nullable ad) {
             NSString* result = [NSString stringWithFormat:@"{\"adId\":\"%@\",\"zoneId\":\"%@\"}",[ad getId],ad.zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyExpiring"),TS_MakeStringCopy(result));
         } onOpened:^(TapsellAd * _Nullable ad) {
             NSString* result = [NSString stringWithFormat:@"{\"adId\":\"%@\",\"zoneId\":\"%@\"}",[ad getId],ad.zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyOpened"),TS_MakeStringCopy(result));
         } onClosed:^(TapsellAd * _Nullable ad) {
             NSString* result = [NSString stringWithFormat:@"{\"adId\":\"%@\",\"zoneId\":\"%@\"}",[ad getId],ad.zoneId];
             TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyClosed"),TS_MakeStringCopy(result));
         }];
        [TapsellExtraPlatformsController setAdShowFinishedCallback:^(TapsellAd *ad, BOOL completed) {
            NSString* isCompleted = @"";
            if(completed){
                isCompleted = @"true";
            }
            else
            {
                isCompleted = @"false";
            }
            NSString* isRewarded = @"";
            if([ad isRewardedAd]){
                isRewarded = @"true";
            }
            else
            {
                isRewarded = @"false";
            }
            NSString* result = [NSString stringWithFormat:@"{\"adId\":\"%@\",\"zoneId\":\"%@\",\"completed\":%@,\"rewarded\":%@}",[ad getId],ad.zoneId, isCompleted, isRewarded];
            TSSafeUnitySendMessage(TS_MakeStringCopy(@"NotifyAdShowFinished"),TS_MakeStringCopy(result));
        }];
    });
    return instance;
}

- (void) initializeWithAppKey:(NSString* )appkey{
    [TapsellExtraPlatformsController initializeWithAppKey:appkey];
}

- (void) setAppUserId:(NSString* )appUserId{
    [TapsellExtraPlatformsController setAppUserId:appUserId];
}

- (NSString*)getAppUserId{
    return [TapsellExtraPlatformsController getAppUserId];
}

- (NSString*) isDebugMode{
    BOOL debugMode = [TapsellExtraPlatformsController isDebugMode];
    if(debugMode)
    {
        return @"true";
    }
    else
    {
        return @"false";
    }
}

- (void) setDebugMode:(NSString*)debugMode{
    BOOL debugModeEnabled = YES;
    if([@"true" isEqualToString:debugMode]){
        debugModeEnabled = YES;
    }
    else{
        debugModeEnabled = NO;
    }
    [TapsellExtraPlatformsController setDebugMode:debugModeEnabled];
}

- (void) requestAdForZone:(NSString* )zoneId isCached:(NSString*)isCached{
    BOOL cached = YES;
    if([@"true" isEqualToString:isCached]){
        cached = YES;
    }
    else{
        cached = NO;
    }
    [TapsellExtraPlatformsController requestAdForZone:zoneId isCached:cached];
}

- (void) showAd:(NSString*)adId backDisabled:(NSString*)backDisabled rotationMode:(NSNumber*)rotationMode showDialog:(NSString*)showDialog{
    TSAdShowOptions* showOptions = [[TSAdShowOptions alloc] init];
    if([@"true" isEqualToString:backDisabled]){
        [showOptions setBackDisabled:YES];
    }
    else{
        [showOptions setBackDisabled:NO];
    }
    if([@"true" isEqualToString:showDialog]){
        [showOptions setShowDialoge:YES];
    }
    else{
        [showOptions setShowDialoge:NO];
    }
    [showOptions setOrientationNumber:rotationMode];
    [[TapsellExtraPlatformsController sharedInstance] showAd:adId withOptions:showOptions];
}

- (NSString*) getVersion{
    return [TapsellExtraPlatformsController getVersion];
}

@end

extern "C" {
    void _TSInitialize(const char *appkey)
    {
        [[TapsellUnityController sharedInstance] initializeWithAppKey:TS_GetStringParam(appkey)];
    }
    
    void _TSRequestAdForZone(const char *zoneId, const char* cached)
    {
        [[TapsellUnityController sharedInstance] requestAdForZone:TS_GetStringParam(zoneId) isCached:TS_GetStringParam(cached)];
    }
    
    void _TSShowAd(const char *adId, const char* backDisabled, const int rotationMode, const char* showDialog)
    {
        [[TapsellUnityController sharedInstance] showAd:TS_GetStringParam(adId) backDisabled:TS_GetStringParam(backDisabled) rotationMode:[NSNumber numberWithInt:rotationMode] showDialog:TS_GetStringParam(showDialog)];
    }
    
    char *_TSGetVersion()
    {
        return TS_MakeStringCopy([[TapsellUnityController sharedInstance] getVersion]);
    }
    
    void _TSSetDebugMode(const char*debugMode)
    {
        [[TapsellUnityController sharedInstance] setDebugMode:TS_GetStringParam(debugMode)];
    }
    
    char *_TSIsDebugMode()
    {
        return TS_MakeStringCopy([[TapsellUnityController sharedInstance] isDebugMode]);
    }
    
    void _TSSetAppUserId(const char *appUserId)
    {
        [[TapsellUnityController sharedInstance] setAppUserId:TS_GetStringParam(appUserId)];
    }
    
    char *_TSGetAppUserId()
    {
        return TS_MakeStringCopy([[TapsellUnityController sharedInstance] getAppUserId]);
    }
}
