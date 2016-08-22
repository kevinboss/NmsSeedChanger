using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace NmsSeedChanger
{
    public class NmsSave
    {
        private string nmsSaveDir = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), "HelloGames\\NMS");
        private static string thisDir = Directory.GetCurrentDirectory();
        private string thisDirBackup = thisDir + "\\BACKUP";
        private string jsonSaveFile;
        private string nmsSaveProgram;
        private dynamic jsonSaveObj;
        private Random random;

        public NmsSave()
        {
            nmsSaveProgram = thisDir + "\\..\\nomanssave.exe";
            random = new Random();
            SaveBackup();
            DecryptSave();
            System.Threading.Thread.Sleep(200);
            jsonSaveFile = getJsonSaveFile(nmsSaveDir);
            jsonSaveObj = convertJsonToObject(jsonSaveFile);
        }

        public void shipSeed(string shipSeed)
        {
            saveShipSeed(shipSeed);
        }

        public void randomizeShipSeed()
        {
            string newSeed = Get16Hex(random);
            saveShipSeed(newSeed);
        }

        private void saveShipSeed(string newSeed)
        {
            jsonSaveObj["PlayerStateData"]["CurrentShip"]["Seed"][1] = newSeed;
            Console.WriteLine("New ship seed: " + newSeed);
            SaveNmsSave();
        }

        public void weaponSeed(string weaponSeed)
        {
            saveWeaponSeed(weaponSeed);
        }


        public void randomizeWeaponSeed()
        {
            string newSeed = Get16Hex(random);
            saveWeaponSeed(newSeed);
        }

        private void saveWeaponSeed(string newSeed)
        {
            jsonSaveObj["PlayerStateData"]["CurrentWeapon"]["GenerationSeed"][1] = newSeed;
            Console.WriteLine("New weapon seed: " + newSeed);
            SaveNmsSave();
        }

        private void SaveNmsSave()
        {
            writeToJsonSave();
            EncryptSave();
        }

        private void SaveBackup()
        {

            foreach (string dirPath in Directory.GetDirectories(nmsSaveDir, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(nmsSaveDir, thisDirBackup));

            foreach (string newPath in Directory.GetFiles(nmsSaveDir, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(nmsSaveDir, thisDirBackup), true);

            Console.WriteLine("Backup Created");
        }

        private void DecryptSave()
        {
            Console.WriteLine("Decrypting save file...");
            NmsSaveFileAction('d');
            Console.WriteLine("Done");
        }

        private void EncryptSave()
        {
            Console.WriteLine("Encrypting save file...");
            NmsSaveFileAction('e');
            Console.WriteLine("Done");
        }

        private void NmsSaveFileAction(char x)
        {
            Process action = new Process();
            action.StartInfo.FileName = "cmd.exe";
            action.StartInfo.WorkingDirectory = Path.GetDirectoryName(nmsSaveProgram);
            // start, wait until finish and don't show window
            action.StartInfo.Arguments = "/c START /WAIT /B " + Path.GetFileName(nmsSaveProgram) + " " + x;
            action.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            action.Start();
            action.WaitForExit();
        }

        private string getJsonSaveFile(string path)
        {
            string[] subDirectories = System.IO.Directory.GetDirectories(path);
            if (subDirectories.Length != 1)
            {
                Console.WriteLine("More than one profile is not supported atm. Make sure you have only one profile in\r\n" + nmsSaveDir);
                Environment.Exit(0);
            }
            return subDirectories[0] + "\\storage.json";
        }

        private dynamic convertJsonToObject(string jsonFile)
        {
            string jsonData = File.ReadAllText(jsonFile);
            //BAD way to do this, I know, but don't care atm.
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);
        }

        private void writeToJsonSave()
        {
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonSaveObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonSaveFile, output);
        }

        private string Get16Hex(Random random)
        {
            string hex16 = "0x";
            for (int i = 1; i <= 16; i++)
            {
                hex16 += random.Next(0, 16).ToString("X");
            }
            return hex16;
        }
    }
}

