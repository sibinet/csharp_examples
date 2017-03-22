using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFlush
{
    public class Utilities
    {
        public static FileStream fileStream;
        public static StreamWriter streamWriter;
        public static void OpenFile()
        {
            string strPath = null;
            strPath = Environment.CurrentDirectory + "\\Log.log";
            if (System.IO.File.Exists(strPath))
            {
                fileStream = new FileStream(strPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fileStream = new FileStream(strPath, FileMode.Create, FileAccess.Write);
            }
            streamWriter = new StreamWriter(fileStream);
        }
        public static void WriteLog(string strComments)
        {
            OpenFile();
            streamWriter.WriteLine(strComments);
            CloseFile();
        }
        public static void CloseFile()
        {
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
