#include <cstdio>
#include <cstdlib>
#include <cstring>
using namespace std;
int main()
{
    FILE* pFile;
    char line[100];
    fopen_s(&pFile, "1.txt", "r");

    if (pFile == NULL) perror("Error opening file");
    while (fgets(line, sizeof(line), pFile))
    {
        int len = (unsigned)strlen(line);
        int count = 0;
        for (size_t i = 0; i < len; ++i) {
            if (line[i] == 'i')
                count++;
        }
        if(count >= 5)
            printf("%s", line);
    }
    fclose(pFile);
    return 0;
}
