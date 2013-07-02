package com.etala.data.utils;

import java.text.SimpleDateFormat;
import java.util.Date;

import javax.xml.bind.annotation.adapters.XmlAdapter;

public class DateAdapterUS extends XmlAdapter<String, Date> {

	// the desired format
    private String pattern = "MM/dd/yyyy hh:mm:ss";
    
    public String marshal(Date date) throws Exception {
    	if (date == null) return null;
        return new SimpleDateFormat(pattern).format(date);
    }
    
    public Date unmarshal(String dateString) throws Exception {
        return new SimpleDateFormat(pattern).parse(dateString);
    }	
}
