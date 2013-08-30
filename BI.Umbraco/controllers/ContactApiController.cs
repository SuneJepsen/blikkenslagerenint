using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BI.Repository.Entities;
using BI.Umbraco.helpers;
using BI.Umbraco.services;
using Umbraco.Web.WebApi;

namespace BI.Umbraco.controllers
{
    public class ContactApiController : UmbracoApiController
    {
        // GET api/<controller>  http://localhost:50379/Umbraco/Api/ContactApi/Get
        public IEnumerable<string> GetAllProducts()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        public EmailStatus SubmitContactForm(ContactformularValues value)
        {
            EmailService emailService = new EmailService();
            return emailService.SendContactFormularEmail(value);
        }
    }
}