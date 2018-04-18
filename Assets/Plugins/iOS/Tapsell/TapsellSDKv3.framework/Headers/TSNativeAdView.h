//
//  TSNativeAdView.h
//  TapsellSDKv3
//
//  Created by Tapsell on 11/16/17.
//  Copyright © 2017 Tapsell. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface TSNativeAdView : UIView

@property(nonatomic, readwrite) NSInteger titleLabelTag;
@property(nonatomic, readwrite) NSInteger descriptionLabelTag;
@property(nonatomic, readwrite) NSInteger logoImageTag;
@property(nonatomic, readwrite) NSInteger callToActionButtonTag;

-(BOOL) loadTitleIntoLabel:(NSString*)title;
-(BOOL) loadDescriptionIntoLabel:(NSString*)description;
-(BOOL) loadLogoIntoImage:(NSString*)logoUrl;
-(BOOL) loadCallToActionIntoButton:(NSString*)callToActionText;
-(void) commonInit;

@end
