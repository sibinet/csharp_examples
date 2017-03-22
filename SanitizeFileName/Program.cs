using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanitizeFileName
{
    class Program
    {
        static void Main(string[] args)
        {
            string checkfilename = "* somefile.txt";
            string clearfilename = FileUtility.SanitizeFileName(checkfilename);
            //
            //output
            //_somefile.txt
        }
    }
}
