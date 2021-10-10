# This code get videos from the folder defined in the __init__ method of Processing class
# And creates a bin\\metadata.csv file where we save metadata including:
#-actor number;
#-emotion type;
#-file path;
#-file type;
#-emotion vector - 0,0,0,0,0,0,0,1 means surprised.

import cv2
import os
import shutil
import pandas as pd

base_path = ''
def parse_file_name(file_name):
    row = {}

    meta, extension = file_name[:-4], file_name[-4:]
    meta = meta.split('-')
    _, vocal_channel, emotion, emotional_intensity, _, _, actor = meta

    row['file_name'] = file_name

    if extension == '.mp4':
        row['modality'] = 'video'
    if extension == '.wav':
        row['modality'] = 'audio'

    if vocal_channel == '01':
        row['vocal_channel'] = 'speech'
    if vocal_channel == '02':
        row['vocal_channel'] = 'song'

    if emotion == '01':
        row['emotion'] = 'neutral'
    if emotion == '02':
        row['emotion'] = 'calm'
    if emotion == '03':
        row['emotion'] = 'happy'
    if emotion == '04':
        row['emotion'] = 'sad'
    if emotion == '05':
        row['emotion'] = 'angry'
    if emotion == '06':
        row['emotion'] = 'fearful'
    if emotion == '07':
        row['emotion'] = 'disgust'
    if emotion == '08':
        row['emotion'] = 'surprised'

    if emotional_intensity == '01':
        row['emotional_intensity'] = 'normal'
    if emotional_intensity == '02':
        row['emotional_intensity'] = 'strong'

    row['actor'] = int(actor)
    return row

class Preprocessing:
    #Defines folder where we get data
    def __init__(self, folder='..\DataSet'):
        self.folder = folder

    def unpack_all(self):
        zips = [x for _, _, files in os.walk(os.getcwd()) for x in files if x.endswith('.zip')]
        os.mkdir('data')
        for archive in zips:
            os.system('unzip {x} -d data/'.format(x=archive))

    def parse_audio_video(self):
        generator = os.walk(self.folder)
        video = [x for _, _, files in os.walk(self.folder) for x in files if x.endswith('.mp4')]
        for record in video:
            os.system('ffmpeg -i ' + os.path.join(self.folder, record) + ' ' + os.path.join(self.folder, record[:-4] + '.wav'))

    def generate_metadata(self):
        data = [os.path.join(paths, x) for paths, dirs, files in os.walk(self.folder) for x in files]
        data = [parse_file_name(file_name) for file_name in data]
        data = pd.DataFrame(data)
        data = pd.concat([data, pd.get_dummies(data['emotion'])], axis=1)
        data.to_csv(base_path + 'bin\\metadata.csv', index=False)

obj = Preprocessing()
obj.generate_metadata()
print("Done")