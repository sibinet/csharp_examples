using System;
using System.IO;

namespace FileFlush
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                Utilities.WriteLog("Process Started:" + String.Format("{0:f}", DateTime.Now));

                //Get Folder Path to be deleted
                string path = System.Configuration.ConfigurationSettings.AppSettings["Path"];

                //All Folders
                foreach (string directory in Directory.GetDirectories(path))
                {
                    //All Files
                    foreach (string file in Directory.GetFiles(directory))
                    {
                        Utilities.WriteLog("deleting " + file);
                        File.Delete(file);
                    }
                    //Sub Folders
                    foreach (string Folders in Directory.GetDirectories(directory))
                    {
                        Utilities.WriteLog("deleting " + Folders);
                        DeleteDirectory(Folders);
                    }

                }

                Utilities.WriteLog("Process Finished:" + String.Format("{0:f}", DateTime.Now));
                Utilities.WriteLog("__________________________________");
            }
            catch (Exception ex)
            {
                Utilities.WriteLog("Error");
                Utilities.WriteLog(ex.Message.ToString());
                Utilities.WriteLog("Process Finished:" + String.Format("{0:f}", DateTime.Now));
                Utilities.WriteLog("__________________________________");
            }
        }
        private static void DeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    //Delete all files 
                    foreach (string file in Directory.GetFiles(path))
                    {
                        File.Delete(file);
                    }
                    //Delete all Sub Directories
                    foreach (string directory in Directory.GetDirectories(path))
                    {
                        DeleteDirectory(directory);
                    }
                    //Delete a Folder
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLog("Delete failed.");
                Utilities.WriteLog(ex.Message.ToString());
            }
        }
    }
}
