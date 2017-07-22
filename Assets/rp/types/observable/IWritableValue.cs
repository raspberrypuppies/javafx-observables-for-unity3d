namespace rp.observable
{
	/// <summary>
	/// A WritableValue is an entity that wraps a value that can be read and set. In general this interface should not be implemented directly but one of its sub-interfaces (WritableBooleanValue etc.).
	/// </summary>
	public interface IWritableValue<T> : IReadableValue<T>
    {
        void set(T value);
    }
}