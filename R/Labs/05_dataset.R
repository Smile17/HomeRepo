# Task 1: Написати власну функцію для сортування елементів вектора. Для перевірки роботи функції виконати сортування вектора випадкових чисел. Вхід функції - вектор, який треба відсортувати. Вихід функції - відсортований вектор
# Sort descending
sort_custom <- function(v)
{
  i <- 1
  n <- length(v)
  while(i <= n)
  {
    max_i <- which.max(v[i:n]) + i - 1
    tmp <- v[max_i]
    v[max_i] <- v[i]
    v[i] <- tmp
    i <- i + 1
  }
  v
}

v <- sample.int(100, 10)
print(v)
v <- sort_custom(v)
print(v)

# Task 2: Написати власну функцію пошуку заданого елемента у векторі. Вхід функції - вектор, елемент. Вихід функції - індекс елемента у векторі.
find_value <- function(v, x)
{
  i <- 1
  n <- length(v)
  res <- -1
  while(i <= n)
  {
    if(v[i]==x)
    {
      res <- i
      break;
    }
    i <- i + 1
  }
  res
}
v <- c(2, 5, 10, 23, -4)
print(find_value(v, 5))
print(find_value(v, -7))

# Task 3 - 5
library(datasets)
data("airquality")
?airquality # View additional documentation about data
data <- airquality # View data
data
# Task 3: Написати скрипт для визначення середньої температури повітря за кожний місяць. Записати результат розрахунків у файл.
res <- aggregate(data$Temp, list(data$Month), mean)
#colnames(res)
names(res) <- c('Month', 'Mean_Temp')
print(res)
write.table(res, "3.txt")

# Task 4: Написати скрипт для визначення середньої швидкості вітру при показнику сонячної радіації більше 100.
data_subset <- subset(data, Solar.R > 100)
data_subset
mean(data_subset$Wind)

# Task 5: Написати скрипт для визначення у якому місяці середній зміст озону маскимальний.
data_subset <- subset(data, !is.na(Ozone)) # drop na rows
data_subset
res <- aggregate(data_subset$Ozone, list(data_subset$Month), mean)
#colnames(res)
names(res) <- c('Month', 'Mean_Ozone')
print(res)
res[which.max(res$Mean_Ozone),]$Month
