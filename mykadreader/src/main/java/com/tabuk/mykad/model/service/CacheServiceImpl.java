package com.tabuk.mykad.model.service;

import java.io.Serializable;

import net.sf.ehcache.Cache;
import net.sf.ehcache.Element;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Service;

@Service("cacheService")
public class CacheServiceImpl implements CacheService {

	private final Cache cache;

	@Autowired
	public CacheServiceImpl(@Qualifier("imageCache") final Cache cache) {
		this.cache = cache;
	}

	public Serializable get(final String id) {
		final Element element = cache.get(id);
		if (element != null) {
			return element.getValue();
		}
		return null;
	}

	public void put(final String id, final Serializable object) {
		final Element element = new Element(id, object);
		cache.put(element);
	}

}
