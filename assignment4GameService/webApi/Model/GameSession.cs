using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApi.Model
{
    class GameSession
    {
        public string SessionId { get; set; }
        public string GameID { get; set; }
        public string UserID { get; set; }
        public int Score { get; set; }
        public string DateTime { get; set; }

        public GameSession(string s, string g, string u, int score, string d)
        {
            SessionId = s;
            GameID = g;
            UserID = u;
            Score = score;
            DateTime = d;
        }

    }
}
