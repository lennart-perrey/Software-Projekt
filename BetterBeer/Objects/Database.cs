﻿using System;
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
        public static bool deleteAccount(int UserID)
        {
            string postData = $"userId={UserID}";
            string responseString = apiCall("deleteAccount", postData);
            if (responseString == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool changePassword(String password, String SaltedPassword, int UserID)
        {
            string postData = $"password={password}&saltedpassword={SaltedPassword}&userId={UserID}";
            string responseString = apiCall("changePassword", postData);
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

        public static List<LastFriendsRating> getFriendRatingLastBeerByCrit(int ratingId)
        {
            string requestString = API + "?action=getFriendRatingLastBeerByCrit";
            string postData = $"ratingId={ratingId}";
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

            if(responseString != "null")
            {
                List<LastFriendsRating> ratings = JsonConvert.DeserializeObject<List<LastFriendsRating>>(responseString);
                return ratings;
            }
            return null;

        }

        public static List<FriendRating> getFriendRatingLastBeer(int friendId)
        {
            string requestString = API + "?action=getFriendRatingLastBeer";
            string postData = $"friendId={friendId}";
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

            if(responseString != "")
            {
                List<FriendRating> ratings = JsonConvert.DeserializeObject<List<FriendRating>>(responseString);
                return ratings;
            }
            return null;  
        }

        public static List<Rating> getAvgGradeByBeerId(string beerId)
        {
            string postData = $"bierId={beerId}";
            string responseString = apiCall("getAvgGradeByBeerId", postData);

            if (responseString != "null")
            {
                List<Rating> beers = JsonConvert.DeserializeObject<List<Rating>>(responseString);
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
        public static int countRatings(int userId)
        {
            string postData = $"userId={userId}";
            string responseString = apiCall("countRatings", postData);

            if (responseString != "null")
            {
                return Convert.ToInt32(responseString);
            }

            return 0;
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

        public static List<Beer> getBeerById(int bierId)
        {
            string postData = $"beerName={bierId}";
            byte[] data = Encoding.UTF8.GetBytes(postData);

            string requestString = API + "?action=getBeerById";
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

        public static Friend ShowUser(int userId){
            string result = apiCall("showUser","userId="+userId);
            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(result);
            if (friends.Count == 0)
                return null;
            else
                return friends[0];
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


        public static bool CreateRating(int beerID, int userID, List<int> rating,List<Criteria> criterias)
        {
            string postData = $"beerId={beerID}&userId={userID}";
            int ratingID = int.Parse(apiCall("createRating", postData));

           
            if (ratingID > 0)
            {
                int i = 0;
                foreach (Criteria crit in criterias)
                {
                    postData = $"ratingId={ratingID}&critId={crit.KriterienID}&grade={rating[i]}";
                    apiCall("createGrade", postData);
                    i++;
                }
                return true;
            }

            return false;
        }


        public static bool CreateGrade(int ratingId, int critId, int grade){
            string postData = $"ratingId={ratingId}&critId={critId}&grade={grade}";
            string res=apiCall("createGrade", postData);
            int result = int.Parse(res);
            if (result > 0)
                return true;
            else
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


        public static List<FriendRating> showFriendLast(int userId){
            string result = apiCall("showLastFriendRatings", "userId=" + userId);
            List <FriendRating> friendRatings = JsonConvert.DeserializeObject<List<FriendRating>>(result);
            return friendRatings;
        }

        public static List<FriendRatingCount> countFriendRatings(int userId)
        {
            string result = apiCall("showFriendRatingCount", "userId=" + userId);
            List<FriendRatingCount> friendRatings = JsonConvert.DeserializeObject<List<FriendRatingCount>>(result);
            return friendRatings;
        }

        public static int showFriendsRatingCounter(int userId)
        {
            string result = apiCall("showFriendRatingCounter", "friendId=" + userId);
            int friendRatings = JsonConvert.DeserializeObject<int>(result);
            return friendRatings;
        }

        public static List<Friend> GetAllUsers()
        {
            string requestString = API + "?action=showAll";
            WebRequest request = WebRequest.Create(requestString);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(responseString);
            return friends;
        }

        public static bool CheckFriendship(int friendID)
        {
            string postdata = $"userID={SpecificUser.UserID}&friendID={friendID}";
            string id = apiCall("checkFriendship", postdata);
            if (id == "0" || id == "")
            {
                return false;   
            }
            return true;
        }

        public static bool CancelFriendship(int friendID)
        {
            string postdata = $"userID={SpecificUser.UserID}&friendID={friendID}";
            string response = apiCall("CancelFriendship", postdata);

            if (response == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Criteria> CheckRating(int beerID)
        {
            string postData = $"userId={SpecificUser.UserID}&beerId={beerID}";
            string requestString = API + "?action=checkRating";
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

            List<Criteria> crits = JsonConvert.DeserializeObject<List<Criteria>>(responseString);
            return crits;

        }

        public static bool UpdateRating(int ratingID, int critID, int grade)
        {
            string postData = $"ratingId={ratingID}&critId={critID}&grade={grade}";

            string response = apiCall("updateRating", postData);

            try
            {
                Convert.ToInt32(response);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
