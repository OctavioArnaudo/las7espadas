using UnityEngine;

/// <summary>
/// 
/// A collection of fuzzy logic functions for comparing floating point values.
/// 
/// Fuzzy logic is used to handle the inherent imprecision of floating point arithmetic,
/// 
/// especially in game development where physics and player input can lead to small variations
/// 
/// in values that should be considered equal or near-equal.
/// 
/// </summary>
static class Fuzzy
{
    /// <summary>
    /// 
    /// Determines if two floating point values are considered equal within a specified fuzziness range.
    /// 
    /// The fuzziness parameter allows for a small margin of error in the comparison, which is useful
    /// 
    /// in scenarios where exact equality is not necessary, such as physics calculations or user input.
    /// 
    /// </summary>
    public static bool ValueLessThan(float value, float test, float fuzz = 0.1f)
    {
        // If the value is less than the test value, return true with a probability based on fuzziness.
        var delta = value - test;
        // If the delta is negative, it means value is less than test.
        return delta < 0 ? true : Random.value > delta / (fuzz * test);
    }

    /// <summary>
    /// 
    /// Determines if a floating point value is greater than another value within a specified fuzziness range.
    /// 
    /// The fuzziness parameter allows for a small margin of error in the comparison, which is useful
    /// 
    /// in scenarios where exact equality is not necessary, such as physics calculations or user input.
    /// 
    /// </summary>
    public static bool ValueGreaterThan(float value, float test, float fuzz = 0.1f)
    {
        // If the value is greater than the test value, return true with a probability based on fuzziness.
        var delta = value - test;
        // If the delta is positive, it means value is greater than test.
        return delta < 0 ? Random.value > -1 * delta / (fuzz * test) : true;
    }

    /// <summary>
    /// 
    /// Determines if a floating point value is near another value within a specified fuzziness range.
    /// 
    /// The fuzziness parameter allows for a small margin of error in the comparison, which is useful
    /// 
    /// in scenarios where exact equality is not necessary, such as physics calculations or user input.
    /// 
    /// </summary>
    public static bool ValueNear(float value, float test, float fuzz = 0.1f)
    {
        // Check if the absolute difference between value and test is within the fuzziness range.
        return Mathf.Abs(1f - (value / test)) < fuzz;
    }

    /// <summary>
    /// 
    /// Applies a fuzziness factor to a floating point value, creating a range around the value
    /// 
    /// within which the value can vary. This is useful for simulating imprecision in game mechanics.
    /// 
    /// </summary>
    public static float Value(float value, float fuzz = 0.1f)
    {
        // Return a value that is within the range of value ± (value * fuzz).
        return value + value * Random.Range(-fuzz, +fuzz);
    }
}