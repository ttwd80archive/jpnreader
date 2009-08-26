package com.tabuk.mykad.controller.action;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

import org.apache.struts2.ServletActionContext;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.config.BeanDefinition;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.tabuk.mykad.model.service.CacheService;

@Scope(BeanDefinition.SCOPE_PROTOTYPE)
@Component("pullImageAction")
// TODO: refactor to single input for base64 content
public class PullImageAction implements Action {
	private String id;
	private final CacheService cacheService;
	private InputStream inputStream;
	private int contentLength;

	@Autowired
	public PullImageAction(final CacheService cacheService) {
		this.cacheService = cacheService;
	}

	public String execute() throws Exception {
		final String sessionId = ServletActionContext.getRequest().getSession().getId();
		final String key = sessionId + ":" + id;
		final byte[] content = (byte[]) cacheService.get(key);
		if (content != null) {
			inputStream = new ByteArrayInputStream(content);
			contentLength = content.length;
			return SUCCESS;
		}
		return ERROR;
	}

	public int getContentLength() {
		return contentLength;
	}

	public InputStream getInputStream() {
		return inputStream;
	}

	public void setId(final String id) {
		this.id = id;
	}

}
