using System;
using System.Collections.Generic;

// use this for set basic player stats and modifiers 
// for basic stats you need to fill every parameters
// for modifiers (level, items and other) just fill special parameters (0 don't change stat, -value decrees stat, +value increase stat);
[Serializable]
public class PlayerStats
{
	[StatUIName("Current health")]
	public float currentHealth;
	[StatUIName("Max health")]
	public float maxHealth;
	[StatUIName("Current mana")]
	public float currentMana;
	[StatUIName("Max mana")]
	public float maxMana;
	[StatUIName("Mana replenish rate")]
	public float manaReplenishRate;
	[StatUIName("Fire")]
	public ElementStat fire = new ElementStat(ElementRawType.fire);
	[StatUIName("Earth")]
	public ElementStat earth = new ElementStat(ElementRawType.earth);
	[StatUIName("Water")]
	public ElementStat water = new ElementStat(ElementRawType.air);
	[StatUIName("Air")]
	public ElementStat air = new ElementStat(ElementRawType.water);
	public StatsDamage statsDamage;
	public StatsResist statsResist;

	// clone player stats from basic stats SO
	public PlayerStats Clone() => new PlayerStats 
	{
		currentHealth       =    this.currentHealth,
		maxHealth           =    this.maxHealth,
		currentMana         =    this.currentMana,
		maxMana             =    this.maxMana,
		manaReplenishRate   =    this.manaReplenishRate,
		fire                =    new ElementStat(ElementRawType.fire, this.fire.chargeRate, this.fire.windDownRate),
		earth               =    new ElementStat(ElementRawType.earth, this.earth.chargeRate, this.earth.windDownRate),
		air                 =    new ElementStat(ElementRawType.air, this.air.chargeRate, this.air.windDownRate),
		water               =    new ElementStat(ElementRawType.water, this.water.chargeRate, this.water.windDownRate),
		statsDamage         =    new StatsDamage(this.statsDamage.aerosol,this.statsDamage.force,this.statsDamage.pierce),
		statsResist         =    new StatsResist(this.statsResist.deflection,this.statsResist.toughness,this.statsResist.push)
	};

	public void ApplyModifier(PlayerStats modifier)
	{
		PlayerStats statsModifier = modifier;

		currentHealth           +=    statsModifier.currentHealth;
		maxHealth               +=    statsModifier.maxHealth;
		currentMana             +=    statsModifier.currentMana;
		maxMana                 +=    statsModifier.maxMana;
		manaReplenishRate       +=    statsModifier.manaReplenishRate;

		fire.chargeRate         +=    statsModifier.fire.chargeRate;
		fire.windDownRate       +=    statsModifier.fire.windDownRate;

		earth.chargeRate        +=    statsModifier.earth.chargeRate;
		earth.windDownRate      +=    statsModifier.earth.windDownRate;

		water.chargeRate        +=    statsModifier.water.chargeRate;
		water.windDownRate      +=    statsModifier.water.windDownRate;

		air.chargeRate          +=    statsModifier.air.chargeRate;
		air.windDownRate        +=    statsModifier.air.windDownRate;

		statsDamage.aerosol     +=    statsModifier.statsDamage.aerosol;
		statsDamage.force       +=    statsModifier.statsDamage.force;
		statsDamage.pierce      +=    statsModifier.statsDamage.pierce;

		statsResist.deflection  +=    statsModifier.statsResist.deflection;
		statsResist.toughness   +=    statsModifier.statsResist.toughness;
		statsResist.push        +=    statsModifier.statsResist.push;
	}
	public void SpellBuffHandle(BuffEffect buffEffect, bool apply)
	{
		BuffCondition buffCondition = buffEffect.BuffCondition;
		
		float buffValue = buffCondition.Value * (

			(buffCondition.Operation == BuffOperation.gain ||
			buffCondition.Operation == BuffOperation.gainPerRound) 
				? 1 : -1

		);

		buffValue = apply ? buffValue : buffValue*-1;

		switch (buffCondition.Stat)
		{
			case BuffStat.pierce:
				statsDamage.pierce += buffValue;
			break;

			case BuffStat.force:
				statsDamage.force += buffValue;
			break;

			case BuffStat.aerosol:
				statsDamage.aerosol += buffValue;
			break;

			case BuffStat.deflection:
				statsResist.deflection += buffValue;
			break;

			case BuffStat.toughness:
				statsResist.toughness += buffValue;
			break;

			case BuffStat.push:
				statsResist.push += buffValue;
			break;

			case BuffStat.current_mana:
				currentMana += buffValue;
			break;

			case BuffStat.current_hp:
				currentHealth += buffValue;
			break;
			
			case BuffStat.manaReplenishRate:
                manaReplenishRate += buffValue;
            break;
		}
	}
	public ElementStat GetElementStat(ElementRawType elementRawType)
	{
		List<ElementStat> elementStats = new List<ElementStat>();
		elementStats.AddRange(new ElementStat[]{fire,earth,air,water});
		return elementStats.Find(e=>e.ElementRawType == elementRawType);
	}

}
[Serializable]
public class ElementStat
{
	private ElementRawType type;
	public ElementRawType ElementRawType => type;
	[StatUIName("charge rate")]
	public float chargeRate;
	[StatUIName("wind down rate")]
	public float windDownRate;

	public ElementStat(ElementRawType type, float chargeRate = 0, float windDownRate = 0)
	{
		this.type = type;
		this.chargeRate = chargeRate;
		this.windDownRate = windDownRate;
	}
}
