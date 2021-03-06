package com.tabuk.mykad.model.entity;
// Generated Aug 24, 2009 8:06:58 AM by Hibernate Tools 3.2.4.GA


import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToOne;
import javax.persistence.PrimaryKeyJoinColumn;
import javax.persistence.Table;
import org.hibernate.annotations.GenericGenerator;
import org.hibernate.annotations.Parameter;

/**
 * StudentImage generated by hbm2java
 */
@Entity
@Table(name="student_image"
)
public class StudentImage  implements java.io.Serializable {


     private String id;
     private Student student;
     private byte[] content;

    public StudentImage() {
    }

    public StudentImage(Student student, byte[] content) {
       this.student = student;
       this.content = content;
    }
   
     @GenericGenerator(name="generator", strategy="foreign", parameters=@Parameter(name="property", value="student"))@Id @GeneratedValue(generator="generator")

    
    @Column(name="id", unique=true, nullable=false, length=12)
    public String getId() {
        return this.id;
    }
    
    public void setId(String id) {
        this.id = id;
    }

@OneToOne(fetch=FetchType.LAZY)@PrimaryKeyJoinColumn
    public Student getStudent() {
        return this.student;
    }
    
    public void setStudent(Student student) {
        this.student = student;
    }

    
    @Column(name="content", nullable=false)
    public byte[] getContent() {
        return this.content;
    }
    
    public void setContent(byte[] content) {
        this.content = content;
    }




}


