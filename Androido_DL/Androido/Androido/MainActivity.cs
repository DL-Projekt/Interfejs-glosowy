﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech;


//text2 to jest to co powiedzialem, a text3 to bledy

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView text1;
        TextView text2;
        TextView text3;
        Button recButton;
        ImageView image;


        private EventArgs e;






        public bool isRecording;

        Work mWork;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            text1 = FindViewById<TextView>(Resource.Id.textView1);
            text2 = FindViewById<TextView>(Resource.Id.textView2);
            text3 = FindViewById<TextView>(Resource.Id.textView3);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
            recButton = FindViewById<Button>(Resource.Id.btnRecord);

            text1.Text = "Wpisz komendę";

            mWork = new Work(this);



            recButton.Click += delegate
            {
                speech_recognition();
            };
        }






        public void speech_recognition()
        {
            isRecording = true;

            if (check_micro(Android.Content.PM.PackageManager.FeatureMicrophone)) //check to see if we can actually record - if we can, assign the event to the button
            {
                var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                // if there is more then 1.5s of silence, consider the speech over
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default); //język
                StartActivityForResult(voiceIntent, mWork.VOICE);
            }
        }

        public bool check_micro(string rec)
        {
            if (rec != "android.hardware.microphone") return false; // no microphone, no recording. Disable the button and output an alert
            else return true;
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == mWork.VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];
                        text2.Text = textInput;
                       // Update_Text2(textInput);
                        isRecording = mWork.Execute(textInput);
                    }
                    else text2.Text = "Nie zarejestrowalem mowy";
                }
            }

            if (isRecording) speech_recognition();
           // else Finish();
        }
    }
}
