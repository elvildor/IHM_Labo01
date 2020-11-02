using PostIt.Database;
using PostIt.Model;
using PostIt.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace PostIt.ViewModel
{
    public class CommandsViewModel : BaseViewModel
    {
        public CommandsViewModel(Category defaultCategory)
        {
            DefaultCategory = defaultCategory;
        }

        public Category DefaultCategory { get; protected set; }

        private bool _buttonVisibility;
        public bool ButtonVisibility
        {
            get => _buttonVisibility;
            protected set
            {
                _buttonVisibility = value;
                OnPropertyChanged();
            }
        }

        private ICommand _mouseEnter;
        public ICommand MouseEnter => _mouseEnter ??= new CommandHandler(MouseEnterAction, true);
        private void MouseEnterAction(object parameter)
        {
            ButtonVisibility = true;
        }

        private ICommand _mouseLeave;
        public ICommand MouseLeave => _mouseLeave ??= new CommandHandler(MouseLeaveAction, true);
        private void MouseLeaveAction(object parameter)
        {
            ButtonVisibility = false;

        }


        private ICommand _addButton;
        public ICommand AddButton => _addButton ??= new CommandHandler(AddButtonAction, true);
        private void AddButtonAction(object parameter)
        {
            ButtonVisibility = false;
            System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            if (parameter is SolidColorBrush colorBrush)
                color = System.Drawing.Color.FromArgb(colorBrush.Color.A, colorBrush.Color.R, colorBrush.Color.G, colorBrush.Color.B);
            PostItContext.CreatePostIt(string.Empty, DefaultCategory, color);
        }
    }
}
