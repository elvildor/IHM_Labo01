using PostIt.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostIt.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IEnumerable<Category> categories)
        {
            foreach(var category in categories)
            {
                Categories.Add(new CategoryViewModel(category));
            }
        }

        public List<BaseViewModel> Categories { get; protected set; } = new List<BaseViewModel>();

        public BaseViewModel Commands { get; protected set; } = new CommandsViewModel();
    }
}
