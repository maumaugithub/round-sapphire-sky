package com.etala.data.utils;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

import javax.xml.bind.annotation.adapters.XmlAdapter;

public class DateAdapter extends XmlAdapter<String, Date> {

	// the desired format
    private String pattern = "dd/MM/yyyy hh:mm:ss";
    private String patternUS = "MM/dd/yyyy hh:mm:ss";
    
    
    public String marshal(Date date) throws Exception {
    	if (date == null) return null;
        return new SimpleDateFormat(pattern).format(date);
    }
    
    public Date unmarshal(String dateString) throws Exception {
    	Date d =new SimpleDateFormat(pattern).parse(dateString);
    	Calendar c= Calendar.getInstance();
    	c.setTime(d);
    	if (c.get(Calendar.YEAR) < 2012 || d.after(new Date())) {
    		return new SimpleDateFormat(patternUS).parse(dateString);
    	}
    	return d;
    }	
}
