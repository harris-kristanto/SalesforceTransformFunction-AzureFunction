using FileHelpers;

namespace SalesforceTransformFunctions.Model.Flatfile
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    [IgnoreEmptyLines]
    public class ContactModel
    {
        [FieldQuoted]
        public string ContactId;
        [FieldQuoted]
        public string FName;
        [FieldQuoted]
        public string LName;
        [FieldQuoted]
        public string SfId;
        [FieldQuoted]
        public string Dept;
        [FieldQuoted]
        public string Email;
        [FieldQuoted]
        public string Fax;
        [FieldQuoted]
        public string PhoneNo;
        [FieldQuoted]
        public string LeadSource;
        [FieldQuoted]
        public string Address;
    }
}