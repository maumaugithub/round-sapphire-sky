using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Collections.Generic;
using System.Globalization;

[XmlRoot("content")]
public class Content
{
	
	[XmlArray("houses")]
	[XmlArrayItem("house")]
	public List<House> houses;
		

		
	[XmlAttribute("promo")]	
	public Promo promo;
		
	[XmlArray("schedules")]
	[XmlArrayItem("schedule")]
	public List<Schedule> schedules;
	
	public Content(){}
	
	public void debug() {
		Debug.Log ("content:");
		foreach(House h in houses) {
			h.debug();
		}
		if (promo != null) {
			promo.debug();
		}
		if (schedules != null) {
			foreach(Schedule s in schedules) {
				s.debug();
			}
		}
	}
	
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Content pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Content)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Content DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Content)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Content)xs.Deserialize(memoryStream); 
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
