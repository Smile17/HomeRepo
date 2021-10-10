#include<stdio.h>
#include <string.h>

// Split string into words
char ** split(char str[], int* n) {
    char ** res  = NULL;
    char *  p    = strtok (str, " ");
    int n_spaces = 0, i;
    
    /* split string and append tokens to 'res' */
    while (p) {
      res = realloc (res, sizeof (char*) * ++n_spaces);
      if (res == NULL)
        exit (-1); /* memory allocation failed */
      res[n_spaces-1] = p;
      p = strtok (NULL, " ");
    }
    
    /* realloc one extra element for the last NULL */
    
    res = realloc (res, sizeof (char*) * (n_spaces+1));
    res[n_spaces] = 0;
    
    (*n) = n_spaces;
    
    return res; 
};
// Shift left array of strings
void shift_left(char ** words, int n)
{
    char * tmp = words[0];
    for (int i = 0; i < n - 1; ++i)
        words[i] = words[i + 1];
    words[n - 1] = tmp;
};

// Shift right array of strings
void shift_right(char ** words, int n)
{
    char * tmp = words[n - 1];
    for (int i = n - 1; i > 0; --i)
        words[i] = words[i - 1];
    words[0] = tmp;
};

int main() {
    char str[] = "asd sdaasd asdasd da sd asd";
    int n;
    char ** words = split(str, &n);
    for (int i = 0; i < n; ++i)
        printf ("%s ", words[i]);
    printf("\n");
    shift_left(words, n);
    for (int i = 0; i < n; ++i)
        printf ("%s ", words[i]);
    printf("\n");
    shift_right(words, n);
    for (int i = 0; i < n; ++i)
        printf ("%s ", words[i]);
    printf("\n");
    
    return 0;
}

