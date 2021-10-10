#include <iostream>

using namespace std;

class B
{
    int b;
    public:
        B(){}
        B(int _b){b = _b;}
        void output() // виводимо дані про обєкт
    	{
    		cout << "B: "<< b;
    	}
};
class D1 : public B
{
    int d1;
    public:
        D1(){}
        D1(int _b, int _d1):B(_b){d1 = _d1;}
        void output() // виводимо дані про обєкт
    	{
    	    B::output();
    		cout << "; D1: "<< d1;
    	}
};
class D2 : private B
{
    int d2;
    public:
        D2(){}
        D2(int _b, int _d2):B(_b){d2 = _d2;}
        void output() // виводимо дані про обєкт
    	{
    	    B::output();
    		cout << "; D2: "<< d2;
    	}
};
class D3 : private B
{
    int d3;
    public:
        D3(){}
        D3(int _b, int _d3):B(_b){d3 = _d3;}
        void output() // виводимо дані про обєкт
    	{
    	    B::output();
    		cout << "; D3: "<< d3;
    	}
};
class D4 : private D1, public D2
{
    int d4;
    public:
        D4(int _b1, int _d1, int _b2, int _d2, int _d4):D1(_b1, _d1), D2(_b2, _d2){d4 = _d4;}
        void output() // виводимо дані про обєкт
    	{
    	    D1::output(); cout << "; "; D2::output();
    		cout << "; D4: "<< d4;
    	}
};
class D5 : private D3, public D2
{
    int d5;
    public:
        D5(int _b3, int _d3, int _b2, int _d2, int _d5):D3(_b3, _d3), D2(_b2, _d2){d5 = _d5;}
        void output() // виводимо дані про обєкт
    	{
    	    D3::output(); cout << "; "; D2::output();
    		cout << "; D5: "<< d5;
    	}
};
int main()
{
    cout << "D4: \n";
    D4 d4 = D4(1, 2, 3, 4, 5);
    d4.output();
    
    cout << "\nD5: \n";
    D5 d5 = D5(10, 20, 30, 40, 50);
    d5.output();
    

    return 0;
}

