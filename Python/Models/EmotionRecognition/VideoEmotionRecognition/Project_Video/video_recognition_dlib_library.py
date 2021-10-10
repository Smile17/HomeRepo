#This code is used for extract and preprocess data from video files for dlib data
import cv2
import numpy as np
import video_recognition_library as vrl
import dlib

#Constants
shape_predictor = dlib.shape_predictor(
    'C:\\Users\\kam\\Documents\\DiplomaExperiments\\Project_Video\\models\\shape_predictor_68_face_landmarks.dat')
    #To find face landmarks

### For video preprocessing
def get_landmarks(image):
    d = dlib.rectangle(0,0,vrl.face_size[0],vrl.face_size[1])
    shape = shape_predictor(image, d) #Draw Facial Landmarks with the predictor class
    xlist = []
    ylist = []
    for i in range(0,68): #Store X and Y coordinates in two lists
        xlist.append(float(shape.part(i).x))
        ylist.append(float(shape.part(i).y))

    #for i in range(1,68): #There are 68 landmark points on each face
                    #For each point, draw a red circle with thickness2 on the original frame
    #    cv2.circle(image, (shape.part(i).x, shape.part(i).y), 1, (0,0,255), thickness=2)
        
    dist_list = []

    xmin = np.min(xlist)
    xmax = np.max(xlist)
    xnorm = [(x-xmin)/(xmax - xmin) for x in xlist]

    ymin = np.min(ylist)
    ymax = np.max(ylist)
    ynorm = [(y-ymin)/(ymax - ymin) for y in ylist]

    #plt.plot(xnorm, ynorm, 'o', color='black')
    #plt.show()

    for x1, y1 in zip(xnorm, ynorm):
        for x2, y2 in zip(xnorm, ynorm):
            dist = (y2-y1)*(y2-y1)+(x2-x1)*(x2-x1)
            dist_list.append(dist)
    if len(dist_list) == 0:
        print('stop here')
    return dist_list
### For video preprocessing - end