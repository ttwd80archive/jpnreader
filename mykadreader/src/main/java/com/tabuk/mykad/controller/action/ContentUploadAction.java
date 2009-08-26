package com.tabuk.mykad.controller.action;

import java.util.Date;

import org.apache.commons.codec.binary.Base64;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.config.BeanDefinition;
import org.springframework.context.annotation.Scope;
import org.springframework.dao.DataAccessException;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;
import com.opensymphony.xwork2.conversion.annotations.TypeConversion;
import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;
import com.tabuk.mykad.model.service.MyKadReaderService;

@Scope(BeanDefinition.SCOPE_PROTOTYPE)
@Component
public class ContentUploadAction implements Action {

	private String content;
	private Student student;
	private Date dob;
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

	@TypeConversion(converter = "com.tabuk.mykad.controller.converter.DateConverter")
	public void setDob(final Date dob) {
		this.dob = dob;
	}

	public String execute() throws Exception {
		try {
			student.setDob(dob);
			final String id = student.getId();
			final StudentImage studentImage = new StudentImage();
			studentImage.setId(id);
			studentImage.setContent(Base64.decodeBase64(content.getBytes()));
			if (myKadReaderService.getStudent(id) == null) {
				myKadReaderService.insertStudent(student, studentImage);

			} else {
				myKadReaderService.updateStudent(student, studentImage);
			}
			return SUCCESS;
		} catch (final DataAccessException e) {
			logger.error(e.toString());
		}
		return ERROR;
	}
}
