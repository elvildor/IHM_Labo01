using PostIt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void ItemDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(PostItView));
            var categoryViewModel = DataContext as CategoryViewModel;
            if (data is PostItView postItView)
            {
                var postItViewModel = postItView.DataContext as PostItViewModel;
                if (!categoryViewModel.PostIts.Contains(postItViewModel))
                    categoryViewModel.Drop(postItView.DataContext as PostItViewModel);
            }
        }

        private void ItemPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is PostItView postItView)
            {
                DragDrop.DoDragDrop(sender as Control,
                    postItView,
                    DragDropEffects.Move);
            }
        }
    }
}
