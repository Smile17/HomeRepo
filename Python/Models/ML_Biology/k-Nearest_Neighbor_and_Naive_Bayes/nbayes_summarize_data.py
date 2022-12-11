"""
Homework  : KNN and Naive Bayes Part 2
Course    : Data Mining (636-0018-00L)

"""

import os
import sys
import argparse
import numpy as np
import pandas as pd

def load_data(datadir):
    data = pd.read_csv("{}/{}".format(datadir, 'tumor_info.txt'), sep="\t", header=None)
    data.columns =['Clump Thickness', 'Uniformity of Cell Size', 'Marginal adhesion', 'Mitoses', 'Class']
    X = data.iloc[:, :-1]
    y = data.iloc[:, -1]
    return X, y

class Naive_Bayes(object):
    def __init__(self):
        self.train_x = None
        self.train_y = None
        self.class_priors = {}
        self.likelihoods = {}
        self.pred_priors = {}
        
    def _calc_class_prior(self):
        for cl in np.unique(self.train_y):
            cl_count = sum(self.train_y == cl)
            self.class_priors[cl] = cl_count / len(self.train_y)
            
    def _calc_likelihoods(self):
        for col in self.train_x:
            self.likelihoods[col] = {}
            for cl in np.unique(self.train_y):
                self.likelihoods[col][cl] = {}
                sample = self.train_x[col][self.train_y== cl]
                cl_count = np.count_nonzero(~np.isnan(sample))
                distribution = sample.value_counts().to_dict()
                for val, count in distribution.items():
                    self.likelihoods[col][cl][val] = count/cl_count
        for col in self.train_x:
            for cl in np.unique(self.train_y):
                dist = self.likelihoods[col][cl]
                s = sum(dist.values())
                if(not np.isclose(s, 1)):
                    print("Something went wrong")
                    
    def _calc_predictor_prior(self):
        for col in self.train_x:
            self.pred_priors[col] = {}
            vals = self.train_x[col].value_counts().to_dict()
            for val, count in vals.items():
                self.pred_priors[col][val] = count/len(self.train_x)
    
    def fit(self, train_x, train_y):
        self.train_x = train_x
        self.train_y = train_y
        self._calc_class_prior()
        self._calc_likelihoods()
        self._calc_predictor_prior()
        
    def predict(self, test_x):
        predictions = []
        for x in np.array(test_x):
            probs = {}
            for cl in np.unique(self.train_y):
                prior = self.class_priors[cl]
                print("P(Class={})={}".format(cl, prior))
                likelihood = 1
                evidence = 1
                for col, col_val in zip(self.train_x, x):
                    d = self.likelihoods[col][cl]
                    if col_val in d:
                        likelihood *= d[col_val]
                        print("P({}={} | Class={})={}".format(col, col_val, cl, d[col_val]))
                    else:
                        likelihood = 0
                    if col_val in self.pred_priors[col]:
                        evidence *= self.pred_priors[col][col_val]
                        print("P({}={})={}".format(col, col_val, self.pred_priors[col][col_val]))
                    else:
                        evidence = 0
                if evidence == 0:
                    probs[cl] = 0
                else:
                    probs[cl] = (likelihood * prior) / (evidence)
            print("Probabilities:", probs)
            predictions.append(max(probs, key = lambda u: probs[u]))
        return predictions

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
        "--outdir",
        required=True,
        help="Path to the output directory where the output file will be saved"
    )

    args = parser.parse_args()

    # Set the paths
    traindir = args.traindir
    outdir = args.outdir

    os.makedirs(outdir, exist_ok=True)

    # Read and parse the data
    print("Load train dataset...")
    train_X, train_y = load_data(traindir)
    
    
    # Train a model
    model = Naive_Bayes()
    model.fit(train_X, train_y)
    
    # Save summaries
    df2 = pd.DataFrame(index = list(range(1, 11)))
    df4 = pd.DataFrame(index = list(range(1, 11)))
    for col in model.train_x:
        df2[col] = model.likelihoods[col][2]
        df4[col] = model.likelihoods[col][4]
    df2 = df2.fillna(0)
    df4 = df4.fillna(0)

    path = "{}/{}".format(outdir, 'output_summary_class_2.txt')
    df2.to_csv(path, index=True, sep='\t')
    path = "{}/{}".format(outdir, 'output_summary_class_4.txt')
    df4.to_csv(path, index=True, sep='\t')
    
    X_test = [5, 2, 3, 1]
    y_test = model.predict([X_test])
        
    print("Done...")