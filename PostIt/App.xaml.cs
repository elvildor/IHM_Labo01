using PostIt.Database;
using PostIt.Model;
using PostIt.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PostIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeModel();
            InitializeView();
        }

        protected void InitializeModel()
        {
            PostItContext.Migrate();
            Categories = PostItContext.GetCategories();
        }

        protected void InitializeView()
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel(Categories);
            MainWindow.WindowState = WindowState.Maximized;
            MainWindow.Show();
        }

        public IEnumerable<Category> Categories { get; protected set; }
    }
}
