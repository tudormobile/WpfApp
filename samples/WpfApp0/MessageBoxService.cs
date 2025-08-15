namespace WpfApp0;

public class MessageBoxService : IMessageBoxService
{
    public void ShowMessage(string message)
    {
        System.Windows.MessageBox.Show(message);
    }
}

public interface IMessageBoxService
{
    void ShowMessage(string message);
}


