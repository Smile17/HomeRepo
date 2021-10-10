#include <iostream>

using namespace std;

// Швидке сортування за спаданням
void QuickSort (int arr[], /* масив для сортування */
		unsigned int N, /* розмір масиву */
		int L, /* ліва межа сортування */ 
		int R) /* права межа сортування */
{
    int iter = L, jter = R ;
    
    int middle = (R+L)/2 ;
    
    int x = arr[middle] ;
    int w ;
    
    do
    {
        while (arr[iter]>x)
            iter++;
 
        while (x>arr[jter])
            jter--;
 
        if (iter<=jter) 
        {
           w = arr[iter]; 
           arr[iter] = arr[jter];
           arr[jter] = w;
           
           iter++;
           jter--;
        }
    }
    while (iter<jter);
    
    if (L<jter) 
        QuickSort(arr, N, L, jter);
    
    if (iter<R)
        QuickSort(arr, N, iter, R);
}
void print(int arr[], /* масив для сортування */
		unsigned int N /* розмір масиву */)
{
    for (int i=0; i<N; i++)
        cout << arr[i] << " ";
    cout << endl;
}

// Сортування вибором за зростанням
void InsertionSort (int arr[], /* масив для сортування */
		unsigned int N) /* розмір масиву */
{
    for (int iter=0; iter<N-1; iter++)
    {
        int kter = iter; 
        int x = arr[iter];
        
        for (int jter=iter+1; jter<N; jter++)
        {
            if (arr[jter] < x) // перевіряємо, чи значення елементу менше 
            {
                kter = jter; // зберігаємо індекс найменшого елементу 
                x = arr[kter]; // зберігаємо його значення 
            }
        }
        // обмінюємо значення найменшого елементу масиву з поточним 
        arr[kter] = arr[iter]; arr[iter] = x;
    }
}

int main()
{
    cout<<"Швидке сортування\n";
    int arr[] = {22, 6, 2, 34, 4, 45, 15, 35};
    int N = sizeof (arr) / sizeof (arr[0]); // дізнаємося довжину масиву 
    print(arr, N); // виводимо масив на екран
    QuickSort (arr, N, 0, N-1);
    print(arr, N); // виводимо масив на екран
    
    cout<<"Cортування вставками\n";
    int arr2[] = {35, 6, 19, 13, 39, 47, 32, -1};
    N = sizeof (arr2) / sizeof (arr2[0]); // дізнаємося довжину масиву 
    print(arr2, N); // виводимо масив на екран
    InsertionSort(arr2, N);
    print(arr2, N); // виводимо масив на екран

    return 0;
}

