using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("login")]
public class Login
{
	[XmlAttribute("email")]	
	public string email;
	[XmlIgnore]
	public DateTime subscriptionExpiry;
	[XmlAttribute("facebookId")]	
	public string facebookId;
	
	public Login(){}
	
	public void debug() {
		Debug.Log("login: email=" + email+", subscriptionExpiry=" + subscriptionExpiry+", facebookId="+facebookId);
	}
	
	[XmlAttribute("subscriptionExpiry")]
	public string SubscriptionExpiryConv {
		get {return this.subscriptionExpiry.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.subscriptionExpiry = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Login pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Login)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Login DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Login)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Login)xs.Deserialize(memoryStream); 
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
