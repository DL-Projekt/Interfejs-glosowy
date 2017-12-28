﻿using Android.App;
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
        string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Music/test.mp3");
        bool IsPlaying = false;
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
            //GetMusicFiles(int choice);
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
    }
}

