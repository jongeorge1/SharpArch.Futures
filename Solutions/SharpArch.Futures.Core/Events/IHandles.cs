namespace SharpArch.Futures.Core.Events
{
	public interface IHandles<T> where T : IDomainEvent
	{
		void Handle(T args); 
	}
}