#pragma once
#include <string>
using namespace std;
class Stat
{
	int count;
	string bar;
public:

	Stat()
	{
	}

	Stat(int val)
	{
		set(val);
	}


	void set(int val) 
	{
		count = val;

		for (int i = 0; i < val; i++)
		{
			bar += "-";
		}
	}


	string print()
	{
		return bar;
	}

	int val()
	{
		return count;
	}



	void operator-- (int)
	{
		if (count > 0) {
			count--;
			bar.pop_back();
		}
	}

	void operator++ (int)
	{
		count++;
		bar += "-";
	}

};