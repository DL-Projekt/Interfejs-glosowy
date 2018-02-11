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
    class Work
    {        
        public readonly int VOICE = 10;
        string pathPic;

        Context context;               
        Music mMusic;

        
        public Work(Context context)
        {
            this.context = context;
            mMusic = new Music();
        }

        public void Update_Text2(string txt)
        {
            TextView text2 = (TextView)((Activity)context).FindViewById<TextView>(Resource.Id.textView2);
            text2.Text = txt;
        }
        public void Update_Image(Bitmap bmImg)
        {
            ImageView image = (ImageView)((Activity)context).FindViewById<ImageView>(Resource.Id.imageView1);
            image.SetImageBitmap(bmImg);
        }


        public bool Execute(string command)
        {
            if(!mMusic.Action_List(CheckInput(command))) return false;

            switch (CheckInput(command))
            {
                case "koniec":
                    return false;

                case "tak":
                    // Toast.MakeText(this, "Tak", ToastLength.Long).Show();
                    Update_Text2("tak");
                    return true;

                 case "aparat":
                     Update_Text2("cyk cyk!");
                    Intent intent = new Intent(context, typeof(Activity_Camera));
                    context.StartActivity(intent);
                    // Camera_Click(this, e);
                    return true;

                case "obrazek":
                    Random rnd2 = new Random();
                    int x2 = rnd2.Next(1, 20);

                    try
                    {
                        pathPic = GetPictureFiles(1);
                        Update_Text2("photo!");
                        Bitmap bmImg = BitmapFactory.DecodeFile(pathPic);
                        Update_Image(bmImg);
                    }
                    catch
                    {
                       // Toast.MakeText(this, "NULL!", ToastLength.Long).Show();
                    }
                    return true;

                default:
                    return true;
            }
        }

        private string CheckInput(string command)
        {
            if (command == "Aparat" || command == "APARAT" || command == "APARAT " || command == "Aparat " || command == "aparat ") command = "aparat";
            else if (command == "Muzyko graj" || command == "muzyko graj" | command == "Muzyka" || command == "MUZYKA" || command == "MUZYKA " || command == "Muzyka " || command == "muzyka ") command = "muzyka";
            else if (command == "Tak" || command == "TAK" || command == "TAK " || command == "Tak " || command == "tak ") command = "tak";
            else if (command == "Obrazek" || command == "OBRAZEK" || command == "Obrazek " || command == "obrazek " || command == "OBRAZEK ") command = "obrazek";
            else if (command == "Nastepna" || command == "nastepna" || command == "Następna" || command == "następna") command = "nastepna";
            else if (command == "Poprzednia" || command == "poprzednia") command = "poprzednia";
            else if (command == "Wyłącz" || command == "wyłącz") command = "wylacz";
            else if (command == "Koniec" || command == "koniec") command = "koniec";

            return command;
        }



      /*  private void Camera_Click(object sender, EventArgs e)
        {
            //Toast.MakeText(this, "Kamera", ToastLength.Long).Show();
            Intent intent = new Intent(Androido.Activity_Camera);
            // StartActivityForResult(intent, 0);
            StartActivity(intent);
        }*/



        //to tez do osobnej pasuje
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
                //Toast.MakeText(this, "Spróbuj ponownie!", ToastLength.Long).Show();
            }
            return s;
        }
    }
}