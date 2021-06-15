using System;

namespace UI
{
    internal class HeaderPrinter
    {
        public static void PrintHeadLine()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            string headLine = string.Format(
                @"
  _____   _   __   __                         _            
 |  __ \ (_) / _| / _|                       | |           
 | |  | | _ | |_ | |_  ___  _ __  ___  _ __  | |_          
 | |  | || ||  _||  _|/ _ \| '__|/ _ \| '_ \ | __|         
 | |__| || || |  | | |  __/| |  |  __/| | | || |_          
 |_____/ |_||_|  |_|  \___||_|   \___||_| |_| \__|         
  _______  _         _______             _______           
 |__   __|(_)       |__   __|           |__   __|          
    | |    _   ___     | |  __ _   ___     | |  ___    ___ 
    | |   | | / __|    | | / _` | / __|    | | / _ \  / _ \
    | |   | || (__     | || (_| || (__     | || (_) ||  __/
    |_|   |_| \___|    |_| \__,_| \___|    |_| \___/  \___|
                                                           
");
            Console.WriteLine(headLine);
            Console.WriteLine(Environment.NewLine);

            Console.ResetColor();
        }
        
        public static void PrintGoodbye()
        {
            string goodBye = string.Format(
                @"

  _____              __  ___            
 / ___/___  ___  ___/ / / _ ) __ __ ___ 
/ (_ // _ \/ _ \/ _  / / _  |/ // // -_)
\___/ \___/\___/\_,_/ /____/ \_, / \__/ 
                            /___/       
");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(goodBye);
            Console.ResetColor();
        }
    }
}
