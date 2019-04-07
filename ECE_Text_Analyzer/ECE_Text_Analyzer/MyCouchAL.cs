using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;

namespace ECE_Text_Analyzer
{
    /// <summary>
    /// Access Layer to the cloudent database instance, used to read all the documents in database.
    /// </summary>
    public class MyCouchAl
    {
        //the base url of cloudent useraccount
        private readonly string _url;

        /// <summary>
        /// Instantiate MyCouchAL with credentials
        /// </summary>
        /// <param name="apiKey">The API Key of user</param>
        /// <param name="password">The password of account</param>
        public MyCouchAl(string apiKey, string password)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(password))
            {
                //if the arguments are null or empty.
                throw new ArgumentNullException();
            }
            //build credential from api and password
            var credentials = $"{apiKey}:{password}";

            //build url from credentials
            _url = $"https://{credentials}@c86ec1db-aece-4f27-b8c7-9cae5a827ef5-bluemix.cloudant.com";
        }

        /// <summary>
        /// Asynchronously query all the documents from an instance of cloudent database, the information
        /// about instance of database is provided during the creation of MyCouchAL objetct.
        /// </summary>
        /// <returns>ObservableCollection holding the collection of documents or null if query to database failed</returns>
        public async Task<ObservableCollection<object>> GetAllDocsAsync()
        {
            //new observable collection
            ObservableCollection<object> ids = new ObservableCollection<object>();
            using (var client = new MyCouchClient(_url, "ecesr"))
            {
                //create query for all documents
                var query = new QueryViewRequest("_all_docs") {IncludeDocs = true};

                //should include document 

                //Execute query Asyncronously
                var requested = await client.Views.QueryAsync<dynamic>(query);
                if (requested.IsSuccess)
                {
                    //on success
                    //read all the documents in as Message type in collection
                    for (int i = 0; i < requested.RowCount; i++)
                    {
                        var message = JsonConvert.DeserializeObject<Message>(requested.Rows[i].IncludedDoc);
                        ids.Add(message);
                    }
                    //return collection
                    return ids;
                }

                throw new Exception("Service Unavailable: Instance of Couldent database or query is wrong");
            }
        }
    }

    /// <summary>
    /// Type to map the cloudent documents
    /// </summary>
    public class Message
    {
        public string Id { get; set; }
        public string Rev { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
    }
}
