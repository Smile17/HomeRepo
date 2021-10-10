#This code using metadata.csv file finds each video, extracts faces and save data to zipped format
import pandas as pd

import video_recognition_library as vrl
import video_save_h5_library as vpl
import video_recognition_dlib_library as vrdl

base_path = 'bin\\'
metadata = pd.read_csv(base_path + 'metadata.csv')
metadata = metadata.loc[metadata.modality == 'video']

def get_video_tensor(file_name):
    return vrl.get_video_tensor(file_name, preprocess_video)

def preprocess_video(image):
    image = vrl.preprocess_face_detection(image)
    image = vrdl.get_landmarks(image)
    return image

vpl.save_hdf5(metadata, base_path, "data_dlib.h5", "labels_dlib.h5", get_video_tensor)
vpl.save_size(metadata, base_path, "data_dlib.h5")
print("Done")