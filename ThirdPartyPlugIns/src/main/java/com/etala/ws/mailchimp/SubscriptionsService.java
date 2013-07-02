/**
 * 
 */
package com.etala.ws.mailchimp;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import org.apache.commons.lang.StringUtils;
import org.apache.commons.lang.math.RandomUtils;
import org.apache.log4j.Logger;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.etala.data.Security;

/**
 * @author Maria  
 *
 */
@Service("newsletterSubsService")
@Path("/newsletter")
@Produces({MediaType.APPLICATION_XML, MediaType.APPLICATION_JSON})
public class SubscriptionsService {
	private static final Logger LOG = Logger.getLogger(SubscriptionsService.class);
	
	private static final String	URL	= "http://us2.api.mailchimp.com/1.3/";
	
	private static final String MAILCHIMP_APIKEY = "ece77c777ec656500000aa4848bda48b-us2";
	
	private static final String MAILCHIMP_NEWSLETTER_ID = "ec77ecff48";

	@PersistenceContext
    private EntityManager em;
	
	@GET
	@Transactional
	@Path("/subscription/{appCode}/{email}")
	public Response subscription(@PathParam("appCode") final String appCode, @PathParam("email") String useremail){
		Response resp = new Response();
		Security security = new Security();
		if(appCode!=null && StringUtils.isNotBlank(useremail)){
			security = processOnMailchimp(useremail, true);
		}
		
		security.setApplication(appCode);
		resp.setSecurity(security);
		return resp;
	}
	
	public Security processOnMailchimp(final String email, Boolean isRegister) {
		Security security = new Security();
		try {
			if (isRegister) {
				final Object o = new URL(URL + "?method=listSubscribe&apikey=" + MAILCHIMP_APIKEY + "&id=" + MAILCHIMP_NEWSLETTER_ID + "&email_address=" + email + "&update_existing=true&double_optin=false&send_welcome=false").getContent();
					if (o instanceof String) {
						final Exception e = new Exception((String) o);
						e.printStackTrace();
						security.setResponse(Security.EXCEPTION);
						security.setComment(e.getMessage());
					} else {
						final BufferedReader br = new BufferedReader(new InputStreamReader((InputStream) o));
						final StringBuffer str = new StringBuffer();
						while (br.ready()) {
							final String s = br.readLine();
							str.append(s);
							security.setResponse(Security.OK);
						}
						security.setComment(str.toString());
					}
			} 
		} catch (final Exception e) {
			e.printStackTrace();
			security.setResponse(Security.EXCEPTION);
			security.setComment(e.getMessage());
		}
		return security;
	}
	
	@GET
	@Transactional
	@Path("/test")
	public Response test(){
		Response resp = new Response();
		Security security = new Security();
		String useremail = "news"+RandomUtils.nextInt(100000)+"@mindshapes.com";
		if(StringUtils.isNotBlank(useremail)){
			security = processOnMailchimp(useremail, true);
		}

		resp.setSecurity(security);
		return resp;
	}
}
