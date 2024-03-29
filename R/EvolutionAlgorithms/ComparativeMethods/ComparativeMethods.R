#########################################################
#######        COMPUTATIONAL BIOLOGY         ############
#######             HOMEWORK 5               ############
#########################################################
#                                                       #
# Compute the normalized independent contrasts on a     #
# given phylogeny                                       #
#                                                       #
#########################################################
#########################################################

#########################################################
######    Code below this should not be changed   #######
######    or deleted.                             #######
#########################################################

library(ape)

get_node_children = function(node, tree) {
    # Return the two child nodes of the given node.
    # In a bifurcating tree, a node will have either two child nodes if it is an internal node,
    # or zero child nodes if it is a tip node.
    # For an internal node, return the two child node numbers, for a tip node return NULL.
    #   node: the node for which child nodes should be returned
    #   tree: the tree encoded in an object of class phylo

    if (node <= length(tree$tip.label)) {
        return(NULL)
    }

    children = which(tree$edge[, 1] == node)
    child1 = tree$edge[children[1], 2]
    child2 = tree$edge[children[2], 2]

    # Return the indices of the child nodes.
    #   child1: first child of the given node
    #   child2: second child of the given node
    return(list(child1 = child1,
                child2 = child2))
}

get_root = function(tree) {
    # Returns the number of the root node of the given tree.
    return(length(tree$tip.label) + 1)
}

is_tip = function(node, tree) {
    # Returns TRUE if the given node is a tip, FALSE otherwise
    return(node <= length(tree$tip.label))
}

initialize_description = function(tree, traits_at_tip) {
    # This function creates a list with two matrices and one vector.
    #
    # The first matrix is called traits and stores the values of the two traits at the nodes of
    # the tree. Here we initialize this matrix with the given trait values at the tips and NA for
    # the internal nodes. The values for the internal nodes have to be calculated in the
    # function get_contrasts_in_subtree(). The row numbers correspond to the node numbers in the tree,
    # i.e. for a tree with N tips the first N rows correspond to the tips, the last N-1 rows to the
    # internal nodes.
    #
    # The second matrix is called normalized_contrasts and stores the values of the normalized contrasts
    # for both traits at all nodes. The matrix will be filled out in the function
    # get_contrasts_in_subtree(). Again, the row numbers correspond to the node numbers allocated in tree.
    # As contrasts are only calculated at the internal nodes, the first N rows of this matrix will stay
    # undefined, i.e. NA, as set here.
    #
    # The third object in the list is a vector that holds the corrected branch lengths which will be
    # calculated in the function get_contrasts_in_subtree(). It is initialized with the original branch lengths.
    # The branches are ordered by the node which they lead to, e.g. branch_lengths[3] will give the branch that
    # leads to node 3. The branch leading to the root will always be NA. Bear in mind that if you implement the
    # tree walking correctly, you will only need this data structure to compute all the corrected branch lengths,
    # you do not need to store the uncorrected values separately.

    number_of_tips = length(tree$tip.label)

    # initialize the trait_matrix
    trait_matrix = matrix(NA, nrow = 2 * number_of_tips - 1, ncol = 2,
                          dimnames = list(1:(2 * number_of_tips - 1),
                                          c("value_of_trait1", "value_of_trait2")))

    # fill in the known trait values
    trait_matrix[1:length(tree$tip.label), ] = cbind(as.numeric(traits_at_tip$trait1[tree$tip.label]),
                                                     as.numeric(traits_at_tip$trait2[tree$tip.label]))

    # initialize the normalized_contrasts matrix
    contrast_matrix = matrix(NA, nrow = 2 * number_of_tips - 1, ncol = 2,
                             dimnames = list(1:(2 * number_of_tips - 1),
                                             c("normalized_contrast_trait1", "normalized_contrast_trait2")))

    # initialize the branch lengths
    branch_lengths = c(tree$edge.length, NA)[order(c(tree$edge[, 2], length(tree$tip) + 1))]

    # Return the three objects in one list
    return(list(traits = trait_matrix, normalized_contrasts = contrast_matrix, corrected_branch_lengths = branch_lengths))
}

