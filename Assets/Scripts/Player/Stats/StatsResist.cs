using System;

[Serializable]
public class StatsResist
{
	[StatUIName("Deflection")]
	public float deflection;
	[StatUIName("Toughness")]
	public float toughness;
	[StatUIName("Push")]
	public float push;

	public StatsResist(float deflection = 0, float toughness = 0, float push = 0)
	{
		this.deflection = deflection;
		this.toughness = toughness;
		this.push = push;
	}
	public StatsResist Clone()
	{
		return new StatsResist(
			this.deflection,
			this.toughness,
			this.push);
	}
}