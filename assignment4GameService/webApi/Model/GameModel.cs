using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    class GameModel
    {
        public string GameID { get; set; }
        public string Name { get; set; }
        public string GameCategory { get; set; }

        public GameModel(string a, string b, string c)
        {
            GameID = a;
            Name = b;
            GameCategory = c;
        }
       
    }
}