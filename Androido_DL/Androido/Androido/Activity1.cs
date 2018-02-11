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
using Com.Joanzapata.Pdfview;
using Android.Webkit;

namespace Androido
{
    [Activity(Label = "Activity1")]
    public class Activity1 : Activity
    { 
        PDFView pdfView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            // Create your application here

            var webView = FindViewById<WebView>(Resource.Id.webView);
            WebSettings settings = webView.Settings;
            settings.JavaScriptEnabled = true;
            webView.SetWebChromeClient(new WebChromeClient());
            webView.LoadUrl("https://www.youtube.com/watch?v=knXqreVC8QA");
           /* try
            {
                pdfView = FindViewById<PDFView>(Resource.Id.pdfView);
                pdfView.FromAsset("DMA.pdf").Load();
            }
            catch
            {
                Toast.MakeText(this, "Spróbuj ponownie!", ToastLength.Long).Show();
            }*/
        }
    }
}