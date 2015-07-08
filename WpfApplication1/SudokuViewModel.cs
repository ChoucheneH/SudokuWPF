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
        OpenFileDialog oFDImport;
        public string nomApp { get; set; }
        public Grille GrilleSelect { get; set; }
        public ObservableCollection<Grille> GrilleList { get; set; }
        public string fileNameImport { get; set; }
        
        public SudokuViewModel()
        {
            nomApp = "Sudoku";
            GrilleList = new ObservableCollection<Grille>();
        }

        internal void ImporterGrilles()
        {
            oFDImport = new OpenFileDialog();
            if (oFDImport.ShowDialog() == true)
            {
                fileNameImport = oFDImport.FileName;
                if (VérifierFichier(fileNameImport))
                {
                    ImporterDepuisUnFichier(fileNameImport);
                    
                }
                else
                {
                    //MessageBox.Show("Erreur ");
                }

            }
        }

        private void ImporterDepuisUnFichier(string path)
        {
            string nom, date, symbole;
            char[,] tab;
            using (StreamReader reader = new StreamReader(fileNameImport))
            {
                while ((reader.ReadLine()) != null)
                {

                    nom = reader.ReadLine();
                    date = reader.ReadLine();
                    symbole = reader.ReadLine();
                    tab = new char[symbole.Length, symbole.Length];
                    for (int i = 0; i < symbole.Length; i++)
                    {
                        char[] ligne = reader.ReadLine().ToCharArray();
                        int j = 0;
                        foreach (char c in ligne)
                        {
                            tab[i, j] = c;
                            j++;
                            // Console.WriteLine(c);
                        }

                    }
                    Grille g = new Grille(nom,date,symbole,tab);
                    GrilleList.Add(g);
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