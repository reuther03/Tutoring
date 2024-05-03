namespace Tutoring.Common.Primitives.Domain;

public interface IArchivable
{
    bool IsArchived { get; }
    DateTime? ArchivedAt { get; }
    void SetArchiveData(bool isArchived, DateTime? archivedAt);
}