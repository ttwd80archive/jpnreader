package com.tabuk.mykad.controller.action;

import java.io.ByteArrayOutputStream;

import net.sf.ehcache.Cache;
import net.sf.ehcache.Element;

import org.apache.commons.codec.binary.Base64;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.tabuk.mykad.controller.form.EncodedImageForm;

@Scope("prototype")
@Component("pushImageAction")
public class PushImageAction implements Action {

	private final Cache cache;

	private EncodedImageForm encodedImageForm;

	@Autowired
	public PushImageAction(@Qualifier("imageCache") Cache cache) {
		this.cache = cache;
	}

	public EncodedImageForm getEncodedImageForm() {
		return encodedImageForm;
	}

	public void setEncodedImageForm(EncodedImageForm encodedImageForm) {
		this.encodedImageForm = encodedImageForm;
	}

	public String execute() throws Exception {
		int count = encodedImageForm.getBlockCount();
		final String id = encodedImageForm.getId();
		ByteArrayOutputStream os = new ByteArrayOutputStream();
		String[] encodedBlocks = encodedImageForm.getBlocks();
		for (int i = 0; i < count; i++) {
			String encoded = encodedBlocks[i];
			byte[] content = Base64.decodeBase64(encoded.getBytes());
			os.write(content);
		}
		byte[] image = os.toByteArray();
		Element element = new Element(id, image);
		cache.put(element);
		return SUCCESS;
	}

}
