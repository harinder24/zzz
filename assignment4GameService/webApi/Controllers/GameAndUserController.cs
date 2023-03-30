using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi.Model;

namespace webApi.Controllers
{
    
    public class GameAndUserController : Controller
    {




        private static GameModel rdr = new GameModel("1", "rdr", "adventure");
        private static GameModel gta = new GameModel("2", "gta", "adventure");
        private static GameModel mario = new GameModel("3", "mario", "arcade");
        private static GameModel cod = new GameModel("4", "cod", "battle royale");
        private static GameModel pokemon = new GameModel("5", "pokemon", "fantasy");
        private static List<UserModel> users = new List<UserModel>();
        private static List<GameModel> games = new List<GameModel>();
        private static List<GameSession> sessionList = new List<GameSession>();

        public static void fillGameModel() {
            if(games.Count == 0)
            {
                games.Add(rdr);
                games.Add(gta);
                games.Add(mario);
                games.Add(cod);
                games.Add(pokemon);
            }
        }


        [HttpGet]
        [Route("api/userinfo")]
        public ActionResult<string> GetUserInfo([FromQuery] string id)
        {
            string result = "\nno";
            foreach (var ppl in users)
            {
                if (ppl.UserID == id)
                {
                    result = "\nUser ID: " + ppl.UserID;
                    result += "\nUser Name: " + ppl.Name;
                    result += "\nUser username: " + ppl.Username;
                    result += "\nUser email: " + ppl.Email;
                }
            }
            return result;
        }

        [HttpGet]
        [Route("api/usergames")]
        public ActionResult<string> GetUserGames([FromQuery] string id)
        {
            string result = "\nno games registered";
            foreach (var ppl in users)
            {
                if (ppl.UserID == id)
                {
                    for(int i = 1; i < ppl.RegisteredGame.Length + 1; i++)
                    {
                        if(ppl.RegisteredGame[i-1] != null)
                        {
                            if (i == 1)
                            {
                                result = "";
                            }
                            result += "\n" + i + ". " + ppl.RegisteredGame[i - 1];
                        }
                        
                    }
                    //result = "\nUser ID: " + ppl.UserID;
                    //result += "\nUser Name: " + ppl.Name;
                    //result += "\nUser username: " + ppl.Username;
                    //result += "\nUser email: " + ppl.Email;
                }
            }
            return result;
        }

        [HttpGet]
        [Route("api/isvalid")]
        public ActionResult<string> GetUserValidation([FromQuery] string id)
        {
           
            foreach (var ppl in users)
            {
                if(ppl.UserID == id)
                {
                    return "\nyes";
                }
            }
            return "\nno";
        }
        [HttpGet]
        [Route("api/getgames")]
        public ActionResult<string> GetGames()
        {
            fillGameModel();
            string result = "";
            if (games.Count == 0)
            {
                result = "\nNo games found";
            }
           
            for (var i = 1; i < games.Count+1; i++)
            {
                result += "\n" + i + ". " + games[i-1].Name + ", id = " + games[i-1].GameID;

            }
            return result;
        }



        [HttpGet]
        [Route("api/getusers")]
        public ActionResult<string> GetUsers()
            {
                string result = "";
            if(users.Count == 0)
            {
                result = "\nNo users found";
            }
              
                for (var i = 1; i < users.Count+1; i++)
                {
                    result += "\n" + i + ". " + users[i-1].Name + ", id = " + users[i-1].UserID;

                }
                return result;
            }

        [HttpGet]
        [Route("api/getsession")]
        public ActionResult<string> getSession([FromQuery] string id)
        {
            string result = "";
            int i = 1;
            foreach(var x in sessionList)
            {
                if(x.UserID == id)
                {
                    result += "\n\n"+i+".\nScore = " + x.Score + " in game id = " + x.GameID + " by user id = " + x.UserID;
                    i++;
                }
                
            }
            if(result == "")
            {
                result = "No sessions";
            }
            return result;
        }

