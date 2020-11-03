using PostIt.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostIt.View
{
    /// <summary>
    /// Logique d'interaction pour PostItView.xaml
    /// </summary>
    public partial class PostItView : UserControl
    {
        public PostItView()
        {
            InitializeComponent();
        }

        private void ItemPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is PostItViewModel pivm
                && pivm.IsNotEditable)
            {
                DragDrop.DoDragDrop(this,
                    this,
                    DragDropEffects.Move);
                e.Handled = false;
            }

        }
    }
}
