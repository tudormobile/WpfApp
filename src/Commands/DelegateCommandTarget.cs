using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf.Commands
{
    // Represents the associated target methods for 'Execute' and 'CanExecute'
    internal class DelegateCommandTarget
    {
        public object Target { get; set; }
        public MethodInfo? ExecuteMethod { get; set; }
        public MethodInfo? CanExecuteMethod { get; set; }
        public string ClassName { get; set; }
        public string CommandName { get; set; }
        public string Key { get; }
        public DelegateCommandTarget(string className, string commandName, object target)
        {
            ClassName = className;
            CommandName = commandName;
            Target = target;
            Key = CreateKey(className, commandName, target);
        }
        public static string CreateKey(string className, string commandName, object target)
            => string.Join("+", className, commandName);
    }
}
