#include <vector>
#include <math.h>
#include <string>
#include <iostream>
#include <bits/stdc++.h>
using namespace std;

// Additional class for Point in space
class Point {    
  public:            
    vector<double> coor;
    Point(vector<double> coordinates)
    {
        coor = coordinates;
    }
    Point(int examples, bool flag)
    {
        coor.push_back(1); coor.push_back(2); coor.push_back(4);
    }
    Point(int size)
    {
        for(int i = 0; i < size; i++)
            coor.push_back(0);
    }
    Point(){}
    static Point copy(Point p) // deep copy
    {
        Point res = Point();
        for(int i = 0; i < p.coor.size(); i++)
            res.coor.push_back(p.coor[i]);
        return res;
    }
    double distance(Point p) // distance between 2 points
    {
        double s = 0;
        for(int i = 0; i < coor.size(); i++)
            s += (coor[i] - p.coor[i]) * (coor[i] - p.coor[i]);
        return sqrt(s);
    }
    Point operator +(Point t)
    {
        Point res = Point(*this);
        for(int i = 0; i < coor.size(); i++)
        {
            res.coor[i] += t.coor[i];
        }
        return res;
    }
    Point operator -(Point t)
    {
        Point res = Point(*this);
        for(int i = 0; i < coor.size(); i++)
        {
            res.coor[i] -= t.coor[i];
        }
        return res;
    }
    Point operator *(double k)
    {
        Point res = Point(*this);
        for(int i = 0; i < coor.size(); i++)
        {
            res.coor[i] *= k;
        }
        return res;
    }
    friend ostream & operator << (ostream &out, const Point &p);
};
// Override standard output
ostream & operator << (ostream &out, const Point &p)
{
    for (int i = 0; i < p.coor.size(); i++)
        out << p.coor[i] << " ";
    return out;
}

class Triangle
{
    private:
        static const int N = 3; // count of points
    public:
        Point points[N];
    #pragma region Constructors
    Triangle()
    {
        for(int i = 0; i < N ; i++)
        {
            points[i] = Point(3);
        }
    }
    Triangle(int dimension) // by default deal with R^3 space
    {
        for(int i = 0; i < N ; i++)
        {
            cout << "Input a point: ";
            vector<double> v;
            for(int j = 0; j < dimension; j++)
            {
                double a;
                cin >> a;
                v.push_back(a);
            }
            points[i] = Point(v);
        }
    }
    Triangle(double min, double max, int dimension = 3) // generate random triangle where coordinates are in [min; max]
    {
        srand(time(NULL));
        for(int i = 0; i < N ; i++)
        {
            vector<double> v;
            for(int j = 0; j < dimension; j++)
            {
                double f = (double)rand() / RAND_MAX;
                double a = min + f * (max - min);
                v.push_back(a);
            }
            points[i] = Point(v);
        }
    }
    Triangle(int example, bool flag) // generate specific examples of triangles; just for testing
    {
        if(example == 0) // for testing perimeter and area methods
        {
            vector<double> v0;
            v0.push_back(0); v0.push_back(0); v0.push_back(0);
            points[0] = Point(v0);
            vector<double> v1;
            v1.push_back(0); v1.push_back(3); v1.push_back(0);
            points[1] = Point(v1);
            vector<double> v2;
            v2.push_back(0); v2.push_back(0); v2.push_back(4);
            points[2] = Point(v2);
            // Perimeter = 12, area = 6
        }
        if(example == 1) // for testing perimeter and area methods
        {
            vector<double> v0;
            v0.push_back(1); v0.push_back(1); v0.push_back(1);
            points[0] = Point(v0);
            vector<double> v1;
            v1.push_back(1); v1.push_back(4); v1.push_back(1);
            points[1] = Point(v1);
            vector<double> v2;
            v2.push_back(1); v2.push_back(1); v2.push_back(5);
            points[2] = Point(v2);
            // Perimeter = 12, area = 6
        }
    }
    #pragma endregion Constructors
    //~Triangle() 
    //{ 
    //    delete points; 
    //} 
    #pragma region Main Methods
    vector<double> side_length() // calculate lenght of triangle sides
    {
        vector<double> sides;
        for(int i = 0; i < N - 1; i++)
        {
            sides.push_back(points[i].distance(points[i + 1]));
        }
        sides.push_back(points[N - 1].distance(points[0]));
        return sides;
    }
    void side_length_print() // just for testing
    {
        cout << "Sides lenght: ";
        vector<double> sides = side_length();
        for(int i =0; i< sides.size(); i++)
            cout << sides[i] << " ";
    }
    double perimeter() // calculate perimeter of triangle
    {
        vector<double> sides = side_length();
        double s = 0;
        for(int i =0; i< sides.size(); i++)
            s += sides[i];
        return s;
    }
    double area() // calculate area of triangle
    {
        vector<double> sides = side_length();
        double a = sides[0], b = sides[1], c = sides[2];
        double p = (a + b + c) / 2;
        return sqrt(p * (p-a) * (p -b) * (p-c));
    }
    #pragma endregion Main Methods
    #pragma region Override Methods
    friend ostream & operator << (ostream &out, const Triangle &t);

