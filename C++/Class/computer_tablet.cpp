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
        double double_rand(double fMin, double fMax) // генеруємо випадкове число типу double
        {
            double f = (double)rand() / RAND_MAX;
            return fMin + f * (fMax - fMin);
        }
    public:
        int id;
        string name;
    	int RAM;
    	double proc_speed;
    	string model;
    protected:
        Computer() 
        {
            id = generateID();
            name = "Device_" + to_string(id);
            RAM = rand() % 32 + 1; // генеруємо випадкове ціле число
            proc_speed = double_rand(1, 10);
            model = "";
        }
    	virtual void output() // виводимо дані про гаджет 
    	{
    		cout << "Name: "<< name;
    		cout << "\nRAM: "<< RAM;
    		cout << "\nProcessor Speed: "<< proc_speed;
    		cout << "\nModel: " << model;
    	}
    	virtual void output_unique_info() = 0;

	
};

string PC_OS_names[3] = {"Windows", "Mac", "Ubuntu"}; // доступні назви операційних систем для комп'ютерів
string PC_model_names[4] = {"ASUS", "Dell", "Lenovo", "HP"};
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
            size = sizeof(PC_model_names)/sizeof(PC_model_names[0]);
            model = PC_model_names[rand() % size];
            has_video_card = bool_arr[rand() % 2];
            USB = rand() % 10;
        }
        
        void output() // виводимо дані про комп'ютер
    	{
    		Computer::output();
    		cout << "\nOS:" << OS;
    		cout << "\nWith video card:" << has_video_card;
    		cout << "\nUSB:" << USB;
    	}
    	void output_unique_info() // виводимо унікальні дані про комп'ютер
    	{
    		cout << "OS:" << OS;
    		cout << "\nWith video card:" << has_video_card;
    		cout << "\nUSB:" << USB;
    	}

};    	
string Netbook_OS_names[2] = {"Win", "Ubuntu Netbook Remix"}; // доступні назви операційних систем для планшетів
string Netbook_model_names[3] = {"ASUS", "Dell Mini", "Acer"};
class Netbook : Computer {
    public:
        string OS;
        double diagonal_length;
    	int resolution;
    	
    	Netbook() : Computer()
        {
            int size = sizeof(Netbook_OS_names)/sizeof(Netbook_OS_names[0]);
            OS = Netbook_OS_names[rand() % size]; // вибираємо випадкове значення операційної системи
            size = sizeof(Netbook_model_names)/sizeof(Netbook_model_names[0]);
            model = Netbook_model_names[rand() % size];
            diagonal_length = double_rand(5, 10);
            resolution = rand() % 100;
        }
        
        void output() // виводимо дані про планшет
    	{
    		Computer::output();
    		cout << "\nOS:" << OS;
    		cout << "\nDiagonal length:" << diagonal_length;
    		cout << "\nResolution:" << resolution;
    	}
    	void output_unique_info() // виводимо унікальні дані про планшет
    	{
    		cout << "OS:" << OS;
    		cout << "\nDiagonal length:" << diagonal_length;
    		cout << "\nResolution:" << resolution;
    	}
};

string Tablet_OS_names[3] = {"Win", "Android", "iOS"}; // доступні назви операційних систем для планшетів
string Tablet_model_names[3] = {"Apple", "Samsung", "Lenovo"};
class Tablet : Computer {
    public:
        string OS;
        double diagonal_length;
    	int resolution;
    	
    	Tablet() : Computer()
        {
            int size = sizeof(Tablet_OS_names)/sizeof(Tablet_OS_names[0]);
            OS = Tablet_OS_names[rand() % size]; // вибираємо випадкове значення операційної системи
            size = sizeof(Tablet_model_names)/sizeof(Tablet_model_names[0]);
            model = Tablet_model_names[rand() % size];
            diagonal_length = double_rand(5, 20);
            resolution = rand() % 200;
        }
        
        void output() // виводимо дані про планшет
    	{
    		Computer::output();
    		cout << "\nOS:" << OS;
    		cout << "\nDiagonal length:" << diagonal_length;
    		cout << "\nResolution:" << resolution;
    	}
    	void output_unique_info() // виводимо унікальні дані про планшет
    	{
    		cout << "OS:" << OS;
    		cout << "\nDiagonal length:" << diagonal_length;
    		cout << "\nResolution:" << resolution;
    	}
};

int main()
{
    srand (time(NULL)); // для генерації випадкових чисел
    PC pc1;
    cout << "Virtual function testing: \n";
    pc1.output();
    cout << endl << endl;
    cout << "Pure virtual function testing: \n";
    pc1.output_unique_info();
    cout << endl << endl;
    
    
    Tablet t1;
    cout << "Virtual function testing: \n";
    t1.output();
    cout << endl << endl;
    cout << "Pure virtual function testing: \n";
    t1.output_unique_info();
    cout << endl << endl;
    
    
    Netbook n1;
    cout << "Virtual function testing: \n";
    n1.output();
    cout << endl << endl;
    cout << "Pure virtual function testing: \n";
    n1.output_unique_info();
    cout << endl << endl;
    
    return 0;
}






