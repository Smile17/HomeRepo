# docker build -t 'smilik/app3:v24' .
# docker run -p 8000:8000 'smilik/app3:v24'
# docker tag 'smilik/app3:v24' 'smilik/app3:v24'
# docker push 'smilik/app3:v24'
import os

DATA_FOLDER = 'data'
TRAIN_CSV = os.path.join(DATA_FOLDER, 'train.csv')
VAL_CSV = os.path.join(DATA_FOLDER, 'val.csv')
SAVED_ESTIMATOR = os.path.join('models', 'GBC.pickle')