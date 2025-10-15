using System;
using System.Collections.Generic;

public abstract class Person
{
    private int _id;
    private string _name;
    private int _age;
    private string _email;

    protected Person(int id, string name, int age, string email)
    {
        _id = id;
        _name = name;
        _age = age;
        _email = email;
    }

    public int Id => _id;
    public string Name => _name;
    public int Age => _age;
    public string Email => _email;

    public abstract string GetRole();

    public virtual string GetInfo()
    {
        return $"ID: {_id}, Имя: {_name}, Возраст: {_age}";
    }


