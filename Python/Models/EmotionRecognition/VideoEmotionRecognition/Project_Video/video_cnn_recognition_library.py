import cv2
import numpy as np
import video_recognition_library as vrl
#import pandas as pd

face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')

def show_image(image):
    cv2.imshow('image',image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

### For video preprocessing
def grayscale(tensor):
    """Convert RGB tensor (n_frames, size_x, size_y, channel) to
    grayscale tensor (n_frames, size_x, size_y, 1)"""
    r = tensor[:, :, :, 0]
    g = tensor[:, :, :, 1]
    b = tensor[:, :, :, 2]
    gray = 0.2989 * r + 0.5870 * g + 0.1140 * b
    gray = gray.reshape((tensor.shape[0], tensor.shape[1], tensor.shape[2], 1))
    return gray

def eq_hist(tensor):
    shape_3d = (tensor.shape[1], tensor.shape[2], tensor.shape[3])
    for i in range(tensor.shape[0]):
        tensor[i] = cv2.equalizeHist(tensor[i].astype('uint8')).reshape(shape_3d)
def blur(tensor):
    shape_3d = (tensor.shape[1], tensor.shape[2], tensor.shape[3])
    for i in range(tensor.shape[0]):
        tensor[i] = cv2.GaussianBlur(tensor[i].astype('uint8'), (5, 5), 3).reshape(shape_3d)
### For video preprocessing - end

def get_few_frames(tensor, center, n_points, step, is_face_detect = False, is_eq_hist=True, is_blur=True):
    """From video tensor with shape (n_frames, ...) get n_points
    frames around center with some step"""
    indexes = center + np.arange(-n_points//2, n_points//2+1)*step
    tensor = tensor[indexes]
    if is_face_detect:
        tensor = vrl.face_detection(tensor)
        tensor = grayscale(tensor)
    if is_eq_hist:
        eq_hist(tensor)
    if is_blur:
        blur(tensor)
    return tensor

def preprocess_video(file_name):
    tensor = vrl.get_video_tensor(file_name)
    frames = get_few_frames(tensor, tensor.shape[0]//2, 4, 7, is_face_detect = True)
    try:
        frames = frames.reshape(1, 5, vrl.face_size[0], vrl.face_size[1], 1)
    except:
        frames = []
        print(file_name)
    return frames

def test():
    frames = preprocess_video('C:\\Users\\kam\\Documents\\DiplomaExperiments\\DataSet\\Video_Song_Actor_03\\Actor_03\\01-02-01-01-01-01-03.mp4')
    print(frames.shape)
    print('Done')
#test()

### For the archive
def sliding_window(data, size, stepsize=1, axis=0, copy=True):
    """Get slices of the video tensor"""
    if axis >= data.ndim:
        raise ValueError(
            "Axis value out of range"
        )
    if stepsize < 1:
        raise ValueError(
            "Stepsize may not be zero or negative"
        )
    if size > data.shape[axis]:
        raise ValueError(
            "Sliding window size may not exceed size of selected axis"
        )
    shape = list(data.shape)
    shape[axis] = np.floor(data.shape[axis] / stepsize - size / stepsize + 1).astype(int)
    shape.insert(1, size)
    
    strides = list(data.strides)
    strides[axis] *= stepsize
    strides.insert(1, data.strides[axis])
    strided = np.lib.stride_tricks.as_strided(
        data, shape=shape, strides=strides
    )

    if copy:
        return strided.copy()
    else:
        return strided
def generate_slices(metadata, n_video=10, size=50, stepsize=10):
    hf_data = h5py.File('data.h5', 'r')
    hf_labels = h5py.File('labels.h5', 'r')
    while True:
        n_video_groups = np.ceil(metadata.shape[0]/n_video).astype(int)
        for index, row in metadata.iterrows():
            tensor = grayscale(hf_data.get(row.file_name)).astype('uint8')
            labels = hf_labels.get(row.file_name)
            tensor = sliding_window(tensor, size, stepsize, copy=False)
            yield tensor, labels[0]
### For the archive - end

    