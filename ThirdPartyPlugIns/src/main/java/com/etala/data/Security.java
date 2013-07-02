package com.etala.data;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlRootElement;

import com.etala.data.enums.DevelopmentEnvironment;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlRootElement(name = "security")
public class Security implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = 2276582347844147115L;
	public static final int OK = 200;
	public static final int INVALID_SECURITY = 400;
	public static final int NO_DATA_AVAILABLE = 900;
	public static final int INVALID_PARAMETERS = 901;
	public static final int PURCHASED_FAILED = 902;
	public static final int UNKNOWN_APP = 950;
	public static final int EXCEPTION=999;

	@XmlAttribute
	private Integer id = -1;
	@XmlAttribute
	private String key = "";
	@XmlAttribute
	private Integer response = 0;
	@XmlAttribute
	private String comment;
	@XmlAttribute
	private String session;
	@XmlAttribute
	private String application; //set app code
	@XmlAttribute
	private String version;
	@XmlAttribute
	private DevelopmentEnvironment environment;

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String getKey() {
		return key;
	}

	public void setKey(String key) {
		this.key = key;
	}

	public Integer getResponse() {
		return response;
	}

	public void setResponse(Integer response) {
		this.response = response;
	}

	public String getComment() {
		return comment;
	}

	public void setComment(String comment) {
		this.comment = comment;
	}

	public String getSession() {
		return session;
	}

	public void setSession(String session) {
		this.session = session;
	}

	public String getApplication() {
		return application;
	}

	public void setApplication(String application) {
		this.application = application;
	}

	public String getVersion() {
		return version;
	}

	public void setVersion(String version) {
		this.version = version;
	}

	public DevelopmentEnvironment getEnvironment() {
		return environment;
	}

	public void setEnvironment(DevelopmentEnvironment environment) {
		this.environment = environment;
	}

}
