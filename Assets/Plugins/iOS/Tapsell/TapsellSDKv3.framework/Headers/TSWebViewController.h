//
//  TSWebViewController.h
//  Tapsell
//
//  Created by Ali Soltani-Farani on 8/28/15.
//  Copyright (c) 2015 Pegah. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol TSWebViewControllerDelegate;

@interface TSWebViewController : UIViewController <UIWebViewDelegate>

@property (nonatomic, strong) NSString * urlString;

@property (nonatomic, weak) id<TSWebViewControllerDelegate> delegate;

- (BOOL)webView:(UIWebView*)webView shouldStartLoadWithRequest:(NSURLRequest*)request navigationType:(UIWebViewNavigationType)navigationType;

@end

@protocol TSWebViewControllerDelegate <NSObject>

-(void) tsWebViewControllerWasDismissed;

-(void) tsWebViewBridgeClose;

-(void) tsWebViewBridgeDismiss;

-(void) tsWebViewBridgeReplay;

-(void) tsWebViewBridgeOpenType:(NSString*)type andTarget:(NSString*)target andDescription:(NSString*)targetDescription;

@end
