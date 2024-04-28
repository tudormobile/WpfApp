using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf
{
    /// <inheritdoc/>
    public class CommandLine : ICommandLine
    {
        /// <inheritdoc/>
        public String ProgramName { get; }

        /// <inheritdoc/>
        public string[] Arguments { get; }

        internal CommandLine(string[] args) : this(null, args) { }

        internal CommandLine(string? programName = null, string[]? args = null)
        {
            ProgramName = programName ?? Environment.ProcessPath!;
            Arguments = args ?? Environment.GetCommandLineArgs();
        }

        /// <inheritdoc/>
        public virtual (bool Exists, string? Value) this[string key]
        {
            get
            {
                var success = Arguments.Contains(key);
                string? value = Arguments.SkipWhile(a => a != key).Skip(1).FirstOrDefault();
                return (success, value != null && value.StartsWith('-') ? null : value);
            }
        }
    }
}
