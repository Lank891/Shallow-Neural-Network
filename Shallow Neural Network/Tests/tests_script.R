tx  <- readLines("./iris0.txt")
tx2  <- gsub(pattern = ",Iris-setosa", replace = "", x = tx)
tx2  <- gsub(pattern = ",Iris-versicolor", replace = "", x = tx2)
tx2  <- gsub(pattern = ",Iris-virginica", replace = "", x = tx2)
tx2  <- gsub(pattern = ",", replace = "\t", x = tx2)

writeLines(tx2, con="./test.txt")

tx  <- readLines("./test.txt")

tx2  <- gsub('.{2}$', '', tx)
writeLines(tx2, con="./test2.txt")

#new
require(ggplot2)
require(stringr)
x <- seq(0,8097)
temp1 <- readLines("./dataset.txt")
temp1[1]<-substr(temp1[1],4,17)
#y1<-substr(temp1,12,13)
#y1<- gsub('\t', '', y1)
y1<-str_sub(temp1,start=-1)
temp2 <- readLines("./output.txt")
#y2<-substr(temp2,12,13)
y2<-str_sub(temp2,start=-1)
#  <- gsub('.{1}$', '', temp2)
#y2<- gsub('\t', '', y2)
y1<-as.integer(y1)
y2<-as.integer(y2)
df <- data.frame(x,y1,y2)
plot.new()
plot(x,y1,type="l",col="red")
plot.new()
plot(x,y2,type="l",col="green")
#lines(x_values,as.integer(y2),col="green")
ggplot(df, aes(x)) +                    # basic graphical object
  geom_line(aes(y=y1), colour="red") +  # first layer
  geom_line(aes(y=y2), colour="green")  # second layer
