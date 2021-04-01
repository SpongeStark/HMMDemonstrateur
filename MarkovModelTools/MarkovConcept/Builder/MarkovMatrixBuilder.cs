using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    class MarkovMatrixBuilder
    {
        private string _name;
        private uint _row;
        private uint _col;
        private double[,] _matrix;
        private Dictionary<int, string> _rowStates;
        private Dictionary<int, string> _colStates;

        public MarkovMatrix Product
        {
            get { return new MarkovMatrix(_name,_row,_col,_matrix,_rowStates,_colStates); }
        }

        public MarkovMatrixBuilder()
        {
            _name = "A";
            _row = 1;
            _col = 1;
            double[,] matrix = { { 1.00 } };
            _matrix = matrix;
            Dictionary<int,string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "A");
            _rowStates = dictionary;
            _colStates = dictionary;

        }

        public void Build(string src)
        {
            string[] separator = { Environment.NewLine };
            string[] lines = src.Split(separator,StringSplitOptions.RemoveEmptyEntries);
            
            _name = lines[0];

            StringBuilder matrix = new StringBuilder();
            for (int i = 3; i < lines.Length; i++)
            {
                matrix.AppendLine(lines[i]);
            }
            BuildMatrix(matrix.ToString());

        }

        public void BuildMatrix(string matrix)
        {
            matrix = matrix.Replace(" ", "");
            matrix = matrix.Replace("[", "");
            matrix = matrix.Replace("]", "");

            string[] separator = { Environment.NewLine };
            string[] matrixRows = matrix.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            List<double> valeurs = new List<double>();

            foreach (string row in matrixRows)
            {
                string[] result = row.Split(';');
                foreach (string e in result)
                {
                    valeurs.Add(Double.Parse(e));
                }
            }

            _row = (uint)matrixRows.Length;
            _col = (uint)(valeurs.Count / matrixRows.Length);
            _matrix = new double[_row,_col];

            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    _matrix[i, j] = valeurs[i * valeurs.Count / matrixRows.Length + j];
                }
            }

            _rowStates = MarkovMatrix.EmptyStates(_row);
            _colStates = MarkovMatrix.EmptyStates(_col);
        }

        
    }
}
