
# This is the server logic for a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#

library(shiny)

shinyServer(function(input, output, session) {

  K<-0.8
  x0<-c(0.1,0.2,0.4,0.5,0.6,0.8,0.9)
  xs<-c(0.07,0.21,0.36,0.5,0.64,0.79,0.93)
  hi<-c(1,0.8,0.75,0.6,0.9,1,1,0.8,0.5,0.65,0.5,0.7,0.65,0.9,0.8,1)
  DataFiles<-reactive({
    infile <- input$datafile
    if (is.null(infile)) {
      
      return(NULL)
    }
    v<-infile$datapath
    f<-list()
    
    
    for(i in 1:length(v))
    {
      f[[i]]<-read.csv(v[i], header=input$header, sep=input$sep, 
                       quote=input$quote)
      cNames<-c()
      rNames<-c()
      for(j in 1:dim(f[[i]])[1])
        rNames[j]<-paste0('Expert ',j)
      for(j in 1:(length(f[[i]])/2))
      {
        cNames[2*j-1]<-paste0('\u03bc',j)
        cNames[2*j]<-paste0('\u03bd',j)
      }
      colnames(f[[i]])<-cNames
      row.names(f[[i]])<-rNames
    }
    f
  })
  observe({
    output$FileSelect <- renderUI({
      infile <- input$datafile
      if(is.null(infile)) return(NULL)
      choicesList<-list()
      choicesExpertList<-list()
      for(i in 1:length(DataFiles()))
      {
        choicesList[infile$name[i]]<-i
      }
      for(i in 1:dim(DataFiles()[[1]])[1])
      {
        choicesExpertList[paste('Expert ',i)]<-i
      }
      updateSelectInput(session, inputId="FilesGraph", label="",
                        choices=choicesList)
      updateSelectInput(session, inputId="Files", label="",
                        choices=choicesList)
      updateSelectInput(session, inputId="ExpertList", label="",
                        choices=choicesExpertList)
      updateSelectInput(session, inputId="", label="",
                        choices=choicesList)
      
      
      
    })
    
  })
  
  output$table <- renderDataTable({
    infile <- input$datafile
    if (is.null(infile)) {
      
      return(NULL)
    }
    if(is.na(as.numeric(input$Files))) return(NULL)
    i<-as.numeric(input$Files)
    DataFiles()[[i]]
    
  })
  
  
  
  muData<-reactive({
    d<-list()
    for(i in 1:length(DataFiles()))
    {
      dataMatrix<-DataFiles()[[i]]
      oddseq<-seq(1,length(dataMatrix),2)
      d[[i]]<-data.frame(dataMatrix[oddseq]) 
    }
    d
  })
  nuData<-reactive({
    d<-list()
    for(i in 1:length(DataFiles()))
    {
      dataMatrix<-DataFiles()[[i]]
      evenseq<-seq(2,length(dataMatrix),2)
      d[[i]]<-data.frame(dataMatrix[evenseq]) 
    }
    d
  })
  
  
  
  intervalMinimum<-function(m,v)
  {
    m<-as.data.frame(m)
    v<-as.data.frame(v)
    return(abs(m-m*(1-v)*K))
  }
  intervalMaximum<-function(m,v)
  {
    m<-as.data.frame(m)
    v<-as.data.frame(v)
    I<-matrix(1,dim(m)[1],length(m))
    pmin(as.data.frame(I),m+m*(1-v)*K)
  }
  
  intervalDataMinimum<-reactive({
    d<-list()
    for(i in 1:length(DataFiles()))
    {
      d[[i]]<-intervalMinimum(muData()[[i]],nuData()[[i]])
    }
    d
    
  })
  intervalDataMaximum<-reactive({
    d<-list()
    for(i in 1:length(DataFiles()))
    {
      d[[i]]<-intervalMaximum(muData()[[i]],nuData()[[i]])
    }
    d
  })
  output$IntervalPlot<-renderPlotly({
    i<-as.numeric(input$FilesGraph)
    j<-as.numeric(input$ExpertList)
    if(is.na(j)) return(NULL)
    p0<-plotly_build(AllTracesPlot())
    p0color<-p0$x$data[[j+1]]$line$color[1]
    pl<-plot_ly(type="scatter", mode='lines+markers',line = list(shape = "spline"),showlegend=FALSE)
    pl<-add_trace(pl,x=x0,y=as.numeric(muData()[[i]][j,]),name=paste0('Expert',j),line=list(color=p0color))
    pl<-add_trace(pl,x=x0,y=as.numeric(intervalDataMaximum()[[i]][j,]),line=list(color='transparent'))
    pl<-add_trace(pl,x=x0,y=as.numeric(intervalDataMinimum()[[i]][j,]),fill = 'tonexty', fillcolor=toRGB(p0color,0.2),line=list(color='transparent'))
    
    
  })
  
  AllTracesPlot<-reactive({
    i<-as.numeric(input$FilesGraph)
    oddDataMatrix<-muData()[[i]]
    if(input$visibility==FALSE) v<-"legendonly"
    else v<-"none"
    pl<-plot_ly(type="scatter", mode='lines+markers',line = list(shape = "spline"))
    
    for(j in 1:dim(oddDataMatrix)[1])
    {
      pl<-add_trace(pl,x=x0,y=as.numeric(oddDataMatrix[j,]),name=paste0("Expert",j),visible=v)
    }
    pl
  })
  
  output$DataPlot<-renderPlotly({
    if(is.null(AllTracesPlot())) return(NULL)
    AllTracesPlot()
  })
  plotSemiColor<-function(p)
  {
    p0<-plotly_build(p)
    for(i in 1:length(p0$x$data)-1)
    {
      p0$x$data[[i+1]]$line$color<-toRGB(p0$x$data[[i+1]]$line$color,0.2)
      p0$x$data[[i+1]]$showlegend<-FALSE
    }
    p0
    
  }
  functionalQuality<-function(m)
  {
    m<-as.data.frame(m)
    colMeans(m)
  }
  functionalQualityData<-function(listM)
  {
    d<-list()
    for(i in 1:length(listM))
    {
      d[[i]]<-functionalQuality(listM[i])
    }
    d
    
  }
  IntegratedData<-function(listM)
  {
    i<-as.numeric(input$FilesGraph)
    m<-listM
    M<-muData()[[i]]
    
    v<-list()
    for(u in 1:length(M))
    {
      v[u]<-M[,u][which.min(abs(M[,u]-m[u]))]
    }
    v
    
    
  }
  MeanMinData<-reactive({
    i<-as.numeric(input$FilesGraph)
    functionalQuality(intervalDataMinimum()[[i]])
  })
  MeanMaxData<-reactive({
    i<-as.numeric(input$FilesGraph)
    functionalQuality(intervalDataMaximum()[[i]])
  })
  IntegratedMinData<-reactive({
    IntegratedData(MeanMinData())
  })
  IntegratedMaxData<-reactive({
    IntegratedData(MeanMaxData())
  })
  
  estimatedFunc<-function(m, par)
  {
    sum(abs((1/(sqrt(2*pi*par[2])))*exp(-(xs-par[1])^2/(2*par[2]))-m))
  }
  GaussDensity<-function(x,M,D,k)
  {
    (1/(sqrt(2*pi*D)))*exp(-(x-M)^2/(2*D))
  }
  GaussData<-function(listM)
  {
    #x<-c(xs[-1],1)
    #K0<-sum((x-xs)*m)
    result<-optim(par=c(0.3,0.5),estimatedFunc,m=as.numeric(listM))
    dnorm(x0,mean=result$par[1],sd=sqrt(result$par[2]),log=FALSE)
    #d<-GaussDensity(x0,M=result$par[1],D=result$par[2],K0)
  }
  GaussMinData<-reactive({
    GaussData(IntegratedMinData())
  })
  GaussMaxData<-reactive({
    GaussData(IntegratedMaxData())
  })
  GaussAverage<-reactive({
    d<-list()
    for(i in 1:length(GaussMinData()))
    {
      m<-as.numeric(GaussMinData()[[i]])
      M<-as.numeric(GaussMaxData()[[i]])
      d[[i]]<-(m+M)/2
    }
    d
  })
  ExpertEstimation<-function()
  {
    d<-list()
    for(i in 1:length(intervalDataMinimum()))
    {
      m<-(intervalDataMinimum()[[i]])
      M<-(intervalDataMaximum()[[i]])
      #g<-(GaussAverage()[[i]])
      g<-functionalQuality(muData()[[i]]) #performance changes
      v<-list()
      for(j in 1:dim(m)[1])
      {
        v[j]<-(1-sum(max(abs(m[j,]-g),abs(M[j,]-g))))*hi[j]
      }
      u<-as.data.frame(v)
      d[[i]]<-u
    }
    d
  }
  MedianEstimation<-reactive({
    d<-list()
    n<-dim(DataFiles()[[1]])[1]
    cNames<-c()
    rNames<-c()
    for(j in 1:n){
      cNames[j]<-paste0('Expert ',j)
      rNames[j]<-paste0('Ex',j)
    }
    
    
    for(i in 1:length(DataFiles()))
    {
      m<-intervalDataMinimum()[[i]]
      M<-intervalDataMaximum()[[i]]
      
      res<-data.frame()
      for(j in 1:(n-1))
      {
        res[j,j]<-0
        tmp<-list()
        for(k in (j+1):n)
        {
          for(u in 1:length(m))
            tmp[u]<-max(abs(m[j,u]-m[k,u]),abs(M[j,u]-M[k,u]))
          res[j,k]<-res[k,j]<-sum(as.numeric(tmp))/length(tmp)
        }
      }
      res[n,n]<-0
      colnames(res)<-cNames
      row.names(res)<-rNames
      d[[i]]<-res
    }
    d
  })
  
  output$distances<-renderDataTable({
    i<-as.numeric(input$FilesGraph)
    MedianEstimation()[[i]]
  })
  MedianEstimationMeanRow<-reactive({
    d<-list()
    n<-length(DataFiles())
    cNames<-c()
    for(j in 1:n){
      cNames[j]<-paste0('Expert ',j)
    }
    for(i in 1:n)
    {
      m<-MedianEstimation()[[i]]
      
      d[[i]]<-as.data.frame(colSums(m)/n)
      #rownames(d[[i]])<-cNames
      #colnames(d[[i]])<-c('Mean distance')
    }
    d
    
  })
  
  MedianEstimationMeanRowMax<-reactive({
    d<-list()
    for(i in 1:length(MedianEstimationMeanRow()[[1]]))
    {
      m<-MedianEstimationMeanRow()[[i]]
      d[[i]]<-min(m)
    }
    d
  })
  
  MedianEstimationExpertNum<-reactive({
    d<-list()
    for(i in 1:length(DataFiles()))
    {
      m<-MedianEstimationMeanRow()[[i]]
      d[[i]]<-which.min(m[,1])
    }
    d
    
  })
  output$MaxMeanDistance<-renderText({
    i<-as.numeric(input$FilesGraph)
    if(is.na(i)) return(NULL)
    m<-MedianEstimationMeanRow()[[i]]
    paste0('min - Expert ',MedianEstimationExpertNum()[[i]],': ',min(m))
  })
  output$EstimationPlot<-renderPlotly({
    if(is.null(AllTracesPlot())) return(NULL)
    pl<-plotSemiColor(AllTracesPlot())
    i<-as.numeric(input$FilesGraph)
    pl<-add_trace(pl,x=x0,y=as.numeric(functionalQuality(muData()[[i]])),name="Average estimation",type="scatter", mode='lines+markers',line = list(shape = "spline"))
    
    pl<-add_trace(pl,x=x0,y=as.numeric(MeanMinData()),name="Functional quality min",mode='lines')
    p0<-plotly_build(pl)$x$data
    lastcol<-p0[[length(p0)]]$line$color
    pl<-add_trace(pl,x=x0,y=as.numeric(MeanMaxData()),name="Functional quality max",mode='lines',line=list(color=lastcol))
    
    pl<-add_trace(pl,x=x0,y=as.numeric(IntegratedMinData()),name="Integrated min",mode='lines')
    p0<-plotly_build(pl)$x$data
    lastcol<-p0[[length(p0)]]$line$color
    pl<-add_trace(pl,x=x0,y=as.numeric(IntegratedMaxData()),name="Integrated max",mode='lines',line=list(color=lastcol))
    
    pl<-add_trace(pl,x=x0,y=as.numeric(GaussMinData()),name="Gauss min",mode='lines')
    p0<-plotly_build(pl)$x$data
    lastcol<-p0[[length(p0)]]$line$color
    pl<-add_trace(pl,x=x0,y=as.numeric(GaussMaxData()),name="Gauss max",mode='lines',line=list(color=lastcol))
    
    j<-MedianEstimationExpertNum()[[i]]
    
    Exprt<-paste0("Expert ",j)
    pl<-add_trace(pl,x=x0,y=as.numeric(intervalDataMinimum()[[i]][j,]),name=paste0(Exprt," min"),mode='lines')
    p0<-plotly_build(pl)$x$data
    lastcol<-p0[[length(p0)]]$line$color
    pl<-add_trace(pl,x=x0,y=as.numeric(intervalDataMaximum()[[i]][j,]),name=paste0(Exprt," max"),mode='lines',line=list(color=lastcol))
    pl<-add_trace(pl,x=x0,y=as.numeric(muData()[[i]][j,]),name=Exprt,mode='lines',line=list(color=lastcol))
    
    pl
  })
  
  po<-function(v1min,v2min,v1max,v2max)
  {
    vmin<-abs(v1min-v2min)
    vmax<-abs(v1max-v2max)
    tmp<-c()
    for(i in 1:length(vmin))
    {
      tmp[i]<-max(vmin,vmax)
    }
    sum(tmp)/length(tmp)
  }
  ConfidenceInterval<-reactive({
    d<-list()
    n<-length(DataFiles())
    for(i in 1:n)
    {
      m1<-as.numeric(intervalDataMinimum()[[i]][MedianEstimationExpertNum()[[i]],])
      m2<-as.numeric(intervalDataMaximum()[[i]][MedianEstimationExpertNum()[[i]],])
      v<-list()
      for(j in 1:dim(intervalDataMinimum()[[i]])[1])
      {
        v1<-as.numeric(intervalDataMinimum()[[i]][j,])
        v2<-as.numeric(intervalDataMaximum()[[i]][j,])
        v[j]<-po(m1,v1,m2,v2)*(1-ExpertEstimation()[[i]][,j])
      }
      d[[i]]<-v
    }
    d
    
  })
  ConfidentExpert<-reactive({
    d<-list()
    n<-length(ConfidenceInterval())
    for(i in 1:n)
    {
      m1<-as.numeric(ConfidenceInterval()[[i]])
      d[[i]]<-m1<input$RT0
    }
    d
    
  })
  
  output$ExpertEstimations<-renderDataTable({
    i<-as.numeric(input$FilesGraph)
    w<-as.matrix(ExpertEstimation()[[i]])
    md<-as.matrix(MedianEstimationMeanRow()[[i]])
    cn<-as.matrix(ConfidenceInterval()[[i]])
    ce<-ConfidentExpert()[[i]]
    v<-list()
    for(j in 1:length(ce))
    {
      if(ce[j]==TRUE)
        v[j]<-'+'
      else v[j]<-'-'
    }
    d<-data.frame(t(w),md,cn,as.matrix(v))
    n<-dim(DataFiles()[[1]])[1]
    rNames<-c()
    for(j in 1:n){
      rNames[j]<-paste0('Expert ',j)
    }
    rownames(d)<-rNames
    colnames(d)<-c("w","Mean distance","Confidence interval","In interval")
    d
  })
  
  output$ConfidentExpertPlot<-renderPlotly({
    i<-as.numeric(input$FilesGraph)
    pl<-plotly_build(AllTracesPlot())
    ce<-ConfidentExpert()[[i]]
    for(j in 1:length(ce))
    {
      if(ce[j]==FALSE) pl$x$data[[j+1]]$visible=FALSE
    }
    pl
  })
  countConfident<-reactive({
    d<-list()
    n<-length(ConfidentExpert())
    for(i in 1:n)
    {
      m1<-ConfidentExpert()[[i]]
      s<-0
      for(j in 1:length(m1))
        if(m1[j]==TRUE) s<-s+1
      d[[i]]<-s/length(m1)
    }
    d
  })
  adjustedEstimation<-reactive({
    d<-list()
    n<-length(DataFiles())
    for(i in 1:n)
    {
      num<-MedianEstimationExpertNum()[[i]]
      m<-intervalDataMinimum()[[i]][num,]
      mm<-muData()[[i]][num,]
      M<-intervalDataMaximum()[[i]][num,]
      numAdj<-1
      for(j in 1:length(M))
      {
        if(M[j]-m[j]<0.2)
        {
          if(mm[numAdj]<mm[j]) numAdj<-j
        }
      }
      d[[i]]<-c(numAdj,(m[numAdj]+M[numAdj])/2)
    }
    d
  })
  output$CountConfidentExperts<-renderText({
    i<-as.numeric(input$FilesGraph)
    paste0(100*countConfident()[[i]]," % estimations are adjusted ")
  })
  output$AdjustedEstimationText<-renderText({
    i<-as.numeric(input$FilesGraph)
    paste0("Adjusted estimation: ", adjustedEstimation()[[i]][[2]], " with level: ", x0[adjustedEstimation()[[i]][[1]]])
    
  })
  
  output$Results_1<-renderDataTable({
    PersentConfident<-as.numeric(countConfident())
    NumExpert<-as.numeric(MedianEstimationExpertNum())
    adjustedEst<-adjustedEstimation()
    v1<-c()
    v2<-c()
    for(i in 1:length(DataFiles()))
    {
      v1[i]<-x0[adjustedEst[[i]][[1]]]
      v2[i]<-adjustedEst[[i]][[2]]
    }
    d<-data.frame(PersentConfident,NumExpert,v1,v2)
    #row.names(DataFiles()$names)
    colnames(d)<-c("Percentage of experts in interval","Median Expert","Adjustment coeficient","Adjusted estimation")
    d
  })
  output$Results_2<-renderDataTable({
    adjustedEst<-adjustedEstimation()
    #v1<-c()
    v<-c()
    for(i in 1:length(DataFiles()))
    {
      v[i]<-adjustedEst[[i]][[2]]
    }
    w<-c(0.35,0.18,0.12,0.45)
    #w<-c(1,0)
    wres<-colSums(t(matrix(v,4,4))*w)/4
    d<-data.frame(data.frame(t(matrix(v,4,4)),wres))
    row.names(d)<-c("Product 1","Product 2","Product 3","Product 4")
    colnames(d)<-c("Material-technical base","Finance","Management","Quantity of fields","W")
    
    d
  })

})
