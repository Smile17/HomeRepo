# docker build -t 'smilik/app:v02' .
# docker run --network=host --env DB_URL=postgresql+psycopg2://test_user:password@localhost:5432/test_db -p 8000:8000 'smilik/app:v02'
# docker tag 'smilik/app:v02' 'smilik/app:v02'
# docker push 'smilik/app:v02'
from flask import jsonify, make_response

from datetime import datetime as dt
from ast import literal_eval

from models.actor import Actor
from models.movie import Movie
from settings.constants import ACTOR_FIELDS  # to make response pretty
from .parse_request import get_request_data
from settings.constants import DATE_FORMAT


def get_all_actors():
    """
    Get list of all records
    """
    all_actors = Actor.query.all()
    actors = []
    for actor in all_actors:
        act = {k: v for k, v in actor.__dict__.items() if k in ACTOR_FIELDS}
        actors.append(act)
    return make_response(jsonify(actors), 200)


def get_actor_by_id():
    """
    Get record by id
    """
    data = get_request_data()
    if 'id' in data.keys():
        try:
            row_id = int(data['id'])
        except:
            err = 'Id must be integer'
            return make_response(jsonify(error=err), 400)

        obj = Actor.query.filter_by(id=row_id).first()
        try:
            actor = {k: v for k, v in obj.__dict__.items() if k in ACTOR_FIELDS}
        except:
            err = 'Record with such id does not exist'
            return make_response(jsonify(error=err), 400)

        return make_response(jsonify(actor), 200)

    else:
        err = 'No id specified'
        return make_response(jsonify(error=err), 400)


def add_actor():
    """
    Add new actor
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    if len(set(data.keys()).difference(ACTOR_FIELDS)) != 0:
        err = 'Inputted fields should exist'
        return make_response(jsonify(error=err), 400)
    if 'date_of_birth' in data.keys():
        date_text = data['date_of_birth']
        try:
            data['date_of_birth'] = dt.strptime(date_text, DATE_FORMAT).date()
        except ValueError:
            err = 'Incorrect data format, should be ' + DATE_FORMAT
            return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    new_record = Actor.create(**data)
    new_actor = {k: v for k, v in new_record.__dict__.items() if k in ACTOR_FIELDS}
    return make_response(jsonify(new_actor), 200)
    ### END CODE HERE ###


def update_actor():
    """
    Update actor record by id
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    if len(set(data.keys()).difference(ACTOR_FIELDS)) != 0:
        err = 'Inputted fields should exist'
        return make_response(jsonify(error=err), 400)
    # Id checking
    if not ('id' in data.keys()):
        err = 'No id specified'
        return make_response(jsonify(error=err), 400)
    try:
        row_id = int(data['id'])
    except:
        err = 'Id must be integer'
        return make_response(jsonify(error=err), 400)
    obj = Actor.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Actor with such id should exist'
        return make_response(jsonify(error=err), 400)

    if 'date_of_birth' in data.keys():
        date_text = data['date_of_birth']
        try:
            data['date_of_birth'] = dt.strptime(date_text, DATE_FORMAT).date()
        except ValueError:
            err = 'Incorrect data format, should be ' + DATE_FORMAT
            return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    upd_record = Actor.update(row_id, **data)
    upd_actor = {k: v for k, v in upd_record.__dict__.items() if k in ACTOR_FIELDS}
    return make_response(jsonify(upd_actor), 200)
    ### END CODE HERE ###


def delete_actor():
    """
    Delete actor by id
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    # Id checking
    if not ('id' in data.keys()):
        err = 'No id specified'
        return make_response(jsonify(error=err), 400)
    try:
        row_id = int(data['id'])
    except:
        err = 'Id must be integer'
        return make_response(jsonify(error=err), 400)
    obj = Actor.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Actor with such id should exist'
        return make_response(jsonify(error=err), 400)
    Actor.delete(row_id)
    # use this for 200 response code
    msg = 'Record successfully deleted'
    return make_response(jsonify(message=msg), 200)
    ### END CODE HERE ###


def actor_add_relation():
    """
    Add a movie to actor's filmography
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    # Id checking
    if not ('id' in data.keys()):
        err = 'No actor id specified'
        return make_response(jsonify(error=err), 400)
    try:
        actor_id = int(data['id'])
    except:
        err = 'Id must be integer'
        return make_response(jsonify(error=err), 400)
    actor = Actor.query.filter_by(id=actor_id).first()
    if not actor:
        err = 'Actor with such id should exist'
        return make_response(jsonify(error=err), 400)

    if not ('relation_id' in data.keys()):
        err = 'No movie id specified'
        return make_response(jsonify(error=err), 400)
    try:
        movie_id = int(data['relation_id'])
    except:
        err = 'Movie_id must be integer'
        return make_response(jsonify(error=err), 400)
    movie = Movie.query.filter_by(id=movie_id).first()
    if not movie:
        err = 'Movie with such id should exist'
        return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    actor = Actor.add_relation(actor_id, movie)
    rel_actor = {k: v for k, v in actor.__dict__.items() if k in ACTOR_FIELDS}
    rel_actor['filmography'] = str(actor.filmography)
    return make_response(jsonify(rel_actor), 200)
    ### END CODE HERE ###


def actor_clear_relations():
    """
    Clear all relations by id
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    # Id checking
    if not ('id' in data.keys()):
        err = 'No id specified'
        return make_response(jsonify(error=err), 400)
    try:
        row_id = int(data['id'])
    except:
        err = 'Id must be integer'
        return make_response(jsonify(error=err), 400)
    obj = Actor.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Actor with such id should exist'
        return make_response(jsonify(error=err), 400)

    actor = Actor.clear_relations(row_id)

    # use this for 200 response code
    rel_actor = {k: v for k, v in actor.__dict__.items() if k in ACTOR_FIELDS}
    rel_actor['filmography'] = str(actor.filmography)
    return make_response(jsonify(rel_actor), 200)
    ### END CODE HERE ###