using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("security")]
public class Security
{
	[XmlAttribute("id")]	
	public string id;
	[XmlAttribute("key")]	
	public string key;
	[XmlAttribute("response")]	
	public int response;
	[XmlAttribute("session")]
	public string session;
	[XmlAttribute("additionalData")]
	public string additionalData;
	[XmlAttribute("application")]	
	public string application;
	[XmlAttribute("version")]
	public string version;
	[XmlAttribute("deviceType")]	
	public string deviceType;
	[XmlAttribute("environment")]	
	public string environment;
	
	
	public Security(){}
	
	public void debug() {
		Debug.Log("security: id=" + id +",key=" +key +", response="+ response+", session=" + session+", additionalData=" + additionalData+", application="+application+", version=" + version+", deviceType="+ deviceType+", environment="+environment);
	}
	
	   // Here we serialize our UserData object of myData 
   public string SerializeObject(Security pObject) 
   { 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Security)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Security DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Security)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Security)xs.Deserialize(memoryStream); 
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
