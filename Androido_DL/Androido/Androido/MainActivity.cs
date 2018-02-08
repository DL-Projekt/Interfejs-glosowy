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
using System.Collections.Generic;//dodane

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity
    { 
     #region deklaracje
        TextView text1;
        EditText edit;
        Button b1;
        TextView text2;
        private EventArgs e;      
        string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Music/test.mp3");
        bool IsPlaying = false;
        MediaPlayer player;
        ImageView image;
        string pathPic;
        #endregion


        //dodane
        private bool isRecording;
        private readonly int VOICE = 10;
        private TextView textBox;
        private Button recButton;
        //

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            #region inicjalizacja 
            SetContentView(Resource.Layout.Main);
            text1 = FindViewById<TextView>(Resource.Id.textView1);
            edit = FindViewById<EditText>(Resource.Id.editText1);
            b1 = FindViewById<Button>(Resource.Id.button1);
            text2 = FindViewById<TextView>(Resource.Id.textView2);
            image = FindViewById<ImageView>(Resource.Id.imageView1);
                     
            string command = "";           
            text1.Text = "Wpisz komendę";
            #endregion

            //to już niepotrzebne
            b1.Click += delegate
            {
                command = edit.Text;
                text1.Text = command;
                Toast.MakeText(this, "Wybrana komenda to :" + command, ToastLength.Long).Show();
                Execute(b1, command);
            };

            edit.TextChanged += delegate
            {
                command = edit.Text;
                text1.Text = command;                
                Execute(b1, command);
            };







            //dodane


            //base.OnCreate(bundle);

            // set the isRecording flag to false (not recording)
            isRecording = false;

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);

            // get the resources from the layout
            recButton = FindViewById<Button>(Resource.Id.btnRecord);
            textBox = FindViewById<TextView>(Resource.Id.textYourText);

            // check to see if we can actually record - if we can, assign the event to the button
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
             {
                 // no microphone, no recording. Disable the button and output an alert
                 var alert = new AlertDialog.Builder(recButton.Context);
                 alert.SetTitle("You don't seem to have a microphone to record with");
                 alert.SetPositiveButton("OK", (sender, e) =>
                 {
                     textBox.Text = "No microphone present";
                     recButton.Enabled = false;
                     return;
                 });

                 alert.Show();
             }
             else
                 recButton.Click += delegate
                 {
                     // change the text on the button
                     recButton.Text = "End Recording";
                     isRecording = !isRecording;
                     if (isRecording)
                     {
                         // create the intent and start the activity
                         var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                         voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

                         // put a message on the modal dialog
                         //voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));

                         // if there is more then 1.5s of silence, consider the speech over
                         voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                         voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                         voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                         voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                         // you can specify other languages recognised here, for example
                         // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
                         // if you wish it to recognise the default Locale language and German
                         // if you do use another locale, regional dialects may not be recognised very well

                         voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                         StartActivityForResult(voiceIntent, VOICE);
                     }
                 };
             //


     


        }



        //dodane
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

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        //textBox.Text = String.Empty;
                        textBox.Text = textInput;
                        /////
                        //command = edit.Text;
                       // text1.Text = String.Empty;
                        text1.Text = textInput;
                        Execute(b1, textInput);
                    }
                    else
                        textBox.Text = "No speech was recognised";
                    // change the text back on the button
                    recButton.Text = "Start Recording";
                }
            }

            base.OnActivityResult(requestCode, resultVal, data);
        }
        //



















        public void Execute(object sender, string command)
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

