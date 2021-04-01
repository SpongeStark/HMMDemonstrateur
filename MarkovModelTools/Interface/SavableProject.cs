using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    interface SavableProject
    {
        void Save();

        void Save(string path);

        void Load(string path);
    }
}
