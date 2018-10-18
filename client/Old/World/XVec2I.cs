using SLua;

/// <summary>
/// Vec2 i.
/// </summary>
[CustomLuaClass]
public class XVec2I{
	/// <summary>
	/// Gets or sets the x.
	/// </summary>
	/// <value>The x.</value>
	public int X
	{ get; set; }

	/// <summary>
	/// Gets or sets the z.
	/// </summary>
	/// <value>The z.</value>
	public int Z
	{ get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Vec2I"/> class.
	/// </summary>
	public XVec2I(){
		X = 0;
		Z = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Vec2I"/> class.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public XVec2I(int x, int z){
		X = x;
		Z = z;
	}

	/// <summary>
	/// Serves as a hash function for a particular type.
	/// </summary>
	/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
	public override int	 	GetHashCode()
	{
		return X.GetHashCode() ^ Z.GetHashCode();
	}

	/// <summary>
	/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Vec2I"/>.
	/// </summary>
	/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Vec2I"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="Vec2I"/>; otherwise, <c>false</c>.</returns>
	public override bool 	Equals(object obj)
	{	
		XVec2I other = obj as XVec2I;
		return this.X == other.X && this.Z == other.Z;
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString(){
		return string.Format ("[{0},{1}]", X, Z);
	}
}
