/*
* File          :   TextViewer.xaml.cs
* Project       :   Capstone - ECE_Text_Analyzer
* Programmer    :   Gurkirt Singh
* Date          :   2019-04-07
* Desc          :   This file holds the interaction logic for TextViewer, which shows the text on screen.
*/
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
            if(message != null)
            {
                lb_creationDetail.Content = $"Created on: {message.Date}";
                tb_text.Text = message.Text;
            }
            
        }
    }
}
