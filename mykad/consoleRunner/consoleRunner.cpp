// consoleRunner.cpp : main project file.

#include "stdafx.h"

using namespace System;

int main(array<System::String ^> ^args)
{
	unsigned char TxBuffer[500] = {'x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x'};
	const unsigned char CmdSelectFile[] =
	{0xCC, 0x00, 0x00, 0x00, 0x08}; //append with pp pp qq qq rr rr ss ss
	const unsigned char CmdSetLength[] =
	{0xC8, 0x32, 0x00, 0x00, 0x05, 0x08, 0x00, 0x00};		//append with ss ss
	int i;
	int FileNum = 1;
	int split_offset = 500;
	int split_length = 252;
	for (i=0; i<8; TxBuffer[i++] = CmdSetLength[i]);
	*(short *) (TxBuffer+i) = split_length;	i += 2;


	Console::WriteLine(L"Hello World");
	return 0;
}
