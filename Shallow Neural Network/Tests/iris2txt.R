require(stringr)
##convert original iris to the correct input format
tx  <- readLines("../Datasets/iris.txt")
tx2  <- gsub(pattern = "Iris-setosa", replace = "1", x = tx)
tx2  <- gsub(pattern = "Iris-versicolor", replace = "2", x = tx2)
tx2  <- gsub(pattern = "Iris-virginica", replace = "3", x = tx2)
tx2  <- gsub(pattern = ",", replace = "\t", x = tx2)
writeLines(tx2, con="../Training/iris_training_150.txt")

#convert iris to iris data set for classification
tx3  <- readLines("../Training/iris_training_150.txt")
tx3  <- gsub('.{2}$', '', tx3)
writeLines(tx3, con="../Classification/iris_testFromTraining_150.txt")

##convert iris with all attributes to iris with the petal width
tx  <- readLines("../Training/iris_training_150.txt")
tx2  <- str_sub(tx,start=-9)
tx2  <- gsub(pattern = ",", replace = "\t", x = tx2)
writeLines(tx2, con="../Training/irisPetals_training_150.txt")

#convert iris to iris data set for classification
tx  <- readLines("../Training/irisPetals_training_150.txt")
tx  <- gsub('.{2}$', '', tx)
writeLines(tx, con="../Classification/irisPetals_testFromTraining_150.txt")

tx3  <- readLines("../Training/Skin_NonSkin.txt")
tx3  <- gsub('.{2}$', '', tx3)
writeLines(tx3, con="../Classification/Skin_NonSkin_test.txt")
