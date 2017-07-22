namespace rp.observable
{
	/// <summary>
	/// Event that's fired every time an observable value changes.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="v">The variable that changed</param>
	/// <param name="o">The old value</param>
	/// <param name="n">The new value</param>
	public delegate void ValueChange<T>(IObservableValue<T> v, T o, T n);

	public interface IObservableValue<T> : IObservable, IReadableValue<T>, System.IDisposable
	{
		event ValueChange<T> OnChanged;
	}
}
