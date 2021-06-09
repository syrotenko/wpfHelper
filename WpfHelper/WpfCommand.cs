using System;
using System.Windows.Input;

namespace GenerateBranchName
{
    public class WpfCommand : ICommand
    {
        /// <summary>
        /// WpfCommand constructor with Action without parameters and "constant" predicate
        /// </summary>
        /// <param name="action">Action without parameters</param>
        /// <param name="canExecuteAction">Constant predicate</param>
        public WpfCommand (Action action, bool constantPredicateValue)
        {
            CustomAction = (dummy) => action?.Invoke();
            CanExecuteAction = (dummy) => constantPredicateValue;
        }

        /// <summary>
        /// WpfCommand constructor with Action with parameters and "constant" predicate
        /// </summary>
        /// <param name="action">Action with parameters</param>
        /// <param name="canExecuteAction">Constant predicate</param>
        public WpfCommand (Action<object> action, bool constantPredicateValue)
        {
            CustomAction = action;
            CanExecuteAction = (dummy) => constantPredicateValue;
        }

        /// <summary>
        /// WpfCommand constructor with Action without parameters
        /// </summary>
        /// <param name="action">Action without parameters</param>
        /// <param name="canExecuteAction">Predicate</param>
        public WpfCommand (Action action, Predicate<object> canExecuteAction)
        {
            CustomAction = (dummy) => action?.Invoke();
            CanExecuteAction = canExecuteAction;
        }

        /// <summary>
        /// WpfCommand constructor with Action with parameters
        /// </summary>
        /// <param name="action">Action with parameters</param>
        /// <param name="canExecuteAction">Predicate</param>
        public WpfCommand (Action<object> action, Predicate<object> canExecuteAction)
        {
            CustomAction = action;
            CanExecuteAction = canExecuteAction;
        }

        /// <summary> Main action </summary>
        private Action<object> CustomAction { get; set; }
        /// <summary> Main predicate </summary>
        private Predicate<object> CanExecuteAction { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged ()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction != null && CanExecuteAction.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            CustomAction?.Invoke(parameter);
        }
    }
}
