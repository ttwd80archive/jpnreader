package com.tabuk.mykad.controller.action;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.util.List;

import org.apache.commons.codec.binary.Base64;
import org.apache.struts2.ServletActionContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.tabuk.mykad.controller.form.EncodedImageForm;
import com.tabuk.mykad.model.service.CacheService;

@Scope("prototype")
@Component("pushImageAction")
public class PushImageAction implements Action {

	private final CacheService cacheService;

	private EncodedImageForm encodedImageForm;

	protected final Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	public PushImageAction(final CacheService cacheService) {
		this.cacheService = cacheService;
	}

	public CacheService getCacheService() {
		return cacheService;
	}

	public EncodedImageForm getEncodedImageForm() {
		return encodedImageForm;
	}

	public void setEncodedImageForm(final EncodedImageForm encodedImageForm) {
		this.encodedImageForm = encodedImageForm;
	}

	private void putInCache() throws IOException {
		final int count = encodedImageForm.getBlockCount();
		final String sessionId = ServletActionContext.getRequest().getSession().getId();
		final String id = sessionId + ":" + encodedImageForm.getId();
		final ByteArrayOutputStream os = new ByteArrayOutputStream();
		final List<String> encodedBlocks = encodedImageForm.getBlocks();
		for (int i = 0; i < count; i++) {
			final String encoded = encodedBlocks.get(i);
			final byte[] content = Base64.decodeBase64(encoded.getBytes());
			os.write(content);
		}
		final byte[] image = os.toByteArray();
		cacheService.put(id, image);
	}

	public String execute() throws Exception {
		try {
			putInCache();
			return SUCCESS;
		} catch (final IOException e) {
			logger.error(e.toString());
		}
		return ERROR;
	}

}
