
# This is the server logic for a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#

library(shiny)
#library(shinyFiles)
library(xml2)

shinyServer(function(input, output, session) {
  options(shiny.maxRequestSize = 30*1024^2)
  FilesPath<<-list()
  ModelNames<<-list()
  ModelNamesDesc<<-list()
  #UI for selection of models which are used for testing 
  #UI elements are uploaded from configuration file
  output$ModelsConfiguration<-renderUI({
    fileContent<-read.table("MethodsConfiguration.txt", header=FALSE,sep=";")
    i<-1
    step=function(i)
    {
      line<-fileContent[i,]
      
      ModelNames[[i]]<<-line$V1
      ModelNamesDesc[[i]]<<-line$V2
      textInput(paste0("Model_",i),label = line$V2, value = line$V3)
      
    }
    res<-lapply(1:dim(fileContent)[1], function(i)step(i))
    r<-selectizeInput("FromScratch", label = "From Scratch", choices = fileContent$V2, selected = NULL, multiple = TRUE,
                   options = NULL)
    updateSelectizeInput(session, "ModelsCustomer", choices = fileContent$V2)
    list(r, res)
  })
  
  #Upload files
  observe({
    infile <- input$SampleFilesPath
    if (is.null(infile)) {
      return(NULL)
    }
    v<-infile$datapath
    fileContent<-read.table(v, header=TRUE,sep=",")
    FilesPath<<-fileContent$file_name
  })
  
  # Anything that calls autoInvalidate will automatically invalidate
  # every 2 seconds.
  #autoInvalidate <- reactiveTimer(2000)
  #Update log
  #observe({
  #    autoInvalidate()
  #    S<-isolate(input$LogText)
      # This will change the value of input$inText, based on previous value
  #    updateTextAreaInput(session, "LogText", value = paste(S, "New text"))
      
      # Can also set the label, this time for input$inText2
      #updateTextAreaInput(session, "inText2",
      #                    label = paste("New label", x),
      #                    value = paste("New text", x))
      
    
  #})
  
  reportPath<-"C:\\Users\\1\\OneDrive\\Diploma\\Projects\\PB\\"
  #Open Power BI report
  observeEvent(input$GetReport,{
    if(input$GetReport)
    {
      step=function(i){
        destinationFile<-input[[paste0("Model_",i)]]
        if(file.exists(destinationFile))
        file.copy(destinationFile, paste0(reportPath,"Model_",i,".txt"), overwrite=TRUE)
      }
      lapply(1:length(ModelNames), function(i) step(i))
      system2("runReport.bat")
    }
    #  system("start run2.bat", intern = TRUE)
  })
  
  EstimationEXE<-"C:\\Users\\1\\OneDrive\\Diploma\\Projects\\FaceAzureAPI\\FaceApiRun\\bin\\Debug\\FaceApiRun.exe"
  #EstimationEXE<-"C:\\Users\\kam\\OneDrive\\Diploma\\Projects\\FaceAzureAPI\\FaceApiRun\\bin\\Debug\\FaceApiRun.exe"
  #Run estimation process
  observe({
    if(input$ModelsEstimationStart)
    {
      v<-isolate(input$FromScratch)
      NewLogMessage<<-""
      flag<-length(FilesPath)
      if(flag>0)
      {
        step=function(i)
        {
          modelName<-ModelNames[[i]]
          destinationFile<-input[[paste0("Model_",i)]]
          if(destinationFile != "")
          {
            mnd<-ModelNamesDesc[[i]]
            appendMode<-!any(v==mnd)
            freq<-as.numeric(isolate(input$LogFrequency))
            substep=function(j)
            {
              
              start<-freq*(j-1)+1
              end<-freq*j
              images<-FilesPath[start:min(end,length(FilesPath))]
              img<-paste0(images,sep=";", collapse="")
              #Run Python script
              comm<-paste('python video_predictor.py', modelName, img, destinationFile, 'False')
              system(comm, wait=TRUE)
              
            }
            
            stepCount<-floor(length(FilesPath)/freq)
            if(length(FilesPath)%%freq!=0)
              stepCount<-stepCount+1
            lapply(1:stepCount, function(k) substep(k))
            #This will change the value of input$inText, based on previous value
            #updateTextAreaInput(session, "LogText", value = paste(S, paste(modelName,": Estimation has been completed.\n")))
            NewLogMessage<<-paste(NewLogMessage, paste(mnd,": Estimation has been completed.\n"))
          }
        }
        lapply(1:length(ModelNames), function(i) step(i))
        S<-isolate(input$LogText)
        # This will change the value of input$inText, based on previous value
        updateTextAreaInput(session, "LogText", value = paste(S, "\n", NewLogMessage))
      }
    }
    
  })
  

  #--------------------------------------------Customer mode--------------------------------------------------------
  #-----------------------------------------------------------------------------------------------------------------
  imgFiles<<-list()
  observeEvent(input$SampleFilesCustomer, {
    imgFiles <<- input$SampleFilesCustomer
    if (is.null(imgFiles))
      return()
    #Update select input
    updateSelectInput(session, "ImageNames",
                      choices = imgFiles$name
                      )
    #Delete previuous created prediction
    step = function(i)
    {
      fileName<-paste0("CustomerMode_",ModelNames[[i]],".txt")
      if(file.exists(fileName))
        file.remove(fileName)
    }
    lapply(1:length(ModelNames), function(i) step(i))
  })
  output$VideoImageFrame<-renderUI({
    i<-input$ImageNames
    if(is.null(imgFiles) || length(imgFiles)==0)
      return(list(src = "",
                  contentType = "image/png",
                  alt = paste("Image / Video preview placeholder"))
      )
    else
    {
      filePath<-imgFiles$datapath[which(imgFiles==input$ImageNames)]
      file.copy(filePath, file.path("www", input$ImageNames) )
      return(
        tags$video(id="video2", type = "video/mp4",
                   src = input$ImageNames, height = 250, controls = "controls")
      )
    }
    
    
  })

  output$ImagePreview<-renderImage({
    i<-input$ImageNames
    if(is.null(imgFiles) || length(imgFiles)==0)
      return(list(src = "",
                  contentType = "image/png",
                  alt = paste("Image preview placeholder"))
      )
    else
    {
      filePath<-imgFiles$datapath[which(imgFiles==input$ImageNames)]
      return(list(src = filePath,
           contentType = "image/png",
           height = "250px",
           alt = paste("Image number"))
      )
    }
  }, deleteFile = FALSE)
  output$TablesPrediction<-renderUI({
    selectedModel<-input$ModelsCustomer
    if(!is.null(selectedModel))
    shiny:::buildTabset(
      id = "TablesResult",
      lapply(1:length(selectedModel), function(i){
        tabPanel(title = sprintf(selectedModel[i]),
                 tableOutput(as.character(ModelNames[[which(unlist(ModelNamesDesc)==selectedModel[i])]]))
        )
      }), paste0("nav nav-","tabs")) 
    
  })
  PredictionResultFromFile<<-list()
  #Estimate a video
  observeEvent(input$StartPredictionProcess,{
    selectedModel<-input$ModelsCustomer
    if(!is.null(imgFiles) && length(imgFiles)>0 && !is.null(selectedModel))
    {
      img<-paste0(imgFiles$datapath,sep=";", collapse="")
      destinationFilePref<-"CustomerMode_"
      appendMode<-"FALSE"
      step=function(i){
        modelName<-ModelNames[[which(unlist(ModelNamesDesc)==selectedModel[i])]]
        destinationFile<-paste0(destinationFilePref, modelName,".txt")
        
        #Run Python script
        #comm<-paste('python video_predictor.py', modelName, img, destinationFile, 'False')
        #system(comm, wait=TRUE)
        
        #Upload results from files
        filePath<-paste0('results//',destinationFile)
        if(file.exists(filePath))
          try(
            PredictionResultFromFile[[as.character(modelName)]]<<-read.table(filePath, header=TRUE,sep=";")
          )
  
      }
      lapply(1:length(selectedModel), function(i) step(i))
    }
  })
  #Update data tables with results
  observe({
    input$StartPredictionProcess
    PredictionResultFromFile
    if(length(PredictionResultFromFile)>0)
    {
      selectedModel<-isolate(input$ModelsCustomer)
      step=function(i)
      {
        modelName<-ModelNames[[which(unlist(ModelNamesDesc)==selectedModel[i])]]
        if(!is.null(PredictionResultFromFile[[as.character(modelName)]]))
        {
          sb<-subset(cbind(imgFiles$name,PredictionResultFromFile[[as.character(modelName)]]), select=-file_name)
          selectedModel<-input$ModelsCustomer
          max<-apply(subset(sb, select=-1),1,max)
          res<-cbind(sb,max)
          step=function(i)
          {
            line<-res[i,]
            colNum<-which(line$max==sb[i,])
            colnames(res)[colNum]
          }
          Emotion<-sapply(1:dim(res)[1], function(i) step(i))
          colnames(res)[1]<-"File name"
          output[[as.character(modelName)]]<-renderTable({cbind(res,Emotion)})
        }
        
      }
      lapply(1:length(selectedModel), function(i) step(i))
    }
  })
  
  output$ImagePrediction<-renderTable({
    input$StartPredictionProcess
    i<-input$ImageNames
    
    if(length(PredictionResultFromFile)>0)
    {
      j<-which(imgFiles==i)
      ns<-names(PredictionResultFromFile)
      step=function(i)
      {
        k<-which(unlist(ModelNames)==ns[i])
        Model<-as.character(unlist(ModelNamesDesc)[k])
        sb<-subset(PredictionResultFromFile[[i]][j,], select=-file_name)
        
        c(Model=Model, sb, max=max(sb),Emotion=colnames(sb)[which(sb==max(sb))])
      }
      df<-sapply(1:length(PredictionResultFromFile), function(i) step(i))
      data.frame(t(df))
    }  
  })
  
})

