#This code using metadata.csv file finds each video, extracts faces and save data to zipped format

#PREREQUISITES FOR LIBRARIES
#Import packages using cmd:
#python -m pip install h5py
#Import cv2:
#python -m pip install opencv-python
#python -m pip install opencv-contrib-python
#Import dlib
#python -m pip install --user dlib
#python -m pip install dlib

import pandas as pd
import video_recognition_library as vrl
import video_save_h5_library as vpl

base_path = 'bin\\'
metadata = pd.read_csv(base_path + 'metadata.csv')
metadata = metadata.loc[metadata.modality == 'video']

def get_video_tensor(file_name):
    return vrl.get_video_tensor(file_name, vrl.preprocess_face_detection)

vpl.save_hdf5(metadata, base_path, "data.h5", "labels.h5", get_video_tensor)
vpl.save_size(metadata, base_path, "data.h5")
print("Done")