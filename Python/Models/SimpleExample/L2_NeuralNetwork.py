import pandas as pd
import re
import numpy as np

# Data loading and preprocessing
path = 'data/promoters.txt'
df = pd.read_csv(path, header = None)
df.columns = ['IsPromoter', 'Amin', 'Sequence']
df['IsPromoter'] = df['IsPromoter'].apply(lambda x: 1 if x == '+' else 0)
df['Sequence'] = df['Sequence'].apply(lambda x: re.sub('\s+', '', x))
def convert(s):
    d = {'t': [1, 0, 0, 0], 'c': [0, 1, 0, 0], 'a': [0, 0, 1, 0], 'g': [0, 0, 0, 1]}
    res = []
    [res.extend(d[c]) for c in s]
    return res
df['Sequence_array'] = df['Sequence'].apply(lambda x: convert(x))
df1 = pd.DataFrame(df['Sequence_array'].values.tolist())
df.head()
X = df1.to_numpy()
y = df['IsPromoter'].to_numpy()

#np.any(df1.isnull())

from sklearn.model_selection import train_test_split
from sklearn.neural_network import MLPClassifier
from sklearn.metrics import accuracy_score
from sklearn.model_selection import KFold

accs = []
cv = KFold(n_splits=5, shuffle=True)
for train_index, test_index in cv.split(X, y):
    X_train, y_train = X[train_index], y[train_index]
    X_test, y_test = X[test_index], y[test_index]
    model = MLPClassifier(solver='lbfgs', alpha=1e-5, hidden_layer_sizes=(5, 2), random_state=1)
    model.fit(X_train, y_train)
    y_pred = model.predict(X_test)
    accs.append(accuracy_score(y_test, y_pred))
accs = np.array(accs)
acc = accs.mean()
print('The mean accuracy is {}'.format(np.round(acc, 4)))

print("Спершу нам потрібно привести дані до числового виду, колонку з класом ми переводимо \"+\" в 1, а \"-\" в 0. Колонку з нуклетидами також переводимо у числові дані, наступним чином, кожну літеру (t, c, g, a) ми дешифруємо у 4 колонки (відовідно t -> (1,0,0,0), c -> (0,1,0,0), g -> (0,0,1,0), a -> (0,0,0,1)). Так робимо із кожною літерою. В результаті отримуємо 4 * 57 = 228 колонок. Ці 228 колонок даних - будуть нашим X, вхідними параметрами, колонка з класов - вихідним параметром. Далі розбиваємо дані на тренувальну та навчальну вибірки, будуємо нейронну мережу за допомогою бібліотеки sklearn, з 5 вершинами у прихованому шарі. Проводимо навчання та тестування моделі. Для того, щоб результати були менш вразливі щодо вибору тестових та тренувальних даних, застосовуємо підхід cross-validation, тобто проводимо кілька навчань та тестувань, кожен раз оцінюючи точність моделі. Після чого точністю вважаємо середнє значення. ")