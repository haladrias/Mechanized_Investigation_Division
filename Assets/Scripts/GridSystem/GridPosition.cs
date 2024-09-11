
/// <summary>
/// this struct is used to store the position of a cell in the grid
/// </summary>
public struct GridPosition
{
	public int x;
	public int z;

	public GridPosition(int x, int z)
	{
		this.x = x;
		this.z = z;
	}

	public override string ToString()
	{
		return $"x: {x}; z: {z}";
	}
}

