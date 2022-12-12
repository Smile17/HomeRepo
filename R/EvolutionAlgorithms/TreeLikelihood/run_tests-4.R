tests_dir <- "C:/Users/kam/Documents/Katya/R/L4/student_test_suite-4"
if(tests_dir == "path/to/tests") stop("tests_dir needs to be set to a proper path")

library("RUnit")
source("C:/Users/kam/Documents/Katya/R/L4/CB_HW4_TreeLikelihood_skeleton.r")

testsuite <- defineTestSuite("HW", tests_dir)
currentdir <- getwd()
setwd(tests_dir)

out <- runTestSuite(testsuite)
printTextProtocol(out)

setwd(currentdir)

