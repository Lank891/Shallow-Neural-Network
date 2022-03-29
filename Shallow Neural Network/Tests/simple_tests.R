require(ggplot2)
require(stringr)
#plot data against one of the attributes
plotDataSetAttribute <- function(inputPath, outputPath,attributeID) {
  dir <- dirname(getwd())
  temp1<-readLines(paste(dir,inputPath,sep=""))
  #temp1[1]<-substr(temp1[1],4,17)
  y1<-str_sub(temp1,start=-1)
  attr_1<-sapply(str_split(temp1,"\t"),"[",attributeID)
  attr_1<- gsub('\t', '', attr_1)
  temp2<-readLines(paste(dir,outputPath,sep=""))
  y2<-str_sub(temp2,start=-1)
  y1<-as.integer(y1)
  y2<-as.integer(y2)
  attr_1<-as.integer(attr_1)
  x<-seq(1,length(attr_1))
  df <- data.frame(x,y1,y2)
  p <- ggplot(df, aes(attr_1)) + geom_point(size = 2.5,aes(y=y1),colour="red")+geom_point(aes(y=y2),size = 1,colour="green")
  p
}


