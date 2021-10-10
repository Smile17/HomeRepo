import matplotlib.pyplot as plt
import numpy as np

from sklearn.metrics import accuracy_score
from sklearn.metrics import confusion_matrix
from itertools import product
#changing style of plots

from matplotlib import rcParams
rcParams['axes.facecolor'] = 'white'
rcParams['figure.facecolor'] = 'white'
rcParams['font.weight'] = 'bold'
rcParams['axes.titleweight'] = 'bold'
rcParams['xtick.color'] = '#64E0C0'
rcParams['ytick.color'] = '#64E0C0'
rcParams['grid.color'] = '#64E0C0'

#beatiful confusion matrix
def plot_confusion_matrix(cm, 
                          classes,
                          normalize=True,
                          title='Confusion matrix',
                          cmap=plt.cm.hot):
    plt.imshow(cm, interpolation='nearest', cmap=cmap)
    plt.title(title)
    tick_marks = np.arange(len(classes))
    plt.xticks(tick_marks, classes, rotation=45)
    plt.yticks(tick_marks, classes)
    thresh = cm.max() / 2.
    
    norm = []
    
    for i, j in product(range(cm.shape[0]), range(cm.shape[1])):
        if normalize: 
            element = cm[i, j] / cm[i].sum()
            element = np.around(element, 3)
        else: 
            element = cm[i, j]
        norm.append(element)
        plt.text(j, i, element,
                 horizontalalignment='center',
                 color='black' if cm[i, j] < thresh else 'white')
    #print(norm)
    plt.tight_layout()
    plt.ylabel('True label', color='black')
    plt.xlabel('Predicted label', color='black')