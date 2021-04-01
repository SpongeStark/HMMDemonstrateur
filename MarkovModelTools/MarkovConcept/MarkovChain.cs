
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovModelTools
{
    /// <summary>
    /// Représente une chaine de Markov, composée d'une matrice d'émission de type
    /// <c>MarkovMatrix</c> et d'une matrice de proba de départ aussi de type <c>MarkovMatrix</c>
    /// </summary>
    public class MarkovChain
    {
        #region MENBERS
        /// <summary>
        /// dimension de la matrice (matrice carrée)
        /// </summary>
        protected uint _size;
        /// <summary>
        /// matrice d'émission
        /// </summary>
        protected MarkovMatrix _transitionMatrix;
        /// <summary>
        /// probabilité de départ
        /// </summary>
        protected MarkovMatrix _startProb;
        /// <summary>
        /// état actulle du model;
        /// </summary>
        protected uint _currentState;
        /// <summary>
        /// état de départ choisie;
        /// </summary>
        protected uint _startState;
        #endregion
        
        #region GETSET
        /// <summary>
        /// Obtient le nom de l'état courrant
        /// </summary>
        public string CurrentState {
            get { return _transitionMatrix.GetRowStates((int)_currentState); } 
            private set { _currentState = (uint)_transitionMatrix.GetRowStates(value); }               
        }

        /// <summary>
        /// Obtient le nom de l'état de départ
        /// </summary>
        public string StartState
        {
            get { return _startProb.GetColStates((int)_startState); }
            private set { _startState = (uint)_transitionMatrix.GetRowStates(value); }
        }

        /// <summary>
        /// Obtient la taille de la chaine de Markov
        /// </summary>
        public int Size
        {
            get { return (int)_size; }
        }

        /// <summary>
        /// Obtient une copie de la matrice de transition
        /// </summary>
        public MarkovMatrix TransitionMatrix
        {
            get { return new MarkovMatrix(_transitionMatrix); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Col != _size || value.Row != _size)
                    throw new ArgumentException();
                _transitionMatrix = value;
            }
        }

        /// <summary>
        /// Obtient une copie de la matrice de départ
        /// </summary>
        public MarkovMatrix StartProb
        {
            get { return new MarkovMatrix(_startProb); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Col != _transitionMatrix.Row)
                    throw new ArgumentException();
                if (!(value.Row == 1))
                    throw new ArgumentException();
                _startProb = value;
            }
        }
        #endregion

        #region CONTRUCTORS
        /// <summary>
        /// Initialise une nouvelle instance de la classe MarkovChain
        /// </summary>
        /// <param name="size">taille de la matrice de transition</param>
        /// <param name="matrix">matrice de transition</param>
        /// <param name="startProb">probabilité de départ</param>
        public MarkovChain(uint size, MarkovMatrix matrix, MarkovMatrix startProb)
        {
            _size = size;
            TransitionMatrix = matrix;
            StartProb = startProb;
            InitStartState();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe MarkovChain
        /// </summary>
        /// <param name="size">taille de la matrice de transition</param>
        /// <param name="matrix">matrice de transition</param>
        public MarkovChain(uint size, MarkovMatrix matrix) :
            this(
                size, 
                matrix, 
                new MarkovMatrix("start matrix",1,size,MarkovMatrix.EquiprobableMatrix(1,size))
                )
        { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe MarkovChain
        /// </summary>
        /// <param name="size">taille de la matrice de transition</param>
        public MarkovChain(uint size) : 
            this(
                size, 
                new MarkovMatrix("transition matrix", size,size), 
                new MarkovMatrix("start matrix", 1, size, MarkovMatrix.EquiprobableMatrix(1, size))
                )
        { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe MarkovChain
        /// </summary>
        /// <param name="markovChain">instance de Markovchain</param>
        public MarkovChain(MarkovChain markovChain)
        {
            if (markovChain == null)
                throw new ArgumentNullException();
            _size = (uint)markovChain.Size;
            TransitionMatrix = markovChain.TransitionMatrix;
            StartProb = markovChain.StartProb;
            StartState = markovChain.StartState;
            CurrentState = markovChain.CurrentState;
        }
        #endregion

        /// <summary>
        /// retourne l'instance sous forme de string 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("MarkovChain (" + _size + "," + _size + ")");
            result.AppendLine("Start State : \"" + StartState + "\"");
            result.AppendLine("Current State : \"" + CurrentState + "\"").AppendLine();
            result.AppendLine(GetStartProbToString());
            result.AppendLine(GetTransitionMatrixToString());
            return result.ToString();
        }

        /// <summary>
        /// Retourne le matrice de départ sous forme de string
        /// </summary>
        /// <returns>retourne une instance de string</returns>
        public string GetStartProbToString()
        {
            return _startProb.GetMatrixToString();
        }

        /// <summary>
        /// Retourne la matrice de transition sous forme de string
        /// </summary>
        /// <returns>retourne une instance de string</returns>
        public string GetTransitionMatrixToString()
        {
            return _transitionMatrix.GetMatrixToString();
        }

        /// <summary>
        /// Permet de tirer au hasard l'état suivant dans le modèle
        /// </summary>
        /// <returns>Retourne un dictionnaire contenant le nom de l'état tiré</returns>
        public virtual Dictionary<string,string> NextState()
        {
            CurrentState = _transitionMatrix.NextState((int)_currentState);
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(_transitionMatrix.Name,CurrentState);
            return result;
        }
        
        /// <summary>
        /// Permet d'initialiser l'état de départ
        /// </summary>
        /// <returns>retourne le nom de l'état de départ initialisé</returns>
        public string InitStartState()
        {
            _startState = (uint)_startProb.GetColStates(_startProb.NextState(0));    
            _currentState = _startState;
            return StartState;
        }  
    }
}

