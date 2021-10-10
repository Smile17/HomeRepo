#This code is used for save results to data.h5 and labels.h5 files

#PREREQUISITES FOR LIBRARIES
#Import packages using cmd:
#python -m pip install h5py

import numpy as np
import h5py
import datetime

def save_hdf5(metadata, base_path, data_file_name, label_file_name, get_video_tensor):
    hf_data = h5py.File(base_path + data_file_name, 'w')
    hf_labels = h5py.File(base_path + label_file_name, 'w')
    cnt=0
    for index, row in metadata.iterrows():
        if cnt % 100 == 0:
            print(datetime.datetime.now())
        cnt+=1
        tensor = get_video_tensor(row.file_name)
        if len(tensor) != 0:
            labels = np.repeat(row[-8:].values.reshape(1, -1), tensor.shape[0], axis=0)
            labels=labels.astype(int)
            hf_data.create_dataset(row.file_name, data=tensor)
            hf_labels.create_dataset(row.file_name, data=labels)
        #if cnt == 10:
        #    break

def save_size(metadata, base_path, data_file_name):
    hf_data = h5py.File(base_path + data_file_name, 'r')
    metadata['size'] = 0
    cnt=0
    for index, row in metadata.iterrows():
        if cnt%100 == 0:
            print('save_size:')
            print(datetime.datetime.now())
        cnt+=1
        tensor = hf_data.get(row.file_name)
        if not tensor:
            break
        metadata.loc[metadata.index == index, 'size'] = tensor.shape[0]
    metadata.to_csv(base_path + 'metadata.csv', index=False)