plot_data_and_regression = function(tree, description, summary_linear_regression) {
    # This function displays two plots:
    # On the left side the raw data points and the linear regression on the raw data, and
    # On the right side the contrast values and the linear regression on the contrasts.
    # As input this function takes two lists that will be generated by the following functions:
    #   description: a list as produced by get_contrasts_in_subtree
    #   summary_linear_regression: a list of two vectors with named values corresponding to the slope, intercept, p.value and R2
    #							   for the linear regression on respectively the raw data and the contrasts, as produced by get_trait_correlation

    par(mfrow = c(1, 2))

    raw = data.frame(description$traits[1:length(tree$tip.label), ])
    plot(raw, pch = 16, main = "raw data")
    x = seq(range(raw[, "value_of_trait1"])[1], range(raw[, "value_of_trait1"])[2], by = 0.01)
    lines(x, summary_linear_regression$linear_regression_raw_data["slope"] * x +
             summary_linear_regression$linear_regression_raw_data["intercept"],
          col = "gray")
    legend("bottomright", bty = "n", cex = 0.7,
           legend = paste0(names(summary_linear_regression$linear_regression_raw_data),
                           "=", signif(summary_linear_regression$linear_regression_raw_data, 3), 4))

    cont = data.frame(description$normalized_contrasts[(length(tree$tip.label) + 1):(2 * length(tree$tip.label) - 1), ])
    plot(cont, pch = 17, main = "contrasts")
    x = seq(range(cont[, "normalized_contrast_trait1"])[1], range(cont[, "normalized_contrast_trait1"])[2], by = 0.01)
    lines(x, summary_linear_regression$linear_regression_contrasts["slope"] * x +
              summary_linear_regression$linear_regression_contrasts["intercept"],
          col = "gray")
    legend("bottomright", bty = "n", cex = 0.7,
           legend = paste0(names(summary_linear_regression$linear_regression_contrasts),
                           "=", signif(summary_linear_regression$linear_regression_contrasts, 3), 4))
}

#########################################################
######    Code above this should not be changed   #######
######    or deleted.                             #######
#########################################################

linear_regression = function(values1, values2) {
    # Perform a simple linear regression on two sets of given values to evaluate the possible correlation between them.
    # The regression is approximated by the intercept alpha and the slope beta, values2 = alpha + beta*values1.
    # The input values should be presented as vectors, in the same order:
    # i.e. if the position i in values1 represents the value of trait or contrast at a certain node n, the same position i in values2
    # should represent the value for the other trait or contrast at the same node n.
    #   values1: first ordered vector of values (numeric)
    #   values2: second ordered vector of values (numeric)

    res = lm(y ~ x , data.frame(x = values1 , y = values2))
    s = summary(res)
    intercept = s$coefficients[1,1]
    slope = s$coefficients[2,1]
    R2 = s$r.squared
    p.value = s$coefficients[2,4]
    # Return a vector with named values that summarizes the linear regression result.
    #   slope: the slope of the linear regression line (beta)
    #   intercept: the intercept of the linear regression line (alpha)
    #   p.value: p-value, the level of significance of the correlation.
    #   R2: R squared, the measure of how close the data are to the fitted regression line.
    return(c(slope = slope, intercept = intercept, p.value = p.value, R2 = R2))
}

calculate_trait_at_internal_node = function(trait_child1, trait_child2, branch_length_to_child1, branch_length_to_child2) {
    # Calculate the trait value at an internal node given the values of this trait at the
    # two child nodes and the branch lengths connecting these child nodes to the parent node.
    #   trait_child1: trait value at first child (numeric)
    #   trait_child2: trait value at second child (numeric)
    #   branch_length_to_child1: (corrected) branch length leading to first child (numeric)
    #   branch_length_to_child2: (corrected) branch length leading to second child (numeric)

    trait_at_internal_node = (trait_child1 * branch_length_to_child2 + trait_child2 * branch_length_to_child1) / (branch_length_to_child1 + branch_length_to_child2)

    # Return the estimated value of the trait at the internal node.
    return(trait_at_internal_node)
}

calculate_contrast = function(trait_child1, trait_child2, branch_length_to_child1, branch_length_to_child2) {
    # Calculate the normalized contrast at a given node given the values of a trait at the two child nodes
    # and the branch lengths connecting these child nodes to the parent node.
    #   trait_child1: trait value at first child (numeric)
    #   trait_child2: trait value at second child (numeric)
    #   branch_length_to_child1: (corrected) branch length leading to first child (numeric)
    #   branch_length_to_child2: (corrected) branch length leading to second child (numeric)

    contrast = (trait_child1 - trait_child2) / sqrt(branch_length_to_child1 + branch_length_to_child2)

    # Return the numerical value of the normalized contrast.
    return(contrast)
}

calculate_corrected_branch_length = function(node, child1, child2, corrected_branch_lengths) {
    # Calculate the corrected branch length leading to the given node.
    #   node: the node at the end of the branch to be corrected (integer)
    #   child1: the first child of the given node (integer)
    #   child2: the second child of the given node (integer)
    #   corrected_branch_lengths: the current vector of corrected branch lengths, where all branches
    #                             below the given node have already been corrected (numeric)

    t1 = corrected_branch_lengths[child1]
    t2 = corrected_branch_lengths[child2]
    t3 = corrected_branch_lengths[node]
    corrected_length = t1 * t2 / (t1 + t2) + t3

    # Return the numerical value of the corrected branch length
    return(corrected_length)
}

