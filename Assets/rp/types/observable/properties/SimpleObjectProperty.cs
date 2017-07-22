using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rp.observable
{
	public class SimpleObjectProperty<T> : AProperty<T>
	{
		public SimpleObjectProperty()
		{
		}

		public SimpleObjectProperty(T value) : base(value)
		{
		}

		public SimpleObjectProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleObjectProperty(string name, object bean, T value) : base(name, bean, value)
		{
		}
	}

	[System.Serializable]
	public class SimpleBooleanProperty : AProperty<bool>
	{
		public SimpleBooleanProperty()
		{
		}

		public SimpleBooleanProperty(bool value) : base(value)
		{
		}

		public SimpleBooleanProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleBooleanProperty(string name, object bean, bool value) : base(name, bean, value)
		{
		}

		public void And(bool value)
		{
			set(value & this.value);
		}

		public void Or(bool value)
		{
			set(value | this.value);
		}

		public void XOr(bool value)
		{
			set(value ^ this.value);
		}

		public void Not()
		{
			set(!this.value);
		}
	}

	[System.Serializable]
	public class SimpleIntegerProperty : AProperty<int>
	{
		public SimpleIntegerProperty()
		{
		}

		public SimpleIntegerProperty(int value) : base(value)
		{
		}

		public SimpleIntegerProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleIntegerProperty(string name, object bean, int value) : base(name, bean, value)
		{
		}

		public void Add(int value)
		{
			set(this.value + value);
		}

		public void AddClamped(int value, int low, int high)
		{
			set(Mathf.Clamp(this.value + value, low, high));
		}

		public void Sub(int value)
		{
			set(this.value - value);
		}

		public void SubClamped(int value, int low, int high)
		{
			set(Mathf.Clamp(this.value - value, low, high));
		}

		public void Mul(int value)
		{
			set(this.value * value);
		}

		public void MulClamped(int value, int low, int high)
		{
			set(Mathf.Clamp(this.value * value, low, high));
		}

		public void Div(int value)
		{
			set(this.value / value);
		}

		public void DivClamped(int value, int low, int high)
		{
			set(Mathf.Clamp(this.value / value, low, high));
		}
	}

	[System.Serializable]
	public class SimpleFloatProperty : AProperty<float>
	{
		public SimpleFloatProperty()
		{
		}

		public SimpleFloatProperty(float value) : base(value)
		{
		}

		public SimpleFloatProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleFloatProperty(string name, object bean, float value) : base(name, bean, value)
		{
		}

		public void Add(float value)
		{
			set(this.value + value);
		}

		public void AddClamped(float value, float low, float high)
		{
			set(Mathf.Clamp(this.value + value, low, high));
		}

		public void Sub(float value)
		{
			set(this.value - value);
		}

		public void SubClamped(float value, float low, float high)
		{
			set(Mathf.Clamp(this.value - value, low, high));
		}

		public void Mul(float value)
		{
			set(this.value * value);
		}

		public void MulClamped(float value, float low, float high)
		{
			set(Mathf.Clamp(this.value * value, low, high));
		}

		public void Div(float value)
		{
			set(this.value / value);
		}

		public void DivClamped(float value, float low, float high)
		{
			set(Mathf.Clamp(this.value / value, low, high));
		}
	}

	[System.Serializable]
	public class SimpleStringProperty : AProperty<string>
	{
		public SimpleStringProperty()
		{
		}

		public SimpleStringProperty(string value) : base(value)
		{
		}

		public SimpleStringProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleStringProperty(string name, object bean, string value) : base(name, bean, value)
		{
		}
	}

	[System.Serializable]
	public class SimpleVector3Property : AProperty<Vector3>
	{
		public SimpleVector3Property()
		{
		}

		public SimpleVector3Property(Vector3 value) : base(value)
		{
		}

		public SimpleVector3Property(string name, object bean) : base(name, bean)
		{
		}

		public SimpleVector3Property(string name, object bean, Vector3 value) : base(name, bean, value)
		{
		}

		public void add(Vector3 value)
		{
			set(this.value + value);
		}

		public void Sub(Vector3 value)
		{
			set(this.value - value);
		}

		public void Scale(Vector3 value)
		{
			set(Vector3.Scale(this.value, value));
		}

		public void Cross(Vector3 value)
		{
			set(Vector3.Cross(this.value, value));
		}
	}

	public class SimpleColorProperty : SimpleObjectProperty<Color>
	{
		public SimpleColorProperty()
		{
		}

		public SimpleColorProperty(Color value) : base(value)
		{
		}

		public SimpleColorProperty(string name, object bean) : base(name, bean)
		{
		}

		public SimpleColorProperty(string name, object bean, Color value) : base(name, bean, value)
		{
		}
	}
}
