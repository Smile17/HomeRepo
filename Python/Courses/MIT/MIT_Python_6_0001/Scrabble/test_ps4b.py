from ps4b import *

#
# Test code
# You don't need to understand how this test code works (but feel free to look it over!)

# To run these tests, simply run this file (open up in your IDE, then run the file as normal)

def test_compChooseWord(wordList):
    """
    Unit test for compChooseWord
    """
    failure=False
    # dictionary of words and scores
    t = [
        ({'a': 1, 'p': 2, 's': 1, 'e': 1, 'l': 1}, 6),
        ({'a': 2, 'c': 1, 'b': 1, 't': 1}, 5),
        ({'a': 2, 'e': 2, 'i': 2, 'm': 2, 'n': 2, 't': 2}, 12),
        ({'x': 2, 'z': 2, 'q': 2, 'n': 2, 't': 2}, 12)
    ]
    for element in t:
        print('hand: ', element[0], '; n: ', element[1], '; Best word: ', compChooseWord(element[0], wordList, element[1]))
    print("SUCCESS: test_compChooseWord(wordList)")

# end of test_compChooseWord

def test_compPlayHand(wordList):
    """
    Unit test for compChooseWord
    """
    failure=False
    # dictionary of words and scores
    t = [
        ({'a': 1, 'p': 2, 's': 1, 'e': 1, 'l': 1}, 6),
        ({'a': 2, 'c': 1, 'b': 1, 't': 1}, 5),
        ({'a': 2, 'e': 2, 'i': 2, 'm': 2, 'n': 2, 't': 2}, 12),
        ({'x': 2, 'z': 2, 'q': 2, 'n': 2, 't': 2}, 12)
    ]
    for element in t:
        print('hand: ', element[0], '; n: ', element[1])
        compPlayHand(element[0], wordList, element[1])
    print("SUCCESS: test_compPlayHand(wordList)")

# end of test_compPlayHand

wordList = loadWords()
print("----------------------------------------------------------------------")
print("Testing compChooseWord...")
test_compChooseWord(wordList)
print("----------------------------------------------------------------------")
print("Testing compPlayHand...")
test_compPlayHand(wordList)
print("----------------------------------------------------------------------")
print("All done!")
