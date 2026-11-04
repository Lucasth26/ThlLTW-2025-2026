using Lab5.Models;

namespace Lab5.Data;

public class SchoolContext
{
    public IReadOnlyList<Major> Majors { get; } = Array.AsReadOnly<Major>(
        [new(1, "IT"), new(2, "Economics"), new(3, "Mathematics")]);

    public IReadOnlyList<Learner> Learners { get; }

    public SchoolContext()
    {
        Learners = Array.AsReadOnly<Learner>(
        [
            new(1, "Carson", "Alexander", Majors[0], new(2005, 9, 1)),
            new(2, "Meredith", "Alonso", Majors[1], new(2002, 9, 1)),
            new(3, "Arturo", "Anand", Majors[2], new(2003, 9, 1)),
            new(9, "Tuấn Nam", "Trần", Majors[0], new(2023, 10, 5))
        ]);
    }
}
