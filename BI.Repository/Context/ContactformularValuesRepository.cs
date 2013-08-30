using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BI.Repository.Entities;

namespace BI.Repository.Context
{
    public class ContactformularValuesRepository
    {

        public void Insert(ContactformularValues servicebooking)
        {
            using (ContactformularValuesContext context = new ContactformularValuesContext())
            {
                context.ContactformularValues.Add(servicebooking);
                context.SaveChanges();
            }
        }
    }
}
