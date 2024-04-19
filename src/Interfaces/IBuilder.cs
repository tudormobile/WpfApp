using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf
{
    /// <summary>
    /// Defines a class that provides method to build an instance of an object.
    /// </summary>
    /// <typeparam name="T">Type of object to build.</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Builds an instance of an object.
        /// </summary>
        /// <returns>An instance of T.</returns>
        T Build();
    }
}
