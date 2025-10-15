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

public class Teacher : Person
{
    private string _department;
    private List<Course> _teachingCourses;

    public Teacher(int id, string name, int age, string email, string department)
        : base(id, name, age, email)
    {
        _department = department;
        _teachingCourses = new List<Course>();
    }

    public string Department => _department;
    public List<Course> TeachingCourses => _teachingCourses;

    public override string GetRole() => "Преподаватель";

    public override string GetInfo()
    {
        return base.GetInfo() + $", Кафедра: {_department}";
    }

    public void AssignToCourse(Course course)
    {
        if (!_teachingCourses.Contains(course))
        {
            _teachingCourses.Add(course);
            course.AssignTeacher(this);
        }
    }
}

public class Course
{
    private int _id;
    private string _name;
    private string _description;
    private Teacher _teacher;
    private List<Student> _students;

    public Course(int id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
        _students = new List<Student>();
        _teacher = null;
    }

    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public Teacher Teacher => _teacher;
    public List<Student> Students => _students;

    public string GetInfo()
    {
        string teacherInfo = _teacher != null ? _teacher.Name : "Не назначен";
        return $"Курс: {_name}\nОписание: {_description}\n" +
               $"Преподаватель: {teacherInfo}\n" +
               $"Студентов: {_students.Count}";
    }

    public void AssignTeacher(Teacher teacher)
    {
        _teacher = teacher;
    }

    public void AddStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            _students.Add(student);
        }
    }

    public string GetStudentList()
    {
        if (_students.Count == 0)
            return "На курс не записаны студенты";

        string studentList = "";
        foreach (var student in _students)
        {
            studentList += $"- {student.Name} ({student.Specialty})\n";
        }
        return studentList;
    }
}



