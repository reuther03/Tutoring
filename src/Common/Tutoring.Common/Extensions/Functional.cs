namespace Tutoring.Common.Extensions;

public static class Functional
{
    /// <summary>
    /// Executes a given action depending on a specified condition.
    /// </summary>
    /// <param name="condition"><c>bool</c> condition to be evaluated.</param>
    /// <param name="ifTrue">The <see cref="Action"/> to be executed if the condition is <c>true</c>.</param>
    /// <param name="ifFalse">The <see cref="Action"/> to be executed if the condition is <c>false</c>.</param>
    public static void IfElse(bool condition, Action ifTrue, Action ifFalse)
    {
        if (condition)
        {
            ifTrue();
        }
        else
        {
            ifFalse();
        }
    }
}