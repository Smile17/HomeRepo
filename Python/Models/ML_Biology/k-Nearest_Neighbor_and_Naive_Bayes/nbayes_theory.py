import matplotlib.pyplot as plt
def calculate_evidence(n_max, d):
    pn = 1/n_max
    ps = [1/x for x in np.arange(d, n_max + 1)]
    #print(ps)
    pd = np.sum(ps) * pn
    return pd
def calculate_prob_pnd(n, d, n_max, pd):
    if(n < d):
        return 0
    pn = 1/n_max
    pdn = 1/n
    pnd = pdn * pn / pd
    return pnd
n_max = 1000
d = 60
pd = calculate_evidence(n_max, d)
print("Evidence:", pd)
n = 60
probs = []
for n in np.arange(d, n_max + 1):
    probs.append(calculate_prob_pnd(n, d, n_max, pd))
probs = np.array(probs)
print(sum(probs)) # Check the sum is 1
plt.plot(np.arange(d, n_max + 1), probs, linestyle = 'dotted')
idx = probs.argmax()
print("Most probable N={} with probability p={}".format(idx + d, probs[idx]))
print("Math approximation: ", 1/(d * np.log(n_max / d)))
expectation = np.sum(probs * np.arange(d, n_max + 1))
print("Expectation: ", expectation)
print("Math approximation for expectation: ", (n_max - d + 1)/ np.log(n_max / d))