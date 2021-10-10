# Setup
from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from datetime import datetime as dt
from sqlalchemy import inspect

from settings.constants import DB_URL
from core import db
from models.actor import Actor
from models.movie import Movie


data_actor = {'name': 'Megan Fox', 'gender': 'female', 'date_of_birth': dt.strptime('16.05.1986', '%d.%m.%Y').date()}
data_actor2 = {'name': 'Cameron Decker', 'gender': 'male', 'date_of_birth': dt.strptime('16.05.1989', '%d.%m.%Y').date()}
data_movie = {'name': 'Transformers', 'genre': 'action', 'year': 2007}
data_movie2 = {'name': 'Teenage Mutant Ninja Turtles', 'genre': 'bad movie', 'year': 2014}

app = Flask(__name__, instance_relative_config=False)
app.config['SQLALCHEMY_DATABASE_URI'] = DB_URL
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False  # silence the deprecation warning

db.init_app(app)

with app.app_context():
    db.reflect()
    db.drop_all()
    db.create_all()
    actor = Actor.create(**data_actor)
    print('created actor:', actor.__dict__, '\n')
    actor = Actor.create(**data_actor2)
    print('created actor:', actor.__dict__, '\n')
    movie = Movie.create(**data_movie)
    print('created movie:', movie.__dict__, '\n')
    movie = Movie.create(**data_movie2)
    print('created movie:', movie.__dict__, '\n')



from flask import jsonify, make_response

from datetime import datetime as dt
from ast import literal_eval

from models.actor import Actor
from models.movie import Movie
from settings.constants import ACTOR_FIELDS  # to make response pretty
from settings.constants import DATE_FORMAT

from controllers.actor import *


with app.app_context():
    res = get_all_actors()
    print(res.json)
    res = get_actor_by_id({'id': 1})
    print(res.json)
    res = get_actor_by_id({'id': 3})
    print(res.json)
    print(res)

with app.app_context():
    data = {'name': 'Norman Decker', 'gender': 'male',
                   'date_of_birth': '26.01.1989'}
    res = add_actor(data)
    print(res.json)
    data = {'something': 'value', 'id': 5}
    res = add_actor(data)
    print(res.json)
    data = {'name': 'Norman Decker', 'gender': 'male'}
    res = add_actor(data)
    print(res.json)
    data = {'name': 'Norman Decker', 'gender': 'male', 'date_of_birth':'23-07-1990'}
    res = add_actor(data)
    print(res.json)

with app.app_context():
    data = {'id': '1', 'name': 'Norman Decker', 'gender': 'male',
            'date_of_birth': '26.01.1989'}
    res = update_actor(data)
    print(res.json)
    data = {'name': 'Norman Decker', 'gender': 'male',
            'date_of_birth': '26.01.1989'}
    res = update_actor(data)
    print(res.json)
    data = {'id': '23dsw', 'name': 'Norman Decker', 'gender': 'male',
            'date_of_birth': '26.01.1989'}
    res = update_actor(data)
    print(res.json)
    data = {'id': '100', 'name': 'Norman Decker', 'gender': 'male',
            'date_of_birth': '26.01.1989'}
    res = update_actor(data)
    print(res.json)

with app.app_context():
    data = {'id': '5'}
    res = delete_actor(data)
    print(res.json)
    data = {'actor_id': 1, 'movie_id': 1}
    res = actor_add_relation(data)
    print(res.json)
    data = {'id': 1}
    res = actor_clear_relations(data)
    print(res.json)



