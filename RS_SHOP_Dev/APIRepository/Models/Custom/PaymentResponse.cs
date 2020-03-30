using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace APIRepository.Models.Custom
{
	[XmlRoot(ElementName = "cardissuer")]
	public class Cardissuer
	{
		[XmlElement(ElementName = "bank")]
		public string Bank { get; set; }
		[XmlElement(ElementName = "country")]
		public string Country { get; set; }
		[XmlElement(ElementName = "countrycode")]
		public string Countrycode { get; set; }
		[XmlElement(ElementName = "region")]
		public string Region { get; set; }
	}

	[XmlRoot(ElementName = "fraudresponse")]
	public class Fraudresponse
	{
		[XmlElement(ElementName = "result")]
		public string Result { get; set; }
		[XmlElement(ElementName = "rules")]
		public string Rules { get; set; }
		[XmlAttribute(AttributeName = "mode")]
		public string Mode { get; set; }
	}

	[XmlRoot(ElementName = "response")]
	public class Response
	{
		[XmlElement(ElementName = "merchantid")]
		public string Merchantid { get; set; }
		[XmlElement(ElementName = "account")]
		public string Account { get; set; }
		[XmlElement(ElementName = "orderid")]
		public string Orderid { get; set; }
		[XmlElement(ElementName = "authcode")]
		public string Authcode { get; set; }
		[XmlElement(ElementName = "result")]
		public string Result { get; set; }
		[XmlElement(ElementName = "cvnresult")]
		public string Cvnresult { get; set; }
		[XmlElement(ElementName = "avspostcoderesponse")]
		public string Avspostcoderesponse { get; set; }
		[XmlElement(ElementName = "avsaddressresponse")]
		public string Avsaddressresponse { get; set; }
		[XmlElement(ElementName = "batchid")]
		public string Batchid { get; set; }
		[XmlElement(ElementName = "message")]
		public string Message { get; set; }
		[XmlElement(ElementName = "pasref")]
		public string Pasref { get; set; }
		[XmlElement(ElementName = "timetaken")]
		public string Timetaken { get; set; }
		[XmlElement(ElementName = "authtimetaken")]
		public string Authtimetaken { get; set; }
		[XmlElement(ElementName = "srd")]
		public string Srd { get; set; }
		[XmlElement(ElementName = "cardissuer")]
		public Cardissuer Cardissuer { get; set; }
		[XmlElement(ElementName = "fraudresponse")]
		public Fraudresponse Fraudresponse { get; set; }
		[XmlElement(ElementName = "sha1hash")]
		public string Sha1hash { get; set; }
		[XmlAttribute(AttributeName = "timestamp")]
		public string Timestamp { get; set; }
	}
}