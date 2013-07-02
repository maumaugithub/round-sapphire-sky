using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("schedule")]
public class Schedule
{
	[XmlAttribute("id")]	
	public int id;
	
	[XmlAttribute("name")]
	public string name;
	
	[XmlAttribute("code")]
	public string code;
	
	[XmlElement("book")]
	public Book book;
	
	public Schedule(){}
	
	public void debug() {
		Debug.Log ("schedule: id=" + id+", name=" + name+", code=" + code);
		book.debug();
	}
	
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Schedule pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Schedule)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Schedule DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Schedule)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Schedule)xs.Deserialize(memoryStream); 
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
