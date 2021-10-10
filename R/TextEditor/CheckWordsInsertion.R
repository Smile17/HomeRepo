#Check the result files after inserting missing words

fileName<-paste(getwd(),"songChanged.txt",sep="/")
file<-readLines(fileName)

for(i in 1:(length(file)-1))
{
  file[i]<-gsub("_","",file[i])
  if(file[i]=="NA") 
    file[i]<-""
}

writeLines(file,paste(getwd(),"songChanged.txt",sep="/"))

