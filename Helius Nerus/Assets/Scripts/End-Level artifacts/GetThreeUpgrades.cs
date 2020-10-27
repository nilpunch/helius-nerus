using UnityEngine;

public class GetThreeUpgrades : MonoBehaviour
{
#if UNITY_EDITOR
    private void Awake()
    {
        ArtifactUpgradePair[] upgrades = new ArtifactUpgradePair[3];
        for (int i = 0; i < 3; ++i)
        {
            upgrades[i] = ArtifactUpgradePair.CreateRandomPair();
            Debug.Log(upgrades[i].Description);
        }
        int upgradeNum = Random.Range(0, 3);
        int upgradeSlot = Random.Range(0, 2);
        Player.PlayerWeapons[upgradeSlot].ApplyPair(upgrades[upgradeNum]);
        Debug.Log("Applied mod " + upgradeNum + " to weapon " + upgradeSlot);
        for (int i = 0; i < 3; ++i)
        {
            if (i != upgradeNum)
                WeaponModifierContainer.Instance.ReturnUnlockedMod(upgrades[i].ModifierID);
                //ModifiersCollection.ReturnModifierToPool(upgrades[i].WeaponModifier);
                continue;
        }
    }
#endif
}
