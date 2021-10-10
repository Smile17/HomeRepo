# Added 2 lines and .env file to read DB_URL
from dotenv import load_dotenv
load_dotenv()

import os
# db connection URL (In order to submit your project do NOT change this value!!!)
DB_URL = os.environ['DB_URL']
# entities properties
ACTOR_FIELDS = ['id', 'name', 'gender', 'date_of_birth']
MOVIE_FIELDS = ['id', 'name', 'year', 'genre']

# date of birth format
DATE_FORMAT = '%d.%m.%Y'