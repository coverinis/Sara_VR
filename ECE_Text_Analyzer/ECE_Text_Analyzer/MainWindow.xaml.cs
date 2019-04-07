using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ECE_Text_Analyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string _ibmServiceAccessAPI = "L8eE0WANUQfQOiEa5iA2TCOI4zbHV-_bHxVko7IBKgkr";
        private NaturalLanguageUnderstanding _languageUnderstanding;
        private Thread _analyzerThread;
        private static MainWindow _window;

        public ObservableCollection<object> Documents { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _window = this;
            DataContext = this;
        }
    
        public static void UpdateSentimentLabel(DocumentSentimentResults sentimentResults)
        {
            _window.Dispatcher.Invoke(()=>
            {
                _window.lb_sentiLable.Content = $"{sentimentResults.Label.ToUpperInvariant()}";
                _window.lb_sentiLable.Foreground = sentimentResults.Score > 0 ? Brushes.Green : Brushes.Red;
            });
        }

        public static void UpdateEmotionChart(DocumentEmotionResults emotionResults)
        {
            _window.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (emotionResults != null)
                    {
                        _window.cs_Emotion.ItemsSource = new[]
                        {
                            new KeyValuePair<string, double?>("Joy", value: (emotionResults.Emotion.Joy * 100)),
                            new KeyValuePair<string, double?>("Anger", (emotionResults.Emotion.Anger * 100)),
                            new KeyValuePair<string, double?>("Disgust", (emotionResults.Emotion.Disgust * 100)),
                            new KeyValuePair<string, double?>("Sadness", (emotionResults.Emotion.Sadness * 100)),
                            new KeyValuePair<string, double?>("Fear", (emotionResults.Emotion.Fear * 100))
                        };
                    }
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e);
                }
                
            });
            
        }

        private void AnalyzeEmotionFromText(string text)
        {
            if(_languageUnderstanding == null)
            {
                _languageUnderstanding = new NaturalLanguageUnderstanding(_ibmServiceAccessAPI);
            }
            _analyzerThread = new Thread(() => _languageUnderstanding.Analyze(text));
            _analyzerThread.Start();
            
        }

        private async void Cb_text_Loaded(object sender, RoutedEventArgs e)
        {
            var myCouchAl = new MyCouchAl("bentionedencepterbyniner", "c286c3f1300c7368ea63f2220f1633b5342f7482");
            try
            {
                Documents = await myCouchAl.GetAllDocsAsync();
                cb_text.ItemsSource = Documents;
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Documents Query to Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Cb_text_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem is Message message) AnalyzeEmotionFromText(message.Text);
        }

        private void Btn_ShowText_Click(object sender, RoutedEventArgs e)
        {
            if (cb_text.SelectedItem is Message message)
            {
                TextViewer textViewer = new TextViewer(message);
                textViewer.ShowDialog();
            }
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_analyzerThread != null)
            {
                if (_analyzerThread.IsAlive) _analyzerThread.Abort();
            }
            base.OnClosing(e);
        }

        public static void NotifyExceptionOnUi(string message)
        {
            _window.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(_window, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }
    }
}
