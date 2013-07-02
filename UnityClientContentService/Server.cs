using UnityEngine; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 
using System;

public class Server : MonoBehaviour {
    // Use this for initialization
//	public string serverUrl="https://v2.mindshapes.com/v2/";
	public string serverUrl="http://192.168.0.14:8080/v2/";
	private static Security security;
	private static Login login;
	private static Content content;
	private static string id="gtunity";
	private static string deviceType="iPad";
	private static string version = "1.0.0";
	private static List<ServerCall> calls = new List<ServerCall>();
	private static bool processing = false;
	public static bool online = false;
	private static float timeToTest = 0.0f;
	void Start () {
    }
	
	void Update() {
		if (!online) {
			if (timeToTest <= 0) {
				testOnline();
			} else {
				timeToTest -=Time.deltaTime;
			}
		} else if (calls.Count > 0 && (!processing)) {
			StartCoroutine(startProcess());
		}
	}
		
	private void testOnline()
	{
		timeToTest = 30.0f;
		Debug.Log("testing for offline " + calls.Count);
		if (calls.Count <=0) {
			Login ();
		}
		StartCoroutine(startProcess());
	}
	
	private void Callback(System.Object data)
	{
		Debug.Log ("got callback with " +data.GetType());
	}
	
	private System.Collections.IEnumerator startProcess() {
		processing = true;
		yield return 0;
		try {
			ServerCall sc = getFirst();
			if (sc != null) {
				calls.Remove(sc);
				if (typeof(LoginServerCall) == sc.GetType()) {
					StartCoroutine(doLogin ((LoginServerCall)sc));
				}
			} else {
				processing = false;
			}
		} catch (Exception e) {
			Debug.Log(e);
			processing = false;
		}
	}
	
	private ServerCall getFirst() {
		foreach(ServerCall sc in calls) {
			return sc;
		}
		return null;
	}
	
	private void setOffline()
	{
		online = false;
		timeToTest = 30.0f;
	}
		
	
	public void Login()
	{
		LoginServerCall lsc = new LoginServerCall(id,deviceType, version, Callback);
		calls.Add(lsc);
	}
	
	private System.Collections.IEnumerator doLogin(LoginServerCall lsc)
	{
		Debug.Log("starting login" + calls.Count);
  		string url = serverUrl+"login/"+lsc.getId()+"/" + lsc.getDeviceType()+"/MSW"+"/" +lsc.getVersion(); 
		
		Debug.Log(url);
		WWW www = new WWW(url); 
  		yield return www; 
  		Debug.Log("after yield");
		try {
		if (www.error == null) {
			online = true;
    		//no error occured 
			Debug.Log (www.text);
    		Response response = Response.DeserializeObject(www.text);
			//login.debug();
			security = response.security;
			login = response.login;
			content = response.content;
			lsc.processCallback(response);
		} else {
    		Debug.Log("ERROR: " + www.error); 
			setOffline();
			calls.Add(lsc);
			
		} 	
  		www.Dispose(); 
  		www = null;
		} catch (Exception e) {
			Debug.Log("got an exception " + e.Message);
		}
		processing = false;
		Debug.Log("ending login"+ calls.Count);
	}
}

