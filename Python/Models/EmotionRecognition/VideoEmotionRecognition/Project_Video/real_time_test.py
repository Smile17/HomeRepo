import cv2
from keras.models import load_model
import numpy as np
import pandas as pd
import h5py
import time

face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')

rescale = 0.5
face_size = (200, 200)

def grayscale(tensor):
    """Convert RGB tensor (n_frames, size_x, size_y, channel) to,
    grayscale tensor (n_frames, size_x, size_y, 1)"""
    r = tensor[:, :, 0]
    g = tensor[:, :, 1]
    b = tensor[:, :, 2]
    gray = 0.2989 * r + 0.5870 * g + 0.1140 * b
    gray = gray.reshape((tensor.shape[0], tensor.shape[1], 1)).astype('uint8')
    return gray

def get_few_frames(tensor, center, n_points, step, eq_hist=True, blur=True, normalize=True):
    """From video tensor with shape (n_frames, ...) get n_points
    frames around center with some step"""
    indexes = center + np.arange(-n_points//2, n_points//2+1)*step
    tensor = tensor[indexes]
    if eq_hist:
        shape_3d = (tensor.shape[1], tensor.shape[2], tensor.shape[3])
        for i in range(tensor.shape[0]):
            tensor[i] = cv2.equalizeHist(tensor[i].astype('uint8')).reshape(shape_3d)

    if blur:
        shape_3d = (tensor.shape[1], tensor.shape[2], tensor.shape[3])
        for i in range(tensor.shape[0]):
            tensor[i] = cv2.GaussianBlur(tensor[i].astype('uint8'), (5, 5), 3).reshape(shape_3d)
    return tensor

def draw_text(coordinates, image_array, text, color, x_offset=0, y_offset=0,
                                                font_scale=2, thickness=2):
    x, y = coordinates[:2]
    cv2.putText(image_array, text, (x + x_offset, y + y_offset),
                cv2.FONT_HERSHEY_SIMPLEX,
                font_scale, color, thickness, cv2.LINE_AA)

size = 5
n_points = 4

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

model = load_model('models//model_11_20_2093_1.hdf5')
#models//model_11_20_2093_1.hdf5

cv2.namedWindow('window_frame')
cap = cv2.VideoCapture('C:\\Users\\kam\\Documents\\DiplomaExperiments\\DataSet_Testing\\Video_Song_Actor_16\\Actor_16\\01-02-03-01-01-01-16.mp4')
#cap = cv2.VideoCapture(0)
shape_3d = (200, 200, 1)

clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8, 8))
emotion = 'neutral'
frames = []

fourcc = cv2.VideoWriter_fourcc(*'XVID')
out = cv2.VideoWriter('result_output.avi', fourcc, 30, (1280, 720))


def show_image(image):
    cv2.imshow('image',image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

while True:
    retval, image = cap.read()
    rgb_image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    if not retval:
        break
    try:
        #show_image(image)
        (x, y, w, h) = face_cascade.detectMultiScale(image)[0]
        image = image[y:y+h, x:x+w, :]
        image = cv2.resize(image, None, fx=rescale, fy=rescale).astype('uint8')
        image = cv2.resize(image, dsize=face_size)
        image = grayscale(image).astype('uint8')
        image = cv2.equalizeHist(image).reshape(shape_3d)
        image = clahe.apply(image)
        image = cv2.GaussianBlur(image, (5, 5), 3).reshape(shape_3d)
        frames.append(image)

        color = (0, 0, 255)
        cv2.rectangle(rgb_image, (x, y), (x + w, y + h), color, 2)
        draw_text((x, y, w, h), rgb_image, emotion,
                  color, 0, -45, 1, 1)
        out.write(cv2.cvtColor(rgb_image, cv2.COLOR_RGB2BGR))
    except IndexError:
        print('no face!')
    if len(frames) == 30:
        frames = np.array(frames)
        frames = get_few_frames(frames, frames.shape[0]//2, 4, 7).reshape(1, 5, 200, 200, 1)
        predictions = model.predict(frames)
        emotion = emotions[predictions.argmax()]
        print('Prediction - {x}'.format(x=emotion))
        frames = []
    bgr_image = cv2.cvtColor(rgb_image, cv2.COLOR_RGB2BGR)
    cv2.imshow('window_frame', bgr_image)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break
