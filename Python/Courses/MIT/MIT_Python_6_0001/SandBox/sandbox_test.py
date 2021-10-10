# 6.00
# SandBox Test Suite
import unittest
from sandbox import *
from datetime import timedelta


class ProblemSandBox(unittest.TestCase):
    def setUp(self):
        pass

    def testVowelsCount(self):
        d = [('azcbobobegghakl', 5), ('addtu', 2), ('wrdt b', 0), ('wq23!,osdo', 2)]
        for case in d:
            self.assertEqual(vowelsCount(case[0]), case[1],
                             'Case is wrong: {0}. Expected result: {1}; Actual result: {2}'
                             .format(case[0], str(case[1]), str(vowelsCount(case[0]))))
    def testSubstrCount(self):
        d = [('Python programming is fun.', 'is fun', 1), ('Python programming is fun.', 'Java', 0),
             ('azcbobobegghakl', 'bob', 2)]
        for case in d:
            self.assertEqual(substrCount(case[0], case[1]), case[2],
                             'Case is wrong: ({0}, {1}). Expected result: {1}; Actual result: {2}'
                             .format(case[0], case[1], str(case[2]), str(substrCount(case[0], case[1]))))
    def testLongestAlphabetic(self):
        d = [('azcbobobegghakl', 'beggh'), ('abcbcd', 'abc')]
        for case in d:
            self.assertEqual(longestAlphabetic(case[0]), case[1],
                             'Case is wrong: {0}. Expected result: {1}; Actual result: {2}'
                             .format(case[0], case[1], longestAlphabetic(case[0])))

if __name__ == "__main__":
    suite = unittest.TestSuite()
    suite.addTest(unittest.makeSuite(ProblemSandBox))
    unittest.TextTestRunner(verbosity=2).run(suite)

