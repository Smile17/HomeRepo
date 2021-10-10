#install.packages("fastAdaboost")
# https://cran.r-project.org/web/packages/fastAdaboost/fastAdaboost.pdf

#install.packages("C50")
# https://cran.r-project.org/web/packages/C50/C50.pdf

#install.packages("randomForest")
# https://cran.r-project.org/web/packages/randomForest/randomForest.pdf


library(mlbench)
data(BreastCancer)

N = dim(BreastCancer)[1]

d1 = BreastCancer[,-1]

ind = sample(1:N, size = round(N * 0.8), replace=FALSE)
trainData = d1[ind, ]
testData  = d1[-ind, ]

# --------- C50 -----------
library(C50)
model = C5.0(Class ~., data=trainData)
summary(model)
prediction = predict(model, trainData)
table(prediction, trainData$Class)

prediction = predict(model, testData)
table(prediction, testData$Class)
print(sprintf("Accuracy on the test set=%3.3f", sum(prediction==testData$Class)/length(prediction)));

#----------- adaBoost ------------
library(fastAdaboost)
model = adaboost(Class ~ ., data=trainData, nIter=100)
prediction = predict(model, trainData)
table(prediction$class, trainData$Class)
prediction = predict(model, testData)
table(prediction$class, testData$Class)
print(sprintf("Accuracy on the test set=%3.3f", sum(prediction$class==testData$Class)/length(prediction$class)));

#---------- randomForest --------
model = randomForest(Class ~ ., data=trainData, na.action=na.omit)
prediction = predict(model, trainData)
table(prediction, trainData$Class)
prediction = predict(model, testData)
table(prediction, testData$Class)
print(sprintf("Accuracy on the test set=%3.3f", sum(prediction==testData$Class, na.rm=TRUE)/length(prediction)));


