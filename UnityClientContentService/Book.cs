using UnityEngine; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("book")]
public class Book
{
	[XmlAttribute("id")]	
	public int id;
	
	[XmlAttribute("title")]
	public string title;
	
	[XmlAttribute("author")]
	public string author;
	
	[XmlAttribute("illustrator")]
	public string illustrator;
	
	[XmlAttribute("publisher")]
	public string publisher;
	
	[XmlAttribute("version")]
	public int version;
	
	[XmlAttribute("status")]
	public string status;
	
	[XmlArray("content")]
	[XmlArrayItem("contentItem")]
	public List<ContentItem> contentItem;
	
	[XmlAttribute("description")]
	public string description;
	
	[XmlAttribute("publisherDescription")]
	public string publisherDescription;
	
	[XmlAttribute("productId")]
	public string productId;
	
	[XmlArray("rewards")]
	[XmlArrayItem("reward")]
	public List<Reward> reward;
	
	[XmlElement("learning")]
	public Learning learning;
	
	[XmlAttribute("age")]
	public int age;
	
	[XmlIgnore]
	public DateTime lastUpdate;
		
	public Book(){}
	public void debug()
	{
		Debug.Log("book: id=" + id +", title=" + title +", author=" + author +", illustrator="+illustrator+", publisher="+publisher+", version=" + version +", status=" + status +",description="+description+",publisherDescription="+publisherDescription+",productId=" + productId+", age=" + age);
		foreach (ContentItem ci in contentItem) {
			ci.debug();
		}
		foreach (Reward r in reward) {
			r.debug();
		}
		if (learning != null) {
			learning.debug();
		}
	}
	
	[XmlAttribute("lastUpdate")]
	public string lastUpdateConv {
		get {return this.lastUpdate.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.lastUpdate = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Book pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Book)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Book DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Book)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Book)xs.Deserialize(memoryStream); 
   } 
	
		/* The following metods came from the referenced URL */ 
   private static string UTF8ByteArrayToString(byte[] characters) 
   {      
      UTF8Encoding encoding = new UTF8Encoding(); 
      string constructedString = encoding.GetString(characters); 
      return (constructedString); 
   } 
 
   private static byte[] StringToUTF8ByteArray(string pXmlString) 
   { 
      UTF8Encoding encoding = new UTF8Encoding(); 
      byte[] byteArray = encoding.GetBytes(pXmlString); 
      return byteArray; 
   } 
}
