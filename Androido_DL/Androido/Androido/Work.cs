using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Androido
{
    class Work
    {
        Context context;

        Info mInfo;
        Music mMusic;
        Image mImage;

        
        public Work(Context context)
        {
            this.context = context;
            mInfo = new Info(context);
            mMusic = new Music(context);
            mImage = new Image(context);
        }

        public bool Execute(string command)
        {
            if (mMusic.Action_List(CheckInput(command))) return true;
            else if (mImage.Action_List(CheckInput(command))) return true;

            switch (CheckInput(command))
            {
                case "koniec":
                    return false;

                case "tak":
                    mInfo.Update_Information("tak");
                    return true;

                case "aparat":
                    mInfo.Update_Information("cyk cyk!");
                    Intent intent = new Intent(context, typeof(Activity_Camera));
                    context.StartActivity(intent);
                    return true;

                default:
                    mInfo. Update_Information("Nie znam takiej komendy");
                    return true;
            }
        }

        private string CheckInput(string command)
        {
            if (command == "Aparat" || command == "APARAT" || command == "APARAT " || command == "Aparat " || command == "aparat ") command = "aparat";
            else if (command == "Tak" || command == "TAK" || command == "TAK " || command == "Tak " || command == "tak ") command = "tak";
            else if (command == "Koniec" || command == "koniec") command = "koniec";

            return command;
        }
    }
}