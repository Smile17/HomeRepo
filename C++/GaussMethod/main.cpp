#include <iostream>
#include <fstream>
#include <vector>
#include <map>
#include <string>
#include <iterator>
#include <iomanip>
#include <cmath>
#include <algorithm> 

using namespace std;

map<string, vector<double>> readFile(string inputFile) {
    map<string, vector<double>> map;
    string text;
    ifstream file(inputFile);
    int substanceNumber = 0;
    int sign = 1;
    try {
        while (getline (file, text)) 
        {
            if (text == "")
            {
                sign = -1;
                continue;
            }
            istringstream iss(text);
            vector<string> words{
                istream_iterator<string>(iss), {}
            };
            if (words.size() % 2 != 0)
            {
                cout << "Wrong number of arguments in line " << text;
                map.clear();
                return map;
            }
            for(int i = 0; i < words.size() / 2; i++)
            {
                if (map.count(words[2 * i]) > 0) // key is found
                {
                    for(int j = map[words[2 * i]].size(); j < substanceNumber; j++)
                        map[words[2 * i]].push_back(0);
                    map[words[2 * i]].push_back(sign * stod(words[2 * i + 1]));
                }
                else
                {
                    vector<double> coof;
                    for(int j = 0; j < substanceNumber; j++) // it is a new element that did not appear before
                        coof.push_back(0);
                    coof.push_back(sign * stod(words[2 * i + 1]));
                    map.insert(make_pair(words[2 * i], coof));
                }
            }
            substanceNumber++;
        }
        for (auto const& pair: map) {
            for(int j = map[pair.first].size(); j < substanceNumber; j++)
            {
                map[pair.first].push_back(0);
            }
        }
    }
    catch (invalid_argument e)
    {
        cout << "Wrong format of number argument in line " << text << endl;
        map.clear();
    }
    file.close();
    return map;
}

const double EPS = 1e-9;
const int INF = 2; // it doesn't actually have to be infinity or a big number

void print(vector< vector<double> > A) {
    int n = A.size();
    int m = (int) A[0].size();
    for (int i=0; i<n; i++) {
        for (int j=0; j<m; j++) {
            cout << A[i][j] << "\t";
            if (j == m-2) {
                cout << "| ";
            }
        }
        cout << "\n";
    }
    cout << endl;
}

int gauss (vector < vector<double> > a, vector<double> & ans) {
    int n = (int) a.size();
    int m = (int) a[0].size() - 1;

    vector<int> where (m, -1);
    for (int col=0, row=0; col<m && row<n; ++col) {
        int sel = row;
        for (int i=row; i<n; ++i)
            if (abs (a[i][col]) > abs (a[sel][col]))
                sel = i;
        if (abs (a[sel][col]) < EPS)
            continue;
        for (int i=col; i<=m; ++i)
            swap (a[sel][i], a[row][i]);
        where[col] = row;

        for (int i=0; i<n; ++i)
            if (i != row) {
                double c = a[i][col] / a[row][col];
                for (int j=col; j<=m; ++j)
                    a[i][j] -= a[row][j] * c;
            }
        ++row;
    }

    ans.assign (m, 0);
    for (int i=0; i<m; ++i)
        if (where[i] != -1)
            ans[i] = a[where[i]][m] / a[where[i]][i];
    for (int i=0; i<n; ++i) {
        double sum = 0;
        for (int j=0; j<m; ++j)
            sum += ans[j] * a[i][j];
        if (abs (sum - a[i][m]) > EPS)
            return 0;
    }

    for (int i=0; i<m; ++i)
        if (where[i] == -1)
            return INF;
    return 1;
}

const int64_t MAX_DENOMINATOR = 1024; 

