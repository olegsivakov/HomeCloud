namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System.IO;
	using System.Threading;
	using System.Threading.Tasks;

	#endregion

	public class CatalogEntryStream : Stream
	{
		private readonly Stream stream = null;

		public CatalogEntryStream(CatalogEntry entry, Stream stream)
			: base()
		{
			this.Entry = entry;
			this.stream = stream;
		}

		public CatalogEntryStream(CatalogEntry entry, long length)
			: base()
		{
			this.Entry = entry;

			this.stream = new MemoryStream();
			this.SetLength(length);
		}

		public CatalogEntry Entry { get; set; }

		public override bool CanRead => this.stream.CanRead;

		public override bool CanSeek => this.stream.CanSeek;

		public override bool CanWrite => this.stream.CanWrite;

		public override long Length => this.stream.Length;

		public override long Position { get; set; }

		public override void Flush()
		{
			this.stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		public new void CopyTo(Stream destination)
		{
			this.stream.CopyTo(destination);
		}

		public override async Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			await this.stream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		public override void Close()
		{
			this.stream.Close();
		}
	}
}
