import string
import time
import threading
import math


#-----------------------------------------------------------------------

#======================
# Code for SandBox problems
#======================

#======================
# Code for Problem Set 1
#======================
def vowelsCount(str):
    vowels = "aeoiu"
    sum = 0
    for char in str:
        if char in vowels:
            sum +=1
    return sum;
def substrCount(str, substr):
    sum = 0
    index = 0
    while index < len(str):
        try:
            index = str.index(substr, index) + 1
            sum += 1
        except ValueError:
            break
    return sum
def longestAlphabetic(str):
    res = []
    max_res = []
    for char in str:
        if len(res) == 0:
            res.append(char)
        else:
            if char >= res[len(res) - 1]:
                res.append(char)
            else:
                if len(max_res) < len(res):
                    max_res = res
                res = [char]
    return "".join(max_res)
#======================
# Code for Problem Set 2
#======================
def creditBalance(balance, annualInterestRate, monthlyPaymentRate):
    monthlyInterestRate = annualInterestRate / 12.0
    prevBalance = balance
    for i in range(12):
        minimumMonthlyPayment = monthlyPaymentRate * prevBalance
        monthlyUnpaidBalance = prevBalance - minimumMonthlyPayment
        updatedBalance = monthlyUnpaidBalance + (monthlyInterestRate * monthlyUnpaidBalance)
        print('Month {0} Remaining balance: {1}'.format(str(i + 1), str(round(updatedBalance, 2))))
        prevBalance = updatedBalance
    return updatedBalance
def lowestPayment(balance, annualInterestRate):
    monthlyInterestRate = annualInterestRate / 12.0
    k = (1+monthlyInterestRate)**12
    res = (k *  monthlyInterestRate * balance) / ((1+monthlyInterestRate) * (k-1))
    return int(math.ceil(res / 10.0)) * 10
def creditBalanceFixed(balance, annualInterestRate, fixedPayment):
    monthlyInterestRate = annualInterestRate / 12.0
    prevBalance = balance
    for i in range(12):
        monthlyUnpaidBalance = prevBalance - fixedPayment
        updatedBalance = monthlyUnpaidBalance + (monthlyInterestRate * monthlyUnpaidBalance)
        print('Month {0} Remaining balance: {1}'.format(str(i + 1), str(round(updatedBalance, 2))))
        prevBalance = updatedBalance
    return updatedBalance
#======================
# Code for Problem Set 3
#======================
#======================
# Code for Problem Set 4
#======================
#======================
# Code for Problem Set 5
#======================

if __name__ == '__main__':
    s = 'azcbobobegghakl'
    print(longestAlphabetic(s))
    print("---------------------------------")
    creditBalance(42, 0.2, 0.04)
    print("---------------------------------")
    creditBalance(484, 0.2, 0.04)
    print("---------------------------------")
    print(lowestPayment(3329, 0.2))
    creditBalanceFixed(3329, 0.2, 310)
    print("---------------------------------")
    print(lowestPayment(4773, 0.2))
    creditBalanceFixed(4773, 0.2, 440)
    print("---------------------------------")
    print(int(math.ceil(43 / 10.0)) * 10)
    print("---------------------------------")
    print('Done')
