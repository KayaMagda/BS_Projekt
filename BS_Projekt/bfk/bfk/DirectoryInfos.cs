using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bfk
{
    class DirectoryInfos
    {
        public static List<string> directoryNames(string[] directories)
        {
            List<string> names = new List<string>();
            foreach(string directorypath in directories)
            {
                DirectoryInfo di = new DirectoryInfo(directorypath);
                names.Add(di.Name);
                

            }
            return names;
        }
    }
}
