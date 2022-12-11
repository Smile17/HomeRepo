def binary_search(arr, x):
    low = 0
    high = len(arr) - 1
    while (high - low >= 0):
        mid = int((high + low) / 2)
        if (arr[mid] == x):
            return mid
        if (arr[mid] > x):
            low = mid + 1
        else:
            high = mid - 1
    return -1

