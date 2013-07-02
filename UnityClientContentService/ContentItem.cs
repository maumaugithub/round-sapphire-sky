using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("contentItem")]
public class ContentItem
{
	[XmlAttribute("type")]	
	public string type;
	
	[XmlAttribute("url")]
	public string url;
	
	[XmlIgnore]
	public DateTime changed;
		
	public ContentItem(){}
	
	public void debug() {
		Debug.Log ("contentItem: type="+type+",url=" + url+",change=" + changed );
	}
	
	[XmlAttribute("changed")]
	public string changedConv {
		get {return this.changed.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.changed = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(ContentItem pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(ContentItem)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static ContentItem DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(ContentItem)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (ContentItem)xs.Deserialize(memoryStream); 
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
