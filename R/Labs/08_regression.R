# Task 1: За допомогою вбудованої довідки вивчити параметри функції  lm() та nls()
?lm
?nls

# Task 2: Для фреймів даних airmiles, freeny, pressure, uspop дослідити залежності між даними. Побудувати рівняння регресії
library(datasets)
data("airmiles")
data("freeny")
data("pressure")
data("uspop")

# freeny
data = freeny
data
plot(data)
x = data$price.index
y = data$y
model = lm(y ~ x, data = data)
summary(model)
cor(y, predict(model))
#plot(model)
par(mfrow=c(1,1))
plot(x, y, type='b', lty=1, col='purple', ylab='Revenue', xlab='Price index')
lines(x,predict(model),col="red",lty=2,lwd=3)
#lines(x, c[1] + c[2] * x, col = 'red', type='o')


# pressure
data = pressure
data
plot(data)
x = data$temperature
y = data$pressure
model = lm(y~poly(x,3))
summary(model)
cor(y, predict(model))
#plot(model)
par(mfrow=c(1,1))
plot(x, y, type='b', lty=1, col='purple', ylab='Pressure', xlab='Temperature')
lines(x,predict(model),col="red",lty=2,lwd=3)


#install.packages("forecast")
library(forecast)

# uspop like time series
data = uspop
data
plot(data)
y = data
model = tslm(y ~ trend)
summary(model)
par(mfrow=c(1,1))
plot(forecast(model, h=10))



data = uspop
data
plot(data)
x = data[1:(length(data)-1)]
y = data[2:length(data)]
model = lm(y~x)
summary(model)
cor(y, predict(model))
#plot(model)
par(mfrow=c(1,1))
plot(x, y, type='b', lty=1, col='purple', ylab='X[i+1]', xlab='X[i]')
lines(x,predict(model),col="red",lty=2,lwd=3)

# airmiles like time series
data = airmiles
data
plot(data)
y = data
model = tslm(y ~ trend)
summary(model)
par(mfrow=c(1,1))
plot(forecast(model, h=10))


data = airmiles
data
plot(data)
x = data[1:(length(data)-1)]
y = data[2:length(data)]
model = lm(y~x)
summary(model)
cor(y, predict(model))
#plot(model)
par(mfrow=c(1,1))
plot(x, y, type='b', lty=1, col='purple', ylab='X[i+1]', xlab='X[i]')
lines(x,predict(model),col="red",lty=2,lwd=3)



# Task 3: Для набору даних x та y з лабораторної №7 дослідити залежність між даними та побудувати рівняння регресії. Для цього необхідно:
# - побудувати кореляційне поле вибраних даних
# - по виду розташування точок на кореляційному полі оцінити вид залежності
# - обчислити вибірковий коефіцієнт кореляції для вибраних даних
# - на основі виду кореляційного поля та значення коефіцієнту кореляції зробити висновок про тип рівняння регресії (лінійна абр нелінійна регресія)
# - побудувати регресійну модель за допомогою функцій lm() або nls()
# - виписати рівняння регресії з числовими значеннями коефіцієнтів регресії
# - оцінити якість побудованої моделі регресії

x=c(1,3,6,8,9,10,11,12,14,16,17,18,20,22,23,24,25,27,29,30) 
y=c(32,36,42,46,48,50,52,54,58,62,64,66,70,74,76,78,80,84,86,88)

# - побудувати кореляційне поле вибраних даних
plot(x, y)

# - по виду розташування точок на кореляційному полі оцінити вид залежності
# Висновок: залежність лінійна

# - обчислити вибірковий коефіцієнт кореляції для вибраних даних
cor(x, y)
# - на основі виду кореляційного поля та значення коефіцієнту кореляції зробити висновок про тип рівняння регресії (лінійна абр нелінійна регресія)
# Висновок: лінійна регресія

# - побудувати регресійну модель за допомогою функцій lm() або nls()
model = lm(y~x)
summary(model)

# - виписати рівняння регресії з числовими значеннями коефіцієнтів регресії
# Висновок: y = 1.963 * x + 30.405

# - оцінити якість побудованої моделі регресії
summary(model)
par(mfrow=c(1,1))
plot(x, y, type='b', lty=1, col='purple')
lines(x,predict(model),col="red",lty=2,lwd=3)




# https://www.r-bloggers.com/2015/09/how-to-perform-a-logistic-regression-in-r/
# https://www.r-bloggers.com/2014/12/a-small-introduction-to-the-rocr-package/
# Importing the dataset
# https://archive.ics.uci.edu/ml/dfs/statlog+(german+credit+data)
# Session > Set Working Directory > To Source File location
# if cannot open file 'german.data-numeric': No such file or directory
set.seed(12345)
df = read.table('german.data-numeric', header=FALSE)
cols = colnames(df)
feature_cols = cols[-length(cols)]
colors <- c("red", "green")

# Taking care of missing data
any(is.na(df)) # no missing data
# Scaling data
df[,ncol(df)] = factor(df[,ncol(df)],
                       levels = c(1, 2),
                       labels = c(0, 1))
for (col in cols[-length(cols)]){
  df[, col] = as.numeric(df[, col])
  df[, col] = scale(df[, col])
}
df # check dataset

# Splitting the df into the Training, Test, Validation sets
spec = c(train = .4, test = .3, validate = .3)

g = sample(cut(
  seq(nrow(df)), 
  nrow(df)*cumsum(c(0,spec)),
  labels = names(spec)
))

res = split(df, g)
train_set <- res$train
test_set <- res$test
val_set <- res$validate
# Check splitting
sapply(res, nrow)/nrow(df)

# Models
model1 <- glm(V25 ~.,family=binomial(link='logit'),data=train_set)
model2 <- glm(V25 ~ V1 + V2 + V3 + V6 + V9 + V17, family=binomial(link='logit'),data=train_set)
summary(model1)
summary(model2)


models <- list(model1, model2)


#install.packages("ROCR")
library(ROCR)
assess_model_roc <- function(models, test_set){
  for(i in 1:length(models))
  {
    model <- models[[i]]
    p <- predict(model, newdata=subset(test_set,select=feature_cols), type="response")
    pr <- prediction(p, test_set$V25)
    prf <- performance(pr, measure = "tpr", x.measure = "fpr")
    plot(prf, col=colors[i])
    par(new=TRUE)
    auc <- performance(pr, measure = "auc")
    auc <- auc@y.values[[1]]
    print(auc)
  }
}
assess_model_roc(models, train_set)
dev.off(dev.list()["RStudioGD"])
assess_model_roc(models, test_set)
dev.off(dev.list()["RStudioGD"])
assess_model_best_threshold <- function(models, test_set){
  for(i in 1:length(models))
  {
    model <- models[[i]]
    p <- predict(model, newdata=subset(test_set,select=feature_cols), type="response")
    pr <- prediction(p, test_set$V25)
    prf <- performance(pr, "cost", cost.fp = 150, cost.fn = 1000)
    plot(prf, col=colors[i])
    par(new=TRUE)
    threshold = pr@cutoffs[[1]][which.min(prf@y.values[[1]])]
    l = paste(paste("Best threshold for model", i), ":")
    print(paste(l, threshold))
    print(paste("Min cost:", min(prf@y.values[[1]])))
    
  }
}

assess_model_best_threshold(models, test_set)




