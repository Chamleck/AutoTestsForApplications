namespace AutoTestsForApplications.Utils;

public static class RandomHelper
{
    public static T GetRandomItem<T>(IReadOnlyList<T> items)
    {
        if (items.Count == 0)
        {
            throw new ArgumentException("Список пустой, выбирать не из чего.", nameof(items));
        }

        return items[Random.Shared.Next(items.Count)];
    }
}
