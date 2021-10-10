from binary_search import *

#
# Test code
#


def test_binary_search():
    """
    Unit test for binary_search
    """
    failure=False
    
    arr = [1, 3, 4, 7, 7, 9, 10]
    x = 7
    k = binary_search(arr, x)
    print(k, arr[k])
    if (arr[k] != x):
        failure=True

    if not failure:
        print("SUCCESS: test_binary_search()")

# end of test_binary_search


print("----------------------------------------------------------------------")
print("Testing binary_search...")
test_binary_search()
print("----------------------------------------------------------------------")
print("All done!")
