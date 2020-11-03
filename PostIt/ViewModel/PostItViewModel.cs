using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PostIt.Database;
using PostIt.Model;
using PostIt.Utils;

namespace PostIt.ViewModel
{
    public class PostItViewModel : BaseViewModel
    {
        public static event EventHandler ElementAreFocused;

        public PostItViewModel(Model.PostIt postIt)
        {
            Model = postIt;
            ElementAreFocused += PostItViewModel_ElementAreFocused;
        }

        private void PostItViewModel_ElementAreFocused(object sender, EventArgs e)
        {
            if (!sender.Equals(this))
            {
                IsNotEditable = true;
            }
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

        private bool _isNotEditable = true;
        public bool IsNotEditable
        {
            get => _isNotEditable;
            protected set
            {
                _isNotEditable = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEditable));
            }
        }
        public bool IsEditable => !IsNotEditable;


        private ICommand _mouseDoubleClick;
        public ICommand MouseDoubleClick => _mouseDoubleClick ??= new CommandHandler(MouseDoubleClickAction, true);
        private void MouseDoubleClickAction(object parameter)
        {
            IsNotEditable = false;
            ElementAreFocused?.Invoke(this, EventArgs.Empty);
            if (parameter is TextBox textBox)
            {
                textBox.Focus();
                textBox.Select(textBox.Text.Length, 0);
            }
        }


        private ICommand _loseFocus;
        public ICommand LoseFocus => _loseFocus ??= new CommandHandler(LoseFocusAction, true);
        private void LoseFocusAction(object parameter)
        {
            IsNotEditable = true;
        }


        private ICommand _clickDelete;
        public ICommand ClickDelete => _clickDelete ??= new CommandHandler(ClickDeleteAction, true);
        private void ClickDeleteAction(object parameter)
        {
            if (parameter is Model.PostIt postIt)
            {
                if (MessageBox.Show("Êtes-vous sûr ?", "Suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    PostItContext.DeletePostIt(postIt);
                }
            }
        }
    }
}
