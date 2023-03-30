

using Assignment3.DocumentFolder;
using DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Assignment3
{
    public class Program
    {
        public DocumentRepositoryAsync documentRepository;

        public Program()
        {
            documentRepository = new DocumentRepositoryAsync();
        }
            static async Task Main(string[] args)
            {

            // Both Document and DocumentRespositryAsync is in DataModel class library 

            var prg = new Program();

            // Part 1
            // await prg.TestDocumentAsync();

            //Part 2
            await prg.CallDocumentServiceThroughRepository();   // for Get(id)
            await prg.CallDocumentServiceThroughRepository();  // for GetAll
        }


        public async void addData()
        {
            var document1 = new Document() { Id = "Envi001", Title = "Environment Report 02/03", Author = "Jon Voight", Text = "some text ..." };
            var document2 = new Document() { Id = "News001", Title = "World News 02/03", Author = "Mark Anthony", Text = "news text ..." };


            await documentRepository.Add(document1);
            await documentRepository.Add(document2);
        }

        private async Task CallDocumentServiceThroughRepository()
        {
            var doc = await documentRepository.GetFromService("Envi001");
        }

        private async Task CallDocumentServiceForAllData()
        {
            var doc = await documentRepository.GetAllFromService();
        }

        public async Task TestDocumentAsync()
        {
            var documentRepository = new DocumentRepositoryAsync();
            var document1 = new Document() { Id = "Envi001", Title = "Environment Report 02/03", Author = "Jon Voight", Text = "some text ..." };
            var document2 = new Document() { Id = "News001", Title = "World News 02/03", Author = "Mark Anthony", Text = "news text ..." };


            await documentRepository.Add(document1);
            await documentRepository.Add(document2);

            await PrintAll(await documentRepository.GetAll());  // to check add and get all method

            var document3 = new Document() { Id = "George007", Title = "Cstp News 02/03", Author = "Harinder", Text = "class text ..." };

            await documentRepository.Update("News001", document3);

            await PrintAll(await documentRepository.GetAll()); // to check update

            Document getDocumentByGetMethod = await documentRepository.Get("George007");
            Console.WriteLine("\nId = " + getDocumentByGetMethod.Id + ", Title = " + getDocumentByGetMethod.Title + ", Author = " + getDocumentByGetMethod.Author + ", Text = " + getDocumentByGetMethod.Text);
            // to check get method

            await documentRepository.Remove("George007");
            await PrintAll(await documentRepository.GetAll()); // to check Remove method

            Console.ReadKey();

        }

        public async Task PrintAll(List<Document> doc)
        {
            String output = "";
            foreach (Document document in doc)
            {
                output += "\nId = " + document.Id + ", Title = " + document.Title + ", Author = " + document.Author + ", Text = " + document.Text;
            }

            Console.WriteLine(output);

            await Task.CompletedTask;
        }
    }
}
