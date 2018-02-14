using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech;
using System.Threading;

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity, IRecognitionListener
    {
        TextView Text_Title;
        TextView Text_Speech;
        TextView Text_Information;
        TextView Text_Error;
        Button recButton;
        ImageView image;

        private SpeechRecognizer sr;
        public bool RecordingOn = false;

        Additional mAdditional;
        Work mWork;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);            

            mWork = new Work(this);
            mAdditional = new Additional(this);

            Text_Title = FindViewById<TextView>(Resource.Id.textView1);
            Text_Speech = FindViewById<TextView>(Resource.Id.textView2);
            Text_Information = FindViewById<TextView>(Resource.Id.textView3);
            Text_Error = FindViewById<TextView>(Resource.Id.textView4);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
            recButton = FindViewById<Button>(Resource.Id.btnRecord);

            sr = SpeechRecognizer.CreateSpeechRecognizer(this);
            sr.SetRecognitionListener(this);
            

            recButton.Click += delegate
            {
                if(!RecordingOn)
                {
                    RecordingOn = true;
                    speech_recognition();
                }                
            };
        }

        public void speech_recognition()
        {
            string test = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (test == "android.hardware.microphone")
            {
                if (RecordingOn)
                {
                    Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 4000);
                    intent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 4000);
                    intent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                    intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                    intent.PutExtra(RecognizerIntent.ExtraCallingPackage, "this package");
                    intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                    Thread.Sleep(1750);
                    sr.StartListening(intent);
                }
            }
            else mAdditional.Update_Information("Brak mikrofonu");
        }

        public void OnReadyForSpeech(Bundle @params)
        {

        }

        public void OnBeginningOfSpeech()
        {

        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {
            Text_Error.Text = error.ToString();
            if (error.ToString() == "NoMatch") speech_recognition();
            else
            {
                Thread.Sleep(1750);
                speech_recognition();
            }
        }

        public void OnResults(Bundle results)
        {
            var data = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            if (data.Count != 0)
            {
                string textInput = data[0];
                Text_Speech.Text = textInput;
                RecordingOn = mWork.Execute(textInput);
            }
            else mAdditional.Update_Information("Nie zarejestrowalem mowy");

           speech_recognition();
        }

        public void OnBufferReceived(byte[] buffer) { }

        public void OnEndOfSpeech() { }

        public void OnEvent(int eventType, Bundle @params) { }

        public void OnPartialResults(Bundle partialResults) { }

        public void OnRmsChanged(float rmsdB) { }
    }
}
