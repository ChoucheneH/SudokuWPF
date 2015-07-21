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
            char x1 = TabJumeau[0].Hypotheses[0];
            char x2 = TabJumeau[0].Hypotheses[1];
            char y1 = TabJumeau[1].Hypotheses[0];
            char y2 = TabJumeau[1].Hypotheses[1];
            //MessageBox.Show("Colonne : "+i + " - " + j);
            int tailleCarré = (int)Math.Sqrt(size);
            if (x1 == y1)
            {
                TraiterColonne(i, j, x1, tailleCarré);

            }
            else if (x1 == y2)
            {
                TraiterColonne(i, j, x1, tailleCarré);

            }
            else if (x2 == y1)
            {
                TraiterColonne(i, j, x2, tailleCarré);
            }
            else if (x2 == y2)
            {
                TraiterColonne(i, j, x2, tailleCarré);

            }
            
        }

        private void TraiterColonne(int i, int j, char x1, int tailleCarré)
        {
           // MessageBox.Show("valeur qu'on va change 0010:" + x1);
            int compte = 0;
            for (int k = 0; k < size; k++)
            {
                if ((k < i || k >= ((i / tailleCarré) * tailleCarré) + tailleCarré))
                {
                    if (TabCase[k, j].NbHypothese > 1 && (TabCase[k, j].HypothesesToString).IndexOf(x1) != -1)
                    {
                        Char[] hypo = new char[TabCase[k, j].NbHypothese - 1];
                        int x = 0;
                        for (int l = 0; l < TabCase[k, j].NbHypothese; l++)
                        {
                            if (TabCase[k, j].Hypotheses[l] != x1)
                                hypo[x++] = TabCase[k, j].Hypotheses[l];
                        }
                        TabCase[k, j].Hypotheses = hypo;
                        TabCase[k, j].NbHypothese = hypo.Length;
                        TabCase[i, j].ColonneJumeauDéjaFait = true;
                    }
                    if (TabCase[k, j].NbHypothese == 1)
                        compte++;
                    
                    if(compte==(size-tailleCarré))
                        TabCase[i, j].ColonneJumeauDéjaFait = true;
                }
            }
        }
        private bool AunJumeauLigne(Case[,] TabCarreJumeau, int lig, int col)
        {
            
            int tailleCarré = (int)Math.Sqrt(size);
            Case[] tabjum = new Case[tailleCarré];
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
            char x1 = TabJumeau[0].Hypotheses[0];
            char x2 = TabJumeau[0].Hypotheses[1];
            char y1 = TabJumeau[1].Hypotheses[0];
            char y2 = TabJumeau[1].Hypotheses[1];
            //MessageBox.Show("Ligne : "+i + " - " + j);
            int tailleCarré = (int)Math.Sqrt(size);
            if (x1 == y1)
            {
                TraiterLigne(i, j, x1, tailleCarré);
            }
            else if (x1 == y2)
            {
                TraiterLigne(i, j, x1, tailleCarré);
            }
            else if (x2 == y1)
            {
                TraiterLigne(i, j, x2, tailleCarré);
            }
            else if (x2 == y2)
            {
                TraiterLigne(i, j, x2, tailleCarré);

                }
                            
        }

        private void TraiterLigne(int i, int j, char x2, int tailleCarré)
        {
           // MessageBox.Show("valeur qui va change:" + x2);
            int compte = 0;
            for (int k = 0; k < size; k++)
            {
                if ((k < j || k >= ((j / tailleCarré) * tailleCarré) + tailleCarré))
                {
                    if (tabCase[i, k].NbHypothese > 1 && (tabCase[i, k].HypothesesToString).IndexOf(x2) != -1)
                    {
                        Char[] hypo = new char[TabCase[j, k].NbHypothese - 1];
                        int x = 0;
                        for (int l = 0; l < TabCase[j, k].NbHypothese; l++)
                        {
                            if (TabCase[j, k].Hypotheses[l] != x2)
                                hypo[x++] = TabCase[j, k].Hypotheses[l];
                        }
                        TabCase[j, k].Hypotheses = hypo;
                        TabCase[j, k].NbHypothese = hypo.Length;
                        TabCase[i, j].LigneJumeauDéjaFait = true;
                    }
                    if (TabCase[j, k].NbHypothese == 1)
                        compte++;

                    if (compte == (size - tailleCarré))
                        TabCase[i, j].LigneJumeauDéjaFait = true;
                }
            }
        }

        private bool AunJumeauColonne(Case[,] TabCarreJumeau,int lig, int col)
        {
            
            int tailleCarré = (int)Math.Sqrt(size);
            Case[] tabjum = new Case[tailleCarré];
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
