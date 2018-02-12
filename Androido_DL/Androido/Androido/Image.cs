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
        Context context;
        string pathPic;

        Info mInfo;


        public Image(Context context)
        {
            this.context = context;
            mInfo = new Info(context);
        }

        public bool Action_List(string command)
        {
            switch (CheckInput(command))
            {
                case "obrazek":
                    Random rnd2 = new Random();
                    int x2 = rnd2.Next(1, 20);

                    try
                    {
                        pathPic = GetPictureFiles(1);
                        mInfo.Update_Information("obrazek!");
                        Bitmap bmImg = BitmapFactory.DecodeFile(pathPic);
                        ImageView image = (ImageView)((Activity)context).FindViewById<ImageView>(Resource.Id.imageView1);
                        image.SetImageBitmap(bmImg);
                    }
                    catch
                    {
                        mInfo.Update_Information("Błąd w aparacie");
                    }
                    return true;

                default:
                    return false;
            }
        }

        private string CheckInput(string command)
        {
            if (command == "Obrazek" || command == "OBRAZEK" || command == "Obrazek " || command == "obrazek " || command == "OBRAZEK ") command = "obrazek";
            
            return command;
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
                mInfo.Update_Information("Błąd z obrazem");
            }
            return s;
        }
    }
}