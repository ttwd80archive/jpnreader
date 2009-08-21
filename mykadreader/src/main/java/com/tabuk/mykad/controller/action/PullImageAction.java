package com.tabuk.mykad.controller.action;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.util.Map;

import net.sf.ehcache.Cache;
import net.sf.ehcache.Element;

import org.apache.struts2.interceptor.ParameterAware;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;

@Scope("prototype")
@Component("pullImageAction")
public class PullImageAction implements Action, ParameterAware {

	private final Cache cache;
	private Map<String, String[]> parameters;
	private InputStream inputStream;
	private int contentLength;

	@Autowired
	public PullImageAction(@Qualifier("imageCache") final Cache cache) {
		this.cache = cache;
	}

	public String execute() throws Exception {
		final String[] ids = parameters.get("id");
		if (ids.length == 1) {
			final String key = ids[0];
			final Element element = cache.get(key);
			if (element != null) {
				final byte[] content = (byte[]) element.getValue();
				inputStream = new ByteArrayInputStream(content);
				contentLength = content.length;
				return SUCCESS;
			}
		}
		return ERROR;
	}

	public void setParameters(final Map<String, String[]> parameters) {
		this.parameters = parameters;
	}

	public int getContentLength() {
		return contentLength;
	}

	public InputStream getInputStream() {
		return inputStream;
	}

}
