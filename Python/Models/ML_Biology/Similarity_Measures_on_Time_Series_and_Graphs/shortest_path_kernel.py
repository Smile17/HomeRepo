"""Skeleton file for your solution to the shortest-path kernel."""
import numpy as np

def floyd_warshall(A):
    """Implement the Floyd--Warshall on an adjacency matrix A.

    Parameters
    ----------
    A : `np.array` of shape (n, n)
        Adjacency matrix of an input graph. If A[i, j] is `1`, an edge
        connects nodes `i` and `j`.

    Returns
    -------
    An `np.array` of shape (n, n), corresponding to the shortest-path
    matrix obtained from A.
    """
    D = A.copy().astype(float)
    D[D == 0] = np.inf
    np.fill_diagonal(D, 0)
    n = len(D)
    
    for k in range(n):
        for i in range(n):
            for j in range(n):
                s = D[i, k] + D[k, j]
                if(D[i, j] > s):
                    D[i, j] = s
    
    return D


def sp_kernel(S1, S2):
    """Calculate shortest-path kernel from two shortest-path matrices.

    Parameters
    ----------
    S1: `np.array` of shape (n, n)
        Shortest-path matrix of the first input graph.

    S2: `np.array` of shape (m, m)
        Shortest-path matrix of the second input graph.

    Returns
    -------
    A single `float`, corresponding to the kernel value of the two
    shortest-path matrices
    """
    s = 0.0
    n = len(S1)
    m = len(S2)
    for i1 in range(n):
        for j1 in range(i1 + 1, n):
            for i2 in range(m):
                for j2 in range(i2 + 1, m):
                    w1 = S1[i1, j1]
                    w2 = S2[i2, j2]
                    if(np.isclose(w1, w2)): # w1 == w2, in case they are float, it would be better to compare them this way
                        s += 1
    return float(s)
