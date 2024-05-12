using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf.Commands
{
    /// <summary>
    /// Identifies a method as an ICommand 'Execute()' handler.
    /// </summary>
    /// <remarks>
    /// The 'ClassName' may be omitted, however, the name of the ICommand property combined
    /// with the name of the class containing the property must be unique throughout the configured
    /// assemblies. You can define 'Execute' or 'CanExecute' methods independently for the same command.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecuteAttribute : Attribute
    {
        /// <summary>
        /// The ICommand property name to link to.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// The class containing the ICommand property.
        /// </summary>
        public string? ClassName { get; }

        /// <summary>
        /// Create an initialize a new instance.
        /// </summary>
        /// <param name="commandName">The ICommand property name.</param>
        public ExecuteAttribute(string commandName)
        {
            CommandName = commandName;
        }

        /// <summary>
        /// Create and initialize a new instance.
        /// </summary>
        /// <param name="className">The class containing the ICommand property.</param>
        /// <param name="commandName">The ICommand property name.</param>
        public ExecuteAttribute(string className, string commandName)
        {
            ClassName = className;
            CommandName = commandName;
        }
    }

    /// <summary>
    /// Identifies a method as an ICommand 'CanExecute()' handler.
    /// </summary>
    /// <remarks>
    /// The 'ClassName' may be omitted, however, the name of the ICommand property combined
    /// with the name of the class containing the property must be unique throughout the configured
    /// assemblies. You can define 'Execute' or 'CanExecute' methods independently for the same command.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class CanExecuteAttribute : ExecuteAttribute
    {
        /// <summary>
        /// Create an initialize a new instance.
        /// </summary>
        /// <param name="commandName">The ICommand property name.</param>
        public CanExecuteAttribute(string commandName) : base(commandName) { }

        /// <summary>
        /// Create and initialize a new instance.
        /// </summary>
        /// <param name="className">The class containing the ICommand property.</param>
        /// <param name="commandName">The ICommand property name.</param>
        public CanExecuteAttribute(string className, string commandName) : base(className, commandName) { }
    }
}