    static Triangle copy(Triangle t) // deep copy
    {
        Triangle res = Triangle();
        for(int i = 0; i < N; i++)
            res.points[i] = Point::copy(t.points[i]);
        return res;
    }
    Triangle operator ++ (int) // Postfix ++ - increase y coordinate
    {
        Triangle res = Triangle();
        for(int i = 0; i < N; i++)
            res.points[i].coor[1] = points[i].coor[1]++;
        return res;
    }
    Triangle operator ++ () // Prefix ++ - increase x coordinate
    {
        Triangle res = Triangle();
        for(int i = 0; i < N; i++)
            res.points[i].coor[0] = ++points[i].coor[0];
        return res;
    }
    Triangle operator -- (int) // Postfix -- - decrease y coordinate
    {
        Triangle res = Triangle();
        for(int i = 0; i < N; i++)
            res.points[i].coor[1] = points[i].coor[1]--;
        return res;
    }
    Triangle operator -- () // Prefix -- - decrease x coordinate
    {
        Triangle res = Triangle();
        for(int i = 0; i < N; i++)
            res.points[i].coor[0] = --points[i].coor[0];
        return res;
    }
    bool operator ==(Triangle& t)
    {
        vector<double> sides = (*this).side_length();
        sort(sides.begin(), sides.end(), greater<int>()); 
        vector<double> sides_t = t.side_length();
        sort(sides_t.begin(), sides_t.end(), greater<int>());
        bool isEqual = true;
        double eps = 0.00001;
        for(int i = 0; i < sides.size(); i++)
        {
            if (abs(sides[i] - sides_t[i]) > eps)
            {
                isEqual = false; break;
            }
        }
        return isEqual;
    }
    Triangle operator +(Triangle t)
    {
        Triangle res = Triangle(*this);
        double area = res.area();
        double res_area = area + t.area();
        double k = sqrt(res_area / area);
        res.points[1] = (res.points[1] - res.points[0]) * k + res.points[0];
        res.points[2] = (res.points[2] - res.points[0]) * k + res.points[0];
        return res;
    }
    Triangle operator -(Triangle t)
    {
        Triangle res = Triangle(*this);
        double area = res.area();
        double res_area = abs(area - t.area());
        double k = sqrt(res_area / area);
        res.points[1] = (res.points[1] - res.points[0]) * k + res.points[0];
        res.points[2] = (res.points[2] - res.points[0]) * k + res.points[0];
        return res;
    }
    
    #pragma endregion Override Methods

};
// Override standard output
ostream & operator << (ostream &out, const Triangle &t)
{
    out << "Triangle: " << endl;
    for (int i = 0; i < t.N; i++)
        out << "Point[" << i << "]: " << t.points[i] << endl;
    return out;
}

int main()
{
    cout << "Starting ..." << endl;
    Triangle t1 = Triangle(0, true);
    cout << t1;
    t1.side_length_print(); cout << endl;
    cout << "Perimeter: " << t1.perimeter() << endl;
    cout << "Area: " << t1.area() << endl;
    t1++;
    cout << "Postfix increase (increase y): \n" << t1;
    ++t1;
    cout << "Prefix increase (increase x): \n" << t1;
    t1--;
    cout << "Postfix decrease (decrease y): \n" << t1;
    --t1;
    cout << "Prefix decrease (decrease x): \n" << t1;

    cout << "Comparison: " << endl;
    Triangle t2 = Triangle(1, true);
    cout << t2;
    cout << t1;
    cout << ((t1 == t2) ? "Triangles are equal" : "Triangles are not equal") << endl;
    cout << "Comparison 2: " << endl;
    Triangle t3 = Triangle(1, true);
    t3.points[0].coor[1]++;
    cout << t3;
    cout << t1;
    cout << ((t1 == t3) ? "Triangles are equal" : "Triangles are not equal") << endl;

    cout << "Addition: " << endl;
    Triangle t4 = Triangle(1, true);
    cout << t1;
    cout << t4;
    Triangle t5 = t4 + t1;
    cout << "Result: " << endl;
    cout << t5;
    cout << "Check the answer: ";
    cout << "Result area: " << t5.area() << " Initial areas: " << t4.area() << " and " << t1.area() << endl;

    cout << "Subtraction: " << endl;
    cout << t1;
    cout << t5;
    Triangle t6 = t5 - t1;
    cout << "Result: " << endl;
    cout << t6;
    cout << "Check the answer: ";
    cout << "Result area: " << t6.area() << " Initial areas: " << t5.area() << " and " << t1.area() << endl;

    cout << "Done ..." << endl;
}
