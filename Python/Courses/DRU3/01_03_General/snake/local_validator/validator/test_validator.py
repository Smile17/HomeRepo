import numpy as np
import random
from env.core.snake import Snake
from env.core.world import World


def full_test_snake_step(test_head_coordinates, test_direction_index, test_snake_length):

    def test_steps(test_snake, test_head_coordinates, test_snake_length):
        try:
            # Snake moves RIGHT, try to press RIGHT
            snake_position_init = test_snake.blocks.copy()
            expected_new_head_1st_step = (test_head_coordinates[0], test_head_coordinates[1] + 1)
            expected_tail_1st_step = (test_head_coordinates[0], test_head_coordinates[1] - test_snake_length + 1)
            new_head_1st_step, tail_1st_step = test_snake.step(1)

            assert new_head_1st_step == expected_new_head_1st_step, '\nNew Snake head is created incorrectly' \
                                                                    f'\nInitial Snake position: {snake_position_init}' \
                                                                    '\nInitial head direction: 1' \
                                                                    '\nUsing snake.step(1)' \
                                                                    f'\nNow Your snake looks like: {test_snake.blocks}'\
                                                                    f'\nYour new Snake head: {new_head_1st_step}' \
                                                                    f'\nExpected: {expected_new_head_1st_step}'

            assert tail_1st_step == expected_tail_1st_step, '\nNew Snake tail is created incorrectly' \
                                                            f'\nInitial Snake position: {snake_position_init}' \
                                                            f'\nInitial head direction: 1' \
                                                            '\nUsing snake.step(1)' \
                                                            f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                            f'\nYour old Snake tail: {tail_1st_step}' \
                                                            f'\nExpected: {expected_tail_1st_step}'

            # Snake moves RIGHT, try to press LEFT
            snake_position_1st = test_snake.blocks.copy()
            expected_new_head_2nd_step = (new_head_1st_step[0], new_head_1st_step[1] + 1)
            expected_tail_2nd_step = (tail_1st_step[0], tail_1st_step[1] + 1)
            new_head_2nd_step, tail_2nd_step = test_snake.step(3)

            assert new_head_2nd_step == expected_new_head_2nd_step, '\nNew Snake head is created incorrectly' \
                                                                    f'\nInitial Snake position: {snake_position_1st}' \
                                                                    '\nInitial head direction: 1' \
                                                                    '\nUsing snake.step(3)' \
                                                                    f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                                    f'\nYour new Snake head: {new_head_2nd_step}' \
                                                                    f'\nExpected: {expected_new_head_2nd_step}'

            assert tail_2nd_step == expected_tail_2nd_step, '\nNew Snake tail is created incorrectly' \
                                                            f'\nInitial Snake position: {snake_position_1st}' \
                                                            f'\nInitial head direction: 1' \
                                                            '\nUsing snake.step(3)' \
                                                            f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                            f'\nYour old Snake tail: {tail_2nd_step}' \
                                                            f'\nExpected: {expected_tail_2nd_step}'

            # Snake moves RIGHT, try to press DOWN
            snake_position_2nd = test_snake.blocks.copy()
            expected_new_head_3rd_step = (new_head_2nd_step[0] + 1, new_head_2nd_step[1])
            expected_tail_3rd_step = (tail_2nd_step[0], tail_2nd_step[1] + 1)
            new_head_3rd_step, tail_3rd_step = test_snake.step(2)

            assert new_head_3rd_step == expected_new_head_3rd_step, '\nNew Snake head is created incorrectly' \
                                                                    f'\nInitial Snake position: {snake_position_2nd}' \
                                                                    '\nInitial head direction: 1' \
                                                                    '\nUsing snake.step(2)' \
                                                                    f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                                    f'\nYour new Snake head: {new_head_3rd_step}' \
                                                                    f'\nExpected: {expected_new_head_3rd_step}'

            assert tail_3rd_step == expected_tail_3rd_step, '\nNew Snake tail is created incorrectly' \
                                                            f'\nInitial Snake position: {snake_position_2nd}' \
                                                            f'\nInitial head direction: 1' \
                                                            '\nUsing snake.step(3)' \
                                                            f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                            f'\nYour old Snake tail: {tail_2nd_step}' \
                                                            f'\nExpected: {expected_tail_2nd_step}'

            # Snake moves DOWN, try to press UP
            snake_position_3rd = test_snake.blocks.copy()
            expected_new_head_4th_step = (new_head_3rd_step[0] + 1, new_head_3rd_step[1])
            expected_tail_4th_step = (tail_3rd_step[0], tail_3rd_step[1] + 1)
            new_head_4th_step, tail_4th_step = test_snake.step(0)

            assert new_head_4th_step == expected_new_head_4th_step, '\nNew Snake head is created incorrectly' \
                                                                    f'\nInitial Snake position: {snake_position_3rd}' \
                                                                    '\nInitial head direction: 1' \
                                                                    '\nUsing snake.step(2)' \
                                                                    f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                                    f'\nYour new Snake head: {new_head_4th_step}' \
                                                                    f'\nExpected: {expected_new_head_4th_step}'

            assert tail_4th_step == expected_tail_4th_step, '\nNew Snake tail is created incorrectly' \
                                                            f'\nInitial Snake position: {snake_position_3rd}' \
                                                            f'\nInitial head direction: 1' \
                                                            '\nUsing snake.step(3)' \
                                                            f'\nNow Your snake looks like: {test_snake.blocks}' \
                                                            f'\nYour old Snake tail: {tail_4th_step}' \
                                                            f'\nExpected: {expected_tail_4th_step}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_length(test_snake, test_snake_length):
        try:
            assert len(test_snake.blocks) == test_snake_length, f'\nWrong length of the Snake:' \
                                                          f'\nExpected: {test_snake_length}' \
                                                          f'\nBut your is: {len(test_snake.blocks)}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_types(test_snake):
        try:
            assert type(test_snake.blocks) == list, f'\nWrong type of snake.blocks:' \
                                                    f'\nIt should be list' \
                                                    f'\nBut your is {type(test_snake.blocks)}'

            for coordinates in test_snake.blocks:
                assert type(coordinates) == tuple, f'\nWrong type of coordinates:' \
                                                   f'\nThey all should be tuples' \
                                                   f'\nBut you have: {type(coordinates)}'

            return 0
        except AssertionError as e:
            print(e)
            return 1

    test_snake = Snake(test_head_coordinates, test_direction_index, test_snake_length)

    step_error_occurred = test_steps(test_snake, test_head_coordinates, test_snake_length)
    length_error_occurred = test_length(test_snake, test_snake_length)
    types_error_occurred = test_types(test_snake)

    if step_error_occurred or length_error_occurred or types_error_occurred:
        return 0
    else:
        return 1


