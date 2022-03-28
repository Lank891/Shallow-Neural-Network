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
require(grid)
grob <- grobTree(textGrob(paste("Training error:", round(train.err.1,3)), x=0.1,  y=0.5, hjust=0,
                          gp=gpar(col="red", fontsize=13, fontface="italic")))
# Plot
p <- ggplot(df, aes(x)) +    geom_point(size = 2.5,aes(y=y1),colour="red")                # basic graphical object
  #geom_line(aes(y=y1),size = 2.5, colour="red")
#p+annotation_custom(grob)
p+geom_point(aes(y=y2),size = 1,colour="green")
  + ggtitle("Plot of classes") +
  xlab("Input ID") + ylab("Class ID")
#text(500, 1.5, label=paste("Training error:", round(train.err.1,3)))


#+ labs(fill = "Classes")# second layer
# first layer
#geom_line(aes(y=y2),size = 1, colour="green")
#p <- df %>%
  #ggplot(aes(x, classID, color = class)) +
 # geom_line(aes(size = class)) +
 # scale_size_manual(values = c(y1 = 1, y2 = 0.5))


vacc <- data.frame(catgry=rep(c("Training Classes", "Predicted Classes"), each=2),
                   dose=rep(df["x"],2),
                   slots=df["y1"]+df["y2"])

# Plotting basic line with multiple groups
plt <- ggplot(data=df, aes(x=dose, y=slots, group=catgry))+
  geom_line()+
  geom_point(color="red", size=3)+
  labs(x="Doses",y="Free Slots")+
  ggtitle("Vaccine Details")
plt
# Adding legends
plt+geom_line(aes(color=catgry))

#TRAIN AND TEST ERRORS
xlim = range(x);
ylim = range(c(y1,y2))
train.err.1 = mean((y1-y2)^2)
l <- as.integer(y1-y2)
sum(l != 0)/length(df$attr_1)
l <- lapply(l, function(x) {x[x!=0]})
#test.err.1 = mean((y0-y0hat.1)^2)
inputID<-x
classesID<-y1
#par(mfrow=1)
plot(inputID, classesID, xlim=xlim, ylim=ylim, main="Training data")
lines(inputID, y2, col=2, lwd=2)
text(500, 1.5, label=paste("Training error:", round(train.err.1,3)))

#plot(x0, y0, xlim=xlim, ylim=ylim, main="Test data")
#lines(x0, y0hat.1, col=3, lwd=2)
#text(0, -6, label=paste("Test error:", round(test.err.1,3)))


#different values
temp1 <- readLines("./dataset.txt")
temp1[1]<-substr(temp1[1],4,17)
y1<-str_sub(temp1,start=-1)
attr_1<-str_sub(temp1,start=1, end=3)
attr_1<- gsub('\t', '', attr_1)

temp2 <- readLines("./output.txt")
y2<-str_sub(temp2,start=-1)
y1<-as.integer(y1)
y2<-as.integer(y2)
attr_1<-as.integer(attr_1)
xlim = range(attr_1);
ylim = range(c(y1,y2))
train.err.1 = mean((y1-y2)^2)

#data frame
require(dplyr)
df <- data.frame(attr_1,classes=y1, predictedClasses=y2)
df <-df[order(df$attr_1),]
#par(mfrow=1)
#plot(attr_1, col=2,y1, xlim=xlim, ylim=ylim, main="Training data",
#pch = 1,)
plot(df$attr_1, col=3,df$classes, xlim=xlim, ylim=ylim, main="Training data",
     pch = 12,)
lines(df$attr_1, df$predictedClasses, col=2, lwd=2)
