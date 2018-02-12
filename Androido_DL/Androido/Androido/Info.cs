using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Androido
{
    class Info
    {
        Context context;


        public Info(Context context)
        {
            this.context = context;
        }

        public void Update_Text_Speech(string txt)
        {
            TextView text = (TextView)((Activity)context).FindViewById<TextView>(Resource.Id.textView2);
            text.Text = txt;
        }

        public void Update_Information(string txt)
        {
            TextView text = (TextView)((Activity)context).FindViewById<TextView>(Resource.Id.textView3);
            text.Text = txt;
        }
    }
}