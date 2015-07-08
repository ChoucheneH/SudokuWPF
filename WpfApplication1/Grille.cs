using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    public class Grille
    {
        private Case[,] tabCase;
        private bool[] tabVérifNombre;
        private int nbHypothese;
        private char[] Hypotheses;
        public Grille(string nom,string date,string symbole,char[,] tab)
        {
            Nom = nom;
            Date = date;
            Symbole = symbole;
            TabGrille = tab;
            tabCase = new Case[size, size];
            CréerLesCases(TabGrille); // à traiter 
        }

        private void CréerLesCases(char[,] Tab)
        {


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    nbHypothese = 1;
                    Hypotheses = new char[nbHypothese];
                    Hypotheses[0] = Tab[i, j];
                    tabCase[i, j] = new Case(Tab[i, j], nbHypothese, Hypotheses);


                    if (Symbole.IndexOf(Tab[i, j]) != -1)
                    {
                        nbHypothese = 1;
                        Hypotheses = new char[nbHypothese];
                        Hypotheses[0] = Tab[i, j];

                        tabCase[i, j] = new Case(Tab[i, j], nbHypothese, Hypotheses);
                    }
                    else
                    {
                        //MessageBox.Show("Il manque qlq fonctionnalité au Grille.cs");
                        
                        InitialiserTabVérifNombre();
                        nbHypothese = size;
                        RechercheParLigne(Tab[i, j], i, j);
                        
                        RechercheParColonne(Tab[i, j], i, j);
                        
                        RechercheParRegion(Tab[i, j], i, j);
                        
                        Hypotheses = new char[nbHypothese];
                        
                        int compte = 0;
                         
                        for (int p = 0; p < size; p++)
                        {
                            if (!tabVérifNombre[p])
                            {
                                Hypotheses[compte] = Symbole[p];
                                compte++;

                            }

                        }
                     
                        tabCase[i, j] = new Case(Tab[i, j], nbHypothese, Hypotheses);
                        
                    }
                }
            }
        }

        private void RechercheParRegion(char c, int lig, int col)
        {
            if (nbHypothese > 1)
            {
                int tailleCarré = (int)Math.Sqrt(size);
                bool CarréEstValide = false;
                int divC, divL, modC, modL;

                divC = col / tailleCarré;
                modC = col % tailleCarré;
                divL = lig / tailleCarré;
                modL = lig % tailleCarré;

                for (int i = divL * tailleCarré; i < divL * tailleCarré + tailleCarré; i++)
                {
                    if (i != col)
                    {
                        for (int j = divC * tailleCarré; j < divC * tailleCarré + tailleCarré; j++)
                        {
                            if (j != col)
                            {
                                if (Symbole.IndexOf(TabGrille[i, j]) != -1 && !tabVérifNombre[Symbole.IndexOf(TabGrille[i, j])])
                                {
                                    setVrai(Symbole.IndexOf(c));
                                    CarréEstValide = true;
                                }
                                else
                                {
                                    CarréEstValide = false;
                                    break;
                                }
                                if (!CarréEstValide)
                                    break;
                            }


                        }
                    }

                }

            }
        }

        private void RechercheParColonne(char c, int lig, int col)
        {
            if (nbHypothese > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i != lig)
                    {
                        if (Symbole.IndexOf(TabGrille[i, col]) != -1 && !tabVérifNombre[Symbole.IndexOf(TabGrille[i, col])])
                        {
                            setVrai(Symbole.IndexOf(TabGrille[i, col]));

                        }
                    }
                }
            }
        }

        private void RechercheParLigne(char c, int lig, int col)
        {
            if (nbHypothese > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (i != col)
                    {
                        if (Symbole.IndexOf(TabGrille[lig, i]) != -1 && !tabVérifNombre[Symbole.IndexOf(TabGrille[lig, i])])
                        {
                            setVrai(Symbole.IndexOf(TabGrille[lig, i]));
                        }
                    }
                }
            }
            
        }

        private void setVrai(int indice)
        {
            tabVérifNombre[indice] = true;
            if (nbHypothese != 0)
                nbHypothese--;
        }

        private void InitialiserTabVérifNombre()
        {
            tabVérifNombre = new bool[size];

            for (int i = 0; i < size; i++)
                tabVérifNombre[i] = false;
        }
        
        public string Nom { get; set; }
        public string Date { get; set; }
        public string Symbole { get; set; }
        public char[,] TabGrille { get; set; }
        public Case[,] TabCase { get{return tabCase;} }
        public int size { get { return Symbole.Length; } }

        public override string ToString()
        {
            return Nom + "" + Date;
        }
        
       
    }
}
