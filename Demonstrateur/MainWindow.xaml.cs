using MarkovModelTools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Demonstrateur
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /// test ///
            Test(50);


        }

        public void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Test(int loop)
        {
            Dictionary<int, string> Etats = new Dictionary<int, string>();
            Etats.Add(0, "Présent");
            Etats.Add(1, "Absent ");

            Dictionary<int, string> CO2 = new Dictionary<int, string>();
            CO2.Add(0, "340");
            CO2.Add(1, "420");
            CO2.Add(2, "580");
            CO2.Add(3, "670");

            Dictionary<int, string> Bruit = new Dictionary<int, string>();
            Bruit.Add(0, "34");
            Bruit.Add(1, "42");
            Bruit.Add(2, "60");

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

            double[] co2 = new double[loop];
            double[] bruit = new double[loop];



            for (int i = 0; i < loop; i++)
            {
                Dictionary<string, string> result = HMM.NextState();
                co2[i] = int.Parse(result["CO2"]);
                bruit[i] = int.Parse(result["Bruit"]);
            }

            graph.AddLineSerie("CO2", co2);
            graph.AddLineSerie("Bruit", bruit);


        }

    }
}