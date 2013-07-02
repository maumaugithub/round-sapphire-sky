package com.etala.ws.mailchimp;

import java.io.Serializable;
import java.util.Date;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.adapters.XmlJavaTypeAdapter;

import com.etala.data.Security;
import com.etala.data.utils.DateAdapter;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlRootElement(name = "response")
public class Response implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = 6310824324018692487L;

	@XmlAttribute
	private String type;
	
	@XmlAttribute
	@XmlJavaTypeAdapter(type = Date.class, value = DateAdapter.class)
	private Date serverTime = new Date();
	
	@XmlElement
	private Security security;

		
	public Security getSecurity() {
		return security;
	}

	public void setSecurity(Security security) {
		this.security = security;
	}

	public Date getServerTime() {
		return serverTime;
	}

	public void setServerTime(Date serverTime) {
		this.serverTime = serverTime;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

}
