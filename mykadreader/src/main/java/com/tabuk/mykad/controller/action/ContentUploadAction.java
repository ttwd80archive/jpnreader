package com.tabuk.mykad.controller.action;

import org.springframework.beans.factory.config.BeanDefinition;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import com.opensymphony.xwork2.Action;

@Scope(BeanDefinition.SCOPE_PROTOTYPE)
@Component("contentUploadAction")
public class ContentUploadAction implements Action {

	@Override
	public String execute() throws Exception {
		// TODO Auto-generated method stub
		return null;
	}

}
