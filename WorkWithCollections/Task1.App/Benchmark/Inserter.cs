//-
using System;
using System.Collections.Generic;


namespace WorkWithCollections.Benchmark;

/// <summary>
/// Класс, содержащий исследуемые методы
///  методы выполняют вставку в определенную область списка,
///  в зависимости от присваиваемого
///  при создании объекта параметра [InsertOption]
/// </summary>
internal class Inserter
{
    internal enum InsertOption
    {
        None,
        NearFirst,
        NearLast,
        Middle,
    }

    private InsertOption _insertOption;
    private string[] _words =[];
    private List<string> _wordList = [];
    private LinkedList<string> _wordLinkedList = [];

    internal Inserter(string text, InsertOption insertOption)
    {
        // разделители
        char[] delimiters = [' ', '\r', '\n'];

        // разбиваем текст на слова
        if (!string.IsNullOrWhiteSpace(text))
            _words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        _insertOption = insertOption;
    }

    internal void InsertInList()
    {
        switch (_insertOption)
        {
            case InsertOption.NearFirst:
                // вставка на место второго элемента
                foreach (string w in _words)
                {
                    if (_wordList.Count > 0)
                    {
                        _wordList.Insert(1, w);
                    }
                    else
                    {
                        _wordList.Add(w);
                    }
                }
                break;
            case InsertOption.NearLast:
                // вставка на место предпоследнего элемента
                foreach (string w in _words)
                {
                    if (_wordList.Count > 0)
                    {
                        _wordList.Insert(_wordList.Count - 1, w);
                    }
                    else
                    {
                        _wordList.Add(w);
                    }
                }
                break;
            case InsertOption.Middle:
                // вставка в середину списка
                int MiddleIndex(List<string> lst) => lst.Count / 2;
                foreach (string w in _words)
                {
                    if (_wordList.Count > 0)
                    {
                        _wordList.Insert(MiddleIndex(_wordList), w);
                    }
                    else
                    {
                        _wordList.Add(w);
                    }
                }
                break;
        }
    }

    internal void InsertInLinkedList()
    {
        switch (_insertOption)
        {
            case InsertOption.NearFirst:
                // вставка на место второго элемента
                foreach (string w in _words)
                {
                    if (_wordLinkedList.First is LinkedListNode<string> node)
                    {
                        _wordLinkedList.AddAfter(node, w);
                    }
                    else
                    {
                        _wordLinkedList.AddLast(w);
                    }
                }
                break;
            case InsertOption.NearLast:
                // вставка на место предпоследнего элемента
                foreach (string w in _words)
                {
                    if (_wordLinkedList.Last is LinkedListNode<string> node)
                    {
                        _wordLinkedList.AddBefore(node, w);
                    }
                    else
                    {
                        _wordLinkedList.AddLast(w);
                    }
                }
                break;
            case InsertOption.Middle:
                // вставка в середину списка
                int MiddleIndex(LinkedList<string> lst) => lst.Count / 2;
                foreach (string w in _words)
                {
                    if (_wordLinkedList.First is LinkedListNode<string> node)
                    {
                        // чтобы вставить в связанный список элемент по индексу,
                        //  нужно найти узел, относительно которого будет выполняться вставка

                        int i = MiddleIndex(_wordLinkedList);

                        // находим узел простым перечислением "до элемента"

                        // можно еще получить узел методом [Find]
                        //  но для этого узел должен иметь уникальное содержимое
                        // это можно получить добавлением к каждому значению уникального id
                        // LinkedList<(string, Guid)>
                        // в случае уникальности значений узлов, значение узла по индексу можно получить
                        //  LINQ-методами [Skip] или [ElementAt], а затем применить метод [Find]

                        // находим узел методом [Find] и [Skip]
                        // LinkedListNode<(string, Guid)>? node = _wordAdvLinkedList.Find(_wordAdvLinkedList.Skip(i - 1).First());
                        // находим узел методом [Find] и [ElementAt]
                        // LinkedListNode<(string, Guid)>? node = _wordAdvLinkedList.Find(_wordAdvLinkedList.ElementAt(i))

                        // самое главное в этом то, что такие конструкции не дают прироста
                        //  в производительности по отношению к простому перечислению "до элемента" )))

                        while ((node.Next != null) && (i > 0))
                        {
                            node = node.Next;
                            i--;
                        }

                        _wordLinkedList.AddBefore(node, w);
                    }
                    else
                    {
                        _wordLinkedList.AddLast(w);
                    }
                }
                break;
        }
    }
}
