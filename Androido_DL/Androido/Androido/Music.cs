using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace Androido
{
    class Music : MainActivity
    {
        static string music_path = "Music";

        MediaPlayer player;
        bool IsPlaying = false;
        int music_number = 1;        
        int music_max = Android.OS.Environment.GetExternalStoragePublicDirectory(music_path).List().Length;       


        public void Run_Music()
        {
            if (!IsPlaying) player = new MediaPlayer();
            else
            {
                player.Stop();
                IsPlaying = false;
                player = new MediaPlayer();
            }

            try
            {
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/Music/");
                path = GetMusicFiles(music_number);
                //text2.Text = path + "          " + music_number.ToString();
                player.SetDataSource(path);
                player.Prepare();
                player.Start();
                IsPlaying = true;
            }
            catch
            {
               // Toast.MakeText(this, "Nie mam czego odtworzyć!", ToastLength.Long).Show();
            }
        }

        

        public bool Action_List(string command)
        {
            switch (command)
            {
                case "muzyka":
                    Run_Music();
                    return true;

                case "nastepna":
                    if (IsPlaying) Next_Music();
                    return true;

                case "poprzednia":
                    if (IsPlaying) Previous_Music();
                    return true;

                case "wylacz":
                    if (IsPlaying) Off_Music();
                    return true;

                default:
                    //Show_Message("Nie znam takiej komendy!");
                    // Context context1 = get_context();
                    ///* if (IsPlaying) */Toast.MakeText(ApplicationContext(), "Nie znam takiej komendy!", ToastLength.Long).Show();
                    return true;
            }
        }


        public void Next_Music()
        {
            music_number++;
            if (music_number >= music_max) music_number = music_max - 1;

            Run_Music();
        }

        public void Previous_Music()
        {
            music_number--;
            if (music_number < 0) music_number = 0;

            Run_Music();
        }

        public void Off_Music()
        {
            player.Stop();
            IsPlaying = false;
        }


        public string GetMusicFiles(int choice)
        {
            string s = "";
            string[] x = new string[100];
            x = Android.OS.Environment.GetExternalStoragePublicDirectory(music_path).List();

            try
            {
                s = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/" + music_path + "/") + x[choice];
                
            }
            catch
            {
               // Toast.MakeText(this, "Zła ścieżka do muzyki", ToastLength.Long).Show();
            }
            return s;
        }
    }
}