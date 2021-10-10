
# This is the user-interface definition of a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#

library(shiny)
library(rmutil)

library("htmltools")
library("vembedr")

shinyUI(fluidPage(
  
  navbarPage(
    title = "Emotion Recognition",
    tabPanel('Test Mode',
             fluidRow(
               column(5,
                      
                      uiOutput("ModelsConfiguration"),
                      fileInput('SampleFilesPath', 'Choose files to upload', multiple = "FALSE",
                                accept = c
                                (
                                  'text/csv',
                                  'text/comma-separated-values',
                                  'text/tab-separated-values',
                                  'text/plain',
                                  '.csv',
                                  '.tsv'
                                )
                      ),
                      actionButton("ModelsEstimationStart", "Estimate model"),
                      actionButton("GetReport", "Get a report")
                      
                      
               ),
               column(5,
                   #numericInput("LogFrequency", label = "Log frequency (file/message)", value = 2),
                   numericInput("LogFrequency", label = "Files per request", value = 10),
                   textAreaInput("LogText", label = "Log", value = "", width = "500px", height = "400px")
                   
               )
             )
             
    ),
    tabPanel('Customer Mode',
             fluidRow(
               column(5,

                      fileInput('SampleFilesCustomer', 'Choose files to upload', multiple = "TRUE",
                                accept = c('mp4','image/png', 'image/jpeg')
                      ),
                      selectizeInput("ModelsCustomer", label = "Select models", choices = c(""), selected = NULL, multiple = TRUE,
                                     options = NULL),
                      actionButton("StartPredictionProcess", "Predict emotions")
                      
                      
               ),
               column(5,
                      selectInput("ImageNames", "Choose an image",
                                  choices = c("")),
                      uiOutput("VideoImageFrame")
                      #imageOutput("ImagePreview", height = "250px")
               )
             ),
             fluidRow(
               column(10,
                      h4("Image / Video results"),
                      tableOutput("ImagePrediction"),
                      h4("Model results"),
                      uiOutput("TablesPrediction"))
             )
             
             
             
    )
  )
  
))
