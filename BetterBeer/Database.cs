using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace BetterBeer
{
    public class Database
    {
        const string API = "http://spbier.bplaced.net/DBConnect.php";
        public static bool CheckUser(string login, string password)
        {
            string postData = usernameOrEmail(login) +$"&password={password}";          
            string responseString = apiCall("validUser", postData);

            if (responseString == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NewUser(String uName, String email, String password, String SaltedPassword)
        {
            string postData = $"username={uName}&email={email}&password={password}&saltedpassword={SaltedPassword}";
            string responseString = apiCall("createUser", postData);

            if (responseString == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetSaltedPW(string login)
        {
            string postData;
            if (login.Contains("@"))
            {
                postData = $"&email={login}";
            }
            else
            {
                postData = $"&username={login}";
            }

            return apiCall("getSaltedPasswordByLogin", postData);
        }


        public static List<Beer> Highscore()
        {
            string requestString = API + "?action=getHighscores";
            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            List<Beer> beers= JsonConvert.DeserializeObject<List<Beer>>(responseString);
            return beers;
        }

        public static void createBeer (int id, string name, int brandId)
        {

        }

        public static Beer getBeerById (int id)
        {
            throw new Exception();
        }


        private static string apiCall(string action, string postData)
        {
            string requestString = API + "?action=" + action;
            byte[] data = Encoding.ASCII.GetBytes(postData);

            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }


        private static string usernameOrEmail(string login)
        {
            string data = "";
            if (login.Contains("@"))
            {
                data += $"email={login}";
            }
            else
            {
                data += $"username={login}";
            }
            return data;
        }
    }
}
