namespace Lab5.Models;

public record Learner(int LearnerID, string FirstMidName, string LastName, Major Major, DateOnly EnrollmentDate)
{
    public int MajorID => Major.MajorID;
}
