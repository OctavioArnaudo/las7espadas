using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

/// <summary>
/// 
/// Simulation is a static class that manages the game simulation, including event scheduling,
/// 
/// event pooling, and model management. It provides methods to create new events, schedule them,
/// 
/// reschedule existing events, and manage game models.
/// 
/// </summary>
public static partial class Simulation
{

    /// <summary>
    /// 
    /// The Event class is the base class for all events in the simulation. It contains a tick property
    /// 
    /// that indicates when the event should be executed, and methods to execute the event and clean up
    /// 
    /// after execution.
    /// 
    /// </summary>
    static HeapQueue<Event> eventQueue = new HeapQueue<Event>();
    /// <summary>
    /// 
    /// The eventPools dictionary holds pools of reusable events to reduce memory allocation overhead.
    /// 
    /// Each event type has its own pool, which is a stack of events that can be reused.
    /// 
    /// </summary>
    static Dictionary<System.Type, Stack<Event>> eventPools = new Dictionary<System.Type, Stack<Event>>();

    /// <summary>
    /// 
    /// The InstanceRegister class is a generic class that holds a single instance of a model type.
    /// 
    /// It is used to manage game models, allowing for easy access and modification of model instances.
    /// 
    /// </summary>
    static public T New<T>() where T : Event, new()
    {
        // Check if the event type already has a pool, if not create a new one
        Stack<Event> pool;
        // If the pool for the event type does not exist, create a new stack and add a new instance of the event type
        if (!eventPools.TryGetValue(typeof(T), out pool))
        {
            // Create a new stack for the event type and add a new instance of the event type to it
            pool = new Stack<Event>(4);
            // Initialize the pool with one instance of the event type
            pool.Push(new T());
            // Add the pool to the eventPools dictionary
            eventPools[typeof(T)] = pool;
        }
        // If the pool has events, pop one from the stack and return it
        if (pool.Count > 0)
            // Cast the popped event to the type T and return it
            return (T)pool.Pop();
        else
            // If the pool is empty, create a new instance of the event type and return it
            return new T();
    }

    /// <summary>
    /// 
    /// Cleans up all event pools and clears the event queue.
    /// 
    /// </summary>
    public static void Clear()
    {
        // Clear all event pools
        eventQueue.Clear();
    }

    /// <summary>
    /// 
    /// Schedules a new event of type T to be executed after a specified tick time.
    /// 
    /// </summary>
    static public T Schedule<T>(float tick = 0) where T : Event, new()
    {
        // Create a new event of type T
        var ev = New<T>();
        // Set the tick time for the event
        ev.tick = Time.time + tick;
        // Add the event to the event queue
        eventQueue.Push(ev);
        // Return the scheduled event
        return ev;
    }

    /// <summary>
    /// 
    /// Reschedules an existing event of type T to be executed after a specified tick time.
    /// 
    /// </summary>
    static public T Reschedule<T>(T ev, float tick) where T : Event, new()
    {
        // Check if the event is null
        ev.tick = Time.time + tick;
        // If the event is null, schedule a new event of type T
        eventQueue.Push(ev);
        // Return the rescheduled event
        return ev;
    }

    /// <summary>
    /// 
    /// Gets the model instance of type T from the InstanceRegister.
    /// 
    /// </summary>
    static public T GetModel<T>() where T : class, new()
    {
        // Check if the InstanceRegister has an instance of type T
        return InstanceRegister<T>.instance;
    }

    /// <summary>
    /// 
    /// Sets the model instance of type T in the InstanceRegister.
    /// 
    /// </summary>
    static public void SetModel<T>(T instance) where T : class, new()
    {
        // Check if the InstanceRegister already has an instance of type T
        InstanceRegister<T>.instance = instance;
    }

    /// <summary>
    /// 
    /// Destroys the model instance of type T in the InstanceRegister.
    /// 
    /// </summary>
    static public void DestroyModel<T>() where T : class, new()
    {
        // Check if the InstanceRegister has an instance of type T
        InstanceRegister<T>.instance = null;
    }

    /// <summary>
    /// 
    /// Executes all scheduled events in the event queue that are due to be executed at the current time.
    /// 
    /// </summary>
    static public int Tick()
    {
        // If the event queue is empty, return 0
        var time = Time.time;
        // If the event queue is empty, return 0
        var executedEventCount = 0;
        // While there are events in the queue and the next event's tick is less than or equal to the current time
        while (eventQueue.Count > 0 && eventQueue.Peek().tick <= time)
        {
            // Pop the next event from the queue
            var ev = eventQueue.Pop();
            // If the event is null, continue to the next iteration
            var tick = ev.tick;
            // Execute the event
            ev.ExecuteEvent();
            // If the event's tick is greater than the current time, it has been rescheduled
            if (ev.tick > tick)
            {
                //event was rescheduled, so do not return it to the pool.
            }
            else
            {
                // Debug.Log($"<color=green>{ev.tick} {ev.GetType().Name}</color>");
                ev.Cleanup();
                try
                {
                    // Return the event to its pool for reuse
                    eventPools[ev.GetType()].Push(ev);
                }
                // If the event type does not have a pool, log an error
                catch (KeyNotFoundException)
                {
                    // If the event type does not have a pool, log an error
                    Debug.LogError($"No Pool for: {ev.GetType()}");
                }
            }
            // Increment the executed event count
            executedEventCount++;
        }
        // Return the number of executed events
        return eventQueue.Count;
    }
}