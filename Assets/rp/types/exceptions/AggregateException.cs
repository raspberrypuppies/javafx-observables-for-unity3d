using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace rp
{
	public class AggregateException : System.Exception
	{
		public readonly Exception[] aggregated;

		public AggregateException(List<Exception> aggregated) : base("An aggregated exception occurred", aggregated[0])
		{
			this.aggregated = aggregated.ToArray();
		}

		public AggregateException(params Exception[] aggregated) : base("An aggregated exception occurred", aggregated[0])
		{
			this.aggregated = aggregated;
		}

		public AggregateException(String message, List<Exception> aggregated) : base(message, aggregated[0])
		{
			this.aggregated = aggregated.ToArray();
		}

		public AggregateException(String message, params Exception[] aggregated) : base(message, aggregated[0])
		{
			this.aggregated = aggregated;
		}
	}
}
