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
        public static bool checkUser(string login, string password)
        {

            var postData = $"username={login}&password={password}";
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

        public static bool newUser(String uName, String email, String password)
        {
            var postData = $"username={uName}&email={email}&password={password}";
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

        private static string apiCall(string action, string postData)
        {
            string requestString = API + "?action=" + action;
            var data = Encoding.ASCII.GetBytes(postData);

            var request = WebRequest.Create(requestString);
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
    }
}
