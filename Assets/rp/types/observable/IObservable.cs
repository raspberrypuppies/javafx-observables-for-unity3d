namespace rp.observable
{
	/// <summary>
	/// An Observable is an entity that wraps content and allows to observe the
	/// content for invalidations. An implementation of Observable may support
	/// lazy evaluation, which means that the content is not immediately
	/// recomputed after changes, but lazily the next time it is requested.All
	/// bindings and properties in this library support lazy evaluation.
	/// Implementations of this class should strive to generate as few events as
	/// possible to avoid wasting too much time in event handlers.Implementations
	/// in this library mark themselves as invalid when the first invalidation
	/// event occurs. They do not generate anymore invalidation events until
	/// their value is recomputed and valid again.
	/// </summary>
	public interface IObservable : System.IDisposable
	{
		//addInvalidationListener
		//removeInvalidationListener
		/// <summary>
		/// The name of the observable object, it defaults to the class name if
		/// nothing is defined
		/// </summary>
		string name
		{
			get; set;
		}

		/// <summary>
		/// is logging enabled for this object?
		/// </summary>
		bool log
		{
			get; set;
		}

		/// <summary>
		/// Returns the Object that contains this property. If this property is
		/// not contained in an Object, null is returned.
		/// </summary>
		object Bean
		{
			get; set;
		}
	}
}
