a = matrix(c(7,2,6,14,9,10,8,4,12,9,8,15), ncol=4, nrow=3)
b = matrix(c(10,15,2,6,11,6,13,3), ncol=2, nrow=4)
print(a %*% b)

d = matrix(c(7,2,3,7,10,6,5,7), ncol=2, nrow=4)
e = matrix(c(1,2,5,10,3,5), ncol=3, nrow=2)
print(t(d %*% e))
print(t(e) %*% t(d))

h = matrix(c(5,12,3,2,13,8,13,15,11,8,10,4,2,11,15,9), ncol=4, nrow=4)
h_inv = solve(h)
print(h_inv)
print(h %*% h_inv)

k = matrix(c(63,69,58,32,20,41,32,31,30), ncol=3, nrow=3)
l = matrix(c(36,61,43,35,39,67,65,36,25), ncol=3, nrow=3)
print(solve(k %*% l))
print(solve(l) %*% solve(k))


# Generate a matrix A with normal distribution and a matrix B with binominal distribution
v <- rnorm(45, mean = 5, sd = 7) # variance = 49
A <-matrix(v, nrow = 5, ncol=9)
print(A)
v <- rbinom(45, size = 20, prob = 0.4)
B <-matrix(v, nrow = 9, ncol=5)
print(A)

# Generate a matrix C
v <- rnorm(25, mean = 5, sd = 7) # variance = 49
C <-matrix(v, nrow = 5, ncol=5)
print(C)
#Prove when a matrix C is multiplied by the identity matrix, the product is the same as the original matrix
I <- diag(5)
print(C %*% I)
print(I %*% C)
print(C - C %*% I)  # here should be zero matrix

# Prove that when matrices A and B are being multiplied by a scalar element s, the order in which multiplication takes place can be disregarded
s <- 5
print(A)
print(s * A)
print(A * s)
print(A * s - s * A) # here should be zero matrix

print(B)
print(s * B)
print(B * s)
print(B * s - s * B) # here should be zero matrix

# Compute expressions without using loops
# Expression 1
a <- seq(2, 20, 2) / seq(3, 21, 2)
b <- 1 + sum(cumprod(a))
print(b)

# Expression 2
c <- outer(1:10, 1:5 , FUN=function(i, j) (i^5/(10 + j ^ i)))
print(sum(c))

# Expression 3
d <- outer(1:10, 1:10 , FUN=function(i, j){
  ifelse(i >= j, (i^5/(10 + j ^ i)), 0)
})
print(sum(d))

# Compute expressions with loops: just to check previous results
# Expression 1
sum <- 1
for(i in 1:10)
{
  e <- 1
  for(j in 1:i)
  {
    e <- e * (2 * j) / (2 * j + 1)
  }
  sum <- sum + e
}
print(sum)

# Expression 2
sum <- 0
for(i in 1:10)
{
  for(j in 1:5)
  {
    sum <- sum + (i^5) / (10 + j ^ i)
  }
}
print(sum)

# Expression 3
sum <- 0
for(i in 1:10)
{
  for(j in 1:i)
  {
    sum <- sum + (i^5) / (10 + j ^ i)
  }
}
print(sum)
