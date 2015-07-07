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
                    if (Symbole.IndexOf(Tab[i, j]) != -1)
                    {
                        nbHypothese = 1;
                        Hypotheses = new char[nbHypothese];
                        Hypotheses[0] = Tab[i, j];

                        tabCase[i, j] = new Case(Tab[i, j], nbHypothese, Hypotheses);
                    }
                    else
                    {
                        MessageBox.Show("Il manque qlq fonctionnalité au Grille.cs");
                        goto Fin;
                        /*
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
                        */
                    }
                }
                         
            }
            Fin:;
        }

        private void RechercheParRegion(char p, int i, int j)
        {
            MessageBox.Show("RechercheParRegion : à traiter ");
        }

        private void RechercheParColonne(char p, int i, int j)
        {
            MessageBox.Show("RechercheParCollonne: à traiter ");
        }

        private void RechercheParLigne(char p, int i, int j)
        {
            MessageBox.Show("RechercheParLigne : à traiter ");
        }

        private void InitialiserTabVérifNombre()
        {
            MessageBox.Show("InitialiserTabVérifNombre : à traiter ");
        }
        private Case[,] tabCase;
        public string Nom { get; set; }
        public string Date { get; set; }
        public string Symbole { get; set; }
        public char[,] TabGrille { get; set; }
        public string GrilleToString { get {return ConvertTabToString(TabGrille); } }
        
        public int size { get { return Symbole.Length; } }

        public override string ToString()
        {
            return Nom + "" + Date;
        }
        public string ConvertTabToString(char[,] tab)
        {
            string s="";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    s += tab[i, j];
                }
                s += "\\n";
            }
            return "";
        }

       
    }
}
