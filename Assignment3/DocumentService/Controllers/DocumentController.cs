using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using Assignment3;

namespace DocumentService.Controllers
{
    public class DocumentController : Controller
    {

        
      [HttpGet]
      [Route("api/[controller]")]

    


      public async Task<ActionResult<Document>> GetAsync(string id)
        {
            var x = new Assignment3.Program();
            x.addData();


            await Task.Delay(200);

            Document doc = await x.documentRepository.Get(id);
            


            //var doc = new Document()
            //{
            //    Id = id,
            //    Title = "Global recession",
            //    Author = "J.K. Rowling",
            //    Text = "news....recession...news"
            //};
            return doc;
        
        }


        [HttpGet]
        [Route("api/all")]
        public async Task<ActionResult<List<Document>>> GetAsync()
        {
            var x = new Assignment3.Program();
            x.addData();


            await Task.Delay(200);

            var doc = await x.documentRepository.GetAll();

            return doc;
        }
    }
}
