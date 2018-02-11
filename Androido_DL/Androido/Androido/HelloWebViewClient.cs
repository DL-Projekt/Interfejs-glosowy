using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

namespace Androido
{
    public class HelloWebViewClient : WebViewClient
    {
        public Activity mActivity;
        public HelloWebViewClient(Activity mActivity)
        {
            this.mActivity = mActivity;
        }
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            Toast.MakeText(mActivity, "Toast Message",
                                 ToastLength.Long).Show();
            return true;
        }
    }
}