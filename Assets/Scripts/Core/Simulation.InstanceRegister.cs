// <summary>
// Simulation is a static class that manages the game simulation, including event scheduling,
// event pooling, and model management. It provides methods to create new events, schedule them,
// reschedule existing events, and manage game models.
// </summary>
public static partial class Simulation
{
    // <summary>
    // The InstanceRegister class is a generic class that holds a single instance of a model type.
    // It is used to manage game models, allowing for easy access and modification of model instances.
    // </summary>
    static class InstanceRegister<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// The instance field holds a single instance of the model type T.
        /// 
        /// </summary>
#pragma warning disable UDR0001 // Domain Reload Analyzer
        public static T instance = new T();
#pragma warning restore UDR0001 // Domain Reload Analyzer
    }
}