using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Plugin.Media.Abstractions;
using BetterBeer.Objects;

namespace BetterBeer
{
    public class Database
    {
        const string API = "http://spbier.bplaced.net/DBConnect.php";
        public static int CheckUser(string login, string password)
        {
            string postData = usernameOrEmail(login) + $"&password={password}";
            int responseString = int.Parse(apiCall("validUser", postData));

            if (responseString > 0)
            {
                
               
                return responseString;
            }
            else
            {
                return -1;
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


            List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
            return beers;
        }


        public static List<Beer> HighscoreForCrit(int critID)
        {
            string requestString = API + "?action=getHighscores";
            string postData = $"critId={critID}";
            byte[] data = Encoding.UTF8.GetBytes(postData);


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


            List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
            return beers;
        }

        public static List<Beer> getAvgGradeByBeerId(string beerId)
        {
            string postData = $"bierId={beerId}";
            string responseString = apiCall("getAvgGradeByBeerId", postData);

            if (responseString != "null")
            {
                List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
                return beers;
            }

            return null;
        }
        public static string countRatings(string beerId)
        {
            string postData = $"beerId={beerId}";
            string responseString = apiCall("countRatings", postData);

            if (responseString != "null")
            {
                return responseString;
            }

            return null;
        }

        public static bool createBeer(string ean, string beerName, int brandId)
        {
            string postData = $"beerName={beerName}&brandId={brandId}&userId={SpecificUser.UserID}";
            int bierId = 0;



            //Pruefung, ob Bier Vorhanden
            string responseString = apiCall("showBeerByName", postData);

            //Wenn nicht, wird es hinzugefügt
            if (responseString == "")
            {
                responseString = apiCall("createBeer", postData);
            }
            try
            {
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

        public static List<Brand> showBrand()
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

        public static Beer getBeerByEAN(string ean)
        {
            string postData = $"ean={ean}";
            byte[] data = Encoding.UTF8.GetBytes(postData);

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

            if (responseString != "null")
            {
                List<Beer> beers = JsonConvert.DeserializeObject<List<Beer>>(responseString);
                return beers[0];
            }

            return null;
        }

        public static List<Beer> getBeerByName(string bier)
        {
            string postData = $"beerName={bier}";
            byte[] data = Encoding.UTF8.GetBytes(postData);

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
            byte[] data = Encoding.UTF8.GetBytes(postData);

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


        public static void uploadImageToDatabase(MediaFile img)
        {

            string requestString = API + "?action=uploadImage";
            byte[] imgData;


            imgData = Pictures.ImgToByte(img);
            string postData = $"image={imgData}&UserId={SpecificUser.UserID}";

            byte[] data = Encoding.UTF8.GetBytes(postData);

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
        }

        //public void getImage()
        //{
        //    string requestString = API + "?action=getImage";
        //    byte[] imgData;

        //    string postData = $"UserId={SpecificUser.UserID}";

        //    byte[] data = Encoding.UTF8.GetBytes(postData);

        //    WebRequest request = WebRequest.Create(requestString);
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = data.Length;

        //    using (var stream = request.GetRequestStream())
        //    {
        //        stream.Write(data, 0, data.Length);
        //    }

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        //    if (responseString != "null")
        //    {
        //        imgData =(byte[] reader[0])
        //    }
        //}

        public static List<Friend> GetFriends()
        {
            string postData = $"userId={SpecificUser.UserID}";
            byte[] data = Encoding.UTF8.GetBytes(postData);

            string requestString = API + "?action=showFriends";
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

            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(responseString);
            return friends;
        }

        public static List<Friend> FindFriends(string friendName)
        {
            string postData = $"userId={SpecificUser.UserID}";
            byte[] data = Encoding.UTF8.GetBytes(postData);

            string requestString = API + "?action=showFriends";
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
                List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(responseString);
                return friends;
            }

            return null;
        }
        
        public static List<Friend> ShowUser(string username)
        {
            string requestString = API + "?action=showUser";
            string postData = $"username={username}";
            byte[] data = Encoding.UTF8.GetBytes(postData);


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


            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(responseString);
            return friends;
        }

        public static bool CreateFriendship(int friendId)
        {
            string postdata = $"user1={SpecificUser.UserID}&user2={friendId}";
            int i = int.Parse(apiCall("createFriendship", postdata));
            if (i > 0)
            {
                return true;
            }
            else
                return false;
        }


        public static bool CreateRating(int beerID, int userID, List<int> rating)
        {
            string postData = $"beerId={beerID}&userId={userID}";
            int ratingID = int.Parse(apiCall("createRating", postData));

            if (ratingID > 0)
            {
                for (int i = 1; i <= rating.Count; i++)
                {
                    postData = $"ratingId={ratingID}&critId={i}&grade={rating[i - 1]}";
                    apiCall("createGrade", postData);

                }

                return true;
            }

            return false;
        }

        public static List<Criteria> ShowCriteria()
        {
            string requestString = API + "?action=showCriteria";
            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            List<Criteria> crits = JsonConvert.DeserializeObject<List<Criteria>>(responseString);
            return crits;
        }
    }
}
