#include <iostream>
#include <string>
#include <vector>

using namespace std;

int generateID() // генеруємо id для елементів списку, які будуть різними для всіх елементів 2 списків
{
    static int s_id{ 0 };
    return ++s_id;
}
 
class Computer {
    protected:
        double fRand(double fMin, double fMax) // генеруємо випадкове число типу double
        {
            double f = (double)rand() / RAND_MAX;
            return fMin + f * (fMax - fMin);
        }
    public:
        int id;
        string name;
    	int RAM;
    	double proc_speed;
    	
        Computer() {
            id = generateID();
            name = "Device_" + to_string(id);
            RAM = rand() % 32 + 1; // генеруємо випадкове ціле число
            proc_speed = fRand(1, 10);
        }
    	
    	void output_information() // виводимо дані про гаджет 
    	{
    		cout << "Name: "<< name;
    		cout << "\nRAM: "<< RAM;
    		cout << "\nProcessor Speed: "<< proc_speed;
    	}

	
};

string PC_OS_names[3] = {"Win", "Dos", "Linux"}; // доступні назви операційних систем для комп'ютерів
bool bool_arr[2] = {true, false};
class PC : Computer {
    public:
        string OS;
    	bool has_video_card;
    	int USB;
    	
        PC() : Computer()
        {
            int size = sizeof(PC_OS_names)/sizeof(PC_OS_names[0]);
            OS = PC_OS_names[rand() % size]; // вибираємо випадкове значення операційної системи
            has_video_card = bool_arr[rand() % 2];
            USB = rand() % 10;
        }
        
        void output_information() // виводимо дані про комп'ютер
    	{
    		Computer::output_information();
    		cout << "\nOS:" << OS;
    		cout << "\nWith video card:" << has_video_card;
    		cout << "\nUSB:" << USB;
    	}

};    	


string Tablet_OS_names[3] = {"Win", "Android", "iOS"}; // доступні назви операційних систем для планшетів
class Tablet : Computer {
    public:
        string OS;
        double diagonal_length;
    	int resolution;
    	
    	Tablet() : Computer()
        {
            int size = sizeof(Tablet_OS_names)/sizeof(Tablet_OS_names[0]);
            OS = Tablet_OS_names[rand() % size]; // вибираємо випадкове значення операційної системи
            diagonal_length = fRand(5, 20);
            resolution = rand() % 200;
        }
        
        void output_information() // виводимо дані про планшет
    	{
    		Computer::output_information();
    		cout << "\nOS:" << OS;
    		cout << "\nDiagonal length:" << diagonal_length;
    		cout << "\nResolution:" << resolution;
    	}
};

class PC_list
{
    public:
        vector<PC> list; // список із комп'ютерів
        
        PC_list()
        {
            for(int i = 0; i < 3; i++)
            {
                PC obj;
                list.push_back(obj);
            }
        }
        PC_list(int count) // генеруємо список із комп'ютерів заданої довжини
        {
            for(int i = 0; i < count; i++)
            {
                PC obj;
                list.push_back(obj);
            }
        }
        void add(PC obj) // додаємо елемент до списку
        {
            list.push_back(obj);
        }
        PC select(int index) // вибираємо елемент із списку по індексу
        {
            return list[index];
        }
        void output_selected(int index) // вибираємо елемент із списку по індексу і виводимо його на екран
        {
            list[index].output_information();
        }
        void output_length() // виводимо довжину списку
        {
            cout << list.size() << endl;
        }
        void output_information() // виводимо весь список
    	{
    		for(int i = 0; i < list.size(); i++)
            {
                list[i].output_information();
                cout << endl << endl;
            }
            cout << "------------------------------------" << endl;
    	}
        
};

class Tablet_list
{
    public:
        vector<Tablet> list; // список із планшетів
        
        Tablet_list()
        {
            for(int i = 0; i < 3; i++)
            {
                Tablet obj;
                list.push_back(obj);
            }
        }
        Tablet_list(int count) // генеруємо список із планшетів заданої довжини
        {
            for(int i = 0; i < count; i++)
            {
                Tablet obj;
                list.push_back(obj);
            }
        }
        void add(Tablet obj) // додаємо елемент до списку
        {
            list.push_back(obj);
        }
        Tablet select(int index) // вибираємо елемент із списку по індексу
        {
            return list[index];
        }
        void output_selected(int index) // вибираємо елемент із списку по індексу і виводимо його на екран
        {
            list[index].output_information();
        }
        void output_length() // виводимо довжину списку
        {
            cout << list.size() << endl;
        }
        void output_information() // виводимо весь список
    	{
    		for(int i = 0; i < list.size(); i++)
            {
                list[i].output_information();
                cout << endl << endl;
            }
            cout << "------------------------------------" << endl;
    	}
        
};

int main()
{
    srand (time(NULL)); // для генерації випадкових чисел
    int count;
    // Генеруємо перший список
    cout << "Input size of the first list: ";
    cin >> count;
    PC_list list1 = PC_list(count);
    cout << "Initial first list: " << endl;
    list1.output_information();
    // Генеруємо другий список
    cout << "Input size of the second list: ";
    cin >> count;
    Tablet_list list2 = Tablet_list(count);
    cout << "Initial second list: " << endl;
    list2.output_information();
    
    // Меню
    while(true)
    {
        int list_num;
	// Вибирамо номер списку
        cout << "Select a number of list (1 or 2) or print 0 for exit: ";
        cin >> list_num;
        if(list_num == 0) break;
	// Вибираємо номер операції
        cout << "Select an operation number: (1) Add new element; (2) Output all list; (3) Output selected element; (4) Output length" << endl;
        int op_num;
        cin >> op_num;
	// Додаємо елемент до списку
        if(op_num == 1)
        {
            if(list_num == 1)
            {
                PC obj;
                list1.add(obj);
            }
            else
            {
                Tablet obj;
                list2.add(obj);
            }
            cout << "Element is added" << endl;
            continue;
        }
	// Виводимо весь список
        if(op_num == 2)
        {
            if(list_num == 1)
                list1.output_information();
            else
                list2.output_information();
            cout << endl;
            continue;
        }
	// Виводимо 1 елемент із списку
        if(op_num == 3)
        {
	    // Вибираємо індекс елемент
            cout << "Select an element (index started from 0): ";
            int index;
            cin >> index;
            string mess = "Index is out of range\n";
	    // Ігноруємо запит, якщо індекс поза межами списку
            if(index < 0)
            {
                cout << mess;
                continue;
            }
            if(list_num == 1)
            {
                if(index >= list1.list.size())
                {
                    cout << mess;
                    continue;
                }
                list1.output_selected(index);
            }
            else
            {
                if(index >= list2.list.size())
                {
                    cout << mess;
                    continue;
                }
                list2.output_selected(index);
            }
            cout << endl;
            continue;
        }
	// Виводимо довжину списку
        if(op_num == 4)
        {
            if(list_num == 1)
                list1.output_length();
            else
                list2.output_length();
        }
        
    }
    
    return 0;
}






