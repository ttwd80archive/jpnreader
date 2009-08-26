package com.tabuk.mykad.controller.converter;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Map;

import org.apache.struts2.util.StrutsTypeConverter;

import com.opensymphony.xwork2.conversion.TypeConversionException;

public class DateConverter extends StrutsTypeConverter {

	private final String DATE_FORMAT = "dd/MM/yyyy";

	@SuppressWarnings("unchecked")
	@Override
	public Object convertFromString(final Map context, final String[] values, final Class toClass) {
		final SimpleDateFormat simpleDateFormat = new SimpleDateFormat(DATE_FORMAT);
		final String value = values[0];
		try {
			return simpleDateFormat.parse(value);
		} catch (final ParseException e) {
			throw new TypeConversionException(e);
		}

	}

	@SuppressWarnings("unchecked")
	@Override
	public String convertToString(final Map context, final Object o) {
		final SimpleDateFormat simpleDateFormat = new SimpleDateFormat(DATE_FORMAT);
		return simpleDateFormat.format((Date) o);
	}

}
