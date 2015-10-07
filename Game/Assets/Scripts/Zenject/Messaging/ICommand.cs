using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlyDown.Messaging
{
    /// <summary>
    /// Defines a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Defines the method that determines whether the command can 
        /// execute in its current state.
        /// </summary>
        /// <param name="param">
        /// Data used by the command. If the command does not require 
        /// data to be passed, this object can be set to null.
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        bool CanExecute(object param);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="param">
        /// Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to null.
        /// </param>
        void Execute(object param);
    }
}
