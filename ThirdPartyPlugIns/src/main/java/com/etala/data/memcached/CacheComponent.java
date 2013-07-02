package com.etala.data.memcached;

import java.io.IOException;
import java.io.InputStream;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Properties;

import net.spy.memcached.AddrUtil;
import net.spy.memcached.MemcachedClient;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class CacheComponent {
	private static MemcachedClient client = null;
	public static final int SHORT = 60;
	public static final int MEDIUM = 240;
	public static final int LONG = 1440;
	public static final int PERMENANT = 43200;
	public static final String SECURITY_KEY = "ETALA:SECURITY:KEY:";

	private static final Logger LOG = LoggerFactory.getLogger(CacheComponent.class);
	private static final CacheComponent instance = new CacheComponent();

	public static CacheComponent getCache() {
		return instance;
	}

	private CacheComponent() {
	}

	public Object getObject(String key) {
		if (client == null) {
			setCacheClient();
		}
		try {
			return client.get(key);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	private void setCacheClient() {
		String memcacheUrl = "localhost:11000";
		LOG.info("starting up cache");
		try {
			InputStream in = CacheComponent.class.getClassLoader().getResourceAsStream("cache.settings");
			Properties props = new Properties();
			props.load(in);
			memcacheUrl = props.getProperty("address");
		} catch (Exception e) {
			e.printStackTrace();
		}
		LOG.info("cache=" + memcacheUrl);
		try {
			getCacheClient(memcacheUrl);
		} catch (IOException e) {
		}
	}

	public void getCacheClient(String memcacheUrl) throws IOException {
		client = new MemcachedClient(AddrUtil.getAddresses(memcacheUrl));
	}

	public Object getAndTouch(String key, int minutes) {
		if (client == null) {
			setCacheClient();
		}
		if (client != null) {
			try {
				Object o = client.get(key);
				if (o != null) {
					set(key, o, minutes);
				}
				return o;
			} catch (Exception e) {
			}
		}
		return null;
	}

	public Object getAndRemove(String key) {
		if (client == null) {
			setCacheClient();
		}
		if (client != null) {
			try {
				Object o = client.get(key);
				if (o != null) {
					set(key, null, 5);
				}
				@SuppressWarnings("unchecked")
				HashMap<String, Date> keys = (HashMap<String, Date>) client.get("ETALA:KEY:LIST");
				if (keys == null) {
					keys = new HashMap<String, Date>();
				}
				keys.remove(key);
				set("ETALA:KEY:LIST", keys, CacheComponent.SHORT);
				return o;
			} catch (Exception e) {
			}
		}
		return null;
	}

	public void set(String key, Object value, int minutes) {
		if (client == null) {
			setCacheClient();
		}
		if (client != null) {
			try {
				if (value == null) {
					client.delete(key);
				} else {
					client.set(key, minutes * 60, value);
				}
				if (!(key.startsWith("ETALA:SECURITY:KEY") || key.startsWith("ETALA:KEY:LIST"))) {

					@SuppressWarnings("unchecked")
					HashMap<String, Date> keys = (HashMap<String, Date>) client.get("ETALA:KEY:LIST");
					if (keys == null) {
						keys = new HashMap<String, Date>();
					}
					if (value == null) {
						keys.remove(key);
					} else {
						Calendar c = Calendar.getInstance();
						c.add(Calendar.MINUTE, minutes);
						keys.put(key, c.getTime());
					}
					set("ETALA:KEY:LIST", keys, CacheComponent.SHORT);
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	public HashMap<String, Date> list() {
		if (client == null) {
			setCacheClient();
		}
		if (client != null) {
			try {
				@SuppressWarnings("unchecked")
				HashMap<String, Date> keys = (HashMap<String, Date>) client.get("ETALA:KEY:LIST");
				if (keys == null) {
					keys = new HashMap<String, Date>();
					set("ETALA:KEY:LIST", keys, CacheComponent.SHORT);
				}
				return keys;
			} catch (Exception e) {
			}
		}
		return new HashMap<String, Date>();
	}

	public String getKey(Object... keyParts) {
		StringBuffer str = new StringBuffer();
		boolean first = true;
		for (Object o : keyParts) {
			if (first) {
				first = false;
			} else {
				str.append(":");
			}
			str.append(o.toString());
		}
		return str.toString();
	}

}
