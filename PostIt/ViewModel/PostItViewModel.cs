using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PostIt.Database;
using PostIt.Model;
using PostIt.Utils;

namespace PostIt.ViewModel
{
    public class PostItViewModel : BaseViewModel
    {
        public PostItViewModel(Model.PostIt postIt)
        {
            Model = postIt;
        }

        public Model.PostIt Model { get; protected set; }

        public string Text 
        { 
            get => Model.Text;
            set
            {
                Model.Text = value;
                OnPropertyChanged();
                UpdatePostIt();
            }
        }

        private void UpdatePostIt()
        {
            PostItContext.UpdatePostIt(Model);
        }
    }
}
