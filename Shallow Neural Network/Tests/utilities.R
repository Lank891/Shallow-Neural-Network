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
  print(p + ggtitle(paste("Training error: ",error,sep="")))
  return(error)
}#graphs data set and returns error

#graph errors
errorList<-list()
errorList[length(errorList)+1]<-plotDataSet("/Training/iris_training_150.txt","/Classification/output.txt")
#epochs<-c(100, 80, 120, 150,170,160, 155, 153,150,50,10,200,300,500,400,800,5,450,190,195,195,170
# ,25,135,60,155,40)
learning_rate = c(0.5,0.5,0.1,0.1, 0.01,0.01,0.001,0.001, 0.01,0.05,
                  0.02,0.005,0.015, 0.017,0.3,0.4)
df<-data.frame(learning_rate,errors=unlist(errorList))
plot(x=df$learning_rate, y=df$errors, xlim=c(0,0.5),ylim = c(0,1), main="Iris training error",sub="e=50,bs=10,m=0.1,af=s,hl=[3]",
     xlab="learning_rate",
     ylab="errors",)
