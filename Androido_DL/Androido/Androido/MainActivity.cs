using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android;
using Android.Content;
using Android.Provider;
using Android.Media;
using System.IO;
using Android.Graphics;
using Android.Speech; //dodane
//using System.Collections.Generic;//dodane

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity
    { 
        TextView text1;
        TextView text2;
        private EventArgs e;      
        string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Music/test.mp3");
        bool IsPlaying = false;
        MediaPlayer player;
        ImageView image;
        string pathPic;

        private bool isRecording;
        private readonly int VOICE = 10;
        private Button recButton;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            text1 = FindViewById<TextView>(Resource.Id.textView1);
            text2 = FindViewById<TextView>(Resource.Id.textView2);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
            recButton = FindViewById<Button>(Resource.Id.btnRecord);

            text1.Text = "Wpisz komendę";            

            isRecording = false;            

            // check to see if we can actually record - if we can, assign the event to the button
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
             {
                 // no microphone, no recording. Disable the button and output an alert
                 var alert = new AlertDialog.Builder(recButton.Context);
                 alert.SetTitle("You don't seem to have a microphone to record with");
                 alert.SetPositiveButton("OK", (sender, e) =>
                 {
                     text2.Text = "No microphone present";
                     recButton.Enabled = false;
                     return;
                 });

                 alert.Show();
             }
            else
            recButton.Click += delegate
            {
                isRecording = true;
                if (isRecording)
                {
                    // create the intent and start the activity
                    var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                    // if there is more then 1.5s of silence, consider the speech over
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default); //język
                    StartActivityForResult(voiceIntent, VOICE);
                }
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];
                        text1.Text = textInput;
                        Execute(textInput);
                    }
                    else text2.Text = "No speech was recognised";
                }
            }

            isRecording = false;

            base.OnActivityResult(requestCode, resultVal, data);
        }

        public void Execute(string command)
        {
            command = CheckInput(command);
            

            switch (command)
            {
                case "tak":
                    text2.Text = "tak";
                    break;

                case "muzyka":
                    if (!IsPlaying) player = new MediaPlayer();
                    else
                    {
                        player.Stop();
                        IsPlaying = false;
                        player = new MediaPlayer();
                    }

                    Random rnd = new Random();
                    int x = rnd.Next(1,20);
                    
                    try
                    {
                        path = GetMusicFiles(x);
                        text2.Text = path + "          " + x.ToString();
                        player.SetDataSource(path);
                        player.Prepare();
                        player.Start();
                        IsPlaying = true;
                    }
                    catch
                    {
                        Toast.MakeText(this, "Nie mam czego odtworzyć!", ToastLength.Long).Show();
                    }

                    break;

                case "aparat":
                    text2.Text = "cyk cyk!";
                    Camera_Click(this, e);
                    break;

                case "obrazek":
                    
                    Random rnd2 = new Random();
                    int x2 = rnd2.Next(1, 20);
                     
                    try
                    {
                        pathPic = GetPictureFiles(x2);
                        text2.Text = "photo!";
                        Bitmap bmImg = BitmapFactory.DecodeFile(pathPic);
                        image.SetImageBitmap(bmImg);
                    }
                    catch { Toast.MakeText(this, "NULL!", ToastLength.Long).Show(); }
                    break;
                default:
                    text2.Text = "Nie znam takiej komendy.";
                    break;

            }
        }


        private string CheckInput(string command)
        {
            if (command == "Aparat" || command == "APARAT" || command == "APARAT " || command == "Aparat " || command == "aparat ") command = "aparat";
            if (command == "Muzyka" || command == "MUZYKA" || command == "MUZYKA " || command == "Muzyka " || command == "muzyka ") command = "muzyka";
            if (command == "Tak" || command == "TAK" || command == "TAK " || command == "Tak " || command == "tak ") command = "tak";
            if (command == "Obrazek" || command == "OBRAZEK" || command == "Obrazek " || command == "obrazek " || command == "OBRAZEK ") command = "obrazek";

            return command;
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }               

        public string GetMusicFiles(int choice)
        {
            string s="";
            string[] x = new string[100];            
            x=Android.OS.Environment.GetExternalStoragePublicDirectory("Music").List(); 
            
            try
            {
             s = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Music/") + x[choice];
            }
            catch
            {
                Toast.MakeText(this, "Spróbuj ponownie!", ToastLength.Long).Show();
            }
            return s;
        }

        public string GetPictureFiles(int choice)
        {
            string s = "";
            string[] x = new string[100];
            x = Android.OS.Environment.GetExternalStoragePublicDirectory("Pictures").List();

            try
            {
                s = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Pictures/") + x[choice];
            }
            catch
            {
                Toast.MakeText(this, "Spróbuj ponownie!", ToastLength.Long).Show();
            }
            return s;
        }
    }
}
