using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 
/// GameController is a MonoBehaviour that manages the game simulation and provides access to the game model.
/// 
/// </summary>
public abstract class MonoComponent : MonoBehaviour
{

    /// <summary>
    /// 
    /// The model that contains the game state and mechanics.
    /// 
    /// </summary>
    //This model field is public and can be therefore be modified in the 
    //inspector.
    //The reference actually comes from the InstanceRegister, and is shared
    //through the simulation and events. Unity will deserialize over this
    //shared reference when the scene loads, allowing the model to be
    //conveniently configured inside the inspector.
    private static MonoComponent instance;

    /// <summary>
    /// 
    /// The singleton instance of the GameController.
    /// 
    /// </summary>
    public static MonoComponent GetInstance()
    {
        if (instance == null)
        {
            SetInstance(FindFirstObjectByType<MonoComponent>());
            if (instance == null)
            {
                GameObject singletonObject = new GameObject(typeof(MonoBehaviour).Name + " (Singleton)");
                SetInstance(singletonObject.AddComponent<MonoComponent>());
                DontDestroyOnLoad(singletonObject);
            }
        }
        return instance;
    }

    private static void SetInstance(MonoComponent value)
    {
        instance = value;
    }

    /// <summary>
    /// 
    /// Prints the name and value of a variable using an expression.
    /// 
    /// <typeparam name="T">The type of the variable.</typeparam>
    /// 
    /// <param name="expression">An expression representing the variable.</param>
    /// 
    /// <example>
    /// 
    /// <code>
    /// 
    /// int myVariable = 42;
    /// DebuggerUtility.PrintVariable(() => myVariable);
    ///
    /// </code>
    /// 
    /// </example>
    /// 
    /// </summary>
    public void PrintVariable<T>(Expression<Func<T>> expression)
    {
        // Get the variable name from the expression
        string variableName = ((MemberExpression)expression.Body).Member.Name;

        // Compile the expression to get the value
        T value = expression.Compile().Invoke();

        // Print the variable name and value
        Console.WriteLine($"{variableName}: {value}");

        Debug.Log($"{variableName}: {value}");
    }

