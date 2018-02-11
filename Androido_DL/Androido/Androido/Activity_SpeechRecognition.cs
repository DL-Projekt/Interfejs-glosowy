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
    [Activity(Label = "Activity_SpeechRecognition")]
    public class Activity_SpeechRecognition : Activity
    {
        public bool isRecording;

        Work mWork;


        public void Update_Text2(string txt)
        {
            TextView text2 = (TextView)this.FindViewById<TextView>(Resource.Id.textView2);
            text2.Text = txt;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            mWork = new Work(this);    


            speech_recognition();
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
                        Update_Text2(textInput);
                        isRecording = mWork.Execute(textInput);
                    }
                    else Update_Text2("Nie zarejestrowalem mowy");
                }
            }

            if (isRecording) speech_recognition();
            else Finish();
        }
    }
}
