using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    public class Case
    {
        bool LigneDéjaFait = false;
        // private string valeur;
        private int nbHypothese;
        private char valeur;
        private char[] hypotheses;




        public Case(char v, int nbhypoth, char[] hypotheses)
        {
            // TODO: Complete member initialization
            this.valeur = v;
            this.nbHypothese = nbhypoth;
            this.hypotheses = hypotheses;
        }
        public char Valeur { get { return valeur; } set { valeur = value; } }
        public int NbHypothese { get { return nbHypothese; } set { nbHypothese = value; } }
        public char[] Hypotheses { get { return hypotheses; } set { hypotheses = value; } }

        public string HypothesesToString { get { return ConvertTabCharToString(); } }
        public bool LigneJumeauDéjaFait { get { return LigneDéjaFait; } set { LigneDéjaFait = value; } }

        public string ConvertTabCharToString() {
            string s="";
            for (int i = 0; i < NbHypothese; i++)
            {
                s += Hypotheses[i].ToString();
            }
            return s;
        }

    }
}
