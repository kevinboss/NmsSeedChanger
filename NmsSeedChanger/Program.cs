﻿using System;

namespace NmsSeedChanger
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            NmsSave nmsSave = new NmsSave();
            Console.WriteLine("=======================================\r\n" +
            "Close your game and choose\r\n" +
            "=======================================\r\n" +
            "Press 1 for a new ship seed (your ship, different parts)\r\n" +
            "Press 2 for a new multitool seed (different multitool)\r\n" +
            "Press 3 for a new ship seed (your ship, different parts, manual seed)\r\n" +
            "Press 4 for a new multitool seed (different multitool, manual seed)\r\n" +
            "More coming soon, leave suggestions on reddit\r\n" +
            "Press ESC to exit\r\n");
            while (true)
            {
                Console.WriteLine("==============CHOOSE ACTION==============");
                ConsoleKey readKey = Console.ReadKey().Key;
                if (readKey == ConsoleKey.D1)
                {
                    Console.WriteLine("");
                    nmsSave.randomizeShipSeed();
                }
                else if (readKey == ConsoleKey.D2)
                {
                    Console.WriteLine("");
                    nmsSave.randomizeWeaponSeed();
                }
                else if (readKey == ConsoleKey.D3)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Enter new seed: ");
                    var line = Console.ReadLine();
                    nmsSave.shipSeed(line);
                }
                else if (readKey == ConsoleKey.D4)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Enter new seed: ");
                    var line = Console.ReadLine();
                    nmsSave.weaponSeed(line);
                }
                else if (readKey == ConsoleKey.Escape)
                {
                    Console.WriteLine("");
                    Environment.Exit(0);
                }
            }
        }
    }
}
