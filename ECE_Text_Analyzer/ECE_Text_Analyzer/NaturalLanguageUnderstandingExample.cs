/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace ECE_Text_Analyzer
{
    public class NaturalLanguageUnderstanding
    {
        private readonly NaturalLanguageUnderstandingService _nluService;

        #region Constructor
        public NaturalLanguageUnderstanding(string apiKey)
        {
            IBM.WatsonDeveloperCloud.Util.TokenOptions token = new IBM.WatsonDeveloperCloud.Util.TokenOptions()
            {
                IamApiKey = apiKey
            };
            
            _nluService = new NaturalLanguageUnderstandingService(token, "2018-12-19");
            //_naturalLanguageUnderstandingService.SetEndpoint(url);
        }
        #endregion

        #region Analyze
        public void Analyze(string textToAnalyze)
        {
            if (textToAnalyze.Length < 20)
            {
                MainWindow.NotifyExceptionOnUi("Not enough text to Analyze");
                return;
            }
            Parameters parameters = new Parameters()
            {
                Text = textToAnalyze,
                Features = new Features()
                {
                    Emotion = new EmotionOptions()
                    {
                        Document = true
                    },
                    Sentiment = new SentimentOptions()
                    {
                        Document = true
                    }
                }
            };
            var result = _nluService.Analyze(parameters);
            MainWindow.UpdateSentimentLabel(result.Sentiment.Document);
            MainWindow.UpdateEmotionChart(result.Emotion.Document);
            
        }
        #endregion
    }
}
