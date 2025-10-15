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
}
public class Student : Person
{
    private List<Course> _courses;
    private string _specialty;

    public Student(int id, string name, int age, string email, string specialty)
        : base(id, name, age, email)
    {
        _courses = new List<Course>();
        _specialty = specialty;
    }

    public List<Course> Courses => _courses;
    public string Specialty => _specialty;

    public override string GetRole() => "Студент";

    public override string GetInfo()
    {
        return base.GetInfo() + $", Специальность: {_specialty}";
    }

    public void EnrollInCourse(Course course)
    {
        if (!_courses.Contains(course))
        {
            _courses.Add(course);
            course.AddStudent(this);
        }
    }

    public string GetCourseList()
    {
        if (_courses.Count == 0)
            return "Студент не записан на курсы";

        string courseList = "";
        foreach (var course in _courses)
        {
            courseList += $"- {course.Name}\n";
        }
        return courseList;
    }
}



