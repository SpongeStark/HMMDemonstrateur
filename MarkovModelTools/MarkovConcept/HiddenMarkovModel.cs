
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HiddenMarkovModel : MarkovChain
    {
        #region ATTRIBUTS
        /// <summary>
        /// Liste des matrices d'émissions
        /// </summary>
        private List<MarkovMatrix> _emissionMatrix;
        #endregion

        #region GETSET
        /// <summary>
        /// Permet de modifier la liste des matrices d'émissions
        /// </summary>
        public List<MarkovMatrix> EmissionMatrix
        {
            get { return new List<MarkovMatrix>(_emissionMatrix); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                foreach (MarkovMatrix e in value)
                {
                    if (e.Row != _size)
                        throw new ArgumentException();
                }
                _emissionMatrix = value;
            }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Initialise une nouvelle instance de la classe HiddenMarkovMatrix
        /// </summary>
        /// <param name="size">taille de la matrice de transition</param>
        /// <param name="matrix">matrice de transition</param>
        /// <param name="emissionMatrix">liste des matrices d'émissions</param>
        public HiddenMarkovModel(uint size, MarkovMatrix matrix, List<MarkovMatrix> emissionMatrix) :
            base(size, matrix)
        {
            EmissionMatrix = emissionMatrix;
        }
        #endregion
        
        /// <summary>
        /// Permet d'avoir l'instance sous forme de string
        /// </summary>
        /// <returns>retourne une instance de string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("HiddenMarkovModel (" + _size + "," + _size + ")");
            result.AppendLine("Start State : \"" + StartState + "\"");
            result.AppendLine("Current State : \"" + CurrentState + "\"").AppendLine();
            result.AppendLine(GetStartProbToString());
            result.AppendLine(GetTransitionMatrixToString());
            foreach (MarkovMatrix e in _emissionMatrix)
            {
                result.AppendLine(e.GetMatrixToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// Permet de tirer au hasard l'état suivant dans le modèle ainsi 
        /// que toutes les émissions.
        /// </summary>
        /// <returns>retourne un dictionnaire contenant le nom de l'état de la matrice de transition tiré 
        /// plus le nom de tout les état d'émission qui en résulte</returns>
        public override Dictionary<string,string> NextState()
        {
            Dictionary<string,string> result = base.NextState();
            foreach (MarkovMatrix e in _emissionMatrix)
            {
                result.Add(e.Name,e.NextState((int)_currentState));
            }
            return result;
        }
    }
}
