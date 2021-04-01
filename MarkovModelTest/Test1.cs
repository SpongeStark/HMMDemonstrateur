
using System.Collections.Generic;
using System.Text;
using MarkovModelTools;

namespace MarkovModelTest
{
    public class Test1
    {
        public static string Execute(int loop)
        {
            Dictionary<int, string> Etats = new Dictionary<int, string>();
            Etats.Add(0, "Présent");
            Etats.Add(1, "Absent ");

            Dictionary<int, string> CO2 = new Dictionary<int, string>();
            CO2.Add(0,"Faible   ");
            CO2.Add(1,"Moyen    ");
            CO2.Add(2,"Fort     ");
            CO2.Add(3,"Très Fort");

            Dictionary<int, string> Bruit = new Dictionary<int, string>();
            Bruit.Add(0,"Faible");
            Bruit.Add(1,"Moyen ");
            Bruit.Add(2,"Fort  ");

            double[,] MatriceTransition =
            {
                { 0.2 , 0.8 },
                { 0.6 , 0.4 }
            };

            double[,] MatriceCO2 =
            {
                { 0.2 , 0.4 , 0.2 , 0.2 },
                { 0.1 , 0.3 , 0.5 , 0.1 }
            };

            double[,] MatriceBruit =
            {
                { 0.4 , 0.4 , 0.2 },
                { 0.2 , 0.3 , 0.5 }
            };

            List<MarkovMatrix> MatriceEmissions = new List<MarkovMatrix>();
            MatriceEmissions.Add(new MarkovMatrix("CO2", 2, 4, MatriceCO2, Etats, CO2));
            MatriceEmissions.Add(new MarkovMatrix("Bruit", 2, 3, MatriceBruit, Etats, Bruit));

            HiddenMarkovModel HMM = new HiddenMarkovModel(
                2, 
                new MarkovMatrix("Transition Matrice", 2, 2, MatriceTransition, Etats, Etats), 
                MatriceEmissions
                );


            StringBuilder builder = new StringBuilder();
            builder.AppendLine("id; CO2; Bruit");

            for (int i = 0; i < loop; i++)
            {
                
                Dictionary<string,string> result = HMM.NextState();

                builder.AppendFormat("Etat : {0} | {1}; {2}; {3} ",
                    result[HMM.TransitionMatrix.Name],
                    i,
                    result["CO2"],
                    result["Bruit"]
                    )
                    .AppendLine();
            }

            return builder.ToString();

        }
    }
}
