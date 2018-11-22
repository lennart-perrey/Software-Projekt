using System;
using Xamarin.Forms;
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

        public static bool createBeer (string ean, string beerName, int brandId)
        {
            string postData = $"beerName={beerName}&brandId={brandId}";
            int bierId=0;

            //Pruefung, ob Bier Vorhanden
            string responseString = apiCall("showBeerByName", postData);

            //Wenn nicht, wird es hinzugefügt
            if(responseString == "")
            {
                responseString = apiCall("createBeer", postData);
            }
            try { 
                bierId = Convert.ToInt32(responseString);
            }
            catch (Exception)
            {
                //Bier ist schon vorhanden in der Datenbank
                return false;
            }

            //create EAN-Code
            if (bierId > 0)
            {
                string eanResult = apiCall("createEAN", $"ean={ean}&bierId={bierId}");
                if (eanResult == "1")
                {
                    return true;
                }
            }
             return false;
        }

        public static List<Brand> showBrand ()
        {
            string requestString = API + "?action=showBrand";
            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(responseString);
            return brands;
        }

        public static Beer getBeerByEAN (string ean)
        {
            string postData =$"ean={ean}";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            string requestString = API + "?action=getBeerByEAN";
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

            if(responseString != "null")
            {
                List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
                return beers[0];
            }

            return null;
        }

        public static List<Beer> getBeerByName(string bier)
        {
            string postData = $"beerName={bier}";
            byte[] data = Encoding.ASCII.GetBytes(postData);

            string requestString = API + "?action=showBeerByName";
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

            if (responseString != "null")
            {
                List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
                return beers;
            }

            return null;
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


        public static bool CreateRating(int beerID, int userID, List<int> rating)
        {
            string postData = $"beerID={beerID}&userID={userID}";
            string ratingID = apiCall("createRating", postData);

            try
            {

                if (Convert.ToInt32(ratingID) > 0)
                {
                    for(int i = 0; i<4; i++)
                    {
                        postData = $"ratingID={ratingID}&critId={i}&grade={rating[i]}";
                        string responseString = apiCall("createGrade", postData);

                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
