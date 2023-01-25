using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Reflection;
using GameManagment;
public static class Helpers 
{
}

public static class EventsSystemHelper
{
	public static void SelectUIElement(GameObject go)
	{
		HistoryManager.prevEventsSystemSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if(go != null)
		EventSystem.current.SetSelectedGameObject(go);
	}
}
public static class CoreCalculation
{
	public static RoundDamages CalculateSpellDamage(float manaChanneled_P1, float manaChanneled_P2, SpellSO spell_P1, SpellSO spell_P2)
	{
		float powerDivisor = GameManager.Instance.GameSettings.SpellCalculationValues.powerDivisor;
		float resistK = GameManager.Instance.GameSettings.SpellCalculationValues.resistK;
		float spellModeBonusK = GameManager.Instance.GameSettings.SpellCalculationValues.spellModeBonusK;
		//Player 1
		SpellShapeModeType spellShapeModeType_P1 = spell_P1.SpellShape.SpellShapeMode;

		StatsDamage elementDamage_P1 = spell_P1.ElementCombo.StatsDamage;
		StatsResist elementResist_P1 = spell_P1.ElementCombo.StatsResist;

		StatsDamage shapeDamage_P1 = spell_P1.SpellShape.StatsDamage;
		StatsResist shapeResist_P1 = spell_P1.SpellShape.StatsResist;

		float pierce_P1 = (elementDamage_P1.pierce + shapeDamage_P1.pierce) * (manaChanneled_P1/powerDivisor);
		float force_P1 = (elementDamage_P1.force + shapeDamage_P1.force) * (manaChanneled_P1/powerDivisor);
		float aerosol_P1 = (elementDamage_P1.aerosol + shapeDamage_P1.aerosol) * (manaChanneled_P1/powerDivisor);

		float deflection_P1 = (elementResist_P1.deflection + shapeResist_P1.deflection) * (manaChanneled_P1/powerDivisor) * resistK;
		float toughness_P1 = (elementResist_P1.toughness + shapeResist_P1.toughness) * (manaChanneled_P1/powerDivisor) * resistK;
		float push_P1 = (elementResist_P1.push + shapeResist_P1.push) * (manaChanneled_P1/powerDivisor) * resistK;
		
		//applySpellModeBonus K
		float offensivePierce_P1 = pierce_P1 + pierce_P1*spellModeBonusK;
		float defensivePierce_P1 = pierce_P1 - pierce_P1*spellModeBonusK;

		float offensiveForce_P1 = force_P1 + force_P1*spellModeBonusK;
		float defensiveForce_P1 = force_P1 - force_P1*spellModeBonusK;

		float offensiveAerosol_P1 = aerosol_P1 + aerosol_P1*spellModeBonusK;
		float defensiveAerosol_P1 = aerosol_P1 - aerosol_P1*spellModeBonusK;

		float defensiveDeflection_P1 = deflection_P1 + deflection_P1*spellModeBonusK;
		float offensiveDeflection_P1 = deflection_P1 - deflection_P1*spellModeBonusK;
		
		float defensiveToughness_P1 = toughness_P1 + toughness_P1*spellModeBonusK;
		float offensiveToughness_P1 = toughness_P1 - toughness_P1*spellModeBonusK;

		float defensivePush_P1 = push_P1 + push_P1*spellModeBonusK;
		float offensivePush_P1 = push_P1 - push_P1*spellModeBonusK;


		pierce_P1 = spellShapeModeType_P1 == SpellShapeModeType.Offensive ? offensivePierce_P1 : defensivePierce_P1;
		force_P1 = spellShapeModeType_P1 == SpellShapeModeType.Offensive ? offensiveForce_P1 : defensiveForce_P1;
		aerosol_P1 = spellShapeModeType_P1 == SpellShapeModeType.Offensive ? offensiveAerosol_P1 : defensiveAerosol_P1;

		deflection_P1 = spellShapeModeType_P1 == SpellShapeModeType.Defensive ? defensiveDeflection_P1 : offensiveDeflection_P1;
		toughness_P1 = spellShapeModeType_P1 == SpellShapeModeType.Defensive ? defensiveToughness_P1 : offensiveToughness_P1;
		push_P1 = spellShapeModeType_P1 == SpellShapeModeType.Defensive ? defensivePush_P1 : offensivePush_P1;

		//Player 2
		SpellShapeModeType spellShapeModeType_P2 = spell_P2.SpellShape.SpellShapeMode;

		StatsDamage elementDamage_P2 = spell_P2.ElementCombo.StatsDamage;
		StatsResist elementResist_P2 = spell_P2.ElementCombo.StatsResist;

		StatsDamage shapeDamage_P2 = spell_P2.SpellShape.StatsDamage;
		StatsResist shapeResist_P2 = spell_P2.SpellShape.StatsResist;

		float pierce_P2 = (elementDamage_P2.pierce + shapeDamage_P2.pierce) * (manaChanneled_P2/powerDivisor);
		float force_P2 = (elementDamage_P2.force + shapeDamage_P2.force) * (manaChanneled_P2/powerDivisor);
		float aerosol_P2 = (elementDamage_P2.aerosol + shapeDamage_P2.aerosol) * (manaChanneled_P2/powerDivisor);

		float deflection_P2 = (elementResist_P2.deflection + shapeResist_P2.deflection) * (manaChanneled_P2/powerDivisor) * resistK;
		float toughness_P2 = (elementResist_P2.toughness + shapeResist_P2.toughness) * (manaChanneled_P2/powerDivisor) * resistK;
		float push_P2 = (elementResist_P2.push + shapeResist_P2.push) * (manaChanneled_P2/powerDivisor) * resistK;

		//applySpellModeBonus K
		float offensivePierce_P2 = pierce_P2 + pierce_P2*spellModeBonusK;
		float defensivePierce_P2 = pierce_P2 - pierce_P2*spellModeBonusK;

		float offensiveForce_P2 = force_P2 + force_P2*spellModeBonusK;
		float defensiveForce_P2 = force_P2 - force_P2*spellModeBonusK;

		float offensiveAerosol_P2 = aerosol_P2 + aerosol_P2*spellModeBonusK;
		float defensiveAerosol_P2 = aerosol_P2 - aerosol_P2*spellModeBonusK;

		float defensiveDeflection_P2 = deflection_P2 + deflection_P2*spellModeBonusK;
		float offensiveDeflection_P2 = deflection_P2 - deflection_P2*spellModeBonusK;
		
		float defensiveToughness_P2 = toughness_P2 + toughness_P2*spellModeBonusK;
		float offensiveToughness_P2 = toughness_P2 - toughness_P2*spellModeBonusK;

		float defensivePush_P2 = push_P2 + push_P2*spellModeBonusK;
		float offensivePush_P2 = push_P2 - push_P2*spellModeBonusK;
		

		pierce_P2 = spellShapeModeType_P2 == SpellShapeModeType.Offensive ? offensivePierce_P2 : defensivePierce_P2;
		force_P2 = spellShapeModeType_P2 == SpellShapeModeType.Offensive ? offensiveForce_P2 : defensiveForce_P2;
		aerosol_P2 = spellShapeModeType_P2 == SpellShapeModeType.Offensive ? offensiveAerosol_P2 : defensiveAerosol_P2;

		deflection_P2 = spellShapeModeType_P2 == SpellShapeModeType.Defensive ? defensiveDeflection_P2 : offensiveDeflection_P2;
		toughness_P2 = spellShapeModeType_P2 == SpellShapeModeType.Defensive ? defensiveToughness_P2 : offensiveToughness_P2;
		push_P2 = spellShapeModeType_P2 == SpellShapeModeType.Defensive ? defensivePush_P2 : push_P2 - offensivePush_P2;


		//Players damage calculations
		float P1_receiveDamage_pierce = (pierce_P2-deflection_P1) < 0 ? 0 : (pierce_P2-deflection_P1);
		float P1_receiveDamage_force = (force_P2-toughness_P1) < 0 ? 0 : (force_P2-toughness_P1);
		float P1_receiveDamage_aerosol = (pierce_P2-push_P1) < 0 ? 0 : (force_P2-push_P1);

		float P2_receiveDamage_pierce = (pierce_P1-deflection_P2) < 0 ? 0 : (pierce_P1-deflection_P2);
		float P2_receiveDamage_force = (force_P1-toughness_P2) < 0 ? 0 : (force_P1-toughness_P2);
		float P2_receiveDamage_aerosol = (pierce_P1-push_P2) < 0 ? 0 : (force_P1-push_P2);

		float P1_Damage = P1_receiveDamage_pierce + P1_receiveDamage_force + P1_receiveDamage_aerosol;
		float P2_Damage = P2_receiveDamage_pierce + P2_receiveDamage_force + P2_receiveDamage_aerosol;

		Debug.Log(
@$"Player1 totalDamage: {P2_Damage}
pierce_P1: {pierce_P1}
force_P1: {force_P1}
aerosol_P1: {aerosol_P1}
deflection_P1: {deflection_P1}
toughness_P1: {toughness_P1}
push_P1: {push_P1}
");
		Debug.Log(
@$"Player2 totalDamage: {P1_Damage}
pierce_P2: {pierce_P2}
force_P2: {force_P2}
aerosol_P2: {aerosol_P2}
deflection_P2: {deflection_P2}
toughness_P2: {toughness_P2}
push_P2: {push_P2}
");

		return new RoundDamages(P1_Damage,P2_Damage);
	}
}
[Serializable]
public class RoundDamages
{
	public float P1_Damage;
	public float P2_Damage;

    public RoundDamages(float p1_Damage, float p2_Damage)
    {
        P1_Damage = p1_Damage;
        P2_Damage = p2_Damage;
    }
}