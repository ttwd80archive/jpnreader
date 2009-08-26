package com.tabuk.mykad.model.dao.hibernate;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.orm.hibernate3.HibernateTemplate;
import org.springframework.stereotype.Repository;

import com.tabuk.mykad.model.dao.StudentImageDao;
import com.tabuk.mykad.model.entity.StudentImage;

@Repository
public class HibernateStudentImageDao implements StudentImageDao {
	private final HibernateTemplate hibernateTemplate;

	@Autowired
	public HibernateStudentImageDao(final HibernateTemplate hibernateTemplate) {
		this.hibernateTemplate = hibernateTemplate;
	}

	@Override
	public void update(final StudentImage studentImage) {
		hibernateTemplate.merge(studentImage);
	}

	@Override
	public void save(final StudentImage studentImage) {
		hibernateTemplate.save(studentImage);
	}

}
