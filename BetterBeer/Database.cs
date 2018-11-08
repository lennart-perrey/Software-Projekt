using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.IO;

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

        public static bool NewUser(String uName, String email, String password)
        {
            string postData = $"username={uName}&email={email}&password={password}";
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


        public static string Highscore()
        {
            string requestString = API + "?action=getHighscores";
            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
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

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
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
