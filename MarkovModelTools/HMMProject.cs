using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HMMProject : SavableProject
    {
        private bool _changed;
        private string _projectName;
        private string _projectPath;
        public HiddenMarkovModel _startHmm;
        public HiddenMarkovModel _hmm;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }//prendre en compte le nom du dossier
        }

        public string ProjectPath
        {
            get { return _projectPath; }
            set { _projectPath = value; }//prendre en compte le nom du dossier
        }

        public HMMProject(string projectName, string projectPath)
        {
            _projectName = projectName;
            _projectPath = projectPath;
            _startHmm = null;
            _hmm = null;
            _changed = false;
        }

        public HMMProject(string projectName) : 
            this(projectName, null)
        {}

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Project Name : \"{0}\"",_projectName).AppendLine();
            return result.ToString();
        }

        public void Save()
        {
            this.Save(_projectPath);
        }

        public void Save(string path)
        {
            _projectPath = path;
            string projectFile = Path.Combine(path, _projectName);
            if (!File.Exists(projectFile))
                Directory.CreateDirectory(projectFile);

            StreamWriter writer = File.CreateText(Path.Combine(projectFile, _projectName + ".proj"));
            writer.WriteLine(this);
            writer.Close();

            writer = File.CreateText(Path.Combine(projectFile,"StartHMM.txt"));
            writer.WriteLine(_startHmm);
            writer.Close();

            writer = File.CreateText(Path.Combine(projectFile, "HMM.txt"));
            writer.WriteLine(_hmm);
            writer.Close();
        }

        public void Load(string path)
        {
            
        }
    }
}
