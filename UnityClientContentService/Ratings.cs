using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("ratings")]
public class Ratings 
{

	[XmlAttribute("showRating")]	
	public Boolean showRating;
	
	public Ratings(){}
	
	public void debug() {
		Debug.Log("ratings: showRating=" + showRating);
	}
	
	// Here we serialize our UserData object of myData 
	public string SerializeObject(Ratings pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Ratings)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Ratings DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Ratings)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Ratings)xs.Deserialize(memoryStream); 
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
