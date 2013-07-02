using System;

public class LoginServerCall : ServerCall
{
	private string id;
	private string deviceType;
	private string version;
	
	public delegate void Callback(object data);
	
	private Callback callback;
	
	public LoginServerCall (string id, string deviceType, string version, Callback callback)
	{
		this.id = id;
		this.deviceType = deviceType;
		this.version = version;
		this.callback = callback;
	}
	
	public string getId()
	{
		return this.id;
	}
	
	public string getDeviceType()
	{
		return this.deviceType;
	}
	
	public string getVersion()
	{
		return this.version;
	}
	
	public void processCallback(object data)
	{
		try {
			this.callback(data);
		} catch (Exception e){}
	}
}

