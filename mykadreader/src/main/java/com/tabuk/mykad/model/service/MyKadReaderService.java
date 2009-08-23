package com.tabuk.mykad.model.service;

import com.tabuk.mykad.model.entity.Student;
import com.tabuk.mykad.model.entity.StudentImage;

public interface MyKadReaderService {

	void saveOrUpdateStudent(Student student, StudentImage studentImage);
}
