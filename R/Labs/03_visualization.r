# Exercise 1:
# Create function, which will compute geometric mean from the vector values. (/1 pt.)
geom_mean <- function(v)
{
  res <- 1
  for(value in v)
    res <- res * value
  res^(1/length(v))
}
vec <- c(3, 9, 27) # answer: 9
print(geom_mean((vec)))

# Create a function that given a vector and an integer will return how many times the integer appears inside the vector. (2 pts.)
freq <- function(v, val)
{
  sum(v == val) # frequency is equal to count of TRUE in vector v == val
}
vec <- c(3, 9, 27, 9, -4, 3)
print(freq(vec, 9)) # Answer: 2
print(freq(vec, 27)) # Answer: 1
print(freq(vec, -34)) # Answer: 0

# Create a function that given an integer will calculate how many divisors it has (other than 1 and itself). 
# Make the divisors appear by screen. (3 pts.)
divisors <- function(num)
{
  res <- 2:(num / 2)
  res[num %% res == 0]
}
print(divisors(10)) # Answer: 2, 5
print(divisors(144)) # Answer: 2  3  4  6  8  9 12 16 18 24 36 48 72
print(divisors(17)) # Answer: empty
print(length(divisors(17)))

# Create a function that given one word, return the position of word’s letters on letters vector.  
# For example, if the word is	abba’, the function will return 1 2 2 1. (3 pts.)
letters_position <- function(word)
{
  v <- strsplit(word, "")[[1]]
  res <- sapply(v, function(x) match(x, letters))
  unname(res)
}
letters_position("abba")
letters_position("cabcba")

# Exercise 2:
# Load the islands dataset
require(graphics)
islands
v <- islands

# Apply your own defined function to compute geometric mean of this vector
geom_mean <- function(v)
{
  res <- 1
  for(value in v)
    res <- res * value
  res^(1/length(v))
}
print(geom_mean((v)))

# Using the function range(), obtain the size of the biggest island and the size of the smallest island
print(range(v))
# Find the interquartile range of islands
print(IQR(v))

# Create an histogram of islands with the following properties
# Showing the frequenct of each group
hist(v,
     main="Size frequency of islands",
     col="darkmagenta",
)
# Showing the proportion of each group
hist(v,
     main="Size of islands",
     col="darkmagenta",
     freq = FALSE
)
# Create box-plot and stem and leaf plot of islands and interpret them
boxplot(v,
        main = "Box plot of islands",
        col = "orange",
        horizontal = TRUE)
# Interpretation: 
# Data contains a lot of small values, that's why quartiles, minimum and maximum values are nearby shown as orange rectangle and whiskers.
# Also, there are some outliers shown by points
# So, we can make a conclusion that there is a lot of islands with similar area and not too many bigger islands.
stem(v)
# Interpretation: 
# Stem and leaf plot shows that there are a lot of small islands with area less than 1000, several middle islands (6, to be precise) with
# area between 2000 and 12000, and 1 huge island (it is Asia).
