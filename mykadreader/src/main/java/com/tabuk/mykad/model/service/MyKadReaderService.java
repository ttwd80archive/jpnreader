package com.tabuk.mykad.model.service;

import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;

public interface MyKadReaderService {

	void insertStudent(Student student, StudentImage studentImage);

	void updateStudent(Student student, StudentImage studentImage);

	Student getStudent(String id);
}
