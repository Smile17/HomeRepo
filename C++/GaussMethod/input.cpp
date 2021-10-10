#include <iostream>
#include <fstream>
#include <vector>
#include <map>
#include <string>
#include <iterator>
#include <iomanip>
#include <cmath>

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
                    coof.push_back(sign * stod(words[2 * i + 1])); // TODO: check if it is a number
                    map.insert(make_pair(words[2 * i], coof));
                }
                
                cout << words[i] << " ";
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

int main() {
    cout << "Starting ..." << endl;
    readFile("input.txt");
    cout << "Done ..." << endl;
}