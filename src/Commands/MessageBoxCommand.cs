using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Converters;

namespace Tudormobile.Wpf.Commands
{
    public class MessageBoxCommand : ProxyCommand
    {
        private readonly Action<MessageBoxResult> _resultAction;
        public MessageBoxCommand(Action<MessageBoxResult>? resultAction = null)
        {
            _resultAction = resultAction ?? (r => { });
        }
        protected override void OnExecute(object? parameter)
        {
            if (parameter is MessageBoxParameters p)
            {
                showMessageBox(p);
            }
            else if (parameter is ICommand cmd)
            {
                showMessageBox(new MessageBoxParameters() { Command = cmd });
            }
            else if (parameter is String s)
            {
                showMessageBox((MessageBoxParameters) new MessageBoxParametersConverter().ConvertFrom(s)!);
            }
            else
            {
                base.OnExecute(parameter);
            }
        }
        private void showMessageBox(MessageBoxParameters p)
        {
            MessageBoxResult result = MessageBoxResult.None;
            if (p.Button == null)
            {
                result = MessageBox.Show(p.Text, p.Caption ?? String.Empty);
            }
            else if (p.Icon == null)
            {
                result = MessageBox.Show(p.Text, p.Caption, (MessageBoxButton)p.Button);
            }
            else if (p.Result == null)
            {
                result = MessageBox.Show(p.Text, p.Caption, (MessageBoxButton)p.Button, (MessageBoxImage)p.Icon);
            }
            else
            {
                result = MessageBox.Show(p.Text, p.Caption, (MessageBoxButton)p.Button, (MessageBoxImage)p.Icon, (MessageBoxResult)p.Result);
            }
            //MessageBox.Show(p.Text);
            //MessageBox.Show(p.Text, p.Caption);
            //MessageBox.Show(p.Text, p.Caption, p.Button);
            //MessageBox.Show(p.Text, p.Caption, p.Button, p.Icon);
            //MessageBox.Show(p.Text, p.Caption, p.Button, p.Icon, p.Result);

            if (p.Command == null)
            {
                _resultAction?.Invoke(result);
            }
            else
            {
                p.Command.Execute(result);
            }
        }
    }
}
