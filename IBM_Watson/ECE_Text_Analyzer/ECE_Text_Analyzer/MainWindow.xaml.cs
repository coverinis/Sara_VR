using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Example;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using System.ComponentModel;

namespace ECE_Text_Analyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread AnalyzerThread;
        private static MainWindow window;
        public MainWindow()
        {
            InitializeComponent();
            window = this;
            AnalyzerThread = new Thread(Example.Satrt);
            AnalyzerThread.Start();
        }

        public static void UpdateSetimentLable(DocumentSentimentResults sentimentResults)
        {
            window.Dispatcher.Invoke(()=>
            {
                window.lb_sentiLable.Content = $"Sentiment: {sentimentResults.Label.ToUpperInvariant()}";
                if (sentimentResults.Score > 0)
                {
                    window.lb_sentiLable.Foreground = Brushes.Green;
                }
                else window.lb_sentiLable.Foreground = Brushes.Red;
            });
        }

        public static void UpdateEmotionChart(DocumentEmotionResults emotionResults)
        {
            window.Dispatcher.Invoke(() =>
            {
                window.cs_Joy.ItemsSource = new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Joy", (int)(emotionResults.Emotion.Joy*100))
                };
                window.cs_Anger.ItemsSource = new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Anger", (int)(emotionResults.Emotion.Anger*100))
                };
                window.cs_Disgust.ItemsSource = new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Disgust", (int)(emotionResults.Emotion.Disgust*100))
                };
                window.cs_Sadness.ItemsSource = new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Sadness", (int)(emotionResults.Emotion.Sadness*100))
                };
                window.cs_Fear.ItemsSource = new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Fear", (int)(emotionResults.Emotion.Fear*100))
                };
            });
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            AnalyzerThread.Abort();
            base.OnClosing(e);
        }
    }
}
