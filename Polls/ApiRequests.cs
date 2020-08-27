using Newtonsoft.Json;
using Polls.Models;
using Polls.UserControls.PassingTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Polls
{
    class ApiRequests
    {
        private static HttpClient client = new HttpClient();

        public static void SetSessionCookie(string domain, string session)
        {
            CookieContainer sessionCookie = new CookieContainer();
            sessionCookie.Add(new Uri(domain), new Cookie("sessionID", session));
            HttpClientHandler hand = new HttpClientHandler();
            hand.CookieContainer = sessionCookie;
            client = new HttpClient(hand);
        }

        public static string SessionGet()
        {
            HttpResponseMessage response = client.GetAsync(Settings.SessionUrl()).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Session was not obtained", e);
            }
            return response.Content.ReadAsStringAsync().Result;
        }

        public static string RegistrationPost(string login, string email, string hashedPassword)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["login"] = login;
            queryCollection["email"] = email;
            queryCollection["hashedPassword"] = hashedPassword;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);
            HttpResponseMessage response = client.PostAsync(Settings.RegistrationUrl(), stringContent).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Registration request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string LoginPost(string email, string hashedPassword)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["email"] = email;
            queryCollection["hashedPassword"] = hashedPassword;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);
            HttpResponseMessage response = client.PostAsync(Settings.LoginUrl(), stringContent).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Login request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string LogoutPost()
        {
            HttpResponseMessage response = client.PostAsync(Settings.LogoutUrl(), null).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Logout request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string ProfileGet(string userID)
        {
            HttpResponseMessage response = client.GetAsync(string.Concat(Settings.ProfileUrl(),
                userID.Equals("") ? "" : $"?userID={userID}")).Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Profile request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string ProfileEmailPut(string oldPass, string newEmail)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["newEmail"] = newEmail;
            queryCollection["oldHashedPassword"] = oldPass;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);
            HttpResponseMessage response = client.PutAsync(Settings.ProfileEditUrl(), stringContent)
                .Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Profile's email changing request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string ProfilePasswordPut(string oldPass, string newPass)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["newHashedPassword"] = newPass;
            queryCollection["oldHashedPassword"] = oldPass;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);
            HttpResponseMessage response = client.PutAsync(Settings.ProfileEditUrl(), stringContent)
                .Result;
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Profile's email changing request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestEditPost(Test test)
        {
            string testJson = JsonConvert.SerializeObject(test);
            File.WriteAllText(string.Concat(Settings.AppDataDirectory, "/last.json"), testJson);
            //return "{\"result\": false}";
            var content = new StringContent(testJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(Settings.TestEdit(), content).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Test saving request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestEditPut(Test test, string testID)
        {
            string testJson = JsonConvert.SerializeObject(test);
            File.WriteAllText("last.json", testJson);
            var jsonContent = new StringContent(testJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(
                string.Concat(Settings.TestEdit(), $"?testID={testID}"), jsonContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Test updating request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestPublishPost(string testID)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PostAsync(Settings.TestPublish(), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Test publishing request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestActivityPut(string testID, bool activity)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            queryCollection["isActive"] = activity.ToString();
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PutAsync(Settings.TestSetActivity(), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Setting test activity request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestFavoritePost(string testID)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PostAsync(Settings.TestFavorites(), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Adding test to favorites request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestFavoriteDelete(string testID)
        {
            //var queryCollection = new Dictionary<string, string>();
            //queryCollection["testID"] = testID;
            //HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.DeleteAsync(
                string.Concat(Settings.TestFavorites(), $"?testID={testID}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Deleting test from favorites request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestDelete(string testID)
        {

            HttpResponseMessage response = client.DeleteAsync(
                string.Concat(Settings.TestDelete(), $"?testID={testID}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Deleting test request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string TestEditGet(string testID)
        {

            HttpResponseMessage response = client.GetAsync(
                string.Concat(Settings.TestEdit(), $"?testID={testID}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting test for editing request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string CreatedTestsGet()
        {
            HttpResponseMessage response = client.GetAsync(Settings.CreatedTests()).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting created tests request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PassedTestsGet()
        {

            HttpResponseMessage response = client.GetAsync(Settings.PassedTests()).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting passed tests request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string FavoriteTestsGet()
        {
            HttpResponseMessage response = client.GetAsync(Settings.FavoriteTests()).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting favorite tests request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PublicTestsGet(bool isSortDesc)
        {
            HttpResponseMessage response = client.GetAsync(string.Concat(Settings.PublicTests(),
                $"?sortDesc={isSortDesc.ToString()}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting public tests request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string SearchTestsGet(string query)
        {
            HttpResponseMessage response = client.GetAsync(string.Concat(Settings.SearchTests(),
                $"?q={query}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Searching tests request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PassingTestGet(string testID)
        {
            HttpResponseMessage response = client.GetAsync(string.Concat(Settings.PassingTest(),
                $"?testID={testID}")).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Retrieving test slide request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PassingNextPost(string testID, NextSlideForm nextSlideForm)
        {
            string testJson = JsonConvert.SerializeObject(nextSlideForm);
            var content = new StringContent(testJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(string.Concat(Settings.PassingNextSlide(),
                $"?testID={testID}"), content).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Sending answer for test request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PassingPrevPut(string testID)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PutAsync(Settings.PassingPrevSlide(), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Getting previous answer for test request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string PassingFinishPost(string testID)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PostAsync(Settings.PassingFinish(), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Finishing test request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }

        public static string RateTestPost(string testID, int value)
        {
            var queryCollection = new Dictionary<string, string>();
            queryCollection["testID"] = testID;
            HttpContent stringContent = new FormUrlEncodedContent(queryCollection);

            HttpResponseMessage response = client.PostAsync(
                string.Concat(Settings.RateTest(), $"?value={value}.0"), stringContent).Result;

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Rating test request failed", e);
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
