package com.tabuk.mykad.controller.action;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.util.Map;

import org.apache.struts2.interceptor.ParameterAware;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.tabuk.mykad.model.service.CacheService;

@Scope("prototype")
@Component("pullImageAction")
public class PullImageAction implements Action, ParameterAware {

	private final CacheService cacheService;
	private Map<String, String[]> parameters;
	private InputStream inputStream;
	private int contentLength;

	public PullImageAction(final CacheService cacheService) {
		this.cacheService = cacheService;
	}

	public String execute() throws Exception {
		final String[] ids = parameters.get("id");
		if (ids.length == 1) {
			final String key = ids[0];
			final byte[] content = (byte[]) cacheService.get(key);
			if (content != null) {
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
