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
    [Activity(Label = "Activity2")]
    public class Activity2 : Activity
    {
        WebView webView;
        EditText textUrl;
        Button btnLoad;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout2);
            btnLoad = FindViewById<Button>(Resource.Id.btnLoad);
            webView = FindViewById<WebView>(Resource.Id.webView);
            textUrl = FindViewById<EditText>(Resource.Id.txtUrl);




            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;

            btnLoad.Click += (s, e) =>
            {
                if (!textUrl.Text.Contains("http://"))
                {
                    string address = textUrl.Text;
                    textUrl.Text = String.Format("http://(0)", address);
                }
                webView.LoadUrl(textUrl.Text);
            };
            // Create your application here
        }
    }
}
