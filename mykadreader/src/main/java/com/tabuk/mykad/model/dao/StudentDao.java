package com.tabuk.mykad.model.dao;

import com.tabuk.mykad.model.entity.Student;

public interface StudentDao {
	void save(Student student);

	void update(Student student);

	Student get(String id);
}
