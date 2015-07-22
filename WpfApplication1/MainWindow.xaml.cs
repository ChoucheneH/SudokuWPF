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
            EtatButtonResoluGrille();
        }

        private void CréerLesComposantsDeGrille(Grille g)
        {
            for (int i = 0; i < g.size; i++)
            {
                string s = "";
                for (int j = 0; j < g.size; j++)
                {
                    string TextToolTip = App.SudokuViewModels.GrilleSelect.TabCase[j, i].HypothesesToString;
                    // Ajouter le text sur griille
                    TextBlock tb = new TextBlock();
                    tb.ToolTip = TextToolTip;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.VerticalAlignment = VerticalAlignment.Center;
                    tb.FontWeight = FontWeights.UltraBold;
                    tb.Text = g.TabCase[j, i].Valeur.ToString();
                   // lbl.Parent = AfficheGrid;
                    if (g.TabGrille[j, i] == '.')
                    {
                        if (TextToolTip.Length == 1)
                            tb.Foreground = new SolidColorBrush(Colors.YellowGreen);
                        else if (TextToolTip.Length == 2)
                            tb.Foreground = new SolidColorBrush(Colors.SeaGreen);
                        else
                            tb.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    Grid.SetColumn(tb, i);
                    Grid.SetRow(tb, j);
                    AfficheGrid.Children.Add(tb);

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

        private void EtatButtonResoluGrille()
        {
            ResoluGrilleButton.Visibility = Visibility.Visible;
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
           
        }

        private void ResoluUnHypotheseButton_Click(object sender, RoutedEventArgs e)
        {
            ResoluUnHypotheseButton.IsEnabled = ResolutionUnHypothese(App.SudokuViewModels.GrilleSelect);
        }

       

        private void ResoluDeuxHypotheseButton_Click(object sender, RoutedEventArgs e)
        {
            ResoluDeuxHypotheseButton.IsEnabled = ResolutionDeuxHypotheses(App.SudokuViewModels.GrilleSelect);
            ResoluUnHypotheseButton.IsEnabled = ResoluDeuxHypotheseButton.IsEnabled;
        }
        private void ResoluGrilleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ok");
            Grille gr = App.SudokuViewModels.GrilleSelect;
            Boolean grilleResolu = false;
            while (!grilleResolu)
            {
            first: while(gr.numberCaseUnHypo != 0)
                {
                    for (int i = 0; i < gr.size; i++)
                    {
                        for (int j = 0; j < gr.size; j++)
                        {
                            if (gr.TabCase[i, j].Valeur.Equals('.') && gr.TabCase[i, j].NbHypothese == 1)
                            {
                                if (ResolutionUnHypothese(gr))
                                { }
                            }
                        }
                    }
                }
                if (gr.numberCaseResoluer != 0 && gr.numberCaseUnHypo == 0)
                {
                    while (gr.numberCaseDeuxHypo!=0 && gr.numberCaseUnHypo==0)
                    {
                     
                    for (int i = 0; i < gr.size; i++)
                    {
                        for (int j = 0; j < gr.size; j++)
                        {
                            if (gr.TabCase[i, j].Valeur.Equals('.') && gr.TabCase[i, j].NbHypothese == 2)
                            {
                                if (ResolutionDeuxHypotheses(gr))
                                { }
                                
                            }
                        }
                    }
   
                    }                }
                if (gr.numberCaseResoluer != 0 && gr.numberCaseUnHypo != 0)
                    goto first;

                    
                if (gr.numberCaseResoluer == 0 || gr.numberCaseUnHypo == 0)
                    grilleResolu = true;
                
            }        }
        private Boolean ResolutionUnHypothese(Grille g)
        {
            Boolean caseResolu = false;
            Grille gr = g;


            for (int i = 0; i < gr.size; i++)
            {
                for (int j = 0; j < gr.size; j++)
                {
                    if (gr.TabCase[i, j].NbHypothese == 1 && gr.TabCase[i, j].Valeur.Equals('.'))
                    {
                       // MessageBox.Show("On va changer la valeur de [" + (i + 1) + "," + (j + 1) + "];");
                        gr.TabGrille[i, j] = gr.TabCase[i, j].Valeur;
                        gr.ChangerLaValeurDuTab(i, j, gr.TabCase[i, j].Hypotheses[0]);
                        gr.GrilleMiseàjour();
                        InitialiserGrille();
                        caseResolu = true;
                        goto Exit;
                    }
                }
            }
        Exit: return caseResolu;
        }

        private Boolean ResolutionDeuxHypotheses(Grille g)
        {
            Boolean caseResolu = false;
            Grille gr = g;


            for (int i = 0; i < gr.size; i++)
            {
                for (int j = 0; j < gr.size; j++)
                {
                    if (gr.TabCase[i, j].NbHypothese == 2 && (!gr.TabCase[i, j].ColonneJumeauDéjaFait) && gr.Aunjumeau(i, j))
                    {
                        //MessageBox.Show("On va changer la valeur de [" + (i + 1) + "," + (j + 1) + "];");
                        InitialiserGrille();
                        caseResolu = true;
                        goto Exit;

                    }

                    /*    
                    else if (gr.TabCase[i, j].NbHypothese == 2)
                        {
                            MessageBox.Show(i + "-" + j + " n a pas un jumeau dans carré ");
                             goto Exit;
                        }
                     * /
                        /*
                         * gr.TabGrille[i, j] = gr.TabCase[i, j].Valeur;
                        gr.ChangerLaValeurDuTab(i, j, gr.TabCase[i, j].Hypotheses[0]);
                        gr.GrilleMiseàjour();
                        RepaintGrille();
                        */
                }
            }


        Exit: ;
            return caseResolu;
        }

        /*
         Les commentaires au dessous c'st un exemple de binding
         * ( hors de travail mais on peux l'utiliser pour améliore le travail

        
         * textbocj tb = nw blocktext
        Binding myb = new Binding("v");
        myb.source= App.sod.grillsele.tab[i,j];
        myb.updatesourcetrigger = UpdateSourceTrigger.proprtychanged;
         tb.setbidding(textbocj.textpropty,mubinding);
         * grid.setcolumn();
         * grid.setrow();
         * frgrid.children.add(tb);
         * et on ajoute propety en clsse case*/

    }
}
