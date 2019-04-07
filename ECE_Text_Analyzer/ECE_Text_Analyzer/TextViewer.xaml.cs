using System.Windows;

namespace ECE_Text_Analyzer
{
    /// <summary>
    /// Interaction logic for TextViewer.xaml
    /// </summary>
    public partial class TextViewer
    {
        public TextViewer(Message message)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            lb_creationDetail.Content = $"Created on: {message.Date}";
            tb_text.Text = message.Text;
        }
    }
}
