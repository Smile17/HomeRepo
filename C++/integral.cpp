#include <iostream>

using namespace std;

double calculateIntegral(double a, double b, int n, double (*f)(double)) 
{
    const double width = (b-a)/n;

    double res = 0;
    for(int step = 0; step < n; step++) {
        const double x1 = a + step*width;
        const double x2 = a + (step+1)*width;

        res += 0.5*(x2-x1)*(f(x1) + f(x2));
    }

    return res;
}
double calculateIntegral(double a, double b, double eps, double (*f)(double), int step = 10) 
{
    int n = 10;
    double tmp = calculateIntegral(a, b, n, f);
    while(true)
    {
        n += step;
        double res = calculateIntegral(a, b, n, f);
        if(abs(res - tmp) < eps)
        {
            cout << "Final steps: " << n << " Final result: " << res << endl;
            return res;
        }
        cout << "Steps: " << n << " Result: " << res << endl;
        tmp = res;
    }
}
double func(double x)
{
    return x * x;
}

int main()
{
    double a = 0;
    double b = 2;
    double eps = 0.001;
    double res = calculateIntegral(a, b, eps, *func);
    cout << res;

    return 0;
}
