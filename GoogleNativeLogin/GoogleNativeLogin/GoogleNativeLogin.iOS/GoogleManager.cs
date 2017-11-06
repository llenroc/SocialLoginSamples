﻿using System;
using Foundation;
using Google.SignIn;
using GoogleNativeLogin.Services.Contracts;
using UIKit;

namespace GoogleNativeLogin.iOS
{
    public class GoogleManager : NSObject, IGoogleManager, ISignInDelegate, ISignInUIDelegate
    {
        private Action<GoogleNativeLogin.Models.GoogleUser, string> _onLoginComplete;
        private UIViewController _viewController { get; set; }

        public GoogleManager()
        {
            SignIn.SharedInstance.UIDelegate = this;
            SignIn.SharedInstance.Delegate = this;
        }

        public void Login(Action<GoogleNativeLogin.Models.GoogleUser, string> OnLoginComplete)
        {
            _onLoginComplete = OnLoginComplete;
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            SignIn.SharedInstance.SignInUser();
        }

        public void Logout()
        {
            SignIn.SharedInstance.SignOutUser();
        }

        public void DidSignIn(SignIn signIn, Google.SignIn.GoogleUser user, NSError error)
        {
            //throw new NotImplementedException();
        }

        [Export("signIn:didDisconnectWithUser:withError:")]
        public void DidDisconnect(SignIn signIn, GoogleUser user, NSError error)
        {
            // Perform any operations when the user disconnects from app here.
        }

        [Export("signInWillDispatch:error:")]
        public void WillDispatch(SignIn signIn, NSError error)
        {
            //myActivityIndicator.StopAnimating();
        }

        [Export("signIn:presentViewController:")]
        public void PresentViewController(SignIn signIn, UIViewController viewController)
        {
            _viewController.PresentViewController(viewController, true, null);
        }

        [Export("signIn:dismissViewController:")]
        public void DismissViewController(SignIn signIn, UIViewController viewController)
        {
            _viewController.DismissViewController(true, null);
        }
    }
}