using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public string[] RegisteredGame { get; set; }

        public int NumberOfGames { get; set; }
        public UserModel(string userID, string userName, string name, string email)
        {
            Username = userName;
            UserID = userID;
            Name = name;
            Email = email;
            RegisteredGame = new string[25];
            NumberOfGames = 0;
        }
        public bool addGame(string x)
        {
            foreach (string game in RegisteredGame)
            {
                if (game == x)
                {
                    return false;
                }
            }
            RegisteredGame[NumberOfGames] = x;
            NumberOfGames++;
            return true;
        }
    }
}
