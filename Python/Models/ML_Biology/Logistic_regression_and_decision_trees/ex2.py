#!/usr/bin/env python3

'''
Skeleton for Homework 4: Logistic Regression and Decision Trees
Part 2: Decision Trees

Authors: Anja Gumpinger, Bastian Rieck
'''

import numpy as np
import sklearn.datasets
from sklearn.datasets import load_iris


if __name__ == '__main__':

    iris = sklearn.datasets.load_iris()
    X = iris.data
    y = iris.target

    feature_names = iris.feature_names
    num_features = len(set(feature_names))

    ####################################################################
    # Your code goes here.
    ####################################################################
    def compute_entropy(probs):
        return -np.sum(np.log2(probs) * probs)

    def split_data(X, y, attribute_index, theta):
        D = X[:,attribute_index]
        y1 = y[D < theta]
        y2 = y[D >= theta]
        return y1, y2

    def compute_information_content(y):
        unique, counts = np.unique(y, return_counts=True)
        return compute_entropy(counts / len(y))

    def compute_information_a(X, y, attribute_index, theta):
        y1, y2 = split_data(X, y, attribute_index, theta)
        ic1 = compute_information_content(y1)
        ic2 = compute_information_content(y2)
        return (ic1 * len(y1) + ic2 * len(y2)) / (len(y1) + len(y2))

    def compute_information_gain(X, y, attribute_index, theta):
        return compute_information_content(y) - compute_information_a(X, y, attribute_index, theta)

    attribute_index = 3
    theta = 1.0
    information_gain = compute_information_gain(X, y, attribute_index, theta)
    print(information_gain)
    print('------------')

    print('Exercise 2.b')

    #1. sepal length < 5.5
    print("IG(sepal length < 5.5):", compute_information_gain(X, y, 0, 5.5))
    #2. sepal width < 3.0
    print("IG(sepal width < 3.0):", compute_information_gain(X, y, 1, 3.0))
    #3. petal length < 2.0
    print("IG(petal length < 2.0):", compute_information_gain(X, y, 2, 2.0))
    #4. petal width < 1.0
    print("IG(petal width < 1.0):", compute_information_gain(X, y, 3, 1.0))

    print('------------')

    print('Exercise 2.c')
    print('I would select (petal length < 2.0) or (petal width < 1.0) to be the first split, because they have the biggest information gain')
    print('------------')

    ####################################################################
    # Exercise 2.d
    ####################################################################

    # Do _not_ remove this line because you will get different splits
    # which make your results different from the expected ones...
    np.random.seed(42)
    print('Exercise 2.d')

    from sklearn.model_selection import KFold
    from sklearn import tree
    from sklearn.metrics import accuracy_score

    def process(X, y):
        accs = []
        cv = KFold(n_splits=5, shuffle=True)
        for train_index, test_index in cv.split(X, y):
            X_train, y_train = X[train_index], y[train_index]
            X_test, y_test = X[test_index], y[test_index]
            model = tree.DecisionTreeClassifier()
            model.fit(X_train, y_train)
            y_pred = model.predict(X_test)
            accs.append(accuracy_score(y_test, y_pred))
            print("Feature importances: ", model.feature_importances_)
        accs = np.array(accs)
        return accs
    print('Original dataset')
    accs = process(X, y)
    print('Reduced dataset')
    X = X[y != 2]
    y = y[y != 2]
    accs2 = process(X, y)

    print('Accuracy score using cross-validation')
    print('The mean accuracy is', np.round(accs.mean(), 2))
    print('-------------------------------------\n')

    print('Feature importances for _original_ data set')
    print('For the original data, the two most important features are:\n- petal length; \n- petal width;')
    print('-------------------------------------------\n')

    print('Feature importances for _reduced_ data set')
    print('For the reduced data, the most important feature is petal length. This means that petal width feature is distinuasible one for class 2')
    print('------------------------------------------\n')