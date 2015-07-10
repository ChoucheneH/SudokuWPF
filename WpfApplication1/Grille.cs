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
        private Case[] tabCaseJum;
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
            tabCaseJum = new Case[2];
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
                                    setVrai(Symbole.IndexOf(TabGrille[i, j]));
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
        public Case[] TabCaseJum { get { return tabCaseJum; } set { tabCaseJum=value;} }
        public int size { get { return Symbole.Length; } }

        public override string ToString()
        {
            return Nom + "" + Date;
        }



        internal void ChangerLaValeurDuTab(int i, int j, char p)
        {
            TabGrille[i, j] = p;
        }

        internal void GrilleMiseàjour()
        {
            CréerLesCases(TabGrille);
        }

        internal bool Aunjumeau(int i, int j)
        {
            Case[,] TabCarreJumeau = new Case[(int) Math.Sqrt(size), (int) Math.Sqrt(size)];
            Case[] TabJumeauResolu = new Case[(int)Math.Sqrt(size)];

            TabCarreJumeau = CarreJumeau(i, j);
            bool EstJumeaux=false;
            if (AunJumeauLigne(TabCarreJumeau,i, j))
            {
                RésoluJumeauLigne(TabCaseJum, i, j);
                EstJumeaux = true;
                
            }
            else if (AunJumeauColonne(TabCarreJumeau,i, j))
            {
                RésoluJumeauColonne(TabCaseJum, i, j);
                EstJumeaux = true;
            }

            return EstJumeaux;
        }

        private void RésoluJumeauColonne(Case[] TabJumeau, int i, int j)
        {
            MessageBox.Show(i + "" + j);
            int tailleCarré = (int)Math.Sqrt(size);
            if (TabJumeau[0].Hypotheses[0] == TabJumeau[1].Hypotheses[0])
            {
                MessageBox.Show("valeur qu'on va change:" + TabJumeau[0].Hypotheses[0]);
                for (int k = 0; k < size; k++)
                {
                    if ((k < i || k >= ((i / tailleCarré) * tailleCarré) + tailleCarré))
                    {
                        if ((TabCase[k, j].HypothesesToString).IndexOf(TabJumeau[0].Hypotheses[0]) != -1 && TabCase[k, j].NbHypothese > 1)
                        {
                            Char[] hypo = new char[TabCase[k, j].NbHypothese - 1];
                            for (int l = 0; l < TabCase[k, j].NbHypothese; l++)
                            {
                                if (TabCase[k, j].Hypotheses[l] != TabJumeau[0].Hypotheses[0])
                                    hypo[l] = TabCase[k, j].Hypotheses[l];
                            }
                            TabCase[k, j].Hypotheses = hypo;
                            TabCase[k, j].NbHypothese = hypo.Length;
                            TabCase[k, j].LigneJumeauDéjaFait = true;
                        }

                    }
                }

            }
            else if (TabJumeau[0].Hypotheses[0] == TabJumeau[1].Hypotheses[1])
            {
                MessageBox.Show("valeur qu'on va change:" + TabJumeau[0].Hypotheses[0]);
            }
            else if (TabJumeau[0].Hypotheses[1] == TabJumeau[1].Hypotheses[0])
            {
                MessageBox.Show("valeur qu'on va change:" + TabJumeau[0].Hypotheses[1]);
            }
            else if (TabJumeau[0].Hypotheses[1] == TabJumeau[1].Hypotheses[1])
            {
                MessageBox.Show("valeur qu'on va change:" + TabJumeau[0].Hypotheses[1]);

                for (int k = 0; k < size; k++)
                {
                    if ((k < j || k >= ((j / tailleCarré) * tailleCarré) + tailleCarré))
                    {
                        if ((TabCase[k, i].HypothesesToString).IndexOf(TabJumeau[0].Hypotheses[1]) != -1 && TabCase[k, i].NbHypothese > 1)
                        {
                            Char[] hypo = new char[TabCase[k, i].NbHypothese - 1];
                            for (int l = 0; l < TabCase[k, i].NbHypothese; l++)
                            {
                                if (TabCase[k, i].Hypotheses[l] != TabJumeau[0].Hypotheses[1])
                                    hypo[l] = TabCase[k, i].Hypotheses[l];
                            }
                            TabCase[k, i].Hypotheses = hypo;
                            TabCase[k, i].NbHypothese = hypo.Length;
                            TabCase[k, i].LigneJumeauDéjaFait = true;
                        }

                    }
                }

            }
            
        }
        private bool AunJumeauLigne(Case[,] TabCarreJumeau, int lig, int col)
        {
            Case[] tabjum = new Case[2];
            int tailleCarré = (int)Math.Sqrt(size);
            bool CarréEstValide = false;
            int divC, divL, modC, modL;

            divC = col / tailleCarré;
            modC = col % tailleCarré;
            divL = lig / tailleCarré;
            modL = lig % tailleCarré;
            int compte = 0;
            for (int i = 0; i < tailleCarré; i++)
            {
                if (TabCarreJumeau[modL, i].NbHypothese == 2)
                {
                    tabjum[compte] = TabCarreJumeau[modL, i];
                    compte++;
                }
                
            }
            if (compte == 2)
            {
                TabCaseJum = tabjum;
                CarréEstValide = true;
            }
             
            return CarréEstValide;

        }
        private void RésoluJumeauLigne(Case[] TabJumeau, int i, int j)
        {
            MessageBox.Show(i + "" + j);
            int tailleCarré = (int)Math.Sqrt(size);
            if (TabJumeau[0].Hypotheses[0] == TabJumeau[1].Hypotheses[0])
            {
                MessageBox.Show("valeur qui va change:" + TabJumeau[0].Hypotheses[0]);
            }
            else if (TabJumeau[0].Hypotheses[0] == TabJumeau[1].Hypotheses[1])
            {
                MessageBox.Show("valeur qui va change:" + TabJumeau[0].Hypotheses[0]);
            }
            else if (TabJumeau[0].Hypotheses[1] == TabJumeau[1].Hypotheses[0])
            {
                MessageBox.Show("valeur qui va change:" + TabJumeau[0].Hypotheses[1]);
            }
            else if (TabJumeau[0].Hypotheses[1] == TabJumeau[1].Hypotheses[1])
            {
                MessageBox.Show("valeur qui va change:" + TabJumeau[0].Hypotheses[1]);
                
                for (int k = 0; k < size; k++)
                {
                    if ((k < j || k >= ((j / tailleCarré)*tailleCarré)+tailleCarré))
                    {
                        if ((TabCase[i,k].HypothesesToString).IndexOf(TabJumeau[0].Hypotheses[1])!=-1 && TabCase[i,k].NbHypothese>1)
                        {
                            Char[] hypo = new char[TabCase[i,k].NbHypothese - 1];
                            for (int l = 0; l < TabCase[i,k].NbHypothese; l++)
                            {
                                if (TabCase[i, k].Hypotheses[l] != TabJumeau[0].Hypotheses[1])
                                hypo[l] = TabCase[i, k].Hypotheses[l];
                            }
                            TabCase[i, k].Hypotheses = hypo;
                            TabCase[i, k].NbHypothese = hypo.Length;
                            TabCase[i, k].LigneJumeauDéjaFait = true;
                        }
                        
                    }
                }

            }
            
        }

        private bool AunJumeauColonne(Case[,] TabCarreJumeau,int lig, int col)
        {
            Case[] tabjum = new Case[2];
            int tailleCarré = (int)Math.Sqrt(size);
            bool CarréEstValide = false;
            int divC, divL, modC, modL;

            divC = col / tailleCarré;
            modC = col % tailleCarré;
            divL = lig / tailleCarré;
            modL = lig % tailleCarré;
            int compte = 0;
            for (int i = 0; i < tailleCarré; i++)
            {

                if (TabCarreJumeau[i, modC].NbHypothese == 2)
                {
                    tabjum[compte] = TabCarreJumeau[i, modC];
                    compte++;
                }

            }

                if (compte == 2)
                {
                    TabCaseJum = tabjum;
                    CarréEstValide = true;
                }
            
            return CarréEstValide;
        }

        

        private Case[,] CarreJumeau(int lig, int col)
        {
            int tailleCarré = (int)Math.Sqrt(size);
            int divC, divL, modC, modL;
            Case[,] tab = new Case[tailleCarré, tailleCarré];

            divC = col / tailleCarré;
            modC = col % tailleCarré;
            divL = lig / tailleCarré;
            modL = lig % tailleCarré;
            int x = 0; 
            for (int i = divL * tailleCarré; i < divL * tailleCarré + tailleCarré; i++)
            {
                int y = 0;
                for (int j = divC * tailleCarré; j < divC * tailleCarré + tailleCarré; j++)
                {

                    tab[x, y] = TabCase[i, j];
                    y++;
                }
                x++;
            }
            return tab;
        }

    }
}
