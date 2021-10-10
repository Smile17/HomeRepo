// https://rosettacode.org/wiki/Convert_decimal_number_to_rational#C.23
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
const double EPS = 1e-9;
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
    vector<double> v = {1, 0.5, 1.0/3};
    for (int i = 0; i < v.size(); i++)
        cout << v[i] << " ";
    cout << endl;
    vector<double> res = flatten(v);
    for (int i = 0; i < res.size(); i++)
        cout << res[i] << " ";
    cout << endl;
    cout << "Done ..." << endl;
}