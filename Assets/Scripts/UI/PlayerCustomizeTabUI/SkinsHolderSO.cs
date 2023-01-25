using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SkinsHolder", menuName = "PlayerSkins/SkinHolderSO", order = 0)]
public class SkinsHolderSO : ScriptableObject {
    public List<SkinSO> allSkins = new List<SkinSO>();
}