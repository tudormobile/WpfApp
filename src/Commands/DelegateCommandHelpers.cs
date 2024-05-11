using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf.Commands
{
    // Help methods for dealing with delegate commands
    internal static class DelegateCommandHelpers
    {
        public static DelegateCommand CreateDelegateCommand(object target, MethodInfo? executeMethod, MethodInfo? canExecuteMethod)
        {
            // First, the execute
            var executeDelegate = CreateDelegate(target, executeMethod) ?? new Action<object>(o => { }); // cannot be (null)
            var canExecuteDelegate = CreateDelegate(target, canExecuteMethod); // can be (null)
            var ps = executeMethod?.GetParameters() ?? canExecuteMethod?.GetParameters()!;  // cannot BOTH be (null)
            if (ps.Length == 0)
            {
                return new DelegateCommand(executeDelegate, canExecuteDelegate);
            }
            else if (ps.Length == 1)
            {
                var commandType = typeof(DelegateCommand<>).MakeGenericType(ps[0].ParameterType);
                return (DelegateCommand)Activator.CreateInstance(commandType, executeDelegate, canExecuteDelegate)!;
            }
            throw new NotSupportedException("Wrong number of arguments.");
        }

        public static Delegate? CreateDelegate(object target, MethodInfo? method)
        {
            Delegate? result = null;
            if (method != null)
            {
                if (method.ReturnType == typeof(void))
                {
                    // Normal, sync, execute method
                    var ps = method.GetParameters();
                    if (ps.Length == 0)
                    {
                        result = new Action(() => method.Invoke(target, []));
                    }
                    else if (ps.Length == 1)
                    {
                        var delegateType = typeof(Action<>).MakeGenericType(ps[0].ParameterType);
                        result = Delegate.CreateDelegate(delegateType, target, method.Name);
                    }
                }
                else if (method.ReturnType == typeof(bool))
                {
                    // Normal, sync can execute method
                    var ps = method.GetParameters();
                    if (ps.Length == 0)
                    {
                        result = new Func<bool>(() => (bool)method.Invoke(target, [])!);
                    }
                    else if (ps.Length == 1)
                    {
                        var delegateType = typeof(Func<,>).MakeGenericType(ps[0].ParameterType, typeof(bool));
                        result = Delegate.CreateDelegate(delegateType, target, method.Name);
                    }
                }
                else if (method.ReturnType.IsAssignableTo(typeof(Task)))
                {
                    // add support for async methods?
                    throw new NotSupportedException("No async support (for now).");
                }
            }
            return result;
        }
    }
}
