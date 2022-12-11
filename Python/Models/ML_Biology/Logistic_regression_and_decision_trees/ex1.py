'''
Skeleton for Homework 4: Logistic Regression and Decision Trees
Part 1: Logistic Regression

Authors: Anja Gumpinger, Dean Bodenham, Bastian Rieck
'''

#!/usr/bin/env python3

import pandas as pd
import numpy as np

from sklearn.linear_model import LogisticRegression
from sklearn.metrics import accuracy_score
from sklearn.metrics import confusion_matrix
from sklearn.preprocessing import StandardScaler


def compute_metrics(y_true, y_pred):
    '''
    Computes several quality metrics of the predicted labels and prints
    them to `stdout`.

    :param y_true: true class labels
    :param y_pred: predicted class labels
    '''

    tn, fp, fn, tp = confusion_matrix(y_true, y_pred).ravel()

    print('Exercise 1.a')
    print('------------')
    print('TP: {0:d}'.format(tp))
    print('FP: {0:d}'.format(fp))
    print('TN: {0:d}'.format(tn))
    print('FN: {0:d}'.format(fn))
    print('Accuracy: {0:.3f}'.format(accuracy_score(y_true, y_pred)))

def load_data(path):
    #read data from file using pandas
    df = pd.read_csv(path)
    # extract first 7 columns to data matrix X (actually, a numpy ndarray)
    X = df.iloc[:, 0:7].values

    # extract 8th column (labels) to numpy array
    y = df.iloc[:, 7].values
    return X, y

if __name__ == "__main__":

###################################################################
# Your code goes here.
###################################################################

    path = 'data/diabetes_train.csv'
    X_train, y_train = load_data(path)

    scaler = StandardScaler()
    scaler.fit(X_train)
    X_train_scaled = scaler.transform(X_train)

    model = LogisticRegression(C=1)
    model.fit(X_train_scaled, y_train)

    path = 'data/diabetes_test.csv'
    X_test, y_test = load_data(path)
    X_test_scaled = scaler.transform(X_test)
    y_pred = model.predict(X_test_scaled)

    compute_metrics(y_test, y_pred)
    print('------------')

    print('Exercise 1.b')
    print('Although the accuracy of the LDA model is a bit less (0.771) in comparison with the LogisticRegression model and it might look like LogisticRegression is better, still, I would choose the LDA model, since for any model accuracy is not the only one parameter that sould be considered, recall and precision are also important, and for disease, detection models are really important TP, FP, TN, FN values, especially FN is a number of people which was predicted wrongly and the model says the negative answer, people are really whereas, in reality, they are not. And if we compare this value for both methods we see that LDA has less value than the LogisticRegression model, so LDA is better in that perspective.')
    print('------------')

    print('Exercise 1.c')
    print('Different sources say that LDA is better than LogisticRegression when all requirements are met. So, LDA requires that all features are continuous (not categorical) and follow a Normal distribution. That makes LDA sensible to outliers and not applicable for categorical data, so in an unknown unpurified dataset, I would choose LogisticRegression as a more robust method.')
    print('------------')

    print('Exercise 1.d')
    npre_c = model.coef_[0][0]
    prob = np.exp(npre_c)
    print("The coefficient for npreg is {}. Calculating the exponential function results in {}, which amounts to an increase in diabetes risk of {} percent per additional pregnancy."
          .format(round(npre_c, 4), round(prob, 4), round((prob - 1) * 100, 4)))
    print('------------')