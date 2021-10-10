#Remove words from each lines and save the result to the other file

fileName<-paste(getwd(),"song.txt",sep="/")
file<-readLines(fileName)

for(i in 1:(length(file)-1))
{
  str<-file[i]
  v<-strsplit(str,' ')
  k<-sample(1:length(v[[1]]),1)
  if(k>0)
    file[i]<-gsub(v[[1]][k],"_______",str)
}

writeLines(file,paste(getwd(),"songChanged.txt",sep="/"))

