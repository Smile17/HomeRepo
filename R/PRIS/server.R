
# This is the server logic for a Shiny web application.
# You can find out more about building applications with Shiny here:
#
# http://shiny.rstudio.com
#

library(shiny)
library(visNetwork)

shinyServer(function(input, output) {

  Matrixes<-list()
  Vertices<-list()
  #Get current graph regarded to string line from TextGraphLevel
  currentGraph<-reactive({
    Vertices<<-strsplit(input$TextGraphLevel," ")[[1]] 
    Vertices<<-as.numeric(Vertices)
    v<-c(1,Vertices) # with first element
    
    #Nodes names and id
    nodes <- data.frame(label=as.character(1:sum(v)), id = 1:sum(v), 
                        level = rep(1:length(v),v))
    nodes$font.size<-20 #node labels apperance
    
    #Edges
    startNode<-cumsum(v)+1
    startNode<-head(startNode,-1)
    endNode<-startNode+Vertices-1
    fromNodeId<-nodes$id[1:(length(nodes$id)-Vertices[length(Vertices)])]
    countNeighbours<-rep(Vertices,v[1:length(v)-1])
    from<-rep(fromNodeId,countNeighbours)
    m<-sapply(1:length(startNode),function(x) startNode[x]:endNode[x])
    if(is.null(dim(m))) to<-(rep(m,v[1:(length(v)-1)]))
    else
    if(all(Vertices==Vertices[1]))
      to<-sapply(1:(length(v)-1),function(x) rep(m[,x],v[x]))
    else
      to<-(rep(m,v[1:(length(v)-1)]))
    to<-unlist(to)
    edges<-data.frame(from = from, to = to,arrows="to")
    
    #Construct matrixes
    Matrixes<<-sapply(c(countNeighbours),
                      function(x) matrix(1,ncol=x, nrow=x))
    
    #Construct graph
    graph <- visNetwork(nodes, edges) %>% 
      visHierarchicalLayout()%>% #to get tree structure
      visOptions(highlightNearest = list(enabled = TRUE, algorithm = "hierarchical", 
                                         degree = list(from = 0, to = 1)) 
                 #nodesIdSelection = TRUE
      ) %>%
      visInteraction(keyboard = TRUE,
                     dragNodes = T, 
                     dragView = T, 
                     zoomView = T) %>%
      visEvents(select = "function(properties){
                Shiny.onInputChange('selectedNode', properties.nodes);
                ;}"
      )
    graph
    
  })
  
  #Output current graph
  output$GraphLevel<-renderVisNetwork({
    currentGraph()
  })
  #Output seelcted node number
  output$SelectedNode<-renderText({
    input$selectedNode[1]
  })
  
  #Get nodes from current graph
  currentEdges<- reactive({
    #i<-input$selectedNode[1]
    if(!is.null(currentGraph()))
    {
      networkProxy<-visNetworkProxy("GraphLevel")
      networkProxy %>% visGetEdges(input = "Edges")
      m<-input$Edges
      from<-sapply(c(1:length(m)), function(x) m[[x]]$from)
      to<-sapply(c(1:length(m)), function(x) m[[x]]$to)
      data.frame(from = from, to = to,arrows="to")
    }
    
  })
  childNodes<-function(id) subset(currentEdges(),from==id)$to
  
  #Render matrix as editable table
  output$SelectedNodeMatrix <- renderRHandsontable({

    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      #rhandsontable(Matrixes[[input$selectedNode[1]]]) some issue with this item
      datatable(Matrixes[[input$selectedNode[1]]])
    }
    else
    {
      data.frame()
    }
  })
  
  #Save matrix changes
  setMatrix = function(x, i) Matrixes[[i]] <<- x
  #Save any changes in matrix
  observe({ # will update Matrix each time the button is pressed
    if(!is.null(input$SelectedNodeMatrix))
    {
      if(!is.null(input$selectedNode[1]))
      {
        tmp<-input$SelectedNodeMatrix$params$data
        DF<-sapply(1:length(tmp), function(x) tmp[[x]])
        setMatrix(DF,input$selectedNode[1])
      }
    }
  })
  
  #Upload files
  observe({
    infile <- input$MatrixFiles
    if (is.null(infile)) {
      return(NULL)
    }
    v<-infile$datapath
    m<-1:length(v)
    idx<-subset(m,file.size(v[m])>0) #not empty files
    #Nodes numbers extract from file names
    t<-strsplit(infile$name,"[.]")
    nodesNumber<-sapply(1:length(t), function(x) t[[x]])[1,]
    k<-1
    for(i in idx)
    {
      fileContent<-read.table(v[i], header=FALSE,sep="\t")
      names(fileContent)<-NULL
      #csv(v[i], header=FALSE, sep="\t")
      k<-as.numeric(nodesNumber[i])
      if(!is.null(k))
      if(dim(Matrixes[[k]])==length(fileContent))
      {
        Matrixes[[k]]<<-as.matrix(t(fileContent)) # due to some errors in handsometable
        #Matrixes[[k]]<<-t(Matrixes[[k]])        
      }
      
    }
    
    i<-0
  })
  #Matrix validation: Matrix is reciprocally symetric if a[i,i]=1 and a[i,j]*a[j,i]=1
  isReciprocallSymetric = function(Matrix){
    idx<-1:dim(Matrix)[1]
    m<-diag(Matrix)
    res<-all(m==1)
    if(res==TRUE)
    {
      m<-Matrix
      mlow<-lower.tri(m, diag = FALSE)*m
      mup<-upper.tri(m, diag = FALSE)*m
      k<-mlow*t(mup)
      res<-all(lower.tri(k, diag = FALSE)*(k-1)**2<=0.0001)
    }
    res
  }
  #To convert string to double
  converToDouble = function(x)
  {
    q<-as.numeric(x)
    if(is.na(q))
    {
      k<-strsplit(x,"/")
      q<-as.numeric(k[[1]][1])/as.numeric(k[[1]][2])
    }
    q
  }
  #Make numeric matrix
  matrixToNumeric = function(Matrix)
  {
    m<-sapply(Matrix,function(x) converToDouble(x))
    m<-matrix(m,nrow=dim(Matrix)[1])
  }
  output$isReciprocallSymetric<-renderText(
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      m<-Matrixes[[input$selectedNode[1]]]
      m<-matrixToNumeric(m)
      isCorrect<-isReciprocallSymetric(m)
      s<-"Matrix is "
      if(isCorrect) paste0(s,"reciprocally symmetric.")
      else paste0(s,"not reciprocally symmetric.")
    }
  )
  #To check that all elements is from fundamental scale.
  isFundamental=function(Matrix)
  {
    v<-c("1/9","1/8","1/7", "1/6","1/5","1/4", "1/3","1/2","1","2","3","4","5","6","7","8","9","0.5","0.25","0.125","0.2")
    all(sapply(Matrix, function(x) any(v==x)))
  }
  output$isFundamental<-renderText(
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      if(isFundamental(Matrixes[[input$selectedNode[1]]]))
      {
        paste0("Matrix is fundamental.")
      }
      else
      {
        paste0("Matrix is not fundamental.")
      }
    }
  )
  isConsistent = function(Matrix){
    k<-Matrix%*%Matrix[,1]
    k<-k/Matrix[,1]
    all(k==k[1])
  }
  #To show if the current matrix is consencual
  output$isConsistent<-renderText(
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      m<-Matrixes[[input$selectedNode[1]]]
      m<-matrixToNumeric(m)
      isCorrect<-isConsistent(m)
      s<-"Matrix is "
      if(isCorrect) paste0(s,"consensual.")
      else paste0(s,"not consensual.")
    }
  )
  RGMM = function(Matrix)
  {
    m<-sapply(Matrix,function(x) log(x))
    m<-matrix(m,nrow=dim(Matrix)[1])
    v<-apply(m,2,mean) # 2 - column 1 - row # it is needed because our matrixes are saved as trans
    v<-sapply(v, function(x) exp(x))
    k<-sum(v)
    v<-v/k
  }
  output$RGMM<-renderText(
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      m<-Matrixes[[input$selectedNode[1]]]
      m<-matrixToNumeric(m)
      v<-RGMM(m)
      paste0(c("RGMM: ",paste(format(round(v, 4), nsmall = 1)," ")))
    }
  )
  GCI = function(Matrix){
    v<-RGMM(Matrix)
    n<-dim(Matrix)[1]
    k<-sapply(v,function(x) v/x)
    m<-k*Matrix
    m<-log(m)**2
    p<-sapply(2:dim(m)[1], function(x) m[x,1:(x-1)])
    s<-sum(unlist(p))
    2*s/((n-1)*(n-2))
  }
  
  #To show GCI
  output$GCI<-renderText(
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      m<-Matrixes[[input$selectedNode[1]]]
      m<-matrixToNumeric(m)
      n<-dim(m)[1]
      if(n<=2)
        "GCI: Dimension must be more than 2"
      else
      {
        k<-GCI(m)
        #k<-GCIs[[input$selectedNode[1]]]
        s<-paste0("GCI ", k,"; ")
        norm<-c(0.1573,0.3526,0.37)
        dim<-c(3,4,5)
        num<-which(dim==n)
        if(length(num)==0) num<-3
        k0<-norm[num]
        if(k<=k0)
        {
          s<-paste0(s,"Matrix is acceptable.")
        }
        else
        {
          s<-paste0(s,"Matrix is not acceptable.")
        }
        s
      }
    }
  )
  #wAll<-list()
  global <- eventReactive(input$GlobalButton, {
    if(is.null(input$GraphLevel))
    {
      wAll<-c(1)
      w<-c(1)
      v<-c(1,Vertices)
      startNode<-cumsum(v)+1
      startNode<-head(startNode,-1)
      endNode<-startNode+Vertices-1
      start<-c(1,startNode)
      end<-c(1,endNode)
      for(i in 1:length(Vertices))
      {
        m<-Matrixes[start[i]:end[i]]
        m<-lapply(m,function(x) matrixToNumeric(x))
        rgmm<-lapply(m,function(x) RGMM(x))
        max<-lapply(rgmm,function(x) max(x))
        v<-lapply(1:length(max), function(x) rgmm[[x]]/max[[x]])
        k<-sapply(1:length(w), function(x) v[[x]]*w[[x]])
        if(length(k)>1)
        w0<-apply(k,1,sum)
        else w0<-k
        w<-w0/sum(w0)
        wAll<-c(wAll,w)
      }
      wAll
    }
  })
  output$GlobalIdeal<-renderText(
    {
      global()
    }
  )
  output$Range<-renderDataTable(
    {
      wAll<-global()
      N<-length(Matrixes)
      w<-wAll[(N+1):length(wAll)]
      k<-as.character((N+1):length(wAll))
      k<-paste0("A_",k)
      names(w)<-k
      w<-rev(sort(w))
      data.frame(w)
    }, options = list(paging=FALSE, searching = FALSE)
  )
  
  
  #--------------------------------------LAB_2---------------------------------------
  #
  #----------------------------------------------------------------------------------
  Matrix<<-NULL
  MRCI<<-c(0,0,0.52,0.89,1.11,1.25,1.35,1.4,1.45,1.49,1.52,1.54,1.56,1.58,1.59)
  #Upload file
  #Render matrix
  output$Matrix <- renderTable({
    infile <- input$MatrixFile
    if (is.null(infile)) {
      return(NULL)
    }
    v<-infile$datapath
    fileContent<-read.table(v, header=FALSE,sep="\t")
    names(fileContent)<-NULL
    fileContent<-as.matrix(fileContent)
    Matrix<<-matrixToNumeric(fileContent)
    format(round(Matrix, 4), nsmall = 1)
    
  },colnames = FALSE)
  
  #Coercion step
  coercionStep = function(M, meth)
  {
    res<-list()
    res["CurrentMatrix"]<-0
    res$CurrentMatrix<-M
    res["w"]<-0
    res["NextMatrix"]<-0
    #CR and weight vector calculation
    ev<-eigen(M)
    i<-which.max(Re(ev$values))
    maxEv<-ev$values[i]
    k<-ev$vectors[,i]
    k<-Re(k)
    res$w<-k/sum(k)
    N<-dim(M)[1]
    CI<-(maxEv-N)/(N-1)
    res["CR"]<-Re(CI/MRCI[N])
    
    #delta and sigma
    L<-M-Matrix
    res["delta"]<-max(abs(L))
    res["sigma"]<-sqrt(sum(L^2))/(dim(L)[1])
    #Accaptance of modification
    res["isAccepted"]<-((res$delta<0.2)&(res$sigma<0.1))
    
    #Next matrix D(k)
    alpha<-input$alpha
    K<-sapply(res$w,function(x) res$w/x)
    res["K"]<-0
    res$K<-K
    res$NextMatrix<-meth(M,K,alpha)
    res
  }
  WGMM = function(M,K,alpha)
  {
    (M^alpha)*(K^(1-alpha))
  }
  WAMM = function(M,K,alpha)
  {
    m<-(alpha*M)+((1-alpha)*K)
    mup<-upper.tri(m, diag = TRUE)*m
    mrev<-1/m
    mlow<-t(upper.tri(mrev,diag=FALSE)*mrev)
    mup+mlow
  }
  coercionResults<-eventReactive(input$Calculate, {
    if(!is.null(Matrix))
    {
      steps<-list()
      Mat<-Matrix
      k<-0
      meth<-switch(input$Meth,
                   WGMM=WGMM,
                   WAMM=WAMM)
      repeat{
        stepRes<-coercionStep(Mat, meth)
        stepRes["num"]<-k
        steps[[k+1]]<-stepRes
        k<-k+1
        if(stepRes$CR<0.1) break;
        #if(k>5) break;
        Mat<-stepRes$NextMatrix
      }
      res<-lapply(steps, function(x){
        fluidRow(column(width=7,renderTable(format(round(x$CurrentMatrix, 4), nsmall = 1)
                                            , colnames = FALSE)
                        #,renderTable(x$K,colnames=FALSE)
        ),
        column(width=5,h4(paste0("Step: ",x$num)),
               p(paste0("CR: ",x$CR)),
               p(paste0("W: ",paste0(x$w,collapse = " "))),
               p(paste0("delta: ",x$delta)),
               p(paste0("sigma: ",x$sigma)),
               p(paste0("isAccepted: ",x$isAccepted))))
      })
      
      res
    }
  })
  #Render steps
  output$CoercionStep<-renderUI({
    coercionResults()
  })
  
  
  
  #--------------------------------------LAB_3---------------------------------------
  # Lab_3 is an improvement of LAB_1
  #----------------------------------------------------------------------------------
  #Children nodes for i-node
  childrenNodes=function(i)
  {
    w<-c(1)
    v<-c(1,Vertices)
    startNode<-cumsum(v)+1
    startNode<-head(startNode,-1)
    endNode<-startNode+Vertices-1
    start<-c(1,startNode)
    end<-c(1,endNode)
    l<-min(which((start<=i)==FALSE))
    start[l]:end[l]
  }
  #Neighbour nodes for i-node
  neighbourNodes=function(i)
  {
    w<-c(1)
    v<-c(1,Vertices)
    startNode<-cumsum(v)+1
    startNode<-head(startNode,-1)
    endNode<-startNode+Vertices-1
    start<-c(1,startNode)
    end<-c(1,endNode)
    l<-min(which((start<=i)==FALSE))
    start[l-1]:end[l-1]
  }
  globalWeightByCriterionWeight=function(v, wc)
  {
    k<-sapply(1:length(wc), function(x) v[[x]]*wc[x])
    w0<-apply(k,1,sum)
    w<-w0/sum(w0)
    w
  }
  localWeight=function(i)
  {
    m<-Matrixes[neighbourNodes(i)]
    m<-lapply(m,function(x) matrixToNumeric(x))
    rgmm<-lapply(m,function(x) RGMM(x))
    max<-lapply(rgmm,function(x) max(x)) #Ideal synthesis
    #v<-lapply(1:length(max), function(x) rgmm[[x]]/max[[x]]) #Ideal synthesis
    sumX<-lapply(rgmm,function(x) sum(x)) #Distrubition synthesis
    v<-lapply(1:length(sumX), function(x) rgmm[[x]]/sumX[[x]]) #Distrubition synthesis
    v
  }
  globalNeighbourWeight=function(i)
  {
    m<-global()
    m[neighbourNodes(i)]
  }
  output$NeighbourGlobalWeight<-renderText({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      numNeig<-neighbourNodes(ins) #neighbours number of selected node
      m<-global() #all global weight
      gwc<-m[numNeig] #global weight criterion of neighbours
      #paste0(c("RGMM: ",paste(format(round(v, 4), nsmall = 1)," ")))
      r<-paste0("[",paste(numNeig, collapse=", "),"]=[",paste(gwc, collapse=", "),"]")
      r
    }
  })
  PlotLine<-reactive({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      numNeig<-neighbourNodes(ins) #neighbours number of selected node
      mgl<-global() #all global weight
      gwc<-mgl[numNeig] #global weight criterion of neighbours
      sumgwc<-sum(gwc)-mgl[ins]
      wc<-gwc/sumgwc
      lw<-localWeight(ins)
      #create new weight vector
      
      step=function(x)
      {
        k<-x
        wc0<-wc*(1-k)
        wc0[numSelected]<-k
        k1<-sapply(1:length(wc), function(x) lw[[x]]*wc0[x])
        w0<-apply(k1,1,sum)
        w<-w0/sum(w0)
        w
      }
      numSelected<-which(numNeig==ins) #number of criterion weight in wc0 vector
      s<-seq(0,1,by=0.1)
      res<-lapply(s, function(x) step(x))
      m<-matrix(unlist(res), nrow=length(lw[[1]]), ncol=length(s))
      m
    }
  })
  output$MainPlot<-renderPlotly({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      mgl<-global() #all global weight
      s<-seq(0,1,by=0.1)
      m<-PlotLine()
      
      ch<-childrenNodes(ins)
      
      f1 <- list(
        family = "Arial, sans-serif",
        size = 18,
        color = "lightgrey"
      )
      f2 <- list(
        family = "Old Standard TT, serif",
        size = 14,
        color = "black"
      )
      a <- list(
        title = paste0("Weight of criterion ",ins),
        titlefont = f1,
        showticklabels = TRUE,
        tickangle = 45,
        tickfont = f2,
        range=c(0,1)
      )
      b <- list(
        title = "Global weight of alternatives",
        titlefont = f1,
        showticklabels = TRUE,
        tickangle = 45,
        tickfont = f2,
        range=c(0,1)
      )
      #UI graph
      pl<-plot_ly(type="scatter", mode='lines+markers',line = list(shape = "spline"),showlegend=TRUE)
      pl<-add_trace(pl, x=mgl[ins],y=s, name=paste0('Global weight of criterion ', ins))
      
      for(i in 1:dim(m)[1])
        pl<-add_trace(pl, x=s,y=m[i,], name=paste('Alternative',ch[i]))
      
      pl%>%layout(xaxis = a, yaxis = b)
    }
    else
    {
      pl<-plot_ly(type="scatter", mode='lines+markers',line = list(shape = "spline"),showlegend=TRUE)
      pl
    }
    
  })
  DeltaAbsoluteMatrix<-reactive({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      mgl<-global() #all global weight
      wc<-mgl[ins]
      s<-seq(0,1,by=0.1)
      m<-PlotLine()
      res<-list()
      for(i in 1:(dim(m)[1]-1))
      {
        step=function(j)
        {
          tmp<-m[i,]-m[j,]
          if(any(tmp<0) && any(tmp>0))
          {
            k<-uniroot(function(x) {approxfun(s,m[i,]-m[j,])(x)},interval=c(0,1))
            k[1]$root
          }
          else{NA}
        }
        k<-sapply((i+1):dim(m)[1], function(j) step(j))
        res[[i]]<-c(rep(0,i),unlist(k))
      }
      res[[dim(m)[1]]]<-rep(0,dim(m)[1])
      m<-t(matrix(unlist(res), nrow=dim(m)[1]))
      m<-m-wc
      mup<-upper.tri(m, diag = FALSE)*m
      mlow<--t(mup)
      re<-mup+mlow
      numNeig<-childrenNodes(ins) #children nodes
      kNames<-paste0("A_",numNeig)
      rownames(re)<-kNames
      colnames(re)<-kNames
      re
    }
  })
  DeltaRelativeMatrix<-reactive({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      mgl<-global() #all global weight
      wc<-mgl[ins]
      re<-DeltaAbsoluteMatrix()
      re/wc
    }
  })
  output$DeltaAbsolute<-renderDataTable({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      re<-DeltaAbsoluteMatrix()
      data.frame(re)
    }
  }, options = list(paging=FALSE, searching = FALSE))
  output$DeltaRelative<-renderDataTable({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      re<-DeltaRelativeMatrix()
      data.frame(re)
    }
  }, options = list(paging=FALSE, searching = FALSE))
  output$DeltaRelativeCrit<-renderDataTable({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      mgl<-global() #all global weight
      wc<-mgl[ins]
      ch<-childrenNodes(ins)
      gw<-mgl[ch]
      #local weight
      m<-Matrixes[[input$selectedNode[1]]]
      m<-matrixToNumeric(m)
      v<-RGMM(m)
      re<-sapply(1:length(v), function(i) sapply(1:length(v), function(j) (gw[i]-gw[j])/(v[i]-v[j])))
      re<-re/wc
      numNeig<-childrenNodes(ins) #children nodes
      kNames<-paste0("A_",numNeig)
      rownames(re)<-kNames
      colnames(re)<-kNames
      re
    }
    
  }, options = list(paging=FALSE, searching = FALSE))
  output$CritValFirst<-renderText({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      m<-DeltaRelativeMatrix()
      m[is.na(m)]<-Inf
      m<-abs(m)
      mi<-min(m[upper.tri(m, diag = FALSE)])
      paste0("CritVal1: ", mi, "; SensVal1: ", 1/mi)
    }
  })
  output$CritValSecond<-renderText({
    if((!is.null(input$selectedNode[1])) && (input$selectedNode[1]<=length(Matrixes)))
    {
      ins<-input$selectedNode[1] #selected node
      mgl<-global() #all global weight
      ch<-childrenNodes(ins)
      mgl<-mgl[ch]
      maxAl<-which.max(mgl)
      m<-DeltaRelativeMatrix()
      m[is.na(m)]<-Inf
      m<-abs(m)
      m[maxAl,maxAl]<-Inf
      mi<-min(m[maxAl,])
      paste0("CritVal2: ", mi, "; SensVal2: ", 1/mi)
    }
  })
  
  
  
  
  
})
