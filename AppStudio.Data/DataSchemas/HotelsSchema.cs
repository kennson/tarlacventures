using System;
using System.Collections;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    /// <summary>
    /// Implementation of the HotelsSchema class.
    /// </summary>
    public class HotelsSchema : BindableSchemaBase, IEquatable<HotelsSchema>, ISyncItem<HotelsSchema>
    {
        private string _title;
        private string _address;
        private string _imageUrl;
        private string _contact;
        [JsonProperty("_id")]
        public string Id { get; set; }

 
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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
 
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }

        public override string DefaultTitle
        {
            get { return Title; }
        }

        public override string DefaultSummary
        {
            get { return null; }
        }

        public override string DefaultImageUrl
        {
            get { return ImageUrl; }
        }

        public override string DefaultContent
        {
            get { return null; }
        }

        override public string GetValue(string fieldName)
        {
            if (!String.IsNullOrEmpty(fieldName))
            {
                switch (fieldName.ToLowerInvariant())
                {
                    case "title":
                        return String.Format("{0}", Title); 
                    case "address":
                        return String.Format("{0}", Address); 
                    case "imageurl":
                        return String.Format("{0}", ImageUrl); 
                    case "contact":
                        return String.Format("{0}", Contact); 
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

        public bool Equals(HotelsSchema other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Id == other.Id;
        }

        public bool NeedSync(HotelsSchema other)
        {

            return this.Id == other.Id && (this.Title != other.Title || this.Address != other.Address || this.ImageUrl != other.ImageUrl || this.Contact != other.Contact);
        }

        public void Sync(HotelsSchema other)
        {
            this.Title = other.Title;
            this.Address = other.Address;
            this.ImageUrl = other.ImageUrl;
            this.Contact = other.Contact;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HotelsSchema);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
