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
    class Music
    {        
        static string music_path = "Music";

        Context context;
        MediaPlayer player;
        bool IsPlaying = false;
        int music_number = 1;        
        int music_max = Android.OS.Environment.GetExternalStoragePublicDirectory(music_path).List().Length;

        Info mInfo;


        public Music(Context context)
        {
            this.context = context;
            mInfo = new Info(context);
        }        

        public bool Action_List(string command)
        {
            switch (CheckInput(command))
            {
                case "muzyka":
                    if (!IsPlaying) Run_Music();
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
                    return false;
            }
        }

        private string CheckInput(string command)
        {
            if (command == "Muzyko graj" || command == "muzyko graj" | command == "Muzyka" || command == "MUZYKA" || command == "MUZYKA " || command == "Muzyka " || command == "muzyka ") command = "muzyka";
            else if (command == "Nastepna" || command == "nastepna" || command == "Następna" || command == "następna") command = "nastepna";
            else if (command == "Poprzednia" || command == "poprzednia") command = "poprzednia";
            else if (command == "Wyłącz" || command == "wyłącz") command = "wylacz";

            return command;
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
                mInfo.Update_Information(path + "          " + music_number.ToString());
                player.SetDataSource(path);
                player.Prepare();
                player.Start();
                IsPlaying = true;
            }
            catch
            {
                mInfo.Update_Information("Błąd z muzyką");
            }
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
                mInfo.Update_Information("Zła ścieżka do muzyki");
            }
            return s;
        }
    }
}