def test_world_init(test_world_size):

    def test_creation(test_world_size):
        try:
            test_world = World(test_world_size, True, (5, 5), 0, (8, 5))

            zero_count = np.count_nonzero(test_world.world == 0.)
            wall_count = np.count_nonzero(test_world.world == 255.)

            expected_zero_count = (test_world_size[0] - 2) * (test_world_size[1] - 2) - 1
            assert zero_count == expected_zero_count, '\nWrong number of "0." (World Block) in your World' \
                                                      f'\nYour World with size: {test_world.size}' \
                                                      f'\nIs looks like: \n{test_world.world}'

            expected_wall_count = test_world_size[0] * test_world_size[1] - expected_zero_count - 1
            assert wall_count == expected_wall_count, '\nWrong number of "255." (Wall Block) in your World' \
                                                      f'\nYour World with size: {test_world.size}' \
                                                      f'\nIs looks like: \n{test_world.world}'

            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_types(test_snake, test_world_size):
        try:
            random.seed(2)
            test_world = World(test_world_size, False, (5, 5), 0, (8, 5))

            assert type(test_world.world) == np.ndarray, '\nWrong type of world:' \
                                                         '\nIt should be np.ndarrays' \
                                                         f'\nBut your is {type(test_snake.blocks)}'

            for row in test_world.world:
                assert type(row) == np.ndarray, '\nWrong type of world rows:' \
                                                '\nThey all should be np.ndarrays' \
                                                f'\nBut you have: {type(row)}'

                for block in row:
                    assert type(block) == np.float64, '\nWrong type of coordinates:' \
                                                      '\nThey all should be np.float64' \
                                                      f'\nBut you have: {type(block)}'

            assert type(test_world.current_available_food_positions) == set, '\nWrong type of world.current_available_food_positions'\
                                                                             '\nIt should be set'\
                                                                             f'\nBut you have: {type(test_world.current_available_food_positions)}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_available_food_pos(test_world_size):

        test_world = World(test_world_size, False, (5, 5), 0, (8, 5))
        try:
            expected_current_available_food_pos = (test_world_size[0] - 2) * (test_world_size[1] - 2) - len(test_world.snake.blocks)
            assert len(test_world.current_available_food_positions) == expected_current_available_food_pos, \
                                                        '\nWrong number of current available food positions'\
                                                        f'\nFor World with size: {test_world.size}' \
                                                        f'\nExpected food positions: {expected_current_available_food_pos}'\
                                                        f'\nBut you have {len(test_world.current_available_food_positions)}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_food_init(test_world_size):
        try:
            world_1 = World(test_world_size, False, (5, 5), 0, (8, 5))
            world_2 = World(test_world_size, False, (5, 5), 0, (8, 5))
            world_3 = World(test_world_size, False, (5, 5), 0, (8, 5))

            food_pos_1 = (np.where(world_1.world == 64.)[0][0], np.where(world_1.world == 64.)[1][0])
            food_pos_2 = (np.where(world_2.world == 64.)[0][0], np.where(world_2.world == 64.)[1][0])
            food_pos_3 = (np.where(world_3.world == 64.)[0][0], np.where(world_3.world == 64.)[1][0])

            food_determined = food_pos_1 == food_pos_2 == food_pos_3
            assert not food_determined, '\nFood is not randomly initialized'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_food_count(test_world_size):
        try:
            random.seed(2)
            test_world = World(test_world_size, False, (5, 5), 0, (8, 5))
            food_count = np.count_nonzero(test_world.world == 64.)
            assert food_count == 1, '\nFood count is incorrect' \
                                    f'\nYour World with size: {test_world.size}' \
                                    f'\nIs looks like:\n{test_world.world}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_snake_init(test_world_size):
        try:
            world_1 = World(test_world_size, False, (5, 5), 0, (8, 5))
            world_2 = World(test_world_size, False, (5, 5), 0, (8, 5))
            world_3 = World(test_world_size, False, (5, 5), 0, (8, 5))

            snake_head_pos_1 = world_1.snake.blocks[0]
            snake_head_pos_2 = world_2.snake.blocks[0]
            snake_head_pos_3 = world_3.snake.blocks[0]

            snake_head_pos_are_equal = snake_head_pos_1 == snake_head_pos_2 == snake_head_pos_3
            assert not snake_head_pos_are_equal, '\nSnake is not randomly initialized'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    creation_error_occurred = test_creation(test_world_size)
    world_types_error_occurred = test_types(test_world_size, test_world_size)
    available_food_pos_error_occurred = test_available_food_pos(test_world_size)
    food_init_error_occurred = test_food_init(test_world_size)
    food_count_error_occurred = test_food_count(test_world_size)
    snake_init_error_occurred = test_snake_init(test_world_size)

    if creation_error_occurred or world_types_error_occurred or food_init_error_occurred \
            or food_count_error_occurred or snake_init_error_occurred or available_food_pos_error_occurred:
        return 0
    else:
        return 1


