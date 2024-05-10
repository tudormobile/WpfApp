using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Tudormobile.Wpf.Interfaces;

namespace Tudormobile.Wpf;

/// <summary>
/// Print and Print Preview service.
/// </summary>
public interface IPrintService : IWpfAppService
{
    /// <summary>
    /// Prints a DocumentPaginator object to the PrintQueue that is currently selected.
    /// </summary>
    /// <param name="printDialog">The printing dialog to utilize.</param>
    /// <param name="documentPaginator">The DocumentPaginator object to print.</param>
    /// <param name="description">A description of the job that is to be printed. This text appears in the user interface (UI) of the printer.</param>
    [ExcludeFromCodeCoverage]
    public void PrintDocument(PrintDialog printDialog, DocumentPaginator documentPaginator, string description)
        => printDialog.PrintDocument(documentPaginator, description);

    /// <summary>
    /// Prints a visual (non-text) object, which is derived from the Visual class, to the PrintQueue that is currently selected.
    /// </summary>
    /// <param name="printDialog">The Print printing dialog to utilize.</param>
    /// <param name="visual">The Visual to print.</param>
    /// <param name="description">A description of the job that is to be printed. This text appears in the user interface (UI) of the printer.</param>
    [ExcludeFromCodeCoverage]
    public void PrintVisual(PrintDialog printDialog, Visual visual, string description)
        => printDialog.PrintVisual(visual, description);

    /// <summary>
    /// Invokes the PrintDialog as a modal dialog box.
    /// </summary>
    /// <param name="printDialog">The printing dialog to invoke.</param>
    /// <returns>true if a user clicks Print; false if a user clicks Cancel; or null if a user closes the dialog box without clicking Print or Cancel.</returns>
    [ExcludeFromCodeCoverage]
    public bool? ShowDialog(PrintDialog printDialog)
        => printDialog.ShowDialog();
}
