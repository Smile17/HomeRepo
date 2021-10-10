from flask import jsonify, make_response

from ast import literal_eval

from models.actor import Actor
from models.movie import Movie
from settings.constants import MOVIE_FIELDS
from .parse_request import get_request_data


def get_all_movies():
    """
    Get list of all records
    """
    all_movies = Movie.query.all()
    movies = []
    for movie in all_movies:
        act = {k: v for k, v in movie.__dict__.items() if k in MOVIE_FIELDS}
        movies.append(act)
    return make_response(jsonify(movies), 200)

def get_movie_by_id():
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

        obj = Movie.query.filter_by(id=row_id).first()
        try:
            actor = {k: v for k, v in obj.__dict__.items() if k in MOVIE_FIELDS}
        except:
            err = 'Record with such id does not exist'
            return make_response(jsonify(error=err), 400)

        return make_response(jsonify(actor), 200)

    else:
        err = 'No id specified'
        return make_response(jsonify(error=err), 400)

def add_movie():
    """
    Add new movie
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    if len(set(data.keys()).difference(MOVIE_FIELDS)) != 0:
        err = 'Inputted fields should exist'
        return make_response(jsonify(error=err), 400)
    if not ('year' in data.keys()):
        err = 'No year specified'
        return make_response(jsonify(error=err), 400)
    try:
        data['year'] = int(data['year'])
    except:
        err = 'Year must be integer'
        return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    new_record = Movie.create(**data)
    new_movie = {k: v for k, v in new_record.__dict__.items() if k in MOVIE_FIELDS}
    return make_response(jsonify(new_movie), 200)
    ### END CODE HERE ###

def update_movie():
    """
    Update movie record by id
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    if len(set(data.keys()).difference(MOVIE_FIELDS)) != 0:
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
    obj = Movie.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Movie with such id should exist'
        return make_response(jsonify(error=err), 400)

    if 'year' in data.keys():
        try:
            data['year'] = int(data['year'])
        except:
            err = 'Year must be integer'
            return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    upd_record = Movie.update(row_id, **data)
    upd_actor = {k: v for k, v in upd_record.__dict__.items() if k in MOVIE_FIELDS}
    return make_response(jsonify(upd_actor), 200)
    ### END CODE HERE ###

def delete_movie():
    """
    Delete movie by id
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
    obj = Movie.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Movie with such id should exist'
        return make_response(jsonify(error=err), 400)
    Movie.delete(row_id)
    # use this for 200 response code
    msg = 'Record successfully deleted'
    return make_response(jsonify(message=msg), 200)
    ### END CODE HERE ###

def movie_add_relation():
    """
    Add actor to movie's cast
    """
    data = get_request_data()
    ### YOUR CODE HERE ###
    # Id checking
    if not ('relation_id' in data.keys()):
        err = 'No actor id specified'
        return make_response(jsonify(error=err), 400)
    try:
        actor_id = int(data['relation_id'])
    except:
        err = 'Actor_id must be integer'
        return make_response(jsonify(error=err), 400)
    actor = Actor.query.filter_by(id=actor_id).first()
    if not actor:
        err = 'Actor with such id should exist'
        return make_response(jsonify(error=err), 400)

    if not ('id' in data.keys()):
        err = 'No movie id specified'
        return make_response(jsonify(error=err), 400)
    try:
        movie_id = int(data['id'])
    except:
        err = 'Movie_id must be integer'
        return make_response(jsonify(error=err), 400)
    movie = Movie.query.filter_by(id=movie_id).first()
    if not movie:
        err = 'Movie with such id should exist'
        return make_response(jsonify(error=err), 400)

    # use this for 200 response code
    movie = Movie.add_relation(movie_id, actor)
    rel_movie = {k: v for k, v in movie.__dict__.items() if k in MOVIE_FIELDS}
    rel_movie['cast'] = str(movie.cast)
    return make_response(jsonify(rel_movie), 200)
    ### END CODE HERE ###

def movie_clear_relations():
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
    obj = Movie.query.filter_by(id=row_id).first()
    if not obj:
        err = 'Movie with such id should exist'
        return make_response(jsonify(error=err), 400)

    movie = Movie.clear_relations(row_id)

    # use this for 200 response code
    rel_movie = {k: v for k, v in movie.__dict__.items() if k in MOVIE_FIELDS}
    rel_movie['cast'] = str(movie.cast)
    return make_response(jsonify(rel_movie), 200)
    ### END CODE HERE ###