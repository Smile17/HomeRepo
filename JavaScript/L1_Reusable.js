'use strict';
// Identifiers
/*
1. Define variable to store your name as a string.
2. Define constant to store your birth year as a number.
3. Prepare function to print greeting with single argument.
4. Call function passing value as literal.
5. Call function passing variable.
6. Call function passing constant.
*/
{
  console.log('====================IDENTIFIERS===============================');

  const name = 'Katya';
  const YEAR = 1995;
  const NAME = 'Kateryna';
  const welcome = name => {
    console.log(`Welcome ${name}`);
  };

  console.log(name);
  console.log(YEAR);
  welcome('Metarhia');
  welcome(name);
  welcome(NAME);
  console.log('====================IDENTIFIERS END===========================');
}
// Loop
/*
1. Print all odd numbers from the range [15, 30] including endpoints.
2. Implement function `range(start: number, end: number)` doing the same task.
*/
{
  console.log('=========================LOOPS================================');

  const START = 15;
  const END = 30;
  const start = (START % 2 === 1) ? START : START + 1;
  for (let i = start; i <= END; i += 2)
    console.log(i);
  const range = (start, end) => {
    start = (start % 2 === 1) ? start : start + 1;
    for (let i = start; i <= end; i += 2)
      console.log(i);
  };
  range(START, END);
  console.log(START);
  console.log('=========================LOOPS END=========================');
}
// Function
/*
1. Implement function `average` with signature
`average(a: number, b: number): number` calculating average (arithmetic mean).
2. Implement function `square` with signature
`square(x: number): number` calculating square of x.
3. Implement function `cube` with signature
`cube(x: number): number` calculating cube of x.
4. Call functions `square` and `cube` in loop, then pass their results to
function `average`. Print what `average` returns.
*/
{
  console.log('=========================FUNCTION=============================');
  const average = (a, b) => (a + b) / 2;
  console.log(average(5, 4));
  const square = x => x ** 2;
  console.log(square(5));
  const cube = x => x ** 3;
  console.log(cube(5));
  for (let i = -5; i < 5; i++) {
    const s = square(i);
    const c = cube(i);
    console.log(`square: ${s}; cube: ${c}; average: ${average(s, c)}`);
  }
  console.log('=========================FUNCTION END======================');
}
// Object
/*
1. Define constant object with single field `name`.
2. Define variable object with single field `name`.
3. Try to change field `name` and assign other object to both identifiers.
Explain script behaviour.
4. Implement function `createUser` with signature
`createUser(name: string, city: string): object`. Example:
`createUser('Marcus Aurelius', 'Roma')` will return object
`{ name: 'Marcus Aurelius', city: 'Roma' }`
*/
{
  console.log('=========================OBJECT=========================');
  const obj = name => ({
    name
  });
  const obj1 = obj('Name 1');
  let obj2 = obj('Name 2');
  console.log(`${obj1.name}; ${obj2.name}`);
  obj1.name = 'Name 1 Changed';
  obj2.name = 'Name 2 Changed';
  console.log(`${obj1.name}; ${obj2.name}`);
  obj2 = obj('Name 4');

  const createUser = (name, city) => ({
    name,
    city,
  });
  const user = createUser('User 1', 'Kyiv');
  console.log(`${user.name} from ${user.city}`);

  const User = class {
    constructor(name, city) {
      this.name = name;
      this.city = city;
    }
  };
  const serializable = User => class extends User {
    toString() {
      return `${this.name} from ${this.city}`;
    }
  };
  const UserEx = serializable(User);
  const user2 = new UserEx('User 2', 'Lutsk');
  console.log(user2.toString());
  console.log('=========================OBJECT END======================');

}
// Array
/*
1. Define array of objects with two fields: `name` and `phone` (phone book).
Example: `{ name: 'Marcus Aurelius', phone: '+380445554433' }`.
2. Implement function `findPhoneByName` with signature
`findPhoneByName(name: string): string`. Returning phone from that object
where field `name` equals argument `name`. Use `for` loop for this search.
*/
{
  console.log('=========================ARRAY=========================');
  const phones = [
    { name: 'Name 1', phone: '+380xxx1' },
    { name: 'Name 2', phone: '+380xxx2' },
  ];
  for (const phone of phones)
    console.log(`${phone.name}; ${phone.phone}`);
  const findPhoneByName = name => {
    for (const phone of phones) {
      if (phone.name === name)
        return phone.phone;
    }
  };
  console.log(findPhoneByName('Name 1'));
  console.log(findPhoneByName('Name 3'));
  console.log('=========================ARRAY END==================');
}
// Hash
/*
1. Define hash with `key` contains `name` (from previous example) and `value`
contains `phone`.
2. Implement function `findPhoneByName` with signature
`findPhoneByName(name: string): string`. Returning phone from hash/object.
Use `hash[key]` to find needed phone.
*/
{
  console.log('=========================HASH TABLE==================');
  const phones = {
    'name1': '+380xxx1',
    'name2': '+380xxx2',
  };
  for (const name in phones)
    console.log(`${name}; ${phones[name]}`);
  const findPhoneByName = name => phones[name];
  console.log(findPhoneByName('name1'));
  console.log(findPhoneByName('name3'));
  console.log('=========================HASH TABLE END=================');
}
