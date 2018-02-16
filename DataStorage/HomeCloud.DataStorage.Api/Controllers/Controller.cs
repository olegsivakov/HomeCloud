namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using HomeCloud.Mapping;

	using ControllerBase = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Provides methods to process the <see cref="ServiceResult" /> data and expose an action supported by <see cref="IHttpMethodResult" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.ControllerBase" />
	public abstract class Controller : ControllerBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Controller" /> class.
		/// </summary>
		/// <param name="mapper">The type mapper.</param>
		protected Controller(IMapper mapper)
			: base()
		{
			this.Mapper = mapper;
		}

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the type mapper.
		/// </summary>
		/// <value>
		/// The type mapper.
		/// </value>
		protected IMapper Mapper { get; private set; }

		#endregion
	}
}
