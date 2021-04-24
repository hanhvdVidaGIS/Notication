using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using FcmSharp.Requests;
using FcmSharp.Settings;
using FcmSharp;
using Microsoft.Toolkit.Uwp.Notifications;
using Firebase;

namespace NoticationProject
{
    class FireBase
    {
        public void sendnotication(string deviceId)
        { 
            //Create the web request with fire base API  
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAy4M6tV0:APA91bEInhI0Keg0PZnwai4vlk7znw6k_Fsia_-sUuSShqa1zwTC76FeEAOscSQDvdF_lmxldB570plk8chvR5LIgI3gmgpYqmE6ziiAMd2dmGGAaG15mOnoD59aOGuk4af5AsYlLgbv"));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "874080023901"));
            tRequest.ContentType = "application/json";
            var deviceIds = deviceId.Split(',');
            var payload = new
            {
                registration_ids = deviceIds,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = "day la thong bao",
                    title = "title",
                    //sound = "sound.caf",
                    //badge = badgeCounter
                },
            };
            //var serializer = new JavaScriptSerializer();
            string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }
    }
    public class FCMResponse
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public List<FCMResult> results { get; set; }
    }
    public class FCMResult
    {
        public string message_id { get; set; }
    }
    
}
