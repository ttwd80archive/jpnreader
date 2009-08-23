package com.tabuk.mykad.model.dao.hibernate;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.orm.hibernate3.HibernateTemplate;
import org.springframework.stereotype.Repository;

import com.tabuk.mykad.model.dao.StudentDao;
import com.tabuk.mykad.model.entity.Student;

@Repository
public class HibernateStudentDao implements StudentDao {

	private final HibernateTemplate hibernateTemplate;

	@Autowired
	public HibernateStudentDao(final HibernateTemplate hibernateTemplate) {
		this.hibernateTemplate = hibernateTemplate;
	}

	public void saveOrUpdate(final Student student) {
		hibernateTemplate.saveOrUpdate(student);
	}

}
