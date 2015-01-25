using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the MayantocSchema class.
    /// </summary>
    public class MayantocSchema : BindableSchemaBase, IEquatable<MayantocSchema>, ISyncItem<MayantocSchema>
    {
        private string _destination;
        private string _locatioon;
        private string _attraction;
        private string _contact;
        private string _person;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Destination
        {
            get { return _destination; }
            set { SetProperty(ref _destination, value); }
        }
 
        public string Locatioon
        {
            get { return _locatioon; }
            set { SetProperty(ref _locatioon, value); }
        }
 
        public string Attraction
        {
            get { return _attraction; }
            set { SetProperty(ref _attraction, value); }
        }
 
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }
 
        public string Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public override string DefaultTitle
        {
            get { return Destination; }
        }

        public override string DefaultSummary
        {
            get { return Person; }
        }

        public override string DefaultImageUrl
        {
            get { return Attraction; }
        }

        public override string DefaultContent
        {
            get { return Person; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLowerInvariant())
                {
                    case "destination":
                        return String.Format("{0}", Destination); 
                    case "locatioon":
                        return String.Format("{0}", Locatioon); 
                    case "attraction":
                        return String.Format("{0}", Attraction); 
                    case "contact":
                        return String.Format("{0}", Contact); 
                    case "person":
                        return String.Format("{0}", Person); 
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

        public bool Equals(MayantocSchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(MayantocSchema other)
        {

            return this.Id == other.Id && (this.Destination != other.Destination || this.Locatioon != other.Locatioon || this.Attraction != other.Attraction || this.Contact != other.Contact || this.Person != other.Person);
        }

        public void Sync(MayantocSchema other)
        {
            this.Destination = other.Destination;
            this.Locatioon = other.Locatioon;
            this.Attraction = other.Attraction;
            this.Contact = other.Contact;
            this.Person = other.Person;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MayantocSchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
