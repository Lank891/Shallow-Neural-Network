getAccuracy <- function(inputPath, outputPath) {
  dir <- dirname(getwd())
  temp1<-readLines(paste(dir,inputPath,sep=""))
  y1<-str_sub(temp1,start=-1)
  temp2<-readLines(paste(dir,outputPath,sep=""))
  y2<-str_sub(temp2,start=-1)
  y1<-as.integer(y1)
  y2<-as.integer(y2)
  array <- as.integer(y1-y2)
  accuracy<- ((length(y1)-sum(array != 0))/length(y1))*100
  accuracy
  error <- round(mean((y1-y2)^2),6)
  return(accuracy)
}#returns accuracy in percentage

plotDataSet <- function(inputPath, outputPath) {
  dir <- dirname(getwd())
  temp1<-readLines(paste(dir,inputPath,sep=""))
  #temp1[1]<-substr(temp1[1],4,17)
  y1<-str_sub(temp1,start=-1)
  temp2<-readLines(paste(dir,outputPath,sep=""))
  y2<-str_sub(temp2,start=-1)
  y1<-as.integer(y1)
  y2<-as.integer(y2)
  x<-seq(1,length(y1))
  attr_1<-as.integer(attr_1)
  df <- data.frame(x,y1,y2)
  error <- round(mean((y1-y2)^2),6)
  p <- ggplot(df, aes(x)) + geom_point(size = 2.5,aes(y=y1),colour="red")+geom_point(aes(y=y2),size = 1,colour="green")+ xlab("Input ID") + ylab("Class ID")
  #+ ggtitle("Plot of classes")
  #
  print(p + ggtitle(paste("Training error: ",error,sep="")))
  return(error)
  #p
  #return(p)
  #return(temp_C)
}#graphs data set and returns error

#use functions
plotDataSet("/Training/iris_training_150.txt","/Classification/output.txt")
a<-getAccuracy("/Training/iris_training_150.txt","/Classification/output.txt")
a<-stringi::stri_join(round(a,1),"%", sep="")
a

