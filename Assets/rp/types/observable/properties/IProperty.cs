namespace rp.observable
{
	/// <summary>
	/// Generic interface that defines the methods common to all (writable)
	/// properties independent of their type.
	/// </summary>
	public interface IProperty<T> : IObservableValue<T>, IWritableValue<T>
	{
		void bind(IObservableValue<T> observable);

		bool isBound
		{
			get;
		}

		void unBind();
	}
}