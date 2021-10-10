import pandas as pd
import numpy as np

from sklearn.preprocessing import LabelEncoder
#from sklearn.preprocessing import OneHotEncoder

class DataLoader(object):
    def fit(self, dataset):
        self.dataset = dataset.copy()

    def load_data(self):
        train = self.dataset
        # target = 'Attrition_Flag'

        # replace value
        train['IsAlone'] = 0
        train.loc[train['Dependent_count'] == 0, 'IsAlone'] = 1

        # encode labels
        le = LabelEncoder()

        # le.fit(train[target])
        # train[target] = le.transform(train[target]) # 1 - Existing, 0 - Attrited

        # encode labels
        col = 'Gender'
        le.fit(train[col])
        train[col] = le.transform(train[col])

        # encode labels one-hot-encoding
        # enc = OneHotEncoder()
        # col = 'Marital_Status'
        # enc_df = pd.DataFrame(enc.fit_transform(train[[col]]).toarray())
        # column_name = enc.get_feature_names([col])
        # enc_df.rename(columns=dict(zip(range(4), column_name)), inplace=True)
        # train = train.join(enc_df)
        col = 'Marital_Status'
        le.fit(train[col])
        train[col] = le.transform(train[col])

        # replace value and fill na
        col = 'Education_Level'
        train[col] = train[col].map(
            {'Uneducated': 0, 'High School': 1, 'College': 2, 'Graduate': 3, 'Post-Graduate': 4, 'Doctorate': 5,
             'Unknown': None})
        train[col] = train[col].fillna(train[col].mode()[0])
        # replace value and fill na
        col = 'Income_Category'
        train[col] = train[col].map(
            {'Less than $40K': 2, '$40K - $60K': 5, '$60K - $80K': 7, '$80K - $120K': 10, '$120K +': 14,
             'Unknown': None})
        train[col] = train[col].fillna(train[col].mode()[0])
        # replace value and fill na
        col = 'Card_Category'
        train[col] = train[col].map({'Blue': 0, 'Silver': 1, 'Gold': 2, 'Platinum': 3})

        # standardize
        cols = ['Customer_Age',
                'Total_Trans_Amt',
                'Total_Trans_Ct',
                'Months_on_book',
                'Credit_Limit',
                'Total_Revolving_Bal',
                'Avg_Open_To_Buy']
        train[cols] = (train[cols] - train[cols].mean()) / train[cols].std()

        # drop columns
        drop_elements = [
            'Education_Level', 'Card_Category', 'IsAlone', 'Income_Category',
            'CLIENTNUM',
                         'Naive_Bayes_Classifier_Attrition_Flag_Card_Category_Contacts_Count_12_mon_Dependent_count_Education_Level_Months_Inactive_12_mon_1',
                         'Naive_Bayes_Classifier_Attrition_Flag_Card_Category_Contacts_Count_12_mon_Dependent_count_Education_Level_Months_Inactive_12_mon_2']
        train = train.drop(drop_elements, axis=1)

        self.dataset = train
        return self.dataset