using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;

namespace Androido
{
    [Activity(Label = "Activity_Camera")]
    public class Activity_Camera : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            camera_run();
        }

        public void camera_run()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivity(intent);
            Finish();
        }
    }
}