using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf;

/// <summary>
/// Represents the current process command line.
/// </summary>
public interface ICommandLine
{
    /// <summary>
    /// Retrieve a command line argument.
    /// </summary>
    /// <param name="key">Argument name.</param>
    /// <returns>(true, value) if the argument exists (value is optional)</returns>
    (bool Exists, string? Value) this[string key] { get; }

    /// <summary>
    /// Name of the process currently running.
    /// </summary>
    public String ProgramName { get; }

    /// <summary>
    /// Command line arguments.
    /// </summary>
    public string[] Arguments { get; }

}
