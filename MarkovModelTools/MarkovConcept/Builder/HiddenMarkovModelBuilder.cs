using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovModelTools
{
    public class HiddenMarkovModelBuilder
    {
        private HiddenMarkovModel _product;

        public HiddenMarkovModel Product
        {
            get { return _product; }
            
        }

        public HiddenMarkovModelBuilder()
        {
            List<MarkovMatrix> emisionMatrix = new List<MarkovMatrix>();
            emisionMatrix.Add(new MarkovMatrix("emission", 1, 1));
            _product = new HiddenMarkovModel(1,new MarkovMatrix("transition",1,1),emisionMatrix);
        }

        public void Build(string src)
        {

        }
 


    }
}
