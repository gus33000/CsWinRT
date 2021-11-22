using System.ComponentModel;

namespace NotifyPropertyChangedTests
{
	public sealed class ObservableObject : INotifyPropertyChanged
	{
		private int value;
		public event PropertyChangedEventHandler? PropertyChanged;

		//private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}

		public int Value
		{
			get => value;
			set
			{
				this.value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
			}
		}
	}
}