using AutoMapper;
using FileHelpers;
using Newtonsoft.Json;
using SalesforceTransformFunctions.Model;
using SalesforceTransformFunctions.Model.Flatfile;
using SalesforceTransformFunctions.Model.Salesforce;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SalesforceTransformFunctions
{
    public class SalesforceJsonToFlatFile : MapperBase
    {
        public static HttpResponseMessage Run(HttpRequestMessage req)
        {
            var engine = new FileHelperEngine<ContactModel>();
            var requestContent = req.Content.ReadAsStringAsync().Result;
            var salesforceObject = JsonConvert.DeserializeObject<Contact>(requestContent);
            var contactList = new List<Contact>() { salesforceObject};
            var map = new Mapping();
            var erpContact = map.MapSalesforceContactToFlatFile(contactList);
            string result = engine.WriteString(erpContact);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            return response;

        }
    }
    public class Mapping : MapperBase
    { 
        public IList<ContactModel> MapSalesforceContactToFlatFile(IList<Contact> sfContacts)
        {
            IMapper mapper = CreateContactMap();
            return mapper.Map<IList<Contact>,IList<ContactModel>>(sfContacts);
        }

        private IMapper CreateContactMap()
        {
            mappings.CreateMap<Contact, ContactModel>()
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SfId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Dept, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.LeadSource, opt => opt.MapFrom(src => src.LeadSource))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.MailingStreet));
            return CreateMap();
        }
    }
}