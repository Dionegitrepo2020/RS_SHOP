using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace APIRepository.Models.Custom
{
    [XmlRoot(ElementName = "amount")]
    public class Amount
    {
        [XmlAttribute(AttributeName = "currency")]
        public string Currency { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "cvn")]
    public class Cvn
    {
        [XmlElement(ElementName = "number")]
        public string CNumber { get; set; }
        [XmlElement(ElementName = "presind")]
        public string Presind { get; set; }
    }

    [XmlRoot(ElementName = "card")]
    public class Card
    {
        [XmlElement(ElementName = "number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "expdate")]
        public string Expdate { get; set; }
        [XmlElement(ElementName = "chname")]
        public string Chname { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "cvn")]
        public Cvn Cvn { get; set; }
    }

    [XmlRoot(ElementName = "autosettle")]
    public class Autosettle
    {
        [XmlAttribute(AttributeName = "flag")]
        public string Flag { get; set; }
    }

    [XmlRoot(ElementName = "request")]
    public class Payment
    {
        [XmlElement(ElementName = "merchantid")]
        public string Merchantid { get; set; }
        [XmlElement(ElementName = "orderid")]
        public string Orderid { get; set; }
        [XmlElement(ElementName = "amount")]
        public Amount Amount { get; set; }
        [XmlElement(ElementName = "card")]
        public Card Card { get; set; }
        [XmlElement(ElementName = "autosettle")]
        public Autosettle Autosettle { get; set; }
        [XmlElement(ElementName = "sha1hash")]
        public string Sha1hash { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }
    }
}