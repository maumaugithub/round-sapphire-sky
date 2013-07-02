using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("section")]
public class Section
{
	[XmlAttribute("id")]	
	public int id;
	
	[XmlAttribute("url")]
	public string url;
	
	[XmlAttribute("enabled")]
	public bool enabled;
	
	[XmlAttribute("page")]
	public string page;
	
	[XmlAttribute("house")]
	public int house;
	
	[XmlAttribute("book")]
	public int book;
	
	[XmlAttribute("closable")]
	public bool closable;
	
	[XmlAttribute("offline")]
	public bool offline;
		
	[XmlIgnore]
	public DateTime updated;
		
	public Section(){}
	
	public void debug() {
		Debug.Log ("section: id=" + id+", url=" + url+", enabled=" + enabled+", page=" + page+", house=" + house+", book=" + book+", closable=" + closable+", offline=" + offline+", updated=" + updated);
	}
	
	[XmlAttribute("updated")]
	public string lastUpdateConv {
		get {return this.updated.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.updated = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Section pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Section)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Section DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Section)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Section)xs.Deserialize(memoryStream); 
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
