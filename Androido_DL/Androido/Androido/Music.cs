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
        int music_max = Android.OS.Environment.GetExternalStoragePublicDirectory(music_path).List().Length;
        int music_number = 0;                

        Additional mAdditional;


        public Music(Context context)
        {
            this.context = context;
            mAdditional = new Additional(context);
        }        

        public bool Action_List(string command)
        {
            switch (CheckInput(command))
            {
                case "muzyka":
                    if (!IsPlaying)
                    {
                        mAdditional.Update_Information("Odpalam");
                        Run_Music();
                        return true;
                    }
                    else return true;

                case "następna muzyka":
                    if (IsPlaying)
                    {
                        mAdditional.Update_Information("Ok następna");
                        Next_Music();
                        return true;
                    }
                    else return false;

                case "poprzednia muzyka":
                    if (IsPlaying)
                    {
                        mAdditional.Update_Information("Ok poprzednia");
                        Previous_Music();
                        return true;
                    }
                    else return false;

                case "wyłącz":
                    if (IsPlaying)
                    {
                        mAdditional.Update_Information("Ok wyłączam");
                        Off_Music();
                        return true;
                    }
                    else return false;

                default:
                    return false;
            }
        }

        private string CheckInput(string command)
        {
            if (command == null) return "Error";
            else command = command.ToLower();

            if (mAdditional.CalculateSimilarity(command, "muzyka") > mAdditional.minimum_of_acceptance) command = "muzyka";
            else if (mAdditional.CalculateSimilarity(command, "następna muzyka") > mAdditional.minimum_of_acceptance) command = "następna muzyka";
            else if (mAdditional.CalculateSimilarity(command, "poprzednia muzyka") > mAdditional.minimum_of_acceptance) command = "poprzednia muzyka";
            else if (mAdditional.CalculateSimilarity(command, "wyłącz") > mAdditional.minimum_of_acceptance) command = "wyłącz";

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
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/" + music_path + "/");
                path = GetMusicFiles(music_number);
                player.SetDataSource(path);
                player.Prepare();
                player.Start();
                IsPlaying = true;
            }
            catch
            {
                mAdditional.Update_Information("Błąd z muzyką");
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
                mAdditional.Update_Information("Zła ścieżka do muzyki");
            }
            return s;
        }
    }
}