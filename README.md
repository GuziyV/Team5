# Team5

### Members: 

Anton Sachko

Huzii Volodymyr

Vovk Yura

Dmytryk Olya


Варіант 5.
1) Визначити клас HTTPError для представлення HTTP помилок з полями Code (код помилки), Description (пояснення)  (Наприклад: 400 - Bad Request, 401- Unauthorized, 403 – Forbidden і ін.) та дата. Цей клас повинен реалізувати інтерфейс IComparable та перевизначити метод Equal(), реалізацію яких узгодити.  У текстовому файлі1 задані  дані про такі HTTP помилки, які сталися протягом певного періоду часу. Зчитати ці дані у колекцію.
2) текстовому файлі2 задано текст, який містить коди помилок і інший текст. Зчитати текст і, використовуючи отриману в попередньому завданні колекцію, замінити в цьому тексті всі коди помилок на їх пояснення і дату. Результат вивести на консоль.
3) Створити новий масив пар: код помилки – список дат цих помилок. Вивести у файл. 
4) Перехоплення винятків. При роботі з текстовими файлами передбачити виняткові ситуації через відсутність файлів, або неможливість їх зчитування і передбачити їх перехоплення у відповідних місцях. І інші винятки
5) Використання Linq
6) Весь код повинен бути покритий юніт тестами
