namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System.IO;
	using System.Threading;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Represents the content stream of <see cref="CatalogEntry"/>.
	/// </summary>
	/// <seealso cref="System.IO.Stream" />
	public class CatalogEntryStream : Stream
	{
		#region Private Members

		/// <summary>
		/// The stream member
		/// </summary>
		private readonly Stream stream = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogEntryStream"/> class.
		/// </summary>
		/// <param name="entry">The catalog entry.</param>
		/// <param name="stream">The stream the current instance is being built from.</param>
		public CatalogEntryStream(CatalogEntry entry, Stream stream)
			: base()
		{
			this.Entry = entry;
			this.stream = stream;
			this.Entry.Size = stream.Length;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogEntryStream"/> class.
		/// </summary>
		/// <param name="entry">The catalog entry.</param>
		/// <param name="length">The stream length.</param>
		public CatalogEntryStream(CatalogEntry entry, long length)
			: base()
		{
			this.Entry = entry;

			this.stream = new MemoryStream();
			this.SetLength(length);
		}

		#endregion

		#region Public Propoerties

		/// <summary>
		/// Gets or sets the catalog  entry.
		/// </summary>
		/// <value>
		/// The entry.
		/// </value>
		public CatalogEntry Entry { get; set; }

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
		/// </summary>
		public override bool CanRead => this.stream.CanRead;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
		/// </summary>
		public override bool CanSeek => this.stream.CanSeek;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
		/// </summary>
		public override bool CanWrite => this.stream.CanWrite;

		/// <summary>
		/// When overridden in a derived class, gets the length in bytes of the stream.
		/// </summary>
		public override long Length => this.stream.Length;

		/// <summary>
		/// When overridden in a derived class, gets or sets the position within the current stream.
		/// </summary>
		public override long Position { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		public override void Flush()
		{
			this.stream.Flush();
		}

		/// <summary>
		/// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>
		/// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
		/// </returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		/// <summary>
		/// When overridden in a derived class, sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		/// <summary>
		/// When overridden in a derived class, sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
			if (this.Entry != null)
			{
				this.Entry.Size = stream.Length;
			}
		}

		/// <summary>
		/// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		/// <summary>
		/// Reads the bytes from the current stream and writes them to another stream.
		/// </summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		public new void CopyTo(Stream destination)
		{
			this.stream.CopyTo(destination);
		}

		/// <summary>
		/// Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.
		/// </summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>
		/// A task that represents the asynchronous copy operation.
		/// </returns>
		public override async Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			await this.stream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		/// <summary>
		/// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.
		/// </summary>
		public override void Close()
		{
			this.stream.Close();
		}

		#endregion
	}
}