/* f : number to convert.
 * num, denom: returned parts of the rational.
 * md: max denominator value.  Note that machine floating point number
 *     has a finite resolution (10e-16 ish for 64 bit double), so specifying
 *     a "best match with minimal error" is often wrong, because one can
 *     always just retrieve the significand and return that divided by 
 *     2**52, which is in a sense accurate, but generally not very useful:
 *     1.0/7.0 would be "2573485501354569/18014398509481984", for example.
 */
void rat_approx(double f, int64_t md, int64_t *num, int64_t *denom)
{
	/*  a: continued fraction coefficients. */
	int64_t a, h[3] = { 0, 1, 0 }, k[3] = { 1, 0, 0 };
	int64_t x, d, n = 1;
	int i, neg = 0;
 
	if (md <= 1) { *denom = 1; *num = (int64_t) f; return; }
 
	if (f < 0) { neg = 1; f = -f; }
 
	while (f != floor(f)) { n <<= 1; f *= 2; }
	d = f;
 
	/* continued fraction and check denominator each step */
	for (i = 0; i < 64; i++) {
		a = n ? d / n : 0;
		if (i && !a) break;
 
		x = d; d = n; n = x % n;
 
		x = a;
		if (k[1] * a + k[0] >= md) {
			x = (md - k[0]) / k[1];
			if (x * 2 >= a || k[1] >= md)
				i = 65;
			else
				break;
		}
 
		h[2] = x * h[1] + h[0]; h[0] = h[1]; h[1] = h[2];
		k[2] = x * k[1] + k[0]; k[0] = k[1]; k[1] = k[2];
	}
	*denom = k[1];
	*num = neg ? -h[1] : h[1];
}
vector<double> make_integer(vector<double> v)
{
    vector<double> res(v.size());
    for (int i = 0; i < v.size(); i++)
        res[i] = v[i];
    for(int i = 0; i < res.size(); i++)
    {
        if (int(res[i]) == res[i])
            continue;
        // we have double value
        int64_t d, n;
        double f = res[i];
        int j = 16;
        while(j <= MAX_DENOMINATOR)
        {
            rat_approx(f, j, &n, &d);
            if(abs(f - 1.0 * n / d) < EPS)
                break;
            j *= 4;
        }
        if(abs(f - 1.0 * n / d) > EPS) // we could not convert it
        {
            cout << "Something went wrong" << endl;
            res.clear();
            return res;
        }
        for (int i = 0; i < res.size(); i++)
            res[i] *= d;
    }
    return res;
}
vector<double> flatten(vector<double> v)
{
    vector<double> res = make_integer(v);
    if (res.size() == 1) return res;
    int gcd = __gcd((int)res[0], (int)res[1]);
    for(int i = 2; i < res.size(); i++)
    {
        gcd = __gcd(gcd, (int)res[i]);
    }
    for (int i = 0; i < res.size(); i++)
        res[i] /= gcd;
    return res;
}


int main() {
    cout << "Starting ..." << endl;
    string fileNumber = "5";
    map<string, vector<double>> map = readFile("data/input_" + fileNumber + ".txt");
    if (map.size() != 0)
    {
        vector<vector<double>> A;
        for (auto const& pair: map) {
            vector<double> v = map[pair.first];
            v[v.size() - 1] = -v[v.size() - 1];
            A.push_back(v);
        }
        cout << "The system: " << endl;
        print(A);

        // Calculate solution
        vector<double> x;
        int res = gauss(A, x);

        // Print result
        cout << "Result of the system:\t";
        for (int i=0; i<x.size(); i++) {
            cout << x[i] << " ";
        }
        cout << endl;
        x.push_back(1); // Add the last variable

        vector<double> coef = flatten(x);
        for (int i=0; i < coef.size(); i++) {
            cout << coef[i] << " ";
        }
        cout << endl;

        // Output result to file
        ofstream outp("result/output_" + fileNumber + ".txt");
        for (int i=0; i < coef.size(); i++) {
            outp << coef[i] << " ";
        }
        outp.close();
    }

    cout << "Done ..." << endl;
}