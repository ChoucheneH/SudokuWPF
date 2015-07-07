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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.SudokuViewModels;
        }

        private void btn_import(object sender, RoutedEventArgs e)
        {
            App.SudokuViewModels.ImporterGrilles();
        }

        private void SudokuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            GrilleAffiche.ShowGridLines = true;
            GrilleAffiche.ColumnDefinitions.Clear();
            GrilleAffiche.Children.Clear();
            GrilleAffiche.RowDefinitions.Clear();
            AfficheGrilleStackPanel.Children.Clear();

            GrilleAffiche.Background = new SolidColorBrush(Colors.Green);
            Grille g = App.SudokuViewModels.GrilleSelect;


            for (int i = 0; i < g.size; i++)
            {
                GrilleAffiche.ColumnDefinitions.Add(new ColumnDefinition());
                GrilleAffiche.RowDefinitions.Add(new RowDefinition());

            }
            for (int i = 0; i < g.size; i++)
            {
                string s = "";
                for (int j = 0; j < g.size; j++)
                {
                    // Ajouter le btn sur griille
                    Button b = new Button();
                    //b.Margin = new Thickness(10);
                  //  b.Content = "ok";
                    b.Content = g.TabGrille[i, j];

                    if (g.TabGrille[i, j]=='.')
                    {
                        b.Background = new SolidColorBrush(Colors.Red);
                    }
                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, j);
                    GrilleAffiche.Children.Add(b);
                    
                    s += g.TabGrille[i, j].ToString();
                    
                }
                // Ajouter le grille à resoluer
                TextBlock tb = new TextBlock();
                tb.Text = s;
                AfficheGrilleStackPanel.Children.Add(tb);
            }
            
        }

        
    }
}
