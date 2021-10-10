// https://cp-algorithms.com/linear_algebra/linear-system-gauss.html
#include <iostream>
#include <cmath>
#include <vector>

using namespace std;

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


int main() {
    cout << "Starting ..." << endl;
    int n, m; // n - number of equations, m - number of variables
    cin >> n >> m;
    vector<double> line(m+1,0);
    // Input example
    //4 3
    //1 0 -2
    //1 4 -4
    //1 2 0
    //0 1 -1
    //0 1 2 0
    // Answer: 1 0.5 0.5


    vector< vector<double> > A(n,line);

    // Read input data
    for (int i=0; i<n; i++) {
        for (int j=0; j<m; j++) {
            cin >> A[i][j];
        }
    }

    for (int i=0; i<n; i++) {
        cin >> A[i][m];
    }

    // Print input
    print(A);

    // Calculate solution
    vector<double> x(m);
    int res = gauss(A, x);

    // Print result
    cout << "Result:\t";
    for (int i=0; i<m; i++) {
        cout << x[i] << " ";
    }
    cout << "Done ..." << endl;
}