# docker build -t 'smilik/app2:v01' .
# docker run --network=host --env DB_URL=postgresql+psycopg2://test_user:password@localhost:5432/test_db -p 8000:8000 'smilik/app2:v01'
# docker tag 'smilik/app2:v01' 'smilik/app2:v01'
# docker push 'smilik/app2:v01'
import os

DATA_FOLDER = 'data'
TRAIN_CSV = os.path.join(DATA_FOLDER, 'train.csv')
VAL_CSV = os.path.join(DATA_FOLDER, 'val.csv')
SAVED_ESTIMATOR = os.path.join('models', 'SVC.pickle')