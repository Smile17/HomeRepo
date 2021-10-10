/******************************************************************************

                              Online C++ Compiler.
               Code, Compile, Run and Debug C++ program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <iostream>
#include <string>

using namespace std;

int MAX = 9;  // максимальна кількість розрядів

void process(string num)
{
    if(num.length() < 3) // якщо число закоротке, то неопрацьовуємо його
    {
        cout << "Input number is too short";
        return;
    }
    if(num.length() > MAX)  // якщо число задовге
    { 
        cout << "Input number is too long";
        return;
    }
    // назви цифр
    string number_names[10] = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
    for(int i = 0; i < num.length(); i++)
    {
        int digit = (int)num[i] - 48; // перевередння символа у число
        cout << number_names[digit] << " ";
    }
}

int main()
{
    string num;
    while (true)
    {
        cout << "Input a number: ";
        cin >> num;
        if (num == "q") break; // зупиняємо роботу, якщо було введено символ q
        try
        {
            int num_int = stoi(num); // переводимо рядок в число, щоб перевірити що було введено насправді число
            num = to_string(num_int); // переводимо число назад у рядок, адже ми могли ввести символи, які починаються з цифр, тоді будуть опрацьовані лише вони
            process(num); // обробляємо число
        }
        catch(invalid_argument& e)
        {
            cout << "Input only a number"; // якщо користувач ввів нечислове значення, то повідомимо його про це
        }
        cout << endl;
    }
    return 0;
}




