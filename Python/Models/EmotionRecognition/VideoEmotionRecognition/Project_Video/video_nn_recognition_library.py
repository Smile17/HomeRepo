import cv2
import numpy as np
import video_recognition_library as vr
import dlib

face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')
shape_predictor = dlib.shape_predictor(
    'C:\\Users\\kam\\Documents\\DiplomaExperiments\\Project_Video\\models\\shape_predictor_68_face_landmarks.dat')

### For video preprocessing
def get_landmarks(image):
    d = dlib.rectangle(0,0,200,200)
    shape = shape_predictor(image, d) #Draw Facial Landmarks with the predictor class
    xlist = []
    ylist = []
    for i in range(0,68): #Store X and Y coordinates in two lists
        xlist.append(float(shape.part(i).x))
        ylist.append(float(shape.part(i).y))
        
    landmarks_vectorised = []

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
            landmarks_vectorised.append(dist)
    if len(landmarks_vectorised) == 0:
        print('stop here')
    #landmarks_vectorised = landmarks_vectorised.reshape(1, len(landmarks_vectorised))
    return landmarks_vectorised
### For video preprocessing - end

def get_few_frames(tensor, center, n_points, step, is_face_detect = False, is_landmarks=True):
    """From video tensor with shape (n_frames, ...) get n_points
    frames around center with some step"""
    indexes = center + np.arange(-n_points//2, n_points//2+1)*step
    tensor = tensor[indexes]
    if is_face_detect:
        tensor = vr.face_detection(tensor)
    if is_landmarks:
        res = []
        for i in range(tensor.shape[0]):
            marks = get_landmarks(tensor[i].reshape(tensor.shape[1], tensor.shape[2]))
            res.append(marks)
        tensor = np.array(res)
    return tensor

def get_video_frames(file_name):
    """Read video file"""
    cap = cv2.VideoCapture(file_name)
    frames = []
    while True:
        retval, image = cap.read()
        if not retval:
            break
        try:
            frames.append(image)
        except IndexError:
            print('omg!')
            print(file_name)
    cap.release()
    frames = np.array(frames)
    return frames

def preprocess_video(file_name):
    tensor = get_video_frames(file_name)
    frames = get_few_frames(tensor, tensor.shape[0]//2, 14, 5, is_face_detect = True).reshape(1, 15, 4624)
    return frames

def test():
    frames = preprocess_video('C:\\Users\\kam\\Documents\\DiplomaExperiments\\DataSet_Testing\\Video_Song_Actor_16\\Actor_16\\01-02-03-01-01-01-16.mp4')
    print(frames.shape)
    print('Done')
#test()