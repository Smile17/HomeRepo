#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <string>
#include <iostream>
using namespace std;
struct PersonPhone {
    string surname;
    int year;
    string phone;
};
void generate()
{
    PersonPhone* objs = new PersonPhone[3];
    PersonPhone* obj = &objs[0];
    obj->surname = "Vasylkov";
    obj->year = 2000;
    obj->phone = "380213523452";
    obj = &objs[1];
    obj->surname = "Pertov";
    obj->year = 2005;
    obj->phone = "380359859435";
    obj = &objs[2];
    obj->surname = "Kolesnik";
    obj->year = 2002;
    obj->phone = "380324534";
    FILE* pFile;
    fopen_s(&pFile, "data.bin", "w+");
    fwrite(objs, sizeof(PersonPhone), sizeof(objs), pFile);
    fclose(pFile);
}
int main()
{
    generate();
    FILE* pFile;
    fopen_s(&pFile, "data.bin", "r");

    if (pFile == NULL) perror("Error opening file");
    int n = 3;
    PersonPhone* objs = new PersonPhone[n];
    fread(objs, sizeof(PersonPhone), sizeof(objs), pFile);
    cout << "Enter year:";
    int year;
    cin >> year;
    for (int i = 0; i < n; i++)
    {
        if(objs[i].year > year)
            cout << objs[i].surname << "\t" << objs[i].year << "\t" << objs[i].phone << endl;
    }
    
    return 0;
}
