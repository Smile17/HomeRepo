/******************************************************************************

                              Online C++ Compiler.
               Code, Compile, Run and Debug C++ program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <iostream>
#include <string>

using namespace std;
int MIN_CAPACITY = 8;
int GROWTH_FACTOR = 2;

template <typename T> 
class Array { 
private: 
    T *m_data; 
    int m_size;
    int m_capacity;
public:
    Array();
    ~Array();
    void print();
    void resize();
    void push(T value);
    void add(T value);
    void add2(T value);
    T pop();
    T get(size_t index);
    void removeat(size_t index);
};

template <typename T>
Array<T>::Array() {
    m_capacity = MIN_CAPACITY;
    m_size = 0;
    m_data = (T*)malloc(m_capacity * sizeof(*m_data));
    if (!m_data)
        throw std::bad_alloc();
}
  
template <class T>
Array<T>::~Array() {
    free(m_data);
}
  
template <typename T> 
void Array<T>::print() { 
    for (int i = 0; i < m_size; i++) 
        cout<<" "<<*(m_data + i); 
    cout<<endl; 
}

template <typename T>
void Array<T>::resize() {
    size_t capacity = m_capacity*GROWTH_FACTOR;
    T *tmp = (T*)realloc(m_data, capacity * sizeof(*m_data));
    if (!tmp)
        throw std::bad_alloc();
    m_data = tmp;
    m_capacity = capacity;
}

template <typename T>
void Array<T>::push(T value) {
    if (m_size >= m_capacity)
        resize();
    *(m_data + m_size++) = value;
}

template <typename T>
int binarySearch(T *arr, int l, int r, T x) 
{
    if (*(arr + r) > x) return r;
    while (l <= r) {
        int m = l + (r - l) / 2; 
        // Check if x is present at mid 
        if (*(arr + m) == x) 
            return m; 
        // If x greater, ignore left half 
        if (*(arr + m) > x) 
            l = m + 1; 
        // If x is smaller, ignore right half 
        else
            r = m - 1; 
    }
    return l;
} 

template <typename T>
void Array<T>::add(T value) {
    if (m_size >= m_capacity)
        resize();
    if (m_size == 0)
    {
        *(m_data + m_size++) = value;
        return;
    }
    m_size++;
    //int i = 0;
    int i = binarySearch(m_data, 0, m_size - 1, value);
    //while((i < m_size) && (*(m_data + i) > value))
    //    i++;
    int j = m_size;
    while (j > i)
    {
        *(m_data + j) = *(m_data + j - 1);
        j--;
    }
    *(m_data + i) = value;
}

template <typename T>
T Array<T>::pop() {
    return *(m_data + --m_size);
}


template <typename T>
T Array<T>::get(size_t index) {
    return *(m_data + index);
}

template <typename T>
void Array<T>::removeat(size_t index) {
    size_t i = index;
    while (i < m_size)
    {
        *(m_data + i) = *(m_data + i + 1);
        i++;
    }
    m_size--;
}


void integer_demo()
{
    // Demonstration with integer
    Array<int> arr;
    for (int i=0; i<10; i++)
        arr.add(10-i);

    arr.print();
    cout << "Popping: " << arr.pop() << endl;
    arr.print();
    cout << "Remove at 4: " << endl;
    arr.removeat(4);
    arr.print();
    int res = arr.get(3);
    cout << "Element at #3: " << res << endl;
    arr.print();
    cout << "Add new element equal 6: " << endl;
    arr.add(6);
    arr.print();
    cout << "Add new element equal -6: " << endl;
    arr.add(-6);
    arr.print();
}
double frand(double fMin, double fMax)
{
    double f = (double)rand() / RAND_MAX;
    return fMin + f * (fMax - fMin);
}
void double_demo()
{
    // Demonstration with double
    Array<double> arr;
    for (int i=0; i<10; i++)
        arr.add(frand(0, 10));

    arr.print();
    cout << "Popping: " << arr.pop() << endl;
    arr.print();
    cout << "Remove at 4: " << endl;
    arr.removeat(4);
    arr.print();
    double res = arr.get(3);
    cout << "Element at #3: " << res << endl;
    arr.print();
    cout << "Add new element equal 3.14: " << endl;
    arr.add(3.14);
    arr.print();
    cout << "Add new element equal -4.32: " << endl;
    arr.add(-4.32);
    arr.print();
}

int main()
{
    cout << "Integer DEMO: " << endl;
    integer_demo();
    cout << "-----------------------------" << endl;
    cout << "Double DEMO: " << endl;
    double_demo();

    return 0;
}

