
'''
Создать функцию с неограниченым кол-в вызовов где будут представленны числа и операторы "+" и "-".
Функция должна узнать ответ сумы всех вызваных чисел, последняя вызваная функция будет иметь оператор "=".
Например, наша функция будет называтся "calculator", тогда все примеры ниже должны быть истины для такой функции:
calculator(1)('+')(4)('-')(2)('=') == 3 calculator(1)('+')(4)('-')(2)('+')(10)('=') == 13
Ограничения: Нельзя ничего импортировать и использывать глобальные переменные.
'''
def calc(v):
    def _inner_calc(val='='):
        if val == '=':
            return _inner_calc.res
        if(val == '+') or (val == '-'):
            _inner_calc.op = val
        else:
            if(_inner_calc.op == '+'):
                _inner_calc.res += int(val)
            else:
                _inner_calc.res -= int(val)
        return _inner_calc
    if(v == '+') or (v == '-'):
        _inner_calc.res = 0
        _inner_calc.op = v
    else:
        _inner_calc.res = int(v)
    return _inner_calc


print(calc('2')('+')('3')('='))

def calculator(v):
    def _inner_calc(val='='):
        if val == '=':
            return parseCalculation(_inner_calc.v)
        _inner_calc.v += val
        return _inner_calc
    _inner_calc.v = v  # save value
    return _inner_calc

def parseCalculation(s):
    res = 0
    op = '+'
    for x in s:
        if(x == '+') or (x == '-'):
            op = x
        else:
            if(op == '+'):
                res += int(x)
            else:
                res -= int(x)
    return res

print(calculator('2')('+')('3')('='))
#print(parseCalculation('2+3-7+2-9'))
