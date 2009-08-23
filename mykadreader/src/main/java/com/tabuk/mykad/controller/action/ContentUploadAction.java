package com.tabuk.mykad.controller.action;

import org.apache.commons.codec.binary.Base64;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.config.BeanDefinition;
import org.springframework.context.annotation.Scope;
import org.springframework.dao.DataAccessException;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;
import com.tabuk.mykad.model.service.MyKadReaderService;

@Scope(BeanDefinition.SCOPE_PROTOTYPE)
@Component("contentUploadAction")
public class ContentUploadAction implements Action {

	private String content;
	private Student student;
	private final MyKadReaderService myKadReaderService;
	protected final Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	public ContentUploadAction(final MyKadReaderService myKadReaderService) {
		this.myKadReaderService = myKadReaderService;
	}

	public Student getStudent() {
		return student;
	}

	public void setStudent(final Student student) {
		this.student = student;
	}

	public void setContent(final String content) {
		this.content = content;
	}

	public String execute() throws Exception {
		try {
			final StudentImage studentImage = new StudentImage();
			studentImage.setId(student.getId());
			studentImage.setContent(Base64.decodeBase64(content.getBytes()));
			myKadReaderService.saveOrUpdateStudent(student, studentImage);
			return SUCCESS;
		} catch (final DataAccessException e) {
			logger.error(e.toString());
		}
		return ERROR;
	}
}
