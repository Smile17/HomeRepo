import pandas as pd
import numpy as np
import re

from sklearn.preprocessing import LabelEncoder


class DataLoader(object):
    def fit(self, dataset):
        self.dataset = dataset.copy()

    # apply regex
    def get_title(self, name):
        pattern = ' ([A-Za-z]+)\.'
        title_search = re.search(pattern, name)
        # If the title exists, extract and return it.
        if title_search:
            return title_search.group(1)
        return ""

    def load_data(self):
        # columns combination
        self.dataset['FamilySize'] = self.dataset['SibSp'] + self.dataset['Parch'] + 1

        # replace value
        self.dataset['IsAlone'] = 0
        self.dataset.loc[self.dataset['FamilySize'] == 1, 'IsAlone'] = 1

        # fill Nan with mode
        self.dataset['Embarked'] = self.dataset['Embarked'].fillna(self.dataset['Embarked'].mode()[0])

        # fill Nan with median
        self.dataset['Fare'] = self.dataset['Fare'].fillna(self.dataset['Fare'].median())
        # binning with qcut
        self.dataset['Fare'] = pd.qcut(self.dataset['Fare'], 4)

        # fill Nan with values from random distribution
        age_avg = self.dataset['Age'].mean()
        age_std = self.dataset['Age'].std()
        age_null_count = self.dataset['Age'].isnull().sum()
        rng = np.random.RandomState(42)
        age_null_random_list = rng.uniform(age_avg - age_std, age_avg + age_std, size=age_null_count)
        self.dataset['Age'][np.isnan(self.dataset['Age'])] = age_null_random_list

        # binning with cut
        self.dataset['Age'] = pd.cut(self.dataset['Age'], 5)

        # apply regex
        self.dataset['Title'] = self.dataset['Name'].apply(self.get_title)
        # replace
        self.dataset['Title'] = self.dataset['Title'].replace(['Lady', 'Countess', 'Capt', 'Col', 'Don',
                                                               'Dr', 'Major', 'Rev', 'Sir', 'Jonkheer', 'Dona'],
                                                              'Rare')
        # replace
        self.dataset['Title'] = self.dataset['Title'].replace(['Mlle', 'Ms'], 'Miss')
        # replace
        self.dataset['Title'] = self.dataset['Title'].replace('Mme', 'Mrs')
        # fill nans
        self.dataset['Title'] = self.dataset['Title'].fillna(0)

        # drop columns
        drop_elements = ['PassengerId', 'Name', 'Ticket', 'Cabin', 'SibSp',
                         'Parch', 'FamilySize']

        self.dataset = self.dataset.drop(drop_elements, axis=1)

        # encode labels
        le = LabelEncoder()

        le.fit(self.dataset['Sex'])
        self.dataset['Sex'] = le.transform(self.dataset['Sex'])

        le.fit(self.dataset['Title'])
        self.dataset['Title'] = le.transform(self.dataset['Title'])

        le.fit(self.dataset['Embarked'].values)
        self.dataset['Embarked'] = le.transform(self.dataset['Embarked'].values)

        le.fit(self.dataset['Fare'])
        self.dataset['Fare'] = le.transform(self.dataset['Fare'])

        le.fit(self.dataset['Age'])
        self.dataset['Age'] = le.transform(self.dataset['Age'])

        return self.dataset
