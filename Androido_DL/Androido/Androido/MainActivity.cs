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
        }
      
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

