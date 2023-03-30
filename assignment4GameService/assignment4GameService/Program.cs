using System;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace assignment4GameService
{
    class Program
    {
        static void Main(string[] args)
        {
            getUserInputInitial();
        }

        public static void getUserInputInitial()
        {
            Console.WriteLine("\nEnter\n1 to create User\n2 delete user\n3 login with User ID\n4. Get all User's name\n5. Get all game's name\n6 to get Top Scores of a game\nq to quit:\nLOGIN AS A USER TO SEE MORE OPTIONS");

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    CreateUser();
                }
                else if (input == "2")
                {
                    DeleteUser();
                }
                else if (input == "3")
                {
                    getUserInput("");
                }
                else if (input == "4")
                {
                    getAllUserName();
                }
                else if (input == "5")
                {
                    getAllGamesName();
                }
                else if (input == "6")
                {
                    topScore();
                }
                else if (input.ToLower() == "q")
                {
                    break;
                }
            }
        }

        public static void topScore()
        {
            string gameId = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameId = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameId));

            string b_url = "https://localhost:44372/api/topscore?id="+gameId;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInputInitial();
                }
            }

        }
        public static void getUserInput(string userID)
        {
            string UserID;
            if (userID == "")
            {
                do
                {
                    Console.WriteLine("\nEnter user id: ");
                    UserID = Console.ReadLine();
                } while (string.IsNullOrEmpty(UserID));

                string b_url = "https://localhost:44372/api/isvalid?id=" + UserID;
                HttpClient client = new HttpClient();
                var response = client.GetAsync(b_url).Result;
                if (response.Content.ReadAsStringAsync().Result == "no") 
                {
                    Console.WriteLine("\nUser with this userId doesnot exist\nEnter\n1 To go back\n2 To try again");
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            getUserInputInitial();
                        }
                        if(input == "2")
                        {
                            getUserInput("");
                        }
                 

                    }
                };
            }
            else
            {
                UserID = userID;
            }

            Console.WriteLine("\nEnter\n1 get user info\n2 to get user games\n3 to register for a game\n4 to get all user session\n5 to add session\n6 to delete session\nb to go back");

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInfo(UserID);
                }
                else if (input == "2")
                {
                    getUserGame(UserID);
                }
                else if (input == "3")
                {
                    addUserGame(UserID);
                }
                else if (input == "4")
                {
                    getSession(UserID);
                }
                else if (input == "5")
                {
                    addSession(UserID);
                }
                else if (input == "6")
                {
                    removeSession(UserID);
                }
                
                else if (input == "b")
                {
                    getUserInputInitial();
                }
            }
        }

        public static void getSession(string id)
        {
            string b_url = "https://localhost:44372/api/getsession?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
            }
        }
        public static void removeSession(string id)
        {
            string sessionID = "";
            do
            {
                Console.WriteLine("\nEnter session id: ");
                sessionID = Console.ReadLine();
            } while (string.IsNullOrEmpty(sessionID));

            var removeSessionVar = new Dictionary<string, string>
            {
                { "sid" , sessionID },
           
                {"uid", id },
          



            };
            var content = new FormUrlEncodedContent(removeSessionVar);

            string b_url = "https://localhost:44372/api/removesession";
            HttpClient client = new HttpClient();
            var response = client.PutAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Remove another session");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
                else if (input == "2")
                {
                    removeSession(id);
                }

            }
        }
        public static void addSession(string id)
        {
            string gameID = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameID = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameID));
            string sessionID = "";
            do
            {
                Console.WriteLine("\nEnter id for this session: ");
                sessionID = Console.ReadLine();
            } while (string.IsNullOrEmpty(sessionID));
            string score = "";
            do
            {
                Console.WriteLine("\nEnter score(in Number): ");
                score = Console.ReadLine();
            } while (string.IsNullOrEmpty(score));

            var newSession = new Dictionary<string, string>
            {
                { "sid" , sessionID },
                { "gid" , gameID },
                {"uid", id },
                {"score", score }



            };
            var content = new FormUrlEncodedContent(newSession);

            string b_url = "https://localhost:44372/api/addsession";
            HttpClient client = new HttpClient();
            var response = client.PostAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Add another session");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
                else if (input == "2")
                {
                    addSession(id);
                }

            }
        }
        public static void addUserGame(string id)
        {
            string gameID = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameID = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameID));

            var newGame = new Dictionary<string, string>
            {
                { "gid" , gameID },
                { "uid" , id },


            };
            //string jsonString = JsonSerializer.Serialize(newUser);
            var content = new FormUrlEncodedContent(newGame);

            string b_url = "https://localhost:44372/api/addgame";
            HttpClient client = new HttpClient();
            var response = client.PostAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Add another game");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
                else if (input == "2")
                {
                    addUserGame(id);
                }

            }
        }
        public static void getUserInfo(string id)
        {
            string b_url = "https://localhost:44372/api/userinfo?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
            }
        }
        public static void getUserGame(string id)
        {
            string b_url = "https://localhost:44372/api/usergames?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInput(id);
                }
            }
        }

        public static void getAllGamesName()
        {
            string b_url = "https://localhost:44372/api/getgames";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInputInitial();
                }
            }
        }

        public static void getAllUserName()
        {
            string b_url = "https://localhost:44372/api/getusers";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInputInitial();
                }
            }
        }



        public static void DeleteUser()
        {
            string UserID = "";
            do
            {
                Console.WriteLine("\nEnter user id: ");
                UserID = Console.ReadLine();
            } while (string.IsNullOrEmpty(UserID));

            string b_url = "https://localhost:44372/api/deleteuser?id=" + UserID;
            HttpClient client = new HttpClient();
            var response =  client.DeleteAsync(b_url).Result;
            Console.WriteLine( response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Delete another user");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    getUserInputInitial();
                }
                else if (input == "2")
                {
                      DeleteUser();
                }

            }
        }
        public static void CreateUser()
        {
            string b_url = "https://localhost:44372/api/adduser";
            HttpClient client = new HttpClient();
            Console.WriteLine("Enter user ID:");
            string userID = Console.ReadLine();
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            string[] res = { userID, username, name, email };
            var newUser = new Dictionary<string, string>
            {
                { "uid" , userID },
                { "una" , username },
                { "na" , name },
                { "ema" , email },

            };
            //string jsonString = JsonSerializer.Serialize(newUser);
            var content = new FormUrlEncodedContent(newUser);
            var response =  client.PostAsync(b_url, content).Result;
            Console.WriteLine( response.Content.ReadAsStringAsync().Result);

            Console.WriteLine("\nEnter\n1 To go back\n2 Add another user");

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                     getUserInputInitial();
                }
                else if (input == "2")
                {
                     CreateUser();
                }

            }
        }
    }
}