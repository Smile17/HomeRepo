#include <iostream>

using namespace std;

typedef int elemtype;

struct elem {
	elemtype value;
	struct elem* next;
	struct elem* prev;
};

struct myList {
	struct elem* head;
	struct elem* tail;
	size_t size;
};

typedef struct elem cNode;
typedef struct myList cList;

// Створення списку:
cList* createList() {
	cList* list = (cList*)malloc(sizeof(cList));
	if (list) {
		list->size = 0;
		list->head = list->tail = NULL;
	}
	return list;
}
// Видалення списку:
void deleteList(cList* list) {
	cNode* head = list->head;
	cNode* next = NULL;
	while (head) {
		next = head->next;
		free(head);
		head = next;
	}
	free(list);
	list = NULL;
}
// Перевірка на «пустоту»:
bool isEmptyList(cList* list) {
	return ((list->head == NULL) || (list->tail == NULL));
}
// Вставка в початок:
int pushFront(cList* list, elemtype* data) {
	cNode* node = (cNode*)malloc(sizeof(cNode));
	if (!node) {
		return(-1);
	}
	node->value = *data;
	node->next = list->head;
	node->prev = NULL;

	if (!isEmptyList(list)) {
		list->head->prev = node;
	}
	else {
		list->tail = node;
	}
	list->head = node;

	list->size++;
	return(0);
}
// Вставка в кінець:
int pushBack(cList* list, elemtype* data) {
	cNode* node = (cNode*)malloc(sizeof(cNode));
	if (!node) {
		return(-3);
	}

	node->value = *data;
	node->next = NULL;
	node->prev = list->tail;
	if (!isEmptyList(list)) {
		list->tail->next = node;
	}
	else {
		list->head = node;
	}
	list->tail = node;

	list->size++;
	return(0);
}
// Видалення та отримання першого елемента:
int popFront(cList* list, elemtype* data) {
	cNode* node;

	if (isEmptyList(list)) {
		return(-2);
	}

	node = list->head;
	list->head = list->head->next;

	if (!isEmptyList(list)) {
		list->head->prev = NULL;
	}
	else {
		list->tail = NULL;
	}

	*data = node->value;
	list->size--;
	free(node);

	return(0);
}
// Видалення та отримання останнього елемента:
int popBack(cList* list, elemtype* data) {
	cNode* node = NULL;

	if (isEmptyList(list)) {
		return(-4);
	}

	node = list->tail;
	list->tail = list->tail->prev;
	if (!isEmptyList(list)) {
		list->tail->next = NULL;
	}
	else {
		list->head = NULL;
	}

	*data = node->value;
	list->size--;
	free(node);

	return(0);
}
// Пошук елемента за індексом:
cNode* getNode(cList* list, int index) {
	cNode* node = NULL;
	int i;

	if (index >= list->size) {
		return (NULL);
	}

	if (index < list->size / 2) {
		i = 0;
		node = list->head;
		while (node && i < index) {
			node = node->next;
			i++;
		}
	}
	else {
		i = list->size - 1;
		node = list->tail;
		while (node && i > index) {
			node = node->prev;
			i--;
		}
	}

	return node;
}
// Вставка після указаного індексу:
int insert(cList* list, int index, elemtype* value) {
	cNode* elm = NULL;
	cNode* ins = NULL;
	elm = getNode(list, index);
	if (elm == NULL) {
		return (-5);
	}
	ins = (cNode*)malloc(sizeof(cNode));
	ins->value = *value;
	ins->prev = elm;
	ins->next = elm->next;
	if (elm->next) {
		elm->next->prev = ins;
	}
	elm->next = ins;

	if (!elm->prev) {
		list->head = elm;
	}
	if (!elm->next) {
		list->tail = elm;
	}

	list->size++;

	return 0;
}
// Видалення та отримання елемента з указаним індексом:
int deleteNode(cList* list, int index, elemtype* data) {
	cNode* elm = NULL;
	elemtype tmp = NULL;
	elm = getNode(list, index);
	if (elm == NULL) {
		return (-6);
	}
	if (elm->prev) {
		elm->prev->next = elm->next;
	}
	if (elm->next) {
		elm->next->prev = elm->prev;
	}
	tmp = elm->value;

	if (!elm->prev) {
		list->head = elm->next;
	}
	if (!elm->next) {
		list->tail = elm->prev;
	}

	*data = elm->value;
	free(elm);
	list->size--;

	return 0;
}
// Виведення одного елемента:
void printNode(elemtype* value) {
	printf("%d ", *((int*)value));
}
// Виведення списку: 
void printList(cList* list, void (*func)(elemtype*)) {
	cNode* node = list->head;

	if (isEmptyList(list)) {
		return;
	}

	while (node) {
		func(&node->value);
		node = node->next;
	}
}