        [HttpGet]
        [Route("api/topscore")]
        public ActionResult<string> TopScore([FromQuery] string id)
        {
            int first = 0;
            int second = 0;
            int third = 0;

            string fuser = "";
            string suser = "";
            string thuser = "";
        
            foreach(var ses in sessionList)
            {
                if(ses.GameID == id)
                {
                    
                    if(ses.Score > first)
                    {

                        third = second;
                        thuser = suser;
                        second = first;
                        suser = fuser;
                        first = ses.Score;
                        fuser = ses.UserID;
                    }else if (ses.Score > second)
                    {
                        third = second;
                        thuser = suser;
                        second = ses.Score;
                        suser = ses.UserID;
                    }
                    else if (ses.Score > third)
                    {
                        third = ses.Score;
                        thuser = ses.UserID;
                    }
                }
            }
            if(first == 0)
            {
                return "No Scores available for this game add sessions first";
            }
            string result = "";
            result += "\n1.\nScore = " + first + " by user id = " + fuser;
            if(second == 0)
            {
                result += "\n2.\nNo Score available for second position";
            }
            else
            {
                result += "\n\n2.\nScore = " + second + " by user id = " + suser;
            }
            if (third == 0)
            {
                result += "\n3.\nNo Score available for third position";
            }
            else
            {
                result += "\n\n3.\nScore = " + third + " by user id = " + thuser;
            }
            return result;
        }

        [HttpPut]
        [Route("api/removesession")]
        public ActionResult<string> RemoveUserSession(string sid, string uid)
        {
            int i = 0;
            foreach(var x in sessionList)
            {
                if(x.SessionId == sid)
                {
                    if(x.UserID == uid)
                    {
                        sessionList.RemoveAt(i);
                        return "\nSession Succesfully Removed";
                    }
                    return "\nThis session id does not belong to you!!!";
                }
                i++;
            }
            return "\nSession does not exist with this id";
        }

        [HttpPost]
        [Route("api/addsession")]
        public ActionResult<string> AddUserSession(string sid, string gid, string uid, string score)
        {
            bool truth = true;
            foreach (var item in games)
            {
                if (item.GameID == gid)
                {
                    truth = false;
                }
            }
            if (truth)
            {
                return "\nNo game exist with this game id";
            }
            foreach (var item in sessionList)
            {
                if(item.SessionId == sid)
                {
                    return "\nSession already exist with this session id";
                }
            }
            string dateTime = DateTime.Now.ToString();
            int sc = int.Parse(score);
            GameSession x = new GameSession(sid,gid,uid,sc,dateTime);
            sessionList.Add(x);
            return "\nSession Successfully Added";
        }
        [HttpPost]
        [Route("api/addgame")]
        public ActionResult<string> AddUserGame(string gid, string uid)
        {
            fillGameModel();
            string gameName = "";
            foreach(var i in games)
            {
                if(i.GameID == gid)
                {
                    gameName = i.Name;
                }
            }
            if(gameName == "")
            {
                return "\nNot valid game id";
            }
            foreach (var ppl in users)
            {
                if (ppl.UserID == uid)
                {
                    for (int i = 0; i < ppl.RegisteredGame.Length; i++)
                    {
                        if(ppl.RegisteredGame[i] == gameName)
                        {
                            return "\nYou are already registed fpr this game";
                        }
                    }
                    ppl.addGame(gameName);

                    //result = "\nUser ID: " + ppl.UserID;
                    //result += "\nUser Name: " + ppl.Name;
                    //result += "\nUser username: " + ppl.Username;
                    //result += "\nUser email: " + ppl.Email;
                }
            }
            return "\nGame succesfully added";

        }

        [HttpPost]
            [Route("api/adduser")]
        public ActionResult<string> AddUsers(string uid, string una, string na , string ema)
            {

           
                for (var i = 0; i < users.Count; i++)
                {
                    if (users[i].UserID == uid)
                    {
                        return "\nUser already exist with this user id: " + uid;
                    }

                }

                UserModel x = new UserModel(uid,una,na,ema);
                users.Add(x);

                return "\nUser added";
            }

        [HttpDelete]
        [Route("api/deleteuser")]
        public ActionResult<string> DeleteUser([FromQuery] string id)
        {
            string result = "\nUser not found";
       
            for (var i = 0; i < users.Count; i++)
            {
                if (users[i].UserID == id)
                {
                    users.RemoveAt(i);
                    result = "\nUser successfully deleted";
                }
            }
            return result;
        }
    }
    }
