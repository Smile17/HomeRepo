#include <cstdio>
#include <cstdlib>
#include <cstring>
using namespace std;
int main()
{
    FILE* pFile;
    char line[100];
    fopen_s(&pFile, "1.txt", "r");

    //pFile = fopen("1.txt", "r");
    if (pFile == NULL) perror("Error opening file");
    while (fgets(line, sizeof(line), pFile))
    {
        bool b = false;
        int len = (unsigned)strlen(line);
        for (size_t i = 0; i < len - 1; ++i) {
            if (line[i] == ' ')
            {
                if ((line[i + 1] == 'a') || (line[i + 1] == 'A'))
                    b = true;
                break;
            }
        }
        if (b)
            printf("%s", line);
    }
    fclose(pFile);
    return 0;
}
