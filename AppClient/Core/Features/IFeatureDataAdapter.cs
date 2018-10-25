namespace AppClient.Core.Features
{
	/// <summary>
	/// Defines where to save the feature and it's respective details
	/// </summary>
	public interface IFeatureDataAdapter
	{
		void Save(Feature feature, string details);
	}
}