namespace HomeCloud.IO
{
	/// <summary>
	/// Delegate to call when a new file is found.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	/// <param name="cancel">Indicates whether the event is canceled.</param>
	public delegate void FileEventHandler(string fileName, ref bool cancel);
}
