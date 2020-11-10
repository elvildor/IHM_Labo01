using PostIt.Database;
using PostIt.View;
using PostIt.ViewModel;
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

namespace PostIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(PostItView));
            if (data is PostItView postItView && postItView.DataContext is PostItViewModel context)
            {
                if (MessageBox.Show("Êtes-vous sûr ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    PostItContext.DeletePostIt(context.Model);
                }
            }

        }
    }
}
