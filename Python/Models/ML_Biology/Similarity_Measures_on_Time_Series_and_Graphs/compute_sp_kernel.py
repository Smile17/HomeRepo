"""
Homework  : Similarity measures on sets
Course    : Data Mining (636-0018-00L)

Compute all pairwise DTW and Euclidean distances of time-series within
and between groups.
"""
# Author: Xiao He <xiao.he@bsse.ethz.ch>
# Author: Bastian Rieck <bastian.rieck@bsse.ethz.ch>

import os
import sys
import argparse
from shortest_path_kernel import floyd_warshall
from shortest_path_kernel import sp_kernel
import scipy.io
import numpy as np

if __name__ == '__main__':

    # Set up the parsing of command-line arguments
    parser = argparse.ArgumentParser(
        description="Compute distance functions on time-series"
    )
    parser.add_argument(
        "--datadir",
        required=True,
        help="Path to input directory containing file MUTAG.mat"
    )
    parser.add_argument(
        "--outdir",
        required=True,
        help="Path to directory where graph_output.txt will be created"
    )

    args = parser.parse_args()

    # Set the paths
    data_dir = args.datadir
    out_dir = args.outdir

    os.makedirs(args.outdir, exist_ok=True)

    # Read data
    path = "{}/{}".format(args.datadir, 'MUTAG.mat')
    mat = scipy.io.loadmat(path)
    label = np.reshape(mat['lmutag'], (len(mat['lmutag'], )))
    data = np.reshape(mat['MUTAG']['am'], (len(label), ))
    # Shortest-path matrices
    SS = []
    for matrix in data:
        SS.append(floyd_warshall(matrix))
    SS = np.array(SS)

    # Create the output file
    try:
        file_name = "{}/graph_output.txt".format(args.outdir)
        f_out = open(file_name, 'w')
    except IOError:
        print("Output file {} cannot be created".format(file_name))
        sys.exit(1)

    cdict = {}
    cdict['non-mutagenic'] = -1
    cdict['mutagenic'] = 1
    lst_group = ['non-mutagenic', 'mutagenic']


    # Iterate through all combinations of pairs
    for idx_g1 in range(len(lst_group)):
        for idx_g2 in range(idx_g1, len(lst_group)):
            # Get the group data
            group1 = SS[label == cdict[lst_group[idx_g1]]]
            group2 = SS[label == cdict[lst_group[idx_g2]]]
            # Get average similarity
            count = 0
            sim = 0
            for x in group1:
                for y in group2:
                    # Skip redundant calculations
                    if(x.shape == y.shape):
                        if idx_g1 == idx_g2 and (x==y).all():
                            continue

                    # Compute sp_kernel
                    sim += sp_kernel(x, y)
                    count += 1
                    
            sim /= count

            # Transform the vector of distances to a string
            str_sim = '{0:.2f}'.format(sim)

            # Save the output
            f_out.write(
                '{}:{}\t{}\n'.format(
                    lst_group[idx_g1], lst_group[idx_g2], str_sim)
            )
            print(
                '{}:{}\t{}\n'.format(
                    lst_group[idx_g1], lst_group[idx_g2], str_sim)
            )
    f_out.close()
    print("Done...")