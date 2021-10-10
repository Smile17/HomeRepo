filename = "peak_finder_2D.txt"
arr = []
with open(filename) as f:
    while True:
        line = f.readline()
        if not line:
            break
        arr.append([int(j) for j in line.split()])
print(arr)
print(len(arr))

def peak_finder_2D(arr):
    # arr is 2D array
    i = int(len(arr) / 2)
    j = int(len(arr[0]) / 2)
    while True:
        neigh = {(i, j): arr[i][j]}
        if (j > 0):
            neigh[(i, j - 1)] = arr[i][j - 1]
        if j < len(arr[0]) - 1:
            neigh[(i, j + 1)] = arr[i][j + 1]
        if (i > 0):
            neigh[(i - 1, j)] = arr[i - 1][j]
        if i < len(arr) - 1:
            neigh[(i + 1, j)] = arr[i + 1][j]
        max_key = next(iter(neigh))
        for key in neigh:
            if(neigh[max_key] < neigh[key]):
                max_key = key
        if (i, j) == max_key:
            return i, j, arr[i][j]
        i, j = max_key
import numpy as np
def binary_peak_finder_2D(arr):
    low = 0
    high = len(arr) - 1
    j_max = 0
    while(high >= low):
        mid  = int((low + high) / 2)
        j_max = np.argmax(arr[mid]) # find maximum in column mid
        if (mid > 0):
            if (arr[mid][j_max] < arr[mid - 1][j_max]):
                high = mid - 1
                break
        if (mid < len(arr) - 1):
            if (arr[mid][j_max] < arr[mid + 1][j_max]):
                low = mid + 1
                break
        return mid, j_max, arr[mid][j_max]
    #return mid, j_max, arr[mid][j_max]
    

res = binary_peak_finder_2D(arr)
print(res)
