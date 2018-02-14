using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Androido
{
    class Image
    {
        static string image_path = "Pictures";

        Context context;
        string pathPic;
        bool IsImage = false;
        int image_max = Android.OS.Environment.GetExternalStoragePublicDirectory(image_path).List().Length;
        int image_number = 1;

        Additional mAdditional;


        public Image(Context context)
        {
            this.context = context;
            mAdditional = new Additional(context);
        }

        public bool Action_List(string command)
        {
            switch (CheckInput(command))
            {
                case "obrazek":
                    mAdditional.Update_Information("Wrzucam obrazek");
                    Run_Image();
                    return true;

                case "następny obrazek":
                    if (IsImage)
                    {
                        mAdditional.Update_Information("Wrzucam następny obrazek");
                        Next_Image();
                        return true;
                    }
                    else return false;

                case "poprzedni obrazek":
                    if (IsImage)
                    {
                        mAdditional.Update_Information("Wrzucam poprzedni obrazek");
                        Previous_Image();
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

            if (mAdditional.CalculateSimilarity(command, "obrazek") > mAdditional.minimum_of_acceptance) command = "obrazek";
            else if (mAdditional.CalculateSimilarity(command, "następny obrazek") > mAdditional.minimum_of_acceptance) command = "następny obrazek";
            else if (mAdditional.CalculateSimilarity(command, "poprzedni obrazek") > mAdditional.minimum_of_acceptance) command = "poprzedni obrazek";

            return command;
        }

        public void Next_Image()
        {
            image_number++;
            if (image_number >= image_max) image_number = image_max - 1;

            Run_Image();
        }

        public void Previous_Image()
        {
            image_number--;
            if (image_number < 0) image_number = 0;

            Run_Image();
        }

        public void Run_Image()
        {            
            try
            {
                pathPic = GetPictureFiles(image_number);
                Bitmap bmImg = BitmapFactory.DecodeFile(pathPic);
                ImageView image = (ImageView)((Activity)context).FindViewById<ImageView>(Resource.Id.imageView1);
                image.SetImageBitmap(bmImg);
                IsImage = true;
            }
            catch
            {
                mAdditional.Update_Information("Błąd z obrazkami");
            }
        }

        public string GetPictureFiles(int choice)
        {
            string s = "";
            string[] x = new string[100];
            x = Android.OS.Environment.GetExternalStoragePublicDirectory(image_path).List();

            try
            {
                s = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + ("/" + image_path + "/") + x[choice];
            }
            catch
            {
                mAdditional.Update_Information("Błąd z obrazem");
            }
            return s;
        }
    }
}