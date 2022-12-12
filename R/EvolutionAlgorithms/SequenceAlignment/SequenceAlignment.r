#####################################################
#######        COMPUTATIONAL BIOLOGY         ########
#######             HOMEWORK 1               ########
#####################################################
#                                                   #
# Implement the pairwise alignment algorithms       #
# Needleman-Wunsch and Smith-Waterman.              #
#                                                   #
#####################################################
#####################################################

# In all functions the following parameters are the same:
# seqA: the first sequence to align
# seqB: the second sequence to align
# score_gap: score for a gap
# score_match: score for a character match
# score_mismatch: score for a character mismatch
# local: (logical) True if alignment is local, False otherwise

init_score_matrix = function(nrow, ncol, local, score_gap) {
    # Initialize the score matrix with zeros.
    # If the alignment is GLOBAL, the leftmost column and the top row will have incremental gap scores,
    # i.e. if the gap score is -2 and the number of columns is 4, the top row will be [0, -2, -4, -6].
    # nrow: (numeric) number of rows in the matrix
    # ncol: (numeric) number of columns in the matrix

    score_matrix <- matrix(0, nrow, ncol)
    
    if(!local) {
      score_matrix[1, ] <- seq(0, (ncol - 1) * score_gap, score_gap)
      score_matrix[, 1] <- seq(0, (nrow - 1) * score_gap, score_gap)
      
    }
    
    # Return the initialized empty score matrix
    # score_matrix: (numeric) nrow by ncol matrix
    return(score_matrix)
}

init_path_matrix = function(nrow, ncol, local) {
    # Initialize the path matrix with empty values ("").
    # Additionally, for GLOBAL alignment (i.e. local==FALSE), make the first row
    # have "left" on all positions except 1st, and make the first column
    # have "up" on all positions except 1st.
    # nrow: (numeric) number of rows in the matrix
    # ncol: (numeric) number of columns in the matrix

    path_matrix <- matrix("-", nrow, ncol)
    if(!local) {
      path_matrix[1,2:ncol] <- rep("left", ncol - 1)
      path_matrix[2:nrow, 1] <- rep("up", nrow - 1)
      
    }

    # Return the initialized empty path matrix
    # path_matrix: (character) nrow by ncol matrix
    return(path_matrix)
}

get_best_score_and_path = function(row, col, nucA, nucB, score_matrix, score_gap, score_match, score_mismatch, local) {
    # Compute the score and the best path for a particular position in the score matrix
    # nucA: (character) nucleotide in sequence A
    # nucB: (character) nucleotide in sequence B
    # row: (numeric) row-wise position in the matrix
    # col: (numeric) column-wise position in the matrix
    # score_matrix: (double) the score_matrix that is being filled out
  
    i <- row
    j <- col
    pathes <- c("diag", "left", "up")
    scores <- c(score_matrix[i - 1, j - 1], score_matrix[i, j - 1] + score_gap, score_matrix [i - 1, j] + score_gap)
    if(nucA==nucB)
      scores[1] <- scores[1] + score_match
    else
      scores[1] <- scores[1] + score_mismatch
    score <- max(scores)
    idx <- which(scores == score)[1]
    path <- pathes[idx]
    
    if(local && score < 0)
    {
      score <- 0
      path <- "-"
    }
    
    # Return the best score for the particular position in the score matrix
    # In the case that there are several equally good paths available, return any one of them.
    # score: (numeric) best score at this position
    # path: (character) path corresponding to the best score, one of ["diag", "up", "left"] in the global case and of ["diag", "up", "left", "-"] in the local case
    return(list("score"=score, "path"=path))
}

fill_matrices = function(seqA, seqB, score_gap, score_match, score_mismatch, local, score_matrix, path_matrix) {
    # Compute the full score and path matrices
    # score_matrix: (numeric)  initial matrix of the scores
    # path_matrix: (character) initial matrix of paths

    for (i in 2:nrow(score_matrix)){
      for (j in 2:ncol(score_matrix)){
        nucA <- substr(seqA, i-1, i-1)
        nucB <- substr(seqB, j-1, j-1)
        best_score_path <- get_best_score_and_path(i, j, nucA, nucB, 
                                             score_matrix, score_gap, score_match, 
                                             score_mismatch, local)
        score_matrix[i, j] = best_score_path$score
        path_matrix[i, j] = best_score_path$path
      }
    }

    # Return the full score and path matrices
    # score_matrix: (numeric) filled up matrix of the scores
    # path_matrix: (character) filled up matrix of paths
    return(list("score_matrix"=score_matrix, "path_matrix"=path_matrix))
}

