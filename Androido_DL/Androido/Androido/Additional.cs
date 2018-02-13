using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Speech.Tts;

namespace Androido
{
    class Additional : Activity, TextToSpeech.IOnInitListener
    {
        Context context;

        TextToSpeech textToSpeech;

        public double minimum_of_acceptance = 0.7;


        public Additional(Context context)
        {
            this.context = context;
            textToSpeech = new TextToSpeech(context, this, "com.google.android.tts");
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
            textToSpeech.Speak(txt, QueueMode.Flush, null);
        }


        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            textToSpeech.SetLanguage(Java.Util.Locale.Default);
        }
        

        public int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
    }
}