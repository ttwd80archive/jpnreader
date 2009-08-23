package com.tabuk.mykad.model.service;

import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.tabuk.mykad.model.dao.StudentDao;
import com.tabuk.mykad.model.dao.StudentImageDao;
import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;

@Service
public class MyKadReaderServiceImpl implements MyKadReaderService {
	private final StudentDao studentDao;
	private final StudentImageDao studentImageDao;

	public MyKadReaderServiceImpl(final StudentDao studentDao, final StudentImageDao studentImageDao) {
		this.studentDao = studentDao;
		this.studentImageDao = studentImageDao;
	}

	@Transactional
	public void saveOrUpdateStudent(final Student student, final StudentImage studentImage) {
		studentDao.saveOrUpdate(student);
		student.setStudentImage(studentImage);
		studentImage.setStudent(student);
		studentImageDao.saveOrUpdate(studentImage);
	}

}
