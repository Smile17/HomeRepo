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
    S1 = np.where(S1 == np.inf, 0, S1)
    S1 = np.triu(S1, k=1)
    bc1 = np.bincount(S1.reshape(-1).astype(int))

    S2 = np.where(S2 == np.inf, 0, S2)
    S2 = np.triu(S2, k=1)
    bc2 = np.bincount(S2.reshape(-1).astype(int))
    
    v1 = np.zeros(max(len(bc1), len(bc2)) - 1)
    v1[range(0, len(bc1)-1)] = bc1[1:]

    v2 = np.zeros(max(len(bc1), len(bc2)) - 1)
    v2[range(0, len(bc2)-1)] = bc2[1:]

    return np.sum(v1 * v2)
