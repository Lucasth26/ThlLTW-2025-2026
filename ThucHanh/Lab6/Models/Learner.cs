namespace Lab6.Models;

public record Learner(int LearnerID, string FirstMidName, string LastName, Major Major, DateOnly EnrollmentDate)
{
    public int MajorID => Major.MajorID;
}

