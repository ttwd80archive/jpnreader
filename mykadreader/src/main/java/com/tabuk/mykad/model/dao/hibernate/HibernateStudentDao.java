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

	@Override
	public void save(final Student student) {
		hibernateTemplate.save(student);
	}

	@Override
	public void update(final Student student) {
		hibernateTemplate.merge(student);
	}

	@Override
	public Student get(final String id) {
		return (Student) hibernateTemplate.get(Student.class, id);
	}

}