get_best_move = function(nucA, nucB, path, row, col) {
    # Compute the aligned characters at the given position in the score matrix and return the new position,
    # i.e. if the path is diagonal both the characters in seqA and seqB should be added,
    # if the path is up or left, there is a gap in one of the sequences.
    # nucA: (character) nucleotide in sequence A
    # nucB: (character) nucleotide in sequence B
    # path: (character) best path pre-computed for the given position
    # row: (numeric) row-wise position in the matrix
    # col: (numeric) column-wise position in the matrix

    if (path == "diag") {
      newrow = row - 1
      newcol = col - 1
      char1 = nucA
      char2 = nucB
    }
    else if (path == "left") {
      newrow = row
      newcol = col - 1
      char1 = '-'
      char2 = nucB
    }
    else if (path == "up") {
      newrow = row - 1
      newcol = col
      char1 = nucA
      char2 = '-'
    }

    # Return the new row and column and the aligned characters
    # newrow: (numeric) row if gap in seqA, row - 1 otherwise
    # newcol: (numeric) col if gap in seqB, col - 1 otherwise
    # char1: (character) '-' if gap in seqA, appropriate character if a match
    # char2: (character) '-' if gap in seqB, appropriate character if a match
    return(list("newrow"=newrow, "newcol"=newcol, "char1"=char1, "char2"=char2))
}

get_best_alignment = function(seqA, seqB, score_matrix, path_matrix, local) {
    # Return the best alignment from the pre-computed score matrix
    # score_matrix: (numeric) filled up matrix of the scores
    # path_matrix: (character) filled up matrix of paths

    alignment <- c("", "")
    
    if (local) {
      idxs <- which(score_matrix == max(score_matrix), arr.ind = TRUE)
      idx1 <- idxs[[1,1]]
      idx2 <- idxs[[1,2]]
    } 
    else {
      idx1 <- nrow(score_matrix)
      idx2 <- ncol(score_matrix)
    }   
    
    score <- score_matrix[idx1,idx2]
    
    if(local) {
      stop_condition <- function(idx1, idx2) {
        score_matrix[idx1,idx2] > 0
      }
    }
    else
    stop_condition <- function(idx1, idx2) {
      (idx1 > 1 || idx2 > 1)
    }
    
    while (stop_condition(idx1, idx2)) {
      path <- path_matrix[idx1, idx2]
      move <- get_best_move(substr(seqA, idx1-1, idx1-1), 
                           substr(seqB, idx2-1, idx2-1), path, idx1, idx2)
      idx1 <- move$newrow
      idx2 <- move$newcol
      alignment[1] <- paste0(move$char1, alignment[1])
      alignment[2] <- paste0(move$char2, alignment[2])
    }
    alignment <- c(alignment[1], alignment[2])

    # Return the best score and alignment (or one thereof if there are multiple with equal score)
    # score: (numeric) score of the best alignment
    # alignment: (character) the actual alignment in the form of a vector of two strings
    return(list("score"=score, "alignment"=alignment))
}

align = function(seqA, seqB, score_gap, score_match, score_mismatch, local) {
    # Align the two sequences given the scoring scheme
    # For testing purposes, use seqA for the rows and seqB for the columns of the matrices
    nA <- nchar(seqA)
    nB <- nchar(seqB)
    
    # Initialize score and path matrices
    score_matrix <- init_score_matrix(nA+1, nB+1, local, score_gap)
    path_matrix <- init_path_matrix(nA+1, nB+1, local)
    
    # Fill in the matrices with scores and paths using dynamic programming
    matrices <- fill_matrices(seqA, seqB, score_gap, score_match, score_mismatch, local, score_matrix, path_matrix)
    score_matrix <- matrices$score_matrix
    path_matrix <- matrices$path_matrix
    
    # Get the best score and alignment (or one thereof if there are multiple with equal score)
    result <- get_best_alignment(seqA, seqB, score_matrix, path_matrix, local)
    
    # Return the best score and alignment (or one thereof if there are multiple with equal score)
    # Returns the same value types as get_best_alignment
    return(result)
}

test_align = function() {
    seqA = "TCACACTAC"
    seqB = "AGCACAC"
    score_gap = -2
    score_match = +3
    score_mismatch = -1
    local = F
    result = align(seqA, seqB, score_gap, score_match, score_mismatch, local)
    print(result$alignment)
    print(result$score)
}

test_align()