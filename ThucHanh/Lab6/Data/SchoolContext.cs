using Lab6.Models;

namespace Lab6.Data;

public class SchoolContext
{
    public IReadOnlyList<Major> Majors { get; } = Array.AsReadOnly<Major>(
        [new(1, "IT"), new(2, "Economics"), new(3, "Mathematics")]);

    private readonly System.Collections.Concurrent.ConcurrentDictionary<int, Learner> learners = new();

    public IReadOnlyList<Learner> Learners => learners.Values.ToArray();

    public Learner? Find(int id) => learners.GetValueOrDefault(id);

    public bool Update(Learner learner, Learner original) =>
        learners.TryUpdate(learner.LearnerID, learner, original);

    public bool Delete(int id) => learners.TryRemove(id, out _);

    public SchoolContext()
    {
        Learner[] samples =
        [
            new(1, "Carson", "Alexander", Majors[0], new(2005, 9, 1)),
            new(2, "Meredith", "Alonso", Majors[1], new(2002, 9, 1)),
            new(3, "Arturo", "Anand", Majors[2], new(2003, 9, 1)),
            new(9, "Tuấn Nam", "Trần", Majors[0], new(2023, 10, 5))
        ];

        foreach (var learner in samples)
        {
            learners[learner.LearnerID] = learner;
        }
    }
}