int main()
{
    cList* list = NULL;
    string menuOption[10] = {
        "1) Добавить элемент в начало списка",
        "2) Добавить элемент в конец списка",
        "3) Добавить элемент после указаного индекса",
        "4) Удалить первый элемент списка",
        "5) Удалить последний элемент списка",
        "6) Удалить элемент с указаним индексом",
        "7) Вывести отдельный элемент",
        "8) Вывести весь список",
        "9) Удалить список",
        "0) Выйти"
    };
    
    string menuNextMessage[10] = {
        "",
        "1) Введите элемент: ",
        "2) Введите элемент: ",
        "3) Введите элемент и индекс: ",
        "",
        "",
        "6) Введите индекс: ",
        "7) Введите индекс: ",
        "",
        ""
    };
    
    string menuAnswer[10] = {
        "0) Выход",
        "1) Элемент добавлен в начало списка",
        "2) Элемент добавлен в конец списка",
        "3) Элемент добавлен после указаного индекса",
        "4) Первый элемент удален из списка",
        "5) Последний элемент удален из списка",
        "6) Элемент с указаним индексом удален из списка",
        "7) Отдельный элемент выведен",
        "8) Весь список выведен",
        "9) Список удален"
    };
    
    int menuOptionCount = 10;
    while(true)
    {
        cout << "Что нужно сделать?" << endl;
        // Print all options
        for(int i = 0; i < menuOptionCount; i++)
            cout << menuOption[i] << endl;
        // Select an option
        string option;
        cin >> option;
        int optionNum = stoi(option);
        if(optionNum == 0) break;
        // Create a list
        if(list == NULL)
            list = createList();
        // Print next step
        cout << menuNextMessage[optionNum];
        int value; int* valuep; int index = 0; cNode* node;
        switch(optionNum)
        {
            // 1) Добавить элемент в начало списка"
            case 1:
                cin >> value; valuep = &(value);
                pushFront(list, valuep);
                break;
            // 2) Добавить элемент в конец списка
            case 2:
                cin >> value; valuep = &(value);
                pushBack(list, valuep);
                break;
            // 3) Добавить элемент после указаного индекса
            case 3:
                cin >> value >> index; valuep = &(value);
                insert(list, index, valuep);
                break;
            // 4) Удалить первый элемент списка
            case 4:
                popFront(list, valuep);
                break;
            // 5) Удалить последний элемент списка
            case 5:
                popBack(list, valuep);
                break;
            // 6) Удалить элемент с указаним индексом
            case 6:
                cin >> index;
                deleteNode(list, index, valuep);
                break;
            // 7) Вывести отдельный элемент
            case 7:
                cin >> index;
                node = getNode(list, index);
                if(node == NULL) cout << "null";
                else
                    printNode(&(node->value));
                cout << endl;
                break;
            // 8) Вывести весь список
            case 8:
                printList(list, printNode); cout << endl;
                break;
            // 9) Удалить список
            case 9:
                deleteList(list);
                list = NULL;
                break;
                
        };
        
        cout << menuAnswer[optionNum] << endl;
        
        cout << endl;
        
    }

    return 0;
}

