tests_dir <- "C:/Users/kam/Documents/Katya/R/Homework_5_Comparative Methods/student_test_suite-4"
if(tests_dir == "path/to/tests") stop("tests_dir needs to be set to a proper path")

library("RUnit")
source("C:/Users/kam/Documents/Katya/R/Homework_5_Comparative Methods/CB_HW5_ComparativeMethods_skeleton.r")

original_dir <- getwd()
setwd(tests_dir)

testsuite <- defineTestSuite("HW", ".")
out <- runTestSuite(testsuite)
printTextProtocol(out)

setwd(original_dir)
