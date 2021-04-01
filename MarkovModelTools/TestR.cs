using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;

namespace MarkovModelTools
{
    public class TestR
    {

        public static void Test()
        {
            
            REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();
            engine.Evaluate("library(HMM)");

            engine.Evaluate("initHMM(c(\"A\", \"B\"), c(\"L\", \"R\"), transProbs = matrix(c(0.8, 0.2, 0.2, 0.8), 2), emissionProbs = matrix(c(0.6, 0.4, 0.4, 0.6), 2))");
        }
    }
}
