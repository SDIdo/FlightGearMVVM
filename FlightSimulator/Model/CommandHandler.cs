using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <summary>
/// Responsible for the commands
/// </summary>
namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        /// <summary>
        /// ctor for this command handler
        /// </summary>
        /// <param name="action">upon condition match this action is performed</param>
        public CommandHandler(Action action)
        {
            _action = action;
        }
        /// <summary>
        /// conditioning whether a certain command is applicable
        /// </summary>
        /// <param name="parameter">offered param to comply the condition</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// fires up an action
        /// </summary>
        /// <param name="parameter">offerd param to comply</param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
