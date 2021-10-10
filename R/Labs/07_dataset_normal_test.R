# Task:
#Проаналізувати дані з фрейму даних mtcars та iris, щодо відповідності їх нормального розподілу. Для цього потрібно проаналізувати признаки з фрейму за допомогою тесту Шапіро-Уілка та виділити ті признаки, які розподілені за нормальним законом.

# Upload data
library(datasets)
data("mtcars")
data("iris")

normal_analysis <- function(data, n, m)
{
  cols = colnames(data)
  print(length(cols))
  par(mfrow=c(n, m))
  for (col in cols)
  {
    x = data[,col]
    if(is.numeric(x))
    {
      hist(x, breaks = 10, main=col, density=20, col='purple')
      y = shapiro.test(x)
      mtext(paste0("p-value: ", round(y$p.value, 5)), side=3)
      print(paste0("p-value: ", y$p.value))
    }
  }
}

# Analysis of mtcars
?mtcars # View additional documentation about data
data = mtcars
normal_analysis(data, 3, 4)

# Analysis of iris
?iris # View additional documentation about data
data = iris
normal_analysis(data, 1, 4)




