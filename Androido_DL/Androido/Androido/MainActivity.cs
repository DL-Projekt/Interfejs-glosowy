using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android;

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView text1;
        EditText edit;
        Button b1;
        TextView text2;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            text1 = FindViewById<TextView>(Resource.Id.textView1);
            edit = FindViewById<EditText>(Resource.Id.editText1);
            b1 = FindViewById<Button>(Resource.Id.button1);
            text2 = FindViewById<TextView>(Resource.Id.textView2);
            string command = "";

            text1.Text = "Wpisz komendę";

            b1.Click += delegate
            {
                command = edit.Text;
                text1.Text = command;
                Toast.MakeText(this, "Wybrana komenda to :" + command, ToastLength.Long).Show();
                Execute(b1,command);
            }; 
        }

        public void Execute(object sender,string command)
        {
            switch(command)
            {
                case "tak": text1.Text = "tak!";
                    break;
                default:    text1.Text = "nie!";
                    break;

            }
        }


    }
}

