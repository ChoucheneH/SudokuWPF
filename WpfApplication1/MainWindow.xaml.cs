using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ButtonsEstVisible { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.SudokuViewModels;
            //ButtonsEstVisible = false;
        }

        private void btn_import(object sender, RoutedEventArgs e)
        {
            App.SudokuViewModels.ImporterGrilles();
            importTextBox.Text = App.SudokuViewModels.fileNameImport;
            //importTextBox.ToolTip = App.SudokuViewModels.fileNameImport;
        }

        private void SudokuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            InitialiserGrille();
            
        }

        private void InitialiserGrille()
        {
            InitialiserLesComposants();
            Grille g = App.SudokuViewModels.GrilleSelect;
            AjouteLigColGrid(g);
            CréerLesComposantsDeGrille(g);
            EtatButtonResoluUnHypo();
            EtatButtonResoluDeuxHypo();
        }

        private void CréerLesComposantsDeGrille(Grille g)
        {
            for (int i = 0; i < g.size; i++)
            {
                string s = "";
                for (int j = 0; j < g.size; j++)
                {
                    string TextToolTip = App.SudokuViewModels.GrilleSelect.TabCase[j, i].HypothesesToString;
                    // Ajouter le btn sur griille
                    TextBlock b = new TextBlock();
                    b.ToolTip = TextToolTip;

                    b.Text = g.TabCase[j, i].Valeur.ToString();

                    if (g.TabGrille[j, i] == '.')
                    {
                        if (TextToolTip.Length == 1)
                            b.Background = new SolidColorBrush(Colors.YellowGreen);
                        else if (TextToolTip.Length == 2)
                            b.Background = new SolidColorBrush(Colors.SeaGreen);
                        else
                            b.Background = new SolidColorBrush(Colors.Red);
                    }
                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, j);
                    AfficheGrid.Children.Add(b);

                    s += g.TabGrille[i, j].ToString();

                }
                // Ajouter le grille à resoluer
                AjouterSodukoàResolu(s);
            }
        }

        private void EtatButtonResoluDeuxHypo()
        {
            ResoluDeuxHypotheseButton.Visibility = Visibility.Visible;
        }

        private void EtatButtonResoluUnHypo()
        {
            ResoluUnHypotheseButton.Visibility = Visibility.Visible;
        }

        private void AjouterSodukoàResolu(string s)
        {

            TextBlock tb = new TextBlock();
            tb.Text = s;
            tb.FontSize = 10;
            
            

            AfficheGrilleStackPanel.Children.Add(tb);
        }

        private void AjouteLigColGrid(Grille g)
        {
            for (int i = 0; i < g.size; i++)
            {
                AfficheGrid.ColumnDefinitions.Add(new ColumnDefinition());
                AfficheGrid.RowDefinitions.Add(new RowDefinition());

            }
        }

        private void InitialiserLesComposants()
        {
            AfficheGrid.ColumnDefinitions.Clear();
            AfficheGrid.Children.Clear();
            AfficheGrid.RowDefinitions.Clear();
            AfficheGrid.ShowGridLines = true;
 
            AfficheGrilleStackPanel.Children.Clear();
            AfficheGrid.Background = new SolidColorBrush(Colors.Green);
        }

        private void ResoluUnHypotheseButton_Click(object sender, RoutedEventArgs e)
        {
            Grille gr = App.SudokuViewModels.GrilleSelect;


            for (int i = 0; i < gr.size; i++)
            {
                for (int j = 0; j < gr.size; j++)
                {
                    if (gr.TabCase[i, j].NbHypothese == 1 && gr.TabCase[i, j].Valeur.Equals('.'))
                    {
                        MessageBox.Show("On va changer la valeur de [" + (i+1) + "," + (j+1) + "];");
                        gr.TabGrille[i, j] = gr.TabCase[i, j].Valeur;
                        gr.ChangerLaValeurDuTab(i, j, gr.TabCase[i, j].Hypotheses[0]);
                        gr.GrilleMiseàjour();
                        RepaintGrille();

                        goto Exit;
                    }
                }
            }
        Exit: ;
        }

        private void RepaintGrille()
        {
            InitialiserGrille();
        }

        private void ResoluDeuxHypotheseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("à faire - MainWindow");
        }

        
    }
}
