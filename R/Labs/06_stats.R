# Task 1: Згенерувати випадкові вибірки випадкових величин, об'ємом 50 елементів. Побудувати гістограми варіаційних рядів для отриманих наборів даних
size <- 50
par(mfrow=c(2,4))

y <- rnorm(size, mean = 2.5, sd = 2)
hist(y, main = "Gaussian Distribution")
y <- rbinom(size, 100, prob = 0.5)
hist(y, main = "Binomial Distribution")
y <- rpois(size, 2)
hist(y, main = "Poiss Distribution")
y <- rgeom(size, 0.6)
hist(y, main = "Geom Distribution")
y <- rhyper(size, 15, 10, 4)
hist(y, main = "HyperGeom Distribution")
y <- rchisq(size, 5)
hist(y, main = "ChiSquare Distribution")
y <- rf(size, 5, 10)
hist(y, main = "Fisher Distribution")
y <- rt(size, 5)
hist(y, main = "Student Distribution")

par(mfrow=c(1,1))

# Task 2: В кошику знаходять 20 кульок: 6 білих та 14 червоних. З кошику без повернення виймається 6 кульок. Побудувати розподілення ймовірностей випадкової величини - числа білих кульок, які були вийняті. Побудувати гістограму отриманого розподілення ймовірностей.
n_white <- 6
n_red <- 14
n_get <- 6
ph = numeric(n_get+1)
for(i in 0:n_get)
  ph[i+1] <- dhyper(i, n_white, n_red, n_get)
ph
sum(ph)
barplot(ph, names=as.character(0:n_get), ylim=c(0,0.4), density=16, main="Box problem")

# Task 3: Після відповіді на питання екзаменаційного білету викладач задає студенту додаткові питання. Викладач перестає задавати питання як тільки студент не може відповісти на задане питання. Ймовірність, що студент відповість на задане питання - 0.9.
# - побудувати закон розподілу ймовірностей випадкової дискретної ведичини Х - числа додаткових питань
# - знайти наймовірніше число додатніх питань
prob <- 1 - 0.9
n <- 100
X = numeric(n+1)
for(i in 0:n)
  X[i+1] <- dgeom(i, prob)
X
sum(X)
barplot(X, names=as.character(0:n), ylim=c(0,0.1), density=16, main="Exam problem")

expected <- 0
for(i in 0:n)
  expected <- expected + X[i+1] * (i+1)
expected
text(n * 0.6, 0.1, adj=c(0, 1), paste0("Expected value: ", round(expected, 4)))
# Теоретично математичне сподівання для геометричного розподілу рівне 1/prob = 1/0.1 = 10


# Task 1: Згенеруйте послідовність випадкових 100 чисел, які мають біноміальний закон розподілення ймовірностей
set.seed(0)
x = rbinom(100, 100, 0.5); x

# Task 2 Для отриманої послідовності виконайте наступне:
#  - побудуйте інтервальний варіаційний ряд
#  - обчисліть вибіркове середнє, дисперсію, середнє квадратичне відхилення
#  - побудуйте гістограму розподілу ймовірностей та кругову діаграму
#  - обчисліть та побудуйте графічно емпіричну функцію розподілу
histinfo = hist(x, main = "Binomial Distribution", breaks = 10); histinfo
#cut_x = cut(x, breaks=c(-1, 40, 45, 50, 55, 60, 70)); cut_x
cut_x = cut(x, breaks=histinfo$breaks); cut_x
table_x = table(cut_x); table_x

# вибіркове середнє
m = mean(x); m
# вибіркова дисперсія
var(x)
# середнє квадратичне
sd(x)

summary(x) # Характеристики розподілу: min, max,  1, 3 квартилі, медіана та середнє значення

#  - побудуйте гістограму розподілу ймовірностей та кругову діаграму
par(mfrow=c(1,1))
barplot(height = table_x, density=16, col='purple')
pie(table_x)

#  - обчисліть та побудуйте графічно емпіричну функцію розподілу
Fn = ecdf(x); Fn
summary(Fn)

par(mfrow=c(1, 1))
plot(Fn, verticals=TRUE, col.points="blue", col.hor="red", col.vert="bisque")

Fn(x)