calculate_trait_and_contrast = function(child1, child2, traits, corrected_branch_lengths) {
    # Calculate the trait value and the normalized contrast value at a node given the indices of the node children.
    #   child1: the first child of a given node (integer)
    #   child2: the second child of a given node (integer)
    #   traits: a vector containing trait values for all nodes (numeric)
    #   corrected_branch_lengths: the current vector of corrected branch lengths, where all branches
    #                             below the node in question have already been corrected (numeric)
    
    t1 = traits[child1]
    t2 = traits[child2]
    d1 = corrected_branch_lengths[child1]
    d2 = corrected_branch_lengths[child2]
    trait_value = calculate_trait_at_internal_node(t1, t2, d1, d2)
    contrast = calculate_contrast(t1, t2, d1, d2)

    # Return the trait value and the normalized contrast value at the given node.
    #   trait_value: the numerical value for the trait at given node
    #   contrast: the numerical value for the contrast at given node
    return(list(trait_value = trait_value, contrast = contrast))
}

get_contrasts_in_subtree = function(node, tree, description) {
    # Calculate the normalized independent contrasts, trait values, and corrected branch lengths
    # for the subtree that stems from the given nodelst.
    #   node: the node for which the contrasts should be calculated
    #   tree: the tree encoded in an object of class phylo
    # 	description: a list containing 3 entries:
    #				 a matrix with the current trait values at all nodes (including tips) in the tree (value or NA)
    #				 a matrix with the current contrasts at the internal nodes (value or NA), and
    #				 a vector with the corrected branch lengths.
    #				 This list is initialized by the provided function initialize_description().

    # If the node is a tip, return the same description.
    if(node <= length(tree$tip.label))
      return(description)

    # Otherwise, get the child nodes of the given node and compute the corrected branch lengths, normalized contrasts and trait values
    # for the child nodes, saving the updated description.
    children = get_node_children(node, tree)
    updated_description = get_contrasts_in_subtree(children$child1, tree, description)
    updated_description = get_contrasts_in_subtree(children$child2, tree, updated_description)

    # Update the branch length leading to the given node, save it in the description
    corrected_branch_lengths = updated_description$corrected_branch_lengths
    corrected_length = calculate_corrected_branch_length(node, children$child1, children$child2, corrected_branch_lengths)
    updated_description$corrected_branch_lengths[node] = corrected_length
    
    # Compute the trait values and normalized contrasts for both traits, save the values in the description
    corrected_branch_lengths = updated_description$corrected_branch_lengths
    traits = updated_description$traits
    n = ncol(traits)
    for (i in 1:n)
    {
      trait_values = traits[,i]
      tc = calculate_trait_and_contrast(children$child1, children$child2, trait_values, corrected_branch_lengths)
      updated_description$traits[node, i] = tc$trait_value
      updated_description$normalized_contrasts[node, i] = tc$contrast
    }
    

    # Return a list with updated trait values, normalized contrasts, and corrected branch lengths.
    #   updated_description: list with structure as defined in the initialize_description() function.
    return(updated_description)
}

get_trait_correlation = function(newick_tree, traits_at_tip) {	
    # Compute the normalized contrasts for the whole tree and determine whether there is a correlation between
    # the contrasts.
    #   newick_tree: the tree in newick text format
    #   traits_at_tips: a list of two vectors that represent the two trait values observed for the
    #                   individuals at the tips. (numeric)

    # Transform the tree from text format to an object of the phylo class
    tree = read.tree(text = newick_tree)
    # Reorder the tree for easier traversing
    tree = reorder(tree, order = "cladewise")

    # Initialize the list of traits, contrasts and branch lengths
    description = initialize_description(tree, traits_at_tip)

    # Calculate the new trait values at all the nodes in the tree, normalized contrasts at the internal nodes and the
    # corrected branch lengths, starting at the root of the tree.
    root = get_root(tree)
    updated_description = get_contrasts_in_subtree(root, tree, description)

    # Perform linear regression on the traits at the tips and get the slope, intercept, P-value, and R2-value
    k = length(tree$tip.label)
    y = updated_description$traits[1:k,2]
    x = updated_description$traits[1:k,1]
    raw_data_regression = linear_regression(x, y)

    # Perform linear regression on the normalized contrasts and get the slope, intercept, P-value, and R2-value
    y = updated_description$normalized_contrasts[,2]
    x = updated_description$normalized_contrasts[,1]
    contrasts_regression = linear_regression(x, y)

    # Plot the two linear regression results
    summary_linear_regression <- list(linear_regression_raw_data = raw_data_regression,
                                      linear_regression_contrasts = contrasts_regression)
    plot_data_and_regression(tree, updated_description, summary_linear_regression)

    return(summary_linear_regression)
}


test_trait_correlation = function() {
  newick_tree = "(unicorn:15,(orangutan:13,(gorilla:10.25,(human:5.5,chimp:5.5):4.75):2.75):2);"
  
  traits_at_tip = list(trait1 = c(unicorn=5, orangutan=2, gorilla=9, human=1, chimp=4), 
                       trait2 = c(unicorn=6, orangutan=5, gorilla=15, human=3, chimp=9))
  
  print(get_trait_correlation(newick_tree, traits_at_tip))
}

# test_trait_correlation()
