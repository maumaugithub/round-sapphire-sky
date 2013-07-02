using UnityEngine; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("house")]
public class House
{
	[XmlAttribute("id")]	
	public int id;
	
	[XmlIgnore]
	public DateTime lastUpdate;

	[XmlAttribute("version")]
	public int version;

	[XmlAttribute("title")]
	public string title;

	[XmlAttribute("author")]
	public string author;

	[XmlAttribute("illustrator")]
	public string illustrator;

	[XmlArray("content")]
	[XmlArrayItem("contentItem")]
	public List<ContentItem> contentItem;

	[XmlElement("description")]
	public string description;

	[XmlArray("books")]
	[XmlArrayItem("book")]	
	public List<Book> books;
	
	public House(){}
	
	public void debug() {
		Debug.Log("house: id=" + id +", lastUpdate=" + lastUpdate+", version=" + version+", title=" + title +", author=" + author+",illustrator=" + illustrator+", description=" + description);
		foreach(ContentItem ci in contentItem) {
			ci.debug();
		}
		foreach(Book b in books) {
			b.debug();
		}
	}
	
	[XmlAttribute("lastUpdate")]
	public string lastUpdateConv {
		get {return this.lastUpdate.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.lastUpdate = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
	
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(House pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(House)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static House DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(House)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (House)xs.Deserialize(memoryStream); 
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
