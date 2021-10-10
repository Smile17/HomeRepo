# Print all the odd numbers which are less than 16 using while() loop
i <- 1
while (i < 16) {
  print(i)
  i = i+2
}

# Write a while loop that points out standard random normal numbers (use rnorm()) but stops (breaks) if you get a number smaller than -1
while (TRUE) {
  value <- rnorm(1, mean = 0, sd = 1)
  print(value)
  if (value < -1) break
}
# Using next adapt the loop from last exercise so that does not print positive numbers
while (TRUE) {
  value <- rnorm(1, mean = 0, sd = 1)
  if (value > 0) next
  print(value)
  if (value < -1) break
}

# Find the biggest Fibonacci number which is less than 5000 using the repeat() loop
max <- 5000
a <- 1
b <- 1
repeat {
  nth = a + b
  #print(nth) # uncomment for testing
  # update values
  a <- b
  b <- nth
  if (nth > max) break
}
print(a)

# How many Fibonaacci numbers are in range between 350 and 10000
min <- 350
max <- 10000
a <- 1
b <- 1
count <- 0
repeat {
  nth = a + b
  #print(nth) # uncomment for testing
  # update values
  a <- b
  b <- nth
  if (nth > max) break
  if (nth > min) count <- count + 1
}
print(count)

# Find the smallest Fibonacci number which is divided by 6 without remainder
a <- 1
b <- 1
repeat {
  nth = a + b
  #print(nth) # uncomment for testing
  # update values
  a <- b
  b <- nth
  if (nth %% 6 == 0) break
}
print(b)

