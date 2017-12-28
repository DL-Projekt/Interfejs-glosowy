using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android;
using Android.Content;
using Android.Provider;
using Android.Media;
using System.IO;

namespace Androido
{
    [Activity(Label = "Androido", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView text1;
        EditText edit;
        Button b1;
        TextView text2;
        private EventArgs e;
        MediaPlayer _player;
        static string filename = "/storage/emulated/0/Download/Bitamina - Dom.mp3";
        static string filename2 = "test.mp3";
        //string path = Android.OS.Environment.GetExternalStoragePublicDirectory.Path();
        //string path = Android.OS.Environment.DirectoryDownloads + "/" + filename;
       // string path2 = "Bitamina - Dom.mp3";
        string path3 = Android.OS.Environment.DirectoryMusic + "/" + filename2;
         MediaPlayer player;

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
            //_player = MediaPlayer.Create(this, Android.OS.Environment.DirectoryMusic);
            text1.Text = "Wpisz komendę";

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
                Toast.MakeText(this, "Wybrana komenda to :" + command, ToastLength.Long).Show();
                Execute(b1, command);
            };
        }
        
        public void Execute(object sender, string command)
        {

            if (command == "Aparat" || command == "APARAT" || command == "APARAT " || command == "Aparat " || command == "aparat ") command = "aparat";
            if (command == "Muzyka" || command == "MUZYKA" || command == "MUZYKA " || command == "Muzyka " || command == "muzyka ") command = "muzyka";
            if (command == "Tak" || command == "TAK" || command == "TAK " || command == "Tak " || command == "tak ") command = "tak";

            switch (command)
            {
                case "tak":
                    text2.Text = "tak";
                    break;

                case "muzyka":
                    text2.Text = path3;
                    try
                    {
                        _player.SetDataSource("https://www.searchgurbani.com/audio/sggs/1.mp3");
                        _player.Prepare();
                        _player.Start();
                        _player.Looping = true;
                    }
                    catch
                    {
                        Toast.MakeText(this, "Nie mam czego odtworzyć!", ToastLength.Long).Show();
                    }

                    try
                    {
                        StartPlayer(filename);
                    }
                    catch {
                        Toast.MakeText(this, "CHO CHO CHO", ToastLength.Long).Show();
                    }

                    try
                    {
                        _player.SetDataSource(path3);
                        _player.Prepare();
                        _player.Start();
                        _player.Looping = true;
                    }
                    catch
                    {
                        Toast.MakeText(this, "Nie mam czego odtworzyć!", ToastLength.Long).Show();
                    }

                    try
                    {
                        var mp3TestFile = "https://archive.org/download/testmp3testfile/mpthreetest.mp3";
                        player = new MediaPlayer();
                        player.SetAudioStreamType(Android.Media.Stream.Music);
                        
                        player.Prepare();
                        player.Start();
                    }
                    catch
                    {
                        Toast.MakeText(this, "Nie mam czego odtworzyć!", ToastLength.Long).Show();
                    }
                    break;

                case "aparat":
                    text2.Text = "zdjęcie!";
                    Camera_Click(this, e);
                    break;


                default:
                    text2.Text = "Nie znam takiej komendy.";
                    break;

            }
        }
        
        private void Camera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }

        
        public void StartPlayer(String filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                player.Reset();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
        }
    }
}

