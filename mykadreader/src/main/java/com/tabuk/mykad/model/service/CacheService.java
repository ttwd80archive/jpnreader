package com.tabuk.mykad.model.service;

import java.io.Serializable;

public interface CacheService {
	void put(String id, Serializable object);

	Serializable get(String id);
}
