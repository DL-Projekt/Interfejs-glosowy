using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;

namespace Androido
{
    class Work
    {
        Context context;

        Additional mAdditional;
        Music mMusic;
        Image mImage;

        bool blok = false;

        
        public Work(Context context)
        {
            this.context = context;
            mAdditional = new Additional(context);
            mMusic = new Music(context);
            mImage = new Image(context);
        }

        public bool Execute(string command)
        {
            if (blok && mMusic.Action_List(CheckInput(command))) return true;
            else if (blok && mImage.Action_List(CheckInput(command))) return true;
            else if (blok && dodatek(command)) return true;

            switch (CheckInput(command))
            {
                case "definitywny koniec":
                    mAdditional.Update_Information("No to do widzenia!");
                    blok = false;
                    return false;

                case "youtube":
                    if (blok)
                    {
                        Intent nextActivity1 = new Intent(context, typeof(Activity1));
                        context.StartActivity(nextActivity1);
                    }
                    return true;

                case "internet":
                    if (blok)
                    {
                        Intent nextActivity2 = new Intent(context, typeof(Activity2));
                        context.StartActivity(nextActivity2);
                    }
                    return true;

                case "koniec":
                    mAdditional.Update_Information("OK, będę cicho");
                    blok = false;
                    return true;

                case "android":
                    mAdditional.Update_Information("Słucham!");
                    blok = true;
                    return true;

                case "aparat":
                    if (blok)
                    {
                        mAdditional.Update_Information("cyk cyk!");
                        Intent intent = new Intent(context, typeof(Activity_Camera));
                        context.StartActivity(intent);
                    }
                    return true;

                default:
                    if (blok) mAdditional. Update_Information("Nie znam takiej komendy");
                    return true;
            }
        }

        private string CheckInput(string command)
        {  
            if (command == null) return "Error";
            else command = command.ToLower();

            if (mAdditional.CalculateSimilarity(command, "aparat") > mAdditional.minimum_of_acceptance) command = "aparat";
            else if (mAdditional.CalculateSimilarity(command, "tak") > mAdditional.minimum_of_acceptance) command = "tak";
            else if (mAdditional.CalculateSimilarity(command, "koniec") > mAdditional.minimum_of_acceptance) command = "koniec";
            else if (mAdditional.CalculateSimilarity(command, "android") > mAdditional.minimum_of_acceptance) command = "android";
            else if (mAdditional.CalculateSimilarity(command, "kto ty jesteś") > mAdditional.minimum_of_acceptance) command = "kto ty jesteś";
            else if (mAdditional.CalculateSimilarity(command, "jaki znak twój") > mAdditional.minimum_of_acceptance) command = "jaki znak twój";
            else if (mAdditional.CalculateSimilarity(command, "gdzie ty mieszkasz") > mAdditional.minimum_of_acceptance) command = "gdzie ty mieszkasz";
            else if (mAdditional.CalculateSimilarity(command, "w jakim kraju") > mAdditional.minimum_of_acceptance) command = "w jakim kraju";

            return command;
        }


        public bool dodatek(string command)
        {
            switch (CheckInput(command))
            {
                case "kto ty jesteś":
                    mAdditional.Update_Information("Polak mały");
                    return true;

                case "jaki znak twój":
                    mAdditional.Update_Information("Orzeł biały");
                    return true;

                case "gdzie ty mieszkasz":
                    mAdditional.Update_Information("Między swemi");
                    return true;

                case "w jakim kraju":
                    mAdditional.Update_Information("W polskiej ziemi");
                    return true;

                default:
                    return false;
            }
        }
    }
}