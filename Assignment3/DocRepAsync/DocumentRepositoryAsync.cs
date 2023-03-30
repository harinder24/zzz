
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataModels;

namespace Assignment3.DocumentFolder
{
    public class DocumentRepositoryAsync
    {
        private List<Document> documents = new List<Document>();

        public async Task<Document> Get(string id)
        {
            await Task.Delay(500);
            foreach (Document document in documents)
            {
                if (document.Id == id) return document;
            }

            return null;
        }

        public async Task<string> GetFromService(string id)
        {
            string document = null;
            try
            {
                var httpClient = new HttpClient();
                var url = "https://localhost:44355/api/document?id={id}";
                document = await httpClient.GetStringAsync(url);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            return document;
        }

        public async Task<List<Document>> GetAll()
        {
            await Task.Delay(500);
            return documents;
        }

        public async Task Add(Document document)
        {
            await Task.Delay(500);
            documents.Add(document);
        }

        public async Task Remove(string id)
        {
            await Task.Delay(500);
            for (int i = 0; i < documents.Count; i++)
            {
                if (documents[i].Id == id)
                {
                    documents.RemoveAt(i);
                    break;
                }
            }
        }

        public async Task Update(string id, Document document)
        {
            await Task.Delay(500);
            for (int i = 0; i < documents.Count; i++)
            {
                if (documents[i].Id == id)
                {
                    documents[i] = document;
                    break;
                }
            }
        }

        
    }
}