def test_world_move(test_world_size):

    def test_wall_crushing(test_world_size):
        try:
            test_world = World(test_world_size, True, (3, 4), 0, (1, 1))
            initial_snake = test_world.snake.blocks.copy()

            actions = [0, 0, 0]
            for action in actions:
                reward, done, test_snake_blocks = test_world.move_snake(action)

            assert done, '\nSnake doesnt die after crushing into the Wall'
            assert reward == -1.0, '\nAfter Snakes die given reward is incorrect'
            expected_snake_blocks = [(0, 4), (1, 4), (2, 4)]
            assert test_snake_blocks == expected_snake_blocks, \
                                                    '\nIn custom mode Snake moves incorrectly' \
                                                    f'\nInitial snake is looks like: {initial_snake}' \
                                                    f'\nAfter 3 moves UP it should look like: {expected_snake_blocks}'\
                                                    f'\nBut yours: {test_snake_blocks}'

            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_eating_food(test_world_size):
        try:
            test_world = World(test_world_size, True, (5, 5), 0, (3, 5))
            initial_snake = test_world.snake.blocks.copy()

            actions = [0, 0, 1]
            for action in actions:
                reward, done, test_snake_blocks = test_world.move_snake(action)

            expected_snake_blocks = [(3, 6), (3, 5), (4, 5), (5, 5)]
            assert test_snake_blocks == expected_snake_blocks, \
                                        '\nIn custom mode Snake moves incorrectly' \
                                        f'\nInitial snake is looks like: {initial_snake}' \
                                        f'\nAfter 3 moves UP and 1 LEFT it should look like: {expected_snake_blocks}'\
                                        f'\nBut yours: {test_snake_blocks}'
            assert np.count_nonzero(test_world.world == 64.) == 1, '\nMore then 1 food block at the same time found'
            assert np.count_nonzero(test_world.get_observation() == 100.) == 3, '\nSnake didnt grow after eating food'
            assert type(test_world.snake.blocks) == list, \
                                                         '\nYour World.snake.blocks has wrong type'\
                                                         '\nIt should be list' \
                                                         f'\nBut your is: {type(test_world.snake.blocks)}'
            for coordinates in test_world.snake.blocks:
                assert type(coordinates) == tuple, '\nYour World.snake coordinates has wrong type'\
                                                   '\nIt should be tuples' \
                                                   f'\nBut your is: {type(coordinates)}'


            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_eating_itself(test_world_size):
        try:
            test_world = World(test_world_size, True, (5, 5), 0, (3, 5))

            actions = [0, 0, 0, 1, 2, 3]
            for action in actions:
                reward, done, test_snake_blocks = test_world.move_snake(action)

            assert done, '\nSnake didnt die eating itself'
            assert reward == -1.0, '\nAfter Snakes die given reward is incorrect'

            return 0
        except AssertionError as e:
            print(e)
            return 1

    def test_types(test_world_size):
        try:
            test_world = World(test_world_size, False, (5, 5), 0, (3, 5))

            assert type(test_world.get_observation()) == np.ndarray, \
                                                         '\nYour World.get_observation() has wrong type'\
                                                         '\nIt should be np.ndarray' \
                                                         f'\nBut your is: {type(test_world.get_observation())}'
            for row in test_world.get_observation():
                assert type(row) == np.ndarray, '\nYour World.get_observation() rows have wrong type'\
                                                '\nThey should be np.ndarray' \
                                                f'\nBut your is: {type(row)}'
                for block in row:
                    assert type(block) == np.float64, '\nYour World.get_observation() blocks have wrong type'\
                                                      '\nThey should be np.float64' \
                                                      f'\nBut your is: {type(block)}'
            return 0
        except AssertionError as e:
            print(e)
            return 1

    wall_crushing_error_occurred = test_wall_crushing(test_world_size)

    eating_food_error_occurred = test_eating_food(test_world_size)

    eating_itself_error_occurred = test_eating_itself(test_world_size)

    types_error_occurred = test_types(test_world_size)

    if wall_crushing_error_occurred or eating_food_error_occurred \
            or eating_itself_error_occurred or types_error_occurred:
        return 0
    else:
        return 1





