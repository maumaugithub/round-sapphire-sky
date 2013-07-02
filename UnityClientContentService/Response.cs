using UnityEngine; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("response")]
public class Response
{
	[XmlAttribute("type")]
	public string type;
	
	[XmlIgnore]
	public DateTime serverTime;
	
	[XmlElement("security")]
	public Security security;
	
	[XmlElement("login")]
	public Login login;
	
	[XmlElement("content")]
	public Content content;
	
	[XmlArray("purchaseOptions")]
	[XmlArrayItem("purchaseOption")]
	public List<PurchaseOption> purchaseOptions;
	
	[XmlArray("rewards")]
	[XmlArrayItem("reward")]
	public List<Reward> reward;
	
	[XmlElement("ratings")]
	public Ratings ratings;
	
	public Response(){}
	
	public void debug() {
		Debug.Log ("response: type=" + type+", serverTime=" + serverTime);
		security.debug();
		login.debug();
		content.debug();
		foreach(PurchaseOption po in purchaseOptions) {
			po.debug();
		}
		foreach(Reward r in reward) {
			r.debug();
		}
	}
	
	[XmlAttribute("serverTime")]
	public string serverTimeConv {
		get {return this.serverTime.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.serverTime = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
			
	
	   // Here we serialize our UserData object of myData 
   public string SerializeObject(Response pObject) 
   { 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Response)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Response DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Response)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Response)xs.Deserialize(memoryStream); 
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
