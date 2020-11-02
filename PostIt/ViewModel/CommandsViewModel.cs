using PostIt.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace PostIt.ViewModel
{
    public class CommandsViewModel : BaseViewModel
    {

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
    }
}
