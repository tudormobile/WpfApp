using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    // Locate and wire-up command methods to commands.
    internal class DelegateCommandLocator
    {
        private readonly Dictionary<string, DelegateCommandTarget> _handlers = [];

        // locate and register all handlers on this object.
        internal void RegisterHandlers(object o)
        {
            var t = o.GetType();
            var ms = t.GetMethods().Where(m => m.CustomAttributes.Any()).ToArray();

            foreach (var m in ms)
            {
                // CanExecute handlers (FIRST!)
                var cea = m.GetCustomAttribute<CanExecuteAttribute>();
                if (cea != null)
                {
                    // register
                    RegisterHandler(cea.ClassName ?? String.Empty, cea.CommandName, o, m, isCanExecute: true);
                }
                else
                {
                    // Execute handlers (ELSE!)
                    var ea = m.GetCustomAttribute<ExecuteAttribute>();
                    if (ea != null)
                    {
                        // register
                        RegisterHandler(ea.ClassName ?? String.Empty, ea.CommandName, o, m);
                    }
                }
            }
        }

        // Register a method as an ICommand handler for Execute or CanExecute
        internal void RegisterHandler(string className, string commandName, object target, MethodInfo method, bool isCanExecute = false)
        {
            var commandTarget = new DelegateCommandTarget(className, commandName, target)
            {
                CanExecuteMethod = isCanExecute ? method : null,
                ExecuteMethod = isCanExecute ? null : method,
            };
            if (_handlers.TryGetValue(commandTarget.Key, out var existingTarget))
            {
                // replace existing methods (if null)
                existingTarget.CanExecuteMethod ??= commandTarget.CanExecuteMethod;
                existingTarget.ExecuteMethod ??= commandTarget.ExecuteMethod;
            }
            else
            {
                _handlers.Add(commandTarget.Key, commandTarget);
            }
        }

        // Resolves ICommand handlers to registered methods
        internal void ResolveHandlers(object m)
        {
            var emptyCommands = m.GetType().GetProperties().Where(p =>
                p.CanWrite && p.CanRead &&
                p.PropertyType.IsAssignableTo(typeof(ICommand)) &&
                p.GetValue(m) == null).ToArray();

            var className = m.GetType().Name;
            foreach (var cmd in emptyCommands)
            {
                if (tryCreateICommand(className, cmd.Name, m, out var command))
                {
                    cmd.SetValue(m, command);
                }
            }
        }

        private bool tryCreateICommand(string className, string commandName, object target, out ICommand? command)
        {
            command = null;
            var key = DelegateCommandTarget.CreateKey(className, commandName, target);
            if (_handlers.TryGetValue(key, out DelegateCommandTarget? commandTarget) || _handlers.TryGetValue(DelegateCommandTarget.CreateKey(String.Empty, commandName, target), out commandTarget))
            {
                command = DelegateCommandHelpers.CreateDelegateCommand(commandTarget.Target, commandTarget.ExecuteMethod, commandTarget.CanExecuteMethod);
                return true;
            }
            return false;
        }
    }
}
