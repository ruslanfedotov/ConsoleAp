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

public class UniversityManagement
{
    private List<Student> _students;
    private List<Teacher> _teachers;
    private List<Course> _courses;

    public UniversityManagement()
    {
        _students = new List<Student>();
        _teachers = new List<Teacher>();
        _courses = new List<Course>();
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        AddTeacher("вфвфыфыфв", 12, "фыфвыфвфвыфвы@dasdas.ru", "Информатика");
        AddTeacher("ччсчсчясяч", 38, "фыфвыфвфвыфвы@dasdas.ru", "Математика");

        AddStudent("ууууууу", 1, "фыфвыфвфвыфвы@dasdas.ru", "Математика");
        AddStudent("ы", 20, "фыфвыфвфвыфвы@dasdas.ru", "Математика");

        AddCourse("Программирование");
        AddCourse("Математика");

        AddTeacherToCourse(1, 1);
        AddTeacherToCourse(2, 2);

        EnrollStudentInCourse(1, 1);
        EnrollStudentInCourse(2, 2);
    }

    public void AddStudent(string name, int age, string email, string specialty)
    {
        var id = _students.Count + 1;
        var student = new Student(id, name, age, email, specialty);
        _students.Add(student);
    }

    public Student GetStudentById(int id)
    {
        return _students.Find(student => student.Id == id);
    }

    public void AddTeacher(string name, int age, string email, string department)
    {
        var id = _teachers.Count + 1;
        var teacher = new Teacher(id, name, age, email, department);
        _teachers.Add(teacher);
    }

    public Teacher GetTeacherById(int id)
    {
        return _teachers.Find(teacher => teacher.Id == id);
    }

    public void AddCourse(string name)
    {
        var id = _courses.Count + 1;
        var course = new Course(id, name, "");
        _courses.Add(course);
    }

    public Course GetCourseById(int id)
    {
        return _courses.Find(course => course.Id == id);
    }

    public List<Student> GetAllStudents() => _students;
    public List<Teacher> GetAllTeachers() => _teachers;
    public List<Course> GetAllCourses() => _courses;

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        var student = GetStudentById(studentId);
        var course = GetCourseById(courseId);

        if (student != null && course != null)
        {
            student.EnrollInCourse(course);
            return true;
        }
        return false;
    }

    public bool AddTeacherToCourse(int teacherId, int courseId)
    {
        var teacher = GetTeacherById(teacherId);
        var course = GetCourseById(courseId);

        if (teacher != null && course != null)
        {
            teacher.AssignToCourse(course);
            return true;
        }
        return false;
    }

    public string GetStudentCoursesInfo(int studentId)
    {
        var student = GetStudentById(studentId);
        return student?.GetCourseList() ?? "Студент не найден";
    }

    public string GetCourseStudentsInfo(int courseId)
    {
        var course = GetCourseById(courseId);
        return course?.GetStudentList() ?? "Курс не найден";
    }
}



