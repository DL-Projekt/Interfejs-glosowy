using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech;

namespace Androido
{
    class SpeechRecognition
    {
        Context context;
        public bool isRecording;
        public readonly int VOICE = 10;
        
        public SpeechRecognition(Context context)
        {
            this.context = context;
        }

        public void Update_Text2(string txt)
        {
            TextView text2 = (TextView)((Activity)context).FindViewById<TextView>(Resource.Id.textView2);
            text2.Text = txt;
        }


        public void speech_recognition()
        {            
            //string rec = Android.Content.PM.PackageManager.FeatureMicrophone;

            if(check_micro(Android.Content.PM.PackageManager.FeatureMicrophone)) //check to see if we can actually record - if we can, assign the event to the button
            {
                if (isRecording)
                {
                    isRecording = false;

                    var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                    // if there is more then 1.5s of silence, consider the speech over
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 15000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 15000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default); //język
                    ((Activity)context).StartActivityForResult(voiceIntent, VOICE);
                 }
            }
        }

        public bool check_micro(string rec)
        {
            if (rec != "android.hardware.microphone") return false; // no microphone, no recording. Disable the button and output an alert
            else return true;
        }

        public void mActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];
                        Update_Text2(textInput);
                        //Execute(textInput);
                    }
                    else
                    {
                        speech_recognition();
                        Update_Text2("Nie zarejestrowalem mowy");
                    }

                }

                isRecording = true;
            }

            if (requestCode == 0)
            {
                //if (resultVal == Result.Ok)
                {
                    //Toast.MakeText(this, "Wątek kamery, elo", ToastLength.Long).Show();
                }

                isRecording = false;
            }
            speech_recognition();
        }
    }
}