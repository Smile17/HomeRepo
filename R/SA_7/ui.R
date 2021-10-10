
# This is the user-interface definition of a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#
library(shiny)
library(rmutil)
library(DT)
library(plotly)

shinyUI(fluidPage(
    
  navbarPage(
    title = "Delphi method",
    tabPanel('Upload',
             fluidPage
             (
               titlePanel("Uploading Files"),
               sidebarLayout
               (
                 sidebarPanel
                 (
                   fileInput('datafile', 'Choose files to upload', multiple = "TRUE",
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
                   tags$hr(),
                   checkboxInput('header', 'Header', FALSE),
                   radioButtons('sep', 'Separator',
                                c(Comma=',',
                                  Semicolon=';',
                                  Tab='\t'),
                                '\t'),
                   radioButtons('quote', 'Quote',
                                c(None='',
                                  'Double Quote'='"',
                                  'Single Quote'="'"),
                                '"'),
                   tags$hr()
                 ),
                 mainPanel(
                   titlePanel("Data viewer"),
                   htmlOutput("FileSelect", inline=TRUE),
                   selectInput("Files", label="",choices=htmlOutput("FileSelect")),
                   
                   dataTableOutput("table")
                   
                 )
               )
             )
    ),
    tabPanel('Data View',
             fluidRow(
               column(width = 2,
                      selectInput("FilesGraph", label="Select file",choices=htmlOutput("FileSelect"))
               ),
               column(width = 2,
                      checkboxInput('visibility', 'All traces', TRUE)
               )
               
             ),
             plotlyOutput("DataPlot"),
             h4("Interval estimation"),
             htmlOutput("ExpertSelect", inline=TRUE),
             selectInput("ExpertList", label="Interval estimation",choices=htmlOutput("ExpertSelect")),
             plotlyOutput("IntervalPlot"),
             h4("Estimations"),
             plotlyOutput("EstimationPlot"),
             textOutput("AdjustedEstimationText"),
             h4("Distances between experts"),
             dataTableOutput("distances"),
             h4("Expert estimation"),
             sliderInput("RT0", "RT0",
                         min=0, max=1, value=0.35),
             dataTableOutput("ExpertEstimations"),
             textOutput("MaxMeanDistance"),
             h4("Expert in confidence interval"),
             plotlyOutput("ConfidentExpertPlot"),
             textOutput("CountConfidentExperts")
             
    ),
    tabPanel('Results',
             dataTableOutput("Results_1"),
             dataTableOutput("Results_2"))
  )
  
    
))
