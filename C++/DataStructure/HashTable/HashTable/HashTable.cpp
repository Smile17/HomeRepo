#include <iostream>
#include <fstream>
#include <functional>
#include <vector>
#include <utility>


using namespace std;
int hash1(int x, int M) // M should be prime
{
    return x % M;
}

std::function<int(int)> hashByDivision(int M)
{
    return [=](int x) {
        return x % M;
    };
}
int hash2(int x, double C, int M) // C in [0,1]
{
    //int M = 13; double C = 0.618033;
    int h = M * fmod(x * C, 1);
    return h;
}
std::function<int(int)> hashByMultiplication(double C, int M)
{
    return [=](int x) {
        return int(M * fmod(x * C, 1));
    };
}
std::function<int(int)> hashByMultiplicationMethod(int M)
{
    double C = 0.69;
    return [=](int x) {
        return int(M * fmod(x * C, 1));
    };
}
int getIndex(vector<int> v, int x)
{
    auto it = find(v.begin(), v.end(), x);
    if (it != v.end())
        return (it - v.begin());
    else
        return -1;
}
class HashTable {
public:
    int hash_size;
    vector<int>* hash_table; // array with pointers to list of elements
    int H; // max length in list
    function<int(int)> hash; // hash function
    function<function<int(int)>(int)> hash_method; // hash function generator
    HashTable(int _hash_size, int _H, function<int(int)> _hash, function<function<int(int)>(int)> _hash_method) {
        hash_size = _hash_size;
        H = _H;
        hash_table = new vector<int>[hash_size];
        hash = _hash;
        hash_method = _hash_method;
    }

    void add(int x)
    {
        int key = hash(x);
        hash_table[key].push_back(x);
        if (hash_table[key].size() > H)
        {
            rebuild();
        }
    }
    void rebuild()
    {
        cout << hash_size << " " << keys_with_elements_frequency() << endl;
        int M = hash_size * 2 + 1;
        hash = hash_method(M); // generate new hash function
        vector<int>* h;
        h = new vector<int>[M];
        for (int i = 0; i < hash_size; i++)
        {
            vector<int> v = hash_table[i];
            for (int j = 0; j < v.size(); j++)
            {
                int key = hash(v[j]);
                h[key].push_back(v[j]);
            }
        }
        hash_table = h;
        hash_size = M;
    }
    int find(int x) // return idx in relative list
    {
        int key = hash(x);
        vector<int> v = hash_table[key];
        return getIndex(v, x);
    }
    void remove(int x) // remove first appearance of x
    {
        int key = hash(x);
        vector<int>* v = &hash_table[key];
        (*v).erase((*v).begin() + getIndex(*v, x));
    }
    void print_full()
    {
        for (int i = 0; i < hash_size; i++)
        {
            vector<int> v = hash_table[i];
            cout << "Key:" << i << "\t";
            for (int j = 0; j < v.size(); j++)
                cout << v[j] << " ";
            cout << endl;
        }
    }
    void print()
    {
        cout << "Size:" << hash_size << endl;
        // Print only lists with values
        for (int i = 0; i < hash_size; i++)
        {
            vector<int> v = hash_table[i];

            if (v.size() > 0)
            {
                cout << "Key:" << i << "\t";
                for (int j = 0; j < v.size(); j++)
                    cout << v[j] << " ";
                cout << endl;
            }
        }
    }
    // functions for analysis
    int keys_with_elements_count() // used keys count
    {
        int res = 0;
        for (int i = 0; i < hash_size; i++)
        {
            vector<int> v = hash_table[i];
            if(v.size() > 0)
                res ++;
        }
        return res;
    }
    double keys_with_elements_frequency()
    {
        return (1.0 * keys_with_elements_count()) / hash_size;
    }
    double average_list_length()
    {
        int res = 0;
        for (int i = 0; i < hash_size; i++)
        {
            vector<int> v = hash_table[i];
                res += v.size();
        }
        return (1.0 * res) / hash_size;
    }

};
void demo(function<int(int)> h, function<function<int(int)>(int)> hash_method)
{
    //cout << "Hash Table demonstation: hash by Division" << endl;
    //function<int(int)> h = hashByDivision(23);
    HashTable hash = HashTable(23, 3, h, hash_method);
    // Add some elements
    hash.add(27);
    hash.add(73);
    hash.add(4);
    hash.add(64);
    cout << "HashTable after adding 27, 73, 4, 64" << endl;
    hash.print();
    cout << endl;
    cout << "Check whether 27 is present in hash table" << endl;
    if (hash.find(27) != -1)
        cout << "27 is found" << endl;
    hash.remove(27);
    hash.remove(64);
    cout << "HashTable after removing 27, 64" << endl;
    hash.print();
    cout << endl;
    hash.add(50);
    hash.add(96);
    cout << "HashTable after adding 50, 96. There might be hash rebuilding" << endl;
    hash.print();
    cout << endl;
}
vector<int> generate(int N)
{
    vector<int> res;
    for (int i = 0; i < N; i++)
        res.push_back(rand());
    return res;
}
vector<vector<double>> demo1(vector<int> v, int N, function<int(int)> h, function<function<int(int)>(int)> hash_method)
{
    HashTable hash = HashTable(23, 15, h, hash_method);
    vector<double> x, y, size;
    for (int i = 0; i < N; i++)
    {
        hash.add(v[i]);
        if (i % 20 == 0)
        {
            x.push_back(hash.keys_with_elements_frequency());
            y.push_back(hash.average_list_length());
            size.push_back(hash.hash_size);
        }
    }
    vector<vector<double>> res;
    res.push_back(x); res.push_back(y); res.push_back(size);
    return res;
}
void demo2()
{
    srand(static_cast<unsigned int>(time(0)));
    function<int(int)> h1 = hashByDivision(23);
    function<int(int)> h2 = hashByMultiplication(0.69, 23);
    vector<double> x1, y1, x2, y2, s1, s2;
    vector<vector<double>> res;
    int N = 1000;
    for (int i = 0; i < 3; i++)
    {
        vector<int> v = generate(N);
        cout << "Hash by division" << endl;
        res = demo1(v, N, h1, hashByDivision);
        x1.insert(x1.end(), res[0].begin(), res[0].end());
        y1.insert(y1.end(), res[1].begin(), res[1].end());
        s1.insert(s1.end(), res[2].begin(), res[2].end());
        cout << "Hash by multiplication" << endl;
        res = demo1(v, N, h2, hashByMultiplicationMethod);
        x2.insert(x2.end(), res[0].begin(), res[0].end());
        y2.insert(y2.end(), res[1].begin(), res[1].end());
        s2.insert(s2.end(), res[2].begin(), res[2].end());
    }
    ofstream f1("div.txt");
    for(int i = 0; i < x1.size(); i++)
        f1 << x1[i] << "," << y1[i] << "," << s1[i] << endl;
    f1.close();
    ofstream f2("mul.txt");
    for (int i = 0; i < x1.size(); i++)
        f2 << x2[i] << "," << y2[i] << "," << s2[i] << endl;
    f2.close();
}
int main()
{
    cout << "Hash Table demonstation: hash by Division" << endl;
    function<int(int)> h = hashByDivision(23);
    demo(h, hashByDivision);
    cout << "Hash Table demonstation: hash by Multiplication" << endl;
    function<int(int)> h2 = hashByMultiplication(0.69, 23);
    demo(h2, hashByMultiplicationMethod);
    demo2();
    cout << "Done...";
    return 0;
}
