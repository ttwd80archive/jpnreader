package com.tabuk.mykad.controller.form;

import java.io.Serializable;
import java.util.List;

public class EncodedImageForm implements Serializable {

	private Integer blockCount;
	private String id;
	private List<String> blocks;

	public Integer getBlockCount() {
		return blockCount;
	}

	public void setBlockCount(final Integer blockCount) {
		this.blockCount = blockCount;
	}

	public String getId() {
		return id;
	}

	public void setId(final String id) {
		this.id = id;
	}

	public List<String> getBlocks() {
		return blocks;
	}

	public void setBlocks(final List<String> blocks) {
		this.blocks = blocks;
	}

}
