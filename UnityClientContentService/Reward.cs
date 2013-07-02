using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("reward")]
public class Reward
{
	[XmlAttribute("id")]	
	public int id;
	
	[XmlAttribute("name")]
	public string name;
	
	[XmlAttribute("description")]
	public string description;
	
	[XmlAttribute("type")]
	public string type;
	
	[XmlAttribute("order")]
	public int order;
	
	[XmlAttribute("defaultReward")]
	public bool defaultReward;
	
	[XmlAttribute("premiumReward")]
	public bool premiumReward;
	
	[XmlAttribute("url")]
	public string url;
	
	[XmlAttribute("notRecievedUrl")]
	public string notRecievedUrl;
	
	[XmlAttribute("used")]
	public bool used;
	
	[XmlIgnore]
	public DateTime from;
	
	[XmlIgnore]
	public DateTime to;
	
	[XmlAttribute("houseId")]
	public int houseId;
	
	[XmlAttribute("bookId")]
	public int bookId;
		
	[XmlIgnore]
	public DateTime lastUpdate;
		
	public Reward(){}
	
	public void debug() {
		Debug.Log ("reward: id=" + id+", name=" + name +", description="+description+", type="+type+", order=" + order+", defaultReward"+defaultReward+", premiumReward=" + premiumReward+", url=" + url+", notRecievedUrl=" + notRecievedUrl+", used=" + used+", from" + from+", to="+ to+", houseId=" + houseId+", bookId=" + bookId+ ", lastUpdate=" + lastUpdate);
	}
	
	[XmlAttribute("lastUpdate")]
	public string lastUpdateConv {
		get {return this.lastUpdate.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.lastUpdate = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}

	[XmlAttribute("from")]
	public string fromConv {
		get {return this.from.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.from = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}

	[XmlAttribute("to")]
	public string toConv {
		get {return this.to.ToString("dd/MM/yyyy HH:mm:ss");}
		set {this.to = DateTime.ParseExact(value,"d/MM/yyyy HH:mm:ss",new CultureInfo("en-GB"));}
	}

	
	// Here we serialize our UserData object of myData 
	public string SerializeObject(Reward pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Reward)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Reward DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Reward)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Reward)xs.Deserialize(memoryStream); 
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
