using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;



using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
namespace WpfApplication1
{
    
   public class SudokuViewModel
    {
        public string nomApp { get; set; }
        public Grille GrilleSelect { get; set; }
        public ObservableCollection<Grille> GrilleList { get; set; }
        public string fileNameImport { get;set;}
        public SudokuViewModel()
        {
            nomApp = "Sudoku" ;
            GrilleList = new ObservableCollection<Grille> ();
          
            GrilleList.Add(new Grille { Nom = "Grille 1" ,Date= DateTime.Now.ToString(),symbole = "123456789"});
            GrilleList.Add(new Grille { Nom = "Grille 2", Date = DateTime.Now.ToString(), symbole = "123456789" });
            GrilleList.Add(new Grille { Nom = "Grille 3", Date = DateTime.Now.ToString(), symbole = "123456789"});

        }
       internal void AjouterGrille()
        {
            int x = GrilleList.Count + 1;
            Grille g = new Grille
            {
                Nom = "grille ",
                Date = DateTime.Now.ToString(),
                symbole = "12346789",
            };

            
            GrilleList.Add(g);
        }

        internal void ImporterGrilles()
        {
            OpenFileDialog oFDImport = new OpenFileDialog();
            if (oFDImport.ShowDialog()==true)
	            {
                    if (VérifierFichier(oFDImport.FileName))
                    {
                        MessageBox.Show("Yes");
                    }else{
                        MessageBox.Show("No");
                    }
                    
	            }            
        }
        internal bool VérifierFichier(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(@"" + path);
                if (lines.Length == 0)
                {
                    MessageBox.Show("le fichier " + path + " donnée est vide", "Avertissement", MessageBoxButton.OK);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("le fichier " + path + " donnée est erroné", "Attention", MessageBoxButton.OK);
                return false;
            }

        }
    }
        
}