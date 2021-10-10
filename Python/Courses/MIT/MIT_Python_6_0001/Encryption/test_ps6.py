from ps6 import *

#
# Test code
# You don't need to understand how this test code works (but feel free to look it over!)

# To run these tests, simply run this file (open up in your IDE, then run the file as normal)

def test_build_shift_dict():
    """
    Unit test for build_shift_dict
    """
    failure=False
    # dictionary of words and scores
    mes = Message("test_1")
    res = mes.build_shift_dict(3)
    print(res)
    if not failure:
        print("SUCCESS: test_build_shift_dict()")

# end of test_build_shift_dict

def test_apply_shift():
    """
    Unit test for apply_shift
    """
    failure=False
    # dictionary of words and scores
    mes = Message("Hello !")
    res = mes.apply_shift(3)
    print(res)
    if not failure:
        print("SUCCESS: test_apply_shift()")

# end of test_apply_shift

def test_PlaintextMessage():
    """
    Unit test for apply_shift
    """
    failure=False
    # dictionary of words and scores
    mes = PlaintextMessage("Hello !", 3)
    print("Initial text: ", mes.get_message_text(), " Encrypted text: ", mes.get_message_text_encrypted())
    mes.change_shift(5)
    print("Initial text: ", mes.get_message_text(), " Encrypted text: ", mes.get_message_text_encrypted())
    if not failure:
        print("SUCCESS: test_PlaintextMessage()")

# end of test_PlaintextMessage

def test_CiphertextMessage():
    """
    Unit test for CiphertextMessage
    """
    failure=False
    # dictionary of words and scores
    mes = CiphertextMessage("Khoor !")
    shift,decr_mess = mes.decrypt_message()
    print("Encrypted text: ", mes.get_message_text(), " Decrypted text: ", decr_mess, " Shift: ", shift)
    if not failure:
        print("SUCCESS: test_CiphertextMessage()")

# end of test_CiphertextMessage


print("----------------------------------------------------------------------")
print("Testing build_shift_dict...")
test_build_shift_dict()
print("----------------------------------------------------------------------")
print("Testing build_apply_shift...")
test_apply_shift()
print("----------------------------------------------------------------------")
print("Testing PlaintextMessage...")
test_PlaintextMessage()
print("----------------------------------------------------------------------")
print("Testing CiphertextMessage...")
test_CiphertextMessage()
print("----------------------------------------------------------------------")
print("All done!")
