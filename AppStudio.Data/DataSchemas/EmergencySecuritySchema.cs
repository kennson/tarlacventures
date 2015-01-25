using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the EmergencySecuritySchema class.
    /// </summary>
    public class EmergencySecuritySchema : BindableSchemaBase, IEquatable<EmergencySecuritySchema>, ISyncItem<EmergencySecuritySchema>
    {
        private string _town;
        private string _name;
        private string _contact;
        private string _aFP;
        private string _cPAFP;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Town
        {
            get { return _town; }
            set { SetProperty(ref _town, value); }
        }
 
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
 
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }
 
        public string AFP
        {
            get { return _aFP; }
            set { SetProperty(ref _aFP, value); }
        }
 
        public string CPAFP
        {
            get { return _cPAFP; }
            set { SetProperty(ref _cPAFP, value); }
        }

        public override string DefaultTitle
        {
            get { return Town; }
        }

        public override string DefaultSummary
        {
            get { return Name; }
        }

        public override string DefaultImageUrl
        {
            get { return null; }
        }

        public override string DefaultContent
        {
            get { return Name; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLowerInvariant())
                {
                    case "town":
                        return String.Format("{0}", Town); 
                    case "name":
                        return String.Format("{0}", Name); 
                    case "contact":
                        return String.Format("{0}", Contact); 
                    case "afp":
                        return String.Format("{0}", AFP); 
                    case "cpafp":
                        return String.Format("{0}", CPAFP); 
                    case "defaulttitle":
                        return DefaultTitle;
                    case "defaultsummary":
                        return DefaultSummary;
                    case "defaultimageurl":
                        return DefaultImageUrl;
                    default:
                        break;
                }
            }
            return String.Empty;
        }

        public bool Equals(EmergencySecuritySchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(EmergencySecuritySchema other)
        {

            return this.Id == other.Id && (this.Town != other.Town || this.Name != other.Name || this.Contact != other.Contact || this.AFP != other.AFP || this.CPAFP != other.CPAFP);
        }

        public void Sync(EmergencySecuritySchema other)
        {
            this.Town = other.Town;
            this.Name = other.Name;
            this.Contact = other.Contact;
            this.AFP = other.AFP;
            this.CPAFP = other.CPAFP;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EmergencySecuritySchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
