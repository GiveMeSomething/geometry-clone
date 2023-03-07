using System;

[Serializable]
public class MapPattern
{
	// To identify current pattern and to determine their next pattern
	public int id;

	// Map length
	public int length;

	// Data convertible to a list objects and their position
	public int[] data;
}

