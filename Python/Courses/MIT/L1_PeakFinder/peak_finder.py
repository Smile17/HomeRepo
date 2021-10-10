filename = "peak_finder.txt"
with open(filename) as f:
    arr = [int(j) for j in f.readline().split()]
print(arr)
def peak_finder(arr):
    res = []
    if (arr[0] > arr[1]):
        res.append((0, arr[0]))
    for i in range(1, len(arr) - 1):
        if (arr[i - 1] <= arr[i]) and (arr[i] >= arr[i + 1]):
            res.append((i, arr[i]))
    if (arr[- 1] >= arr[-2]):
        res.append((len(arr) - 1, arr[-1]))
    return res
def binary_peak_finder(arr):
    low = 0
    high = len(arr) - 1
    while(high >= low):
        mid = int((low + high) / 2)
        if (mid > 0):
            if (arr[mid] < arr[mid - 1]):
                high = mid - 1
                break
        if (mid < len(arr) - 1):
            if (arr[mid] < arr[mid + 1]):
                low = mid + 1
                break
        return mid, arr[mid]
    #return mid, arr[mid]
res = binary_peak_finder(arr)
print(res)

