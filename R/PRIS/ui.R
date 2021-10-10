
# This is the user-interface definition of a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#

library(shiny)
library(rmutil)
library(DT)
library(plotly)
library(igraph)
library(visNetwork)
library(webshot)
library(rhandsontable)
shinyUI(fluidPage(


  navbarPage(
    title = "Hierarchy system",
    #--------------------------------------LAB_1---------------------------------------
    #
    #----------------------------------------------------------------------------------
    tabPanel('Lab 1',
             textInput("TextGraphLevel", label = h4("Structure"), value = "3 3 4"),
             sidebarLayout(
               mainPanel(visNetworkOutput("GraphLevel"),
                         actionButton("GlobalButton", "Calculate global weights"),
                         textOutput("GlobalIdeal"),
                         dataTableOutput("Range", width="60%")),
               sidebarPanel(
                 fileInput('MatrixFiles', 'Choose files to upload', multiple = "TRUE",
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
                 h4("Selected node: ",textOutput("SelectedNode")),
                 rHandsontableOutput("SelectedNodeMatrix"),
                 h4("Matrix properties: "),
                 textOutput("isFundamental"),
                 textOutput("isReciprocallSymetric"),
                 textOutput("isConsistent"),
                 textOutput("RGMM"),
                 textOutput("GCI")
                 
               )
               
               
             )
             
             
    ),
    
    
    #--------------------------------------LAB_2---------------------------------------
    #
    #----------------------------------------------------------------------------------
    tabPanel('Lab 2',
             fileInput('MatrixFile', 'Choose files to upload', multiple = "FALSE",
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
             sliderInput("alpha", "Alpha:",
                         min = 0, max = 1,
                         value = 0.5, step = 0.01),
             radioButtons("Meth", "Method:",
                          c("WGMM" = "WGMM",
                            "WAMM" = "WAMM")),
             actionButton("Calculate", "Calculate coercion"),
             h4("Initial matrix"),
             tableOutput("Matrix"),
             uiOutput("CoercionStep")
             
             
    ),
    #--------------------------------------LAB_3---------------------------------------
    #
    #----------------------------------------------------------------------------------
    tabPanel('Lab 3',
             
             textOutput("NeighbourGlobalWeight"),
             plotlyOutput("MainPlot"),
             dataTableOutput("DeltaAbsolute"),
             dataTableOutput("DeltaRelative"),
             dataTableOutput("DeltaRelativeCrit"),
             textOutput("CritValFirst"),
             textOutput("CritValSecond")
             
             
    )
    
    
    
    
    
    
    
  )
  
))
