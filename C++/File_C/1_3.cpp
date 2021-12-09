#include <cstdio>
#include <cstdlib>
#include <cstring>
using namespace std;
int main()
{
    FILE* pFile, *pOutput;
    char line[100];
    fopen_s(&pFile, "1.txt", "r");
    fopen_s(&pOutput, "res.txt", "w+");

    if (pFile == NULL) perror("Error opening file");
    while (fgets(line, sizeof(line), pFile))
    {
        int len = (unsigned)strlen(line);
        int count = 0;
        for (size_t i = 0; i < len; ++i) {
            if (line[i] == ' ')
                count++;
        }
        if(count >= 3)
            fputs(line, pOutput);
    }
    fclose(pOutput);
    return 0;
}
