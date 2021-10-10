import random
from env.core.world import World
from validator.test_validator import test_world_init, test_world_move
from validator.test_constants import TEST_WORLD_SIZE


print('\nChecking World initialization...')

if test_world_init(TEST_WORLD_SIZE):
    test_world = World(TEST_WORLD_SIZE, True, (4, 4), 0, (4, 1))
    print(f'\nYour World without Snake looks like:\n{test_world.world}')

    if test_world_move(TEST_WORLD_SIZE):
        print('\nChecking Snake movements...')
        print(f'\nYour World with Snake looks like:\n{test_world.get_observation()}')

        actions = [3, 3, 3, 2]
        for action in actions:
            reward, done, snake_blocks = test_world.move_snake(action)

        print(f'\nAfter 3 moves LEFT Snake ate the food and moves 1 DOWN'
              f'\nNow your World with Snake looks like: \n{test_world.get_observation()}')
        print('\nAs you can see, Snake grew up for a 1 block')

        test_world.move_snake(3)

        print(f'\nAnd after 1 move LEFT Snake died:\n{test_world.get_observation()}')
        print('\nAll actions were completed correctly'
              '\nWell done!')


