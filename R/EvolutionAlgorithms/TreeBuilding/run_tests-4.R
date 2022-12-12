tests_dir <- "C:/Users/kam/Documents/Katya/R/L3/Homework_3/student_test_suite-4"
if(tests_dir == "path/to/tests") stop("tests_dir needs to be set to a proper path")

library("RUnit")
source("C:/Users/kam/Documents/Katya/R/L3/Homework_3/CB_HW3_TreeBuilding_skeleton.r")

testsuite <- defineTestSuite("HW", tests_dir)
currentdir <- getwd()
setwd(tests_dir)

out <- runTestSuite(testsuite, verbose=TRUE)
printTextProtocol(out)

setwd(currentdir)
