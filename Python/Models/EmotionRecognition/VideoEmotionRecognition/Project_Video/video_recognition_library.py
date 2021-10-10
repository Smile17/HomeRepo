#This code is used for extract and preprocess data from video files

#PREREQUISITES FOR LIBRARIES
#Import packages using cmd:
#Import cv2:
#python -m pip install opencv-python
#python -m pip install opencv-contrib-python

import cv2
import numpy as np
import h5py

#Constants
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml') #For face detection
face_size = (200, 200) #default image size
emotions = {
    0: 'angry',
    1: 'calm',
    2: 'disgust',
    3: 'fearful',
    4: 'happy',
    5: 'neutral',
    6: 'sad',
    7: 'surprised'
}

def show_image(image):
    cv2.imshow('image',image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

def nothing(image):
    return image

### Methods for preprocess data for training
def get_video_tensor(file_name, preprocessing = nothing):
    """Read video file and preprocess it"""
    cap = cv2.VideoCapture(file_name)
    frames = []
    while True:
        retval, image = cap.read()
        if not retval:
            break
        try:
            image = preprocessing(image)
            frames.append(image)
        except IndexError:
            print('omg!')
            print(file_name)
    cap.release()
    frames = np.array(frames)
    return frames

def preprocess_face_detection(image):
    """Extract face and resize it"""
    global face_size
    global face_cascade
    (x, y, w, h) = face_cascade.detectMultiScale(image)[0]
    image = image[y:y+h, x:x+w, :]
    image = cv2.resize(image, dsize=face_size)
    return image

def test_preprocess():
    """Test methods for preprocess data for training"""
    file_names = 'C:\\Users\\kam\\Documents\\DiplomaExperiments\\DataSet\\Video_Song_Actor_03\\Actor_03\\01-02-01-01-01-01-03.mp4'
    #res = get_video_tensor(file_names, preprocess_face_detection) 
    res = get_video_tensor(file_names) 
    print(res)
#test_preprocess()
### Methods for preprocess data for training - end

### For video preprocessing
def face_detection(tensor):
    global face_cascade
    global face_size
    res = []
    for i in range(tensor.shape[0]):
        image = tensor[i]
        (x, y, w, h) = face_cascade.detectMultiScale(image)[0]
        image = image[y:y+h, x:x+w, :]
        image = cv2.resize(image, dsize=face_size)
        #image = grayscale(image)
        res.append(image)
    return np.array(res)
### For video preprocessing - end

### For training process
def generator_few_frames(batch_size, metadata, base_path, data_file_name, label_file_name, process):
    """Generate uniformly few frames around center of the video (center included)"""
    hf_data = h5py.File(base_path + data_file_name, 'r')
    hf_labels = h5py.File(base_path + label_file_name, 'r')
    while True:
        n_batches = np.ceil(metadata.shape[0]/batch_size).astype(int)
        for i in range(n_batches):
            file_names = metadata.file_name[i*batch_size:(i+1)*batch_size]
            batch, labels = [], []
            for name in file_names:
                tensor = hf_data.get(name)
                if not(tensor is None):
                    
                    label = hf_labels.get(name)[0]
                    labels.append(label)
                    #For CNN
                    #tensor = grayscale(tensor)
                    #tensor = get_few_frames(tensor, center=tensor.shape[0]//2, n_points=4, step=7)
                    #For NN
                    #tensor = tensor[:,:]
                    #tensor = get_few_frames(tensor, center=tensor.shape[0]//2, n_points=14, step=5)
                    tensor = process(tensor)
                    batch.append(tensor)
            batch = np.array(batch).astype('uint8')
            labels = np.array(labels)
            yield batch, labels
### For training process - end




