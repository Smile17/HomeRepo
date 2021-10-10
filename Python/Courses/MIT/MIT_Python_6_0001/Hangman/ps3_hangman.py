# Hangman game
#

# -----------------------------------
# Helper code
# You don't need to understand this helper code,
# but you will have to know how to use the functions
# (so be sure to read the docstrings!)

import random

WORDLIST_FILENAME = "words.txt"
MAX_GUESSES_ATTEMPTS = 8

def loadWords():
    """
    Returns a list of valid words. Words are strings of lowercase letters.
    
    Depending on the size of the word list, this function may
    take a while to finish.
    """
    print("Loading word list from file...")
    # inFile: file
    inFile = open(WORDLIST_FILENAME, 'r')
    # line: string
    line = inFile.readline()
    # wordlist: list of strings
    wordlist = line.split()
    print("  ", len(wordlist), "words loaded.")
    return wordlist

def chooseWord(wordlist):
    """
    wordlist (list): list of words (strings)

    Returns a word from wordlist at random
    """
    return random.choice(wordlist)

# end of helper code
# -----------------------------------

# Load the list of words into the variable wordlist
# so that it can be accessed from anywhere in the program
wordlist = loadWords()

def isWordGuessed(secretWord, lettersGuessed):
    '''
    secretWord: string, the word the user is guessing
    lettersGuessed: list, what letters have been guessed so far
    returns: boolean, True if all the letters of secretWord are in lettersGuessed;
      False otherwise
    '''
    for char in secretWord:
        if not(char in lettersGuessed):
            return False;
    return True;

# Test isWordGuessed
secretWord = 'apple'
lettersGuessed = ['e', 'i', 'k', 'p', 'r', 's']

print(isWordGuessed(secretWord, lettersGuessed))

def getGuessedWord(secretWord, lettersGuessed):
    '''
    secretWord: string, the word the user is guessing
    lettersGuessed: list, what letters have been guessed so far
    returns: string, comprised of letters and underscores that represents
      what letters in secretWord have been guessed so far.
    '''
    res = list(secretWord);
    for i in range(len(secretWord)):
        if not (secretWord[i] in lettersGuessed):
            res[i] = "_ "
    return "".join(res);

print(getGuessedWord(secretWord, lettersGuessed))

import string
def getAvailableLetters(lettersGuessed):
    '''
    lettersGuessed: list, what letters have been guessed so far
    returns: string, comprised of letters that represents what letters have not
      yet been guessed.
    '''
    l = list(string.ascii_lowercase)
    for char in lettersGuessed:
        l.remove(char);
    return "".join(l);
    
print(string.ascii_lowercase)
print(getAvailableLetters(lettersGuessed))

def hangman(secretWord):
    '''
    secretWord: string, the secret word to guess.

    Starts up an interactive game of Hangman.

    * At the start of the game, let the user know how many 
      letters the secretWord contains.

    * Ask the user to supply one guess (i.e. letter) per round.

    * The user should receive feedback immediately after each guess 
      about whether their guess appears in the computers word.

    * After each round, you should also display to the user the 
      partially guessed word so far, as well as letters that the 
      user has not yet guessed.

    Follows the other limitations detailed in the problem write-up.
    '''
    print('A secret word consist of ', len(secretWord), ' letters')
    lettersGuessed = []
    availableLetters = string.ascii_lowercase
    mistakeMade = 0

    while (mistakeMade < MAX_GUESSES_ATTEMPTS):
        guess = input('Input your guess: ')
        guessInLowerCase = guess.lower()
        if not(guessInLowerCase in availableLetters):
            print('You have already used this letter or the symbol is not a letter')
            print('Available letters: ', availableLetters)
        else:
            lettersGuessed.append(guessInLowerCase)
            if(isWordGuessed(secretWord, lettersGuessed)):
                print('Congratulations, the word is: ', secretWord)
                break
            else:
                if(guessInLowerCase in secretWord):
                    print('You have gueessed')
                else:
                    mistakeMade += 1
                    print('You have made a mistake, ', str(MAX_GUESSES_ATTEMPTS - mistakeMade), ' attempts are left')
                print(getGuessedWord(secretWord, lettersGuessed))
                availableLetters = getAvailableLetters(lettersGuessed)
                print('Available letters: ', availableLetters)
    if not(isWordGuessed(secretWord, lettersGuessed)):
        print('Attempts are not left, the secret word was: ', secretWord)






# When you've completed your hangman function, uncomment these two lines
# and run this file to test! (hint: you might want to pick your own
# secretWord while you're testing)

secretWord = chooseWord(wordlist).lower()
hangman(secretWord)