    public void AssignDefaults(object instance, GameObject fallback)
    {
        Type type = instance.GetType();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;

        FieldInfo[] fields = type.GetFields(flags);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(GameObject))
            {
                GameObject value = field.GetValue(instance) as GameObject;
                if (value == null)
                {
                    field.SetValue(instance, fallback);
                }
            }
        }
    }
    public void AssignDefaults(object instance, GameObject fallback, List<string> excludedFieldNames = null)
    {
        Type type = instance.GetType();
        BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;

        FieldInfo[] fields = type.GetFields(flags);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(GameObject))
            {
                if (excludedFieldNames != null && excludedFieldNames.Contains(field.Name))
                    continue;

                GameObject value = field.GetValue(instance) as GameObject;
                if (value == null)
                {
                    field.SetValue(instance, fallback);
                }
            }
        }
    }
    
    protected Dictionary<string, object> modelData = new Dictionary<string, object>();

    public void CopyFrom(object source)
    {
        var sourceType = source.GetType();
        var targetType = this.GetType();

        foreach (var sourceProp in sourceType.GetProperties())
        {
            var targetProp = targetType.GetProperty(sourceProp.Name);
            if (targetProp != null && targetProp.CanWrite && targetProp.PropertyType == sourceProp.PropertyType)
            {
                targetProp.SetValue(this, sourceProp.GetValue(source));
            }
        }

        foreach (var sourceField in sourceType.GetFields())
        {
            var targetField = targetType.GetField(sourceField.Name);
            if (targetField != null && targetField.FieldType == sourceField.FieldType)
            {
                targetField.SetValue(this, sourceField.GetValue(source));
            }
        }
    }

    public void LoadFrom(object source)
    {
        var sourceType = source.GetType();
        foreach (var sourceProp in sourceType.GetProperties())
        {
            modelData[sourceProp.Name] = sourceProp.GetValue(source);
        }
    }

    // -----------------------------------------------------------
    // Métodos de Inicialización
    // -----------------------------------------------------------

    /// <summary>
    /// Se llama una vez cuando el script se carga. Es el primer método que se ejecuta, incluso antes que Awake.
    /// Se usa principalmente para inicializar variables, pero no para interactuar con otros GameObjects.
    /// </summary>
    public virtual void OnAwake()
    {
    }
    protected virtual void Awake()
    {
        if (GetInstance() == null)
        {
            SetInstance(this);
            DontDestroyOnLoad(gameObject);
        }
        else if (GetInstance() != this)
        {
            Destroy(gameObject);
        }
        OnAwake();
    }

    public virtual void OnStart()
    {
    }
    /// <summary>
    /// Se llama una vez después de Awake. Es ideal para inicializar referencias a otros componentes o GameObjects,
    /// ya que para este momento, todos los GameObjects y sus scripts ya han ejecutado su Awake.
    /// </summary>
    protected virtual void Start()
    {
        OnStart();
    }

    /// <summary>
    /// Se llama una vez cuando el GameObject está activado. Es útil para lógica que necesita ejecutarse
    /// cada vez que el GameObject se activa, no solo al inicio.
    /// </summary>
    /// <summary>
    /// 
    /// The simulation that manages the game events and mechanics.
    /// 
    /// </summary>
    public virtual void Enable()
    {
        // Ensure that only one instance of GameController exists
        SetInstance(this);
    }
    protected virtual void OnEnable()
    {
        Enable();
    }

    // -----------------------------------------------------------
    // Métodos de Actualización
    // -----------------------------------------------------------

    /// <summary>
    /// Se llama en cada fotograma. Es el lugar principal para la lógica del juego, como movimiento de personajes,
    /// detección de entrada del usuario o actualizaciones de estado.
    /// </summary>
    /// <summary>
    /// 
    /// Update is called once per frame. It ticks the simulation if this GameController is the current instance.
    /// 
    /// </summary>
    public virtual void OnUpdate()
    {
        // If this GameController is the current instance, tick the simulation
        if (GetInstance() == this) Simulation.Tick();
    }
    protected virtual void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// Se llama en cada fotograma después de que se ha ejecutado todo el código en Update. Es útil
    /// para la lógica que debe ocurrir después de que se han completado todas las actualizaciones de posición,
    /// como la lógica de cámaras o de seguimiento.
    /// </summary>
    public virtual void OnLateUpdate()
    {
    }
    protected virtual void LateUpdate()
    {
        OnLateUpdate();
    }

    /// <summary>
    /// Se llama en un intervalo de tiempo fijo, independientemente de los fotogramas. Se usa para
    /// física (Physics), como la manipulación de Rigidbodies.
    /// </summary>
    public virtual void OnFixedUpdate()
    {
    }
    protected virtual void FixedUpdate()
    {
        OnFixedUpdate();
    }

    // -----------------------------------------------------------
    // Métodos de Destrucción y Desactivación
    // -----------------------------------------------------------

    /// <summary>
    /// Se llama cuando el GameObject se desactiva. Es el opuesto de OnEnable.
    /// </summary>
    /// <summary>
    /// 
    /// Called when the GameController is disabled. It sets the Instance to null if it is the current instance.
    /// 
    /// </summary>
    public virtual void Disable()
    {
        // Set Instance to null when this GameController is disabled
        if (GetInstance() == this) SetInstance(null);
    }
    protected virtual void OnDisable()
    {
        Disable();
    }

    public virtual void Destroy()
    {
    }
    /// <summary>
    /// Se llama cuando el script o GameObject se destruye. Es útil para limpiar recursos, como
    /// desuscribirse de eventos.
    /// </summary>
    protected virtual void OnDestroy()
    {
        Destroy();
    }

}