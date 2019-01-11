// Tamogochi.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//


#include "pch.h"
#include <iostream>
#include <stdio.h>
#include <Windows.h>
#include <conio.h>
#include <dos.h>
#include <string>
#include "Stat.h"
#include "Header.h"
#include <thread> 
#include <vector>
#include <fstream>
using namespace std;


struct pet {
	string petim;
	int health;
	int hunger;
	int spirit;

};


bool  alive = true;
bool selfkill = false;

Stat health(25);
Stat hunger(25);
Stat spirit(25);
string petim = "";

void screen();
void press();
void live_sycle();


void press()
{
	while (alive)
	{
		GetKEY();
		if (KEY[70])
		{
			spirit++;
			cout << "repsect" << endl;
		}
		if (KEY[71])
		{
			hunger++;
			cout << " fead " << endl;
		}

		if (KEY[72])
		{
			health++;
			cout << " Heal " << endl;
		}
		Sleep(100);
	}
}

void live_sycle() {
	cout << "livesycle join";
	while (alive)
	{
		if (hunger.val() == 0)
		{
			health--;
		}

		hunger--;
		spirit--;


		if (spirit.val() == 0)
		{
			selfkill = true;
			alive = false;
			break;
		}

		if (health.val() == 0)
		{
			alive = false;
			break;
		}
		Sleep(1000);
	}

}


void screen() {
	while (alive)
	{

		cout << petim << endl;
		cout << health.val() << " health    " << health.print() << endl;
		cout << hunger.val() << " hunger    " << hunger.print() << endl;
		cout << spirit.val() << " spirit    " << spirit.print() << endl;

		cout << "press F to pay respect      press G to fead       presss  H to heal " << endl;

		Sleep(100);
		system("cls");
	}
}
thread draw_screen(screen);
thread button_handler(press);
thread stat_count(live_sycle);



int main()
{
	setlocale(LC_ALL, "");

	ifstream read_pets;

	string str = "";
	vector<string> petss;
	
	int pointer = 0;
	petss.push_back("");

	read_pets.open("pets.txt");



	while (getline(read_pets,str) )
	{
		if (str != "q") {
			
			petss[pointer] += "\n"+str;
		}
		else {
			petss.push_back(" ");
			pointer++;
		}
	}

	read_pets.close();

	pet *array = new pet[5];

	

	for (int i = 0; i < petss.size(); i++) {
		array[i].health = rand() % 10 + 40;
		array[i].hunger = rand() % 50 + 40;
		array[i].spirit = rand() % 50 + 40;
		array[i].petim = petss[i];
	}
	

	health.set(array[0].health);
	spirit.set(array[0].spirit);
	hunger.set(array[0].hunger);
	petim = array[0].petim;




	button_handler.join();
	stat_count.join();
	draw_screen.join();





	if (!alive) {
		if (!selfkill)
		{
			cout << "Game over!!" << endl;
		}
		else
		{
			cout << "Game over!! selfkill !!" << endl;
		}
	}


	system("pause");
	return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
