// Decompiled with JetBrains decompiler
// Type: IntelligenceApp.Controllers.HomeController
// Assembly: IntelligenceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEAE96B3-BC8E-4A80-BA78-34BF5DFDBE99
// Assembly location: E:\Sangeeta\IntelligenceApp\IntelligenceApp\bin\IntelligenceApp.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace PipedriveIntelligenceApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public void RecieveNotification()
        {
            try
            {
                Stream inputStream = this.Request.InputStream;
                inputStream.Seek(0L, SeekOrigin.Begin);
                string end = new StreamReader(inputStream).ReadToEnd();
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(end);
                this.EmailCreator(end, "Activity deserialized for Deal: " + rootObject.current.deal_id.ToString());
                string activitiesOfDeal = this.GetAllActivitiesOfDeal(rootObject.current.deal_id);
                RootObjectUpdateDeal deal = JsonConvert.DeserializeObject<RootObjectUpdateDeal>(activitiesOfDeal);
                this.EmailCreator(activitiesOfDeal, "Deal deserialized : " + deal.data.First<Datum>().subject);
                int npc_count = this.CountActivity("CALL AGAIN @CX-NPC", deal);
                int talked_count = this.CountActivity("CALL AGAIN @CX-TALKED", deal);
                int cut_count = this.CountActivity("CALL AGAIN @CX-CUT", deal);
                int unreachable_count = this.CountActivity("CALL AGAIN @CX-NSO/NNR", deal);
                this.UpdateActivityCount(rootObject.current.deal_id.ToString(), npc_count, talked_count, cut_count, unreachable_count);
            }
            catch (Exception ex)
            {
                this.EmailCreator(ex.ToString(), "Exception occured in RecieveNotification method.");
            }
        }

        private void EmailCreator(string body, string subject)
        {
            MailAddress To = new MailAddress(ConfigurationManager.AppSettings["ToEnquiryForm"]);
            MailAddress From = new MailAddress(ConfigurationManager.AppSettings["FromEnquiryForm"]);
            this.sendMessage(body, subject, To, From, true);
        }

        private void sendMessage(string body, string subject, MailAddress To, MailAddress From, bool isBodyHTML = true)
        {
            MailMessage message = new MailMessage();
            try
            {
                message.To.Add(To);
                message.From = From;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isBodyHTML;
                message.BodyEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                new SmtpClient().Send(message);
            }
            catch (Exception ex)
            {
                message.To.Add(new MailAddress(ConfigurationManager.AppSettings["ToEnquiryForm"]));
            }
        }

        private string GetAllActivitiesOfDeal(int deal_id)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.pipedrive.com/v1/deals/" + (object)deal_id + "/activities?start=0&api_token=xxxxxxx");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            try
            {
                using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                    return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadLine();
                return "";
            }
        }

        private int CountActivity(string activity_type, RootObjectUpdateDeal deal)
        {
            int num = 0;
            foreach (Datum datum in (IEnumerable<Datum>)deal.data)
            {
                if (datum.subject.Equals(activity_type))
                    ++num;
            }
            return num;
        }

        private void UpdateActivityCount(string deal_id, int npc_count, int talked_count, int cut_count, int unreachable_count)
        {
            this.SendtoRestAPI("{\"bef669c383b9495b11a1c7c5b6c02f4fe3872cb6\": \"" + (object)npc_count + "\",\"058d3af52928b130d36622b852fdf81cb2f05245\": \"" + (object)talked_count + "\",\"e22c139b433292432f75353f47941ffd5d538571\": \"" + (object)cut_count + "\",\"d1ea18cbb57fed4506ad94f3d3d59c21a3ede6de\": \"" + (object)unreachable_count + "\"}", "deals", "PUT", deal_id);
        }

        private string SendtoRestAPI(string postData, string addtype, string method, string deal_id)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.pipedrive.com/v1/" + addtype + "/" + deal_id + "?api_token=xxxxxxxx");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = method;
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                    return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadLine();
                this.EmailCreator(ex.ToString(), "After posting activity count Update for Deal: " + deal_id);
                return "";
            }
        }
    }
}
