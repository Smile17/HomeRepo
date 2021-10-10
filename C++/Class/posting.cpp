#include <iostream>

using namespace std;

// Поштове відправлення
class Posting {
    protected:
        double double_rand(double fMin, double fMax) // генеруємо випадкове число типу double
        {
            double f = (double)rand() / RAND_MAX;
            return fMin + f * (fMax - fMin);
        }
    private:
        string address;
        string date;
        double price;
    public:
        // Конструктор по замовчуванню
        Posting() 
        {
            cout << "Creation of Posting object\n";
            address = "Address";
            date = "05/07/2020";
            price = double_rand(1, 10);
        }
        // Конструктор з параметрами
        Posting(string _address, string _date, double _price)
        {
            cout << "Creation of Posting object\n";
            address = _address; date = _date; price = _price;
        }
        // Конструктор копіювання
        Posting(const Posting &p) 
        {
            cout << "Creation of Posting object\n";
            address = p.address; date = p.date; price = p.price;
        }
        
        // Деструктор
        ~Posting()
        {
            cout << "Deletion of Posting object: ";
            outputPosting();
            cout << "\n";
        }
        
        // Get / set
        string GetAddress(){return address;}
        Posting* SetAddress(string value){address = value; return this;}
        string GetDate(){return date;}
        Posting* SetDate(string value){date = value; return this;}
        double GetPrice(){return price;}
        Posting* SetAddress(double value){price = value; return this;}
        
        void output() // виводимо дані про обєкт
    	{
    		cout << "Address: "<< address;
    		cout << "; Date: "<< date;
    		cout << "; Price: "<< price;
    	}
    	void outputPosting()
    	{
    	    cout << "Address: " << address;
    	}
};
class RecommenedLetter : public Posting {
    private:
        string delivery_date;
    	string postman_surname;
    public:
        // Конструктор по замовчуванню
        RecommenedLetter() : Posting()
        {
            cout << "Creation of RecommenedLetter object\n";
            delivery_date = "06/07/2020";
            postman_surname = "Surname ";
        }
        // Конструктор з параметрами
        RecommenedLetter(string _address, string _date, 
        double _price, string _delivery_date, string _postman_surname) : Posting(_address, _date, _price)
        {
            cout << "Creation of RecommenedLetter object\n";
            delivery_date = _delivery_date; postman_surname = _postman_surname;
        }
        // Конструктор копіювання
        RecommenedLetter(const RecommenedLetter &p) : Posting(p)
        {
            cout << "Creation of RecommenedLetter object\n";
            delivery_date = p.delivery_date; postman_surname = p.postman_surname;
        }
        // Деструктор
        ~RecommenedLetter()
        {
            cout << "Deletion of RecommenedLetter: ";
            outputRecommenedLetter();
            cout << "\n";
        }
        void output() // виводимо дані про обєкт
    	{
    		Posting::output();
    		cout << "; Delivery Date: " << delivery_date;
    		cout << "; Postman Surname: " << postman_surname;
    	}
    	void outputRecommenedLetter()
    	{
    		cout << "Postman Surname: " << postman_surname;
    	}

};

class Parcel : public Posting {
    private:
        double weight;
        double width;
        double heigth;
        double length;
    public:
        // Конструктор по замовчуванню
        Parcel() : Posting()
        {
            cout << "Creation of Parcel object\n";
            weight = double_rand(1, 10);
            width = double_rand(1, 10);
            heigth = double_rand(1, 10);
            length = double_rand(1, 10);
        }
        // Конструктор з параметрами
        Parcel(string _address, string _date, double _price, 
        double _weight, double _width, double _heigth, double _length) : Posting(_address, _date, _price)
        {
            cout << "Creation of Parcel object\n";
            weight = _weight; width = _width; heigth = _heigth; length = _length;
        }
        // Конструктор копіювання
        Parcel(const Parcel &p) : Posting(p)
        {
            cout << "Creation of Parcel object\n";
            weight = p.weight; width = p.width; heigth = p.heigth; length = p.length;
        }
        // Деструктор
        ~Parcel()
        {
            cout << "Deletion of Parcel: ";
            outputParcel();
            cout << "\n";
        }
        void output() // виводимо дані про обєкт
    	{
    		Posting::output();
    		cout << "; Weight: " << weight;
    		cout << "; Width: " << width;
    		cout << "; Heigth: " << heigth;
    		cout << "; Length: " << length;
    	}
    	void outputParcel()
    	{
    		cout << "Weight: " << weight;
    	}

};

int main()
{
    // Task 1
    cout << "RecommenedLetter: ";
    cout << "\n----------------------------------------\n";
    RecommenedLetter r1;
    r1.output();
    cout << "\n----------------\n";
    RecommenedLetter r2 = RecommenedLetter("Address 2", "24/12/2020", 3.14, "25/12/2020", "Postman Surname");
    r2.output();
    cout << "\n----------------\n";
    RecommenedLetter r3 = RecommenedLetter(r2);
    r3.output();
    cout << "\n----------------\n";
    
    cout << "Parcel: ";
    cout << "\n----------------------------------------\n";
    Parcel p1;
    p1.output();
    cout << "\n----------------\n";
    Parcel p2 = Parcel("Address 3", "27/12/2020", 3.57, 5.47, 2, 3, 4);
    p2.output();
    cout << "\n----------------\n";
    Parcel p3 = Parcel(p2);
    p3.output();
    cout << "\n----------------\n";
    

    return 0;
}

