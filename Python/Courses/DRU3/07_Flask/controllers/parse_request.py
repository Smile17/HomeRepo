from flask import request


def get_request_data():
    """
    Get keys & values from request (Note that this method should parse requests with content type "application/x-www-form-urlencoded")
    """
    d = request.form
    data = {k:v[0] for k,v in zip(d.keys(), d.listvalues())}
    return data