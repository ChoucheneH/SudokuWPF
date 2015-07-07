using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Grille
    {
        public string Nom { get; set; }
        public string Date { get; set; }
        public string symbole { get; set; }

        public int size { get { return symbole.Length; } }

        public override string ToString()
        {
            return Nom + "" + Date;
        }
    }
}
