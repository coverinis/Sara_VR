/*
* File          :   NaturalLanguageUnderstanding.cs
* Project       :   Capstone - ECE_Text_Analyzer
* Programmer    :   Orignaly by IBM Corp. and edited by Gurkirt Singh to meet the requirements
* Date          :   2019-04-07
* Desc          :   This file has interaction logic for main window
*/
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
    /// <summary>
    /// IBM Waston 's Natural Language Understanding service Accessor
    /// </summary>
    public class NaturalLanguageUnderstanding
    {
        private readonly NaturalLanguageUnderstandingService _nluService;

        #region Constructor
        public NaturalLanguageUnderstanding(string apiKey)
        {
            //create token with api
            IBM.WatsonDeveloperCloud.Util.TokenOptions token = new IBM.WatsonDeveloperCloud.Util.TokenOptions()
            {
                IamApiKey = apiKey
            };
           //using token create IBM Wastone service accessor
            _nluService = new NaturalLanguageUnderstandingService(token, "2018-12-19");
            //_naturalLanguageUnderstandingService.SetEndpoint(url);
        }
        #endregion

        #region Analyze

        /*
         * Method       :   Analyze
         * Purpose      :   To analyze text using IBM Waston's NLU service
         * Parameters   :   string - contain text to analyze
         * Returns      :   Nothing
         */
        public void Analyze(string textToAnalyze)
        {
            // create paramters for what are the things we need to 
            // analyze from text
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
            try
            {
                //start analyzing
                var result = _nluService.Analyze(parameters);
                //update the sentiment on ui
                MainWindow.UpdateSentimentLabel(result.Sentiment.Document);
                //update the chart on ui
                MainWindow.UpdateEmotionChart(result.Emotion.Document);
            }
            catch (System.Exception)
            {
                //on exception
                //show the error on screen
                MainWindow.NotifyExceptionOnUi("The language of text is unsupported or length of text is very small");
            }
            
            
        }
        #endregion
    }
}
