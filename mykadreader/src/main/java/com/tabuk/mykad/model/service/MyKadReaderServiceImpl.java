package com.tabuk.mykad.model.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.tabuk.mykad.model.dao.StudentDao;
import com.tabuk.mykad.model.dao.StudentImageDao;
import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;

@Service("myKadReaderService")
public class MyKadReaderServiceImpl implements MyKadReaderService {
	private final StudentDao studentDao;
	private final StudentImageDao studentImageDao;

	@Autowired
	public MyKadReaderServiceImpl(final StudentDao studentDao, final StudentImageDao studentImageDao) {
		this.studentDao = studentDao;
		this.studentImageDao = studentImageDao;
	}

	@Transactional
	public void insertStudent(final Student student, final StudentImage studentImage) {
		student.setStudentImage(studentImage);
		studentImage.setStudent(student);
		studentDao.save(student);
		studentImageDao.save(studentImage);
	}

	@Transactional
	public void updateStudent(final Student student, final StudentImage studentImage) {
		student.setStudentImage(studentImage);
		studentImage.setStudent(student);
		studentDao.update(student);
		studentImageDao.update(studentImage);
	}

	@Transactional(readOnly = true)
	public Student getStudent(final String id) {
		return studentDao.get(id);
	}

}
