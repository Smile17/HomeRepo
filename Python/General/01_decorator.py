import re
def outer_wrapper(func):
    def wrapper(*args, **kwargs):
        args = list(args)
        for i, s in enumerate(args):
            s = re.sub('["|!|$|,|*|+|/|^|#|&|\\\|.|\]]', '', s)
            args[i] = s
        res = func(*args, **kwargs)
        return res # или func(*args, **kwargs) - можно и тут...
    return wrapper

@outer_wrapper
def func(line):
    print(line)
    return line

func('This is my special text, with special simbols. They are such as "!,$*+/\\]", but what a F..k')