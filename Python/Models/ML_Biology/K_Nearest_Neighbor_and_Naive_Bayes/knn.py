"""
Homework  : KNN and Naive Bayes
Course    : Data Mining (636-0018-00L)

"""

import os
import sys
import argparse
import numpy as np
import pandas as pd

def load_data(datadir):
    data = pd.read_csv("{}/{}".format(datadir, 'matrix_mirna_input.txt'), sep="\t")
    X = data.iloc[:, 1:].values
    print("Number of patients: {}; Number of features: {}".format(X.shape[0], X.shape[1]))
    data = pd.read_csv("{}/{}".format(datadir, 'phenotype.txt'), sep="\t")
    y = data.iloc[:, 1:].values
    y = np.squeeze(y)
    y = [1 if e == '+' else 0 for e in y]
    y = np.array(y)
    print("Number of labels (patients): {0}".format(y.shape[0]))
    return X, y

def pairwise_euclidian_dist(x,x_new):
    """
    Calculates euclidian distance between each pairs of known x and unknown points x_new
    
    """
    num_pred = x_new.shape[0]
    num_data = x.shape[0]
    
    dists = np.zeros((num_pred, num_data))

    for i in range(num_pred):
        for j in range(num_data):
            # calculate euclidian distance here
            dists[i][j] = np.sqrt(np.sum((x_new[i] - x[j])**2))
            
    return dists

def k_nearest_labels(dists, y_known, k):
    """
    This function returns labels of k-nearest neighbours to each sample for unknown data.
    
    """
        
    num_pred = dists.shape[0]
    n_nearest = []
    
    for j in range(num_pred):
        dst = dists[j]
        # count k closest points 
        if(len(dst)==k):
            closest_y = y_known[np.argsort(dst)]
        else:
            closest_y = y_known[np.argpartition(dst, k)][:k]
        
        n_nearest.append(closest_y)
    return np.asarray(n_nearest) 

class KNearest_Neighbours(object):
    def __init__(self, k):
        self.k = k
        self.test_set_x = None
        self.train_set_x = None
        self.train_set_y = None

        
    def fit(self, train_set_x, train_set_y):
        self.train_set_x = train_set_x
        self.train_set_y = train_set_y

        
    def predict(self, test_set_x):
        dists = pairwise_euclidian_dist(self.train_set_x, test_set_x)
        neighbors = k_nearest_labels(dists, self.train_set_y, self.k)
        predictions = [max(set(list(row)), key=list(row).count) for row in neighbors]
        assert len(predictions) == len(test_set_x)
        return predictions
    
def calculate_metrics(predicted, actual):
    TP, FP, TN, FN = 0, 0, 0, 0
    for i in range(len(predicted)):
        if   (predicted[i] == 0) & (actual[i] == 0):
            TP += 1
        elif (predicted[i] == 0) & (actual[i] == 1):
            FP += 1
        elif (predicted[i] == 1) & (actual[i] == 1):
            TN += 1
        else:
            FN += 1

    accuracy  = (TP + TN) / (TP + FP + TN + FN) 
    precision = (TP) / (TP + FP) 
    recall    = (TP) / (TP + FN) 
    
    return accuracy, precision, recall

if __name__ == '__main__':

# Set up the parsing of command-line arguments
    parser = argparse.ArgumentParser(
        description="Compute distance functions on time-series"
    )
    parser.add_argument(
        "--traindir",
        required=True,
        help="Path to the directory where the training data are stored"
    )
    parser.add_argument(
        "--testdir",
        required=True,
        help="Path to the directory with the test data"
    )
    parser.add_argument(
        "--outdir",
        required=True,
        help="Path to the output directory where the output file will be saved"
    )
    parser.add_argument(
        "--mink",
        required=True,
        help="The minimum value of k on which k-NN algorithm will be invoked"
    )
    parser.add_argument(
        "--maxk",
        required=True,
        help="The maximum value of k on which to run k-NN"
    )

    args = parser.parse_args()

    # Set the paths
    traindir = args.traindir
    testdir = args.testdir
    outdir = args.outdir
    mink = int(args.mink)
    maxk = int(args.maxk)

    os.makedirs(outdir, exist_ok=True)

    # Read and parse the data
    print("Load train dataset...")
    train_X, train_y = load_data(traindir)
    print("Load test dataset...")
    test_X, test_y = load_data(testdir)
    
    # Train and save metrics
    metrics = []
    df = pd.DataFrame(columns=['Value of k', 'Accuracy', 'Precision', 'Recall'])
    for k in range(mink, maxk + 1):
        model = KNearest_Neighbours(k)
        model.fit(train_X, train_y)
        y_predictions = model.predict(test_X)
        m = calculate_metrics(y_predictions, test_y)
        new_row = {'Value of k': k, 'Accuracy': m[0], 'Precision': m[1], 'Recall': m[2]}
        new_df = pd.DataFrame([new_row])
        df = pd.concat([df, new_df], axis=0, ignore_index=True)
        
    # Save results to file
    path = "{}/{}".format(outdir, 'output_knn.txt')
    df.to_csv(path, index=False, sep='\t')
    
    print("Done...")