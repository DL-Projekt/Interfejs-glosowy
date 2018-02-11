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
using Android.Speech;

namespace Androido
{
    [Activity(Label = "Activity_SpeechRecognition2")]
    public class Activity_SpeechRecognition2 : Activity, IRecognitionListener
    {
        private SpeechRecognizer mSpeechRecognizer;
        private Intent mSpeechRecognizerIntent;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
           // SetContentView(Resource.Layout.Activity_SpeechRecognition2);


            mSpeechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(this);
            mSpeechRecognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, PackageName);
            //mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraPrompt, GetString(Resource.String.VoiceCommandsDesc));
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, this.Resources.Configuration.Locale.Language);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 5);
            Button StartListening = FindViewById<Button>(Resource.Id.Button1);
            StartListening.Click += StartListening_Click;
        }

        void StartListening_Click(object sender, EventArgs e)
        {
            mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
        }

        public void OnBeginningOfSpeech()
        {
            throw new NotImplementedException();
        }

        public void OnBufferReceived(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public void OnEndOfSpeech()
        {
            throw new NotImplementedException();
        }

        public void OnError(SpeechRecognizerError error)
        {
            throw new NotImplementedException();
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            throw new NotImplementedException();
        }

        public void OnPartialResults(Bundle partialResults)
        {
            throw new NotImplementedException();
        }

        public void OnReadyForSpeech(Bundle @params)
        {
            throw new NotImplementedException();
        }

        public void OnResults(Bundle results)
        {
            IList<string> matches = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
        }

        public void OnRmsChanged(float rmsdB)
        {
            throw new NotImplementedException();
        }
    }
}