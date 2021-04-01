using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class ProjectManager
    {
        public static void Save(HMMProject project, string path)
        {
            if (!File.Exists(path))
                throw new Exception();
            project.Save(path);
        }

        public static HMMProject Load(string fileName)
        {

            return null;
        }
    }
}
