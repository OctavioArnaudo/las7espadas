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
    public abstract class Event : System.IComparable<Event>
    {
        /// <summary>
        /// 
        /// The tick property indicates when the event should be executed.
        /// 
        /// </summary>
        internal float tick;

        /// <summary>
        /// 
        /// The Event constructor initializes the tick property to the current time.
        /// 
        /// </summary>
        public int CompareTo(Event other)
        {
            // Check if the other event is null
            return tick.CompareTo(other.tick);
        }

        /// <summary>
        /// 
        /// The Execute method is an abstract method that must be implemented by derived classes to define
        /// 
        /// the specific behavior of the event when it is executed.
        /// 
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 
        /// The Precondition method is a virtual method that can be overridden by derived classes to define
        /// 
        /// a precondition that must be met before the event can be executed. By default, it returns true,
        /// 
        /// indicating that there are no preconditions.
        /// 
        /// </summary>
        public virtual bool Precondition() => true;

        /// <summary>
        /// 
        /// The ExecuteEvent method checks the precondition and executes the event if the precondition is met.
        /// 
        /// </summary>
        internal virtual void ExecuteEvent()
        {
            // Check if the precondition is met before executing the event
            if (Precondition())
                // Execute the event
                Execute();
        }

        /// <summary>
        /// 
        /// The Cleanup method is a virtual method that can be overridden by derived classes to define
        /// 
        /// any cleanup actions that should be performed after the event is executed. By default, it does nothing.
        /// 
        /// </summary>
        internal virtual void Cleanup()
        {

        }
    }

    /// <summary>
    /// 
    /// The Event class is a generic class that inherits from the base Event class. It provides a static
    /// 
    /// action that can be used to notify when the event is executed, allowing for custom behavior
    /// 
    /// when the event is triggered.
    /// 
    /// </summary>
    public abstract class Event<T> : Event where T : Event<T>
    {
        /// <summary>
        /// 
        /// The OnExecute action is a static action that can be used to notify when the event is executed.
        /// 
        /// It allows for custom behavior when the event is triggered, such as logging or triggering other actions.
        /// 
        /// </summary>
        public static System.Action<T> OnExecute;

        /// <summary>
        /// 
        /// The Execute method is an abstract method that must be implemented by derived classes to define
        /// 
        /// the specific behavior of the event when it is executed.
        /// 
        /// </summary>
        internal override void ExecuteEvent()
        {
            // Check if the precondition is met before executing the event
            if (Precondition())
            {
                // Execute the event
                Execute();
                // Invoke the OnExecute action if it is not null
                OnExecute?.Invoke((T)this);
            }
        }
    }
}