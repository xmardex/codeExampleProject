using System;

[Serializable]
public class StatsDamage
{
	[StatUIName("Pierce")]
	public float pierce;
	[StatUIName("Force")]
	public float force;
	[StatUIName("Aerosol")]
	public float aerosol;

	public StatsDamage(float pierce = 0, float force = 0, float aerosol = 0)
	{
		this.pierce = pierce;
		this.force = force;
		this.aerosol = aerosol;
	}
	public StatsDamage Clone()
	{
		return new StatsDamage(
			this.pierce,
			this.force,
			this.aerosol
		);
	}
}
