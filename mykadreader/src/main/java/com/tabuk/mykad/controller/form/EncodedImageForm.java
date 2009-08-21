package com.tabuk.mykad.controller.form;

import java.io.Serializable;

public class EncodedImageForm implements Serializable {

	private Integer blockCount;
	private String id;
	private String[] blocks;

	public Integer getBlockCount() {
		return blockCount;
	}

	public void setBlockCount(Integer blockCount) {
		this.blockCount = blockCount;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String[] getBlocks() {
		return blocks;
	}

	public void setBlocks(String[] blocks) {
		this.blocks = blocks;
	}

}
