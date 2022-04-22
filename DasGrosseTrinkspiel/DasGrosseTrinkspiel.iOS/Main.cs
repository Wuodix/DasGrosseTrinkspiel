using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace DasGrosseTrinkspiel.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;

            if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.FromRGBA(253,157,40,255);
            }
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
