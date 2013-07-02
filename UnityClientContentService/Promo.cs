using UnityEngine; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;
using System.Globalization;

[XmlRoot("book")]
public class Promo
{
	[XmlArray("carousel")]
	[XmlArrayItem("item")]
	public List<Item> carousel;
	
	[XmlArray("categories")]
	[XmlArrayItem("category")]
	public List<Category> categories;

	[XmlArray("sections")]
	[XmlArrayItem("section")]
	public List<Section> promoSection;
	
	public Promo(){}
	
	public void debug() {
		Debug.Log("promo:");
		foreach(Item i in carousel) {
			i.debug();
		}
		foreach(Category c in categories) {
			c.debug();
		}
		foreach(Section s in promoSection) {
			s.debug();
		}
	}
	
	   // Here we serialize our UserData object of myData 
	public string SerializeObject(Promo pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(Promo)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString;
   } 
 
   // Here we deserialize it back into its original form 
   public static Promo DeserializeObject(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(Promo)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return (Promo)xs.Deserialize(memoryStream); 
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
