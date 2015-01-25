using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the SanJoseSchema class.
    /// </summary>
    public class SanJoseSchema : BindableSchemaBase, IEquatable<SanJoseSchema>, ISyncItem<SanJoseSchema>
    {
        private string _destination;
        private string _location;
        private string _attraction;
        private string _contact;
        private string _person;
        private string _address;
        private string _imageUrl;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Destination
        {
            get { return _destination; }
            set { SetProperty(ref _destination, value); }
        }
 
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
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
 
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
 
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
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
            get { return ImageUrl; }
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
                    case "location":
                        return String.Format("{0}", Location); 
                    case "attraction":
                        return String.Format("{0}", Attraction); 
                    case "contact":
                        return String.Format("{0}", Contact); 
                    case "person":
                        return String.Format("{0}", Person); 
                    case "address":
                        return String.Format("{0}", Address); 
                    case "imageurl":
                        return String.Format("{0}", ImageUrl); 
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

        public bool Equals(SanJoseSchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(SanJoseSchema other)
        {

            return this.Id == other.Id && (this.Destination != other.Destination || this.Location != other.Location || this.Attraction != other.Attraction || this.Contact != other.Contact || this.Person != other.Person || this.Address != other.Address || this.ImageUrl != other.ImageUrl);
        }

        public void Sync(SanJoseSchema other)
        {
            this.Destination = other.Destination;
            this.Location = other.Location;
            this.Attraction = other.Attraction;
            this.Contact = other.Contact;
            this.Person = other.Person;
            this.Address = other.Address;
            this.ImageUrl = other.ImageUrl;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SanJoseSchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
