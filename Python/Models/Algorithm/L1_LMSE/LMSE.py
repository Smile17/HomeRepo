# -*- coding: utf-8 -*-
"""
Created on Sun Oct 10 12:41:51 2021

"""

# Import libraries
import numpy as np
import matplotlib.pyplot as plt

class LMSE(object):
    """
    Parameters:
    -----------
    learning rate: double
        The speed of learning, updating weights.
    """
    def __init__(self, learning_rate = 1):
        self.h = learning_rate
    

    def initialize(self, X, y):
        """
        This function creates a matrix V based on training sets
        
        Argument:
        X -- train set, a numpy array of size (number of features, number of observations)
        y -- result set describes class number, a numpy array of size (1, number of observations)
        
        Returns:
        V -- unified matrix, a numpy array of size (number of features + 1, number of observations)
        V_inv -- pseudoinverse matrix to V (inverse to V^t*V)
        """
        V = np.concatenate((X, y), axis=1)
        Vt = np.transpose(V)
        V_inv = np.linalg.inv(Vt @ V) @ Vt
        y0 = np.ones(V.shape[0])
        #y0 = np.random.rand(V.shape[0])
        
        assert(V.shape[0] == X.shape[0] == y.shape[0])
        assert(V.shape[1] == (X.shape[1] + y.shape[1]))
        assert(V_inv.shape == (V.shape[1], V.shape[0]))
        I = V_inv @ V # I should be identity matrix
        assert (I.shape[0] == I.shape[1]) and np.allclose(I, np.eye(I.shape[0]))
        assert((y0 > 0).all())
        
        return V, V_inv, y0

    def fit(self, X, y):
        V, V_inv, y0 = self.initialize(X, y)
        yk = y0
        
        while(True):
            wk = V_inv @ yk
            res = V @ wk
            print(wk)
            print(res)
            if((res > 0).all()):
                break
            dyk = self.h * (res - yk)
            print(dyk)
            if((dyk < 0.00001).all()):
                print("Algorithm does not converge")
                break
            yk = yk + dyk
            
        return wk
        
        
    def predict(self, test_set_x):
        pass

# Read data from file and create train set
X1 = np.genfromtxt('data\X1.txt',delimiter=';')
X2 = np.genfromtxt('data\X2.txt',delimiter=';')
# Check dimensions
#print(X1.shape)
#print(X2.shape)
y1 = np.ones(X1.shape[0])
y2 = (-1) * np.ones(X2.shape[0])
X = np.concatenate((X1, (-1) * X2))
y = np.concatenate((y1, y2)).reshape(-1, 1)

np.random.seed(5)
# Algorithm of LMSE
model = LMSE()
wk = model.fit(X, y)
print("Result:")
print(wk)

# Show results in the plot
plt.scatter(X1[:, 0], X1[:, 1])
plt.scatter(X2[:, 0], X2[:, 1])
# w0 * x + w1 * y + w2 = 0
# y = -(w2 + w0 * x) / w1
x = np.arange(-3, 4, 0.5)
y = (-1) * (wk[0] * x + wk[2]) / wk[1]
plt.plot(x, y)




