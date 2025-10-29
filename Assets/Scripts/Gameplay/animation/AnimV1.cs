using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public class AnimV1 : MonoModular
{
    private Animator animComponent;

    public AnimV1(Animator animator)
    {
        this.animComponent = animator;
    }

    public bool WasInState(string stateName, Animator animator = null)
    {
        Animator finalAnimator = animator ?? animComponent;

        if (finalAnimator == null)
        {
            Debug.LogWarning("Animator is null. Please provide a valid Animator.");
            return false;
        }

        AnimatorTransitionInfo transitionInfo = finalAnimator.GetAnimatorTransitionInfo(0);

        return finalAnimator.IsInTransition(0) && transitionInfo.IsName(stateName);
    }

    public T GetAnimatorParameter<T>(Expression<Func<T>> expression, Animator animator = null)
    {
        Animator finalAnimator = animator ?? animComponent;

        if (finalAnimator == null)
        {
            Debug.LogError("Animator is null. Cannot get parameter.");
            return default;
        }

        if (!(expression.Body is MemberExpression memberExpr))
        {
            Debug.LogError("Expression must be a member expression (e.g., () => variableName).");
            return default;
        }

        string parameterName = memberExpr.Member.Name;
        Type type = typeof(T);

        if (type == typeof(bool))
        {
            return (T)(object)finalAnimator.GetBool(parameterName);
        }
        else if (type == typeof(float))
        {
            return (T)(object)finalAnimator.GetFloat(parameterName);
        }
        else if (type == typeof(int))
        {
            return (T)(object)finalAnimator.GetInteger(parameterName);
        }
        else
        {
            Debug.LogWarning($"Parameter type '{type.Name}' not supported. Supported types are bool, float, and int.");
            return default;
        }
    }
    public void SetAnimatorParameter<T>(Expression<Func<T>> expression, Animator animator = null)
    {
        if (expression == null)
        {
            Debug.LogError("Expression is null. Cannot set parameter.");
            return;
        }

        Animator finalAnimator = animator ?? animComponent;

        if (finalAnimator == null)
        {
            Debug.LogError("Animator is null. Cannot set parameter.");
            return;
        }

        if (!(expression.Body is MemberExpression memberExpr))
        {
            Debug.LogError("Expression must be a member expression (e.g., () => variableName).");
            return;
        }

        string parameterName = memberExpr.Member.Name;
        T value = expression.Compile().Invoke();

        if (!finalAnimator.parameters.Any(p => p.name == parameterName))
        {
            Debug.LogWarning($"Animator does not have a parameter named '{parameterName}'.");
            return;
        }

        if (value is bool boolValue)
        {
            finalAnimator.SetBool(parameterName, boolValue);
        }
        else if (value is float floatValue)
        {
            finalAnimator.SetFloat(parameterName, floatValue);
        }
        else if (value is int intValue)
        {
            finalAnimator.SetInteger(parameterName, intValue);
        }
        else
        {
            Debug.LogWarning($"Parameter type not supported for variable '{parameterName}'. Supported types are bool, float, and int.");
        }
    }
    public void SetAnimatorParameter<T>(Expression<Func<T>> parameterExpression, T value, Animator animator = null)
    {
        Animator finalAnimator = animator ?? animComponent;

        if (finalAnimator == null)
        {
            Debug.LogError("Animator is null. Cannot set parameter.");
            return;
        }

        if (parameterExpression == null)
        {
            Debug.LogError("Expression is null. Cannot set parameter.");
            return;
        }

        if (!(parameterExpression.Body is MemberExpression memberExpression))
        {
            Debug.LogError("Expression must be a member expression (e.g., () => variableName).");
            return;
        }

        string parameterName = memberExpression.Member.Name;

        if (!finalAnimator.parameters.Any(p => p.name == parameterName))
        {
            Debug.LogWarning($"Animator does not have a parameter named '{parameterName}'.");
            return;
        }

        if (value is bool boolValue)
        {
            finalAnimator.SetBool(parameterName, boolValue);
        }
        else if (value is float floatValue)
        {
            finalAnimator.SetFloat(parameterName, floatValue);
        }
        else if (value is int intValue)
        {
            finalAnimator.SetInteger(parameterName, intValue);
        }
        else
        {
            Debug.LogWarning($"Parameter type not supported for variable '{parameterName}'. Supported types are bool, float, and int.");
        }
    }
    public void SetAnimatorParameters(object source, Animator animator = null)
    {
        if (source == null)
        {
            Debug.LogError("Source object is null.");
            return;
        }

        Animator finalAnimator = animator ?? animComponent;

        if (finalAnimator == null)
        {
            Debug.LogError("Animator is null.");
            return;
        }

        var parameters = finalAnimator.parameters;
        var sourceType = source.GetType();

        var members = sourceType.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property);

        foreach (var member in members)
        {
            string paramName = member.Name;

            var attr = member.GetCustomAttribute<AnimatorModel>();
            if (attr != null)
            {
                paramName = attr.Name;
            }

            var param = parameters.FirstOrDefault(p => p.name == paramName);
            if (param == null) continue;

            object value = member.MemberType switch
            {
                MemberTypes.Field => ((FieldInfo)member).GetValue(source),
                MemberTypes.Property => ((PropertyInfo)member).CanRead ? ((PropertyInfo)member).GetValue(source) : null,
                _ => null
            };

            if (value == null) continue;

            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    if (value is bool b) finalAnimator.SetBool(param.name, b);
                    break;
                case AnimatorControllerParameterType.Float:
                    if (value is float f) finalAnimator.SetFloat(param.name, f);
                    break;
                case AnimatorControllerParameterType.Int:
                    if (value is int i) finalAnimator.SetInteger(param.name, i);
                    break;
                default:
                    Debug.LogWarning($"Unsupported parameter type for '{param.name}'.");
                    break;
            }
        }
    }

}