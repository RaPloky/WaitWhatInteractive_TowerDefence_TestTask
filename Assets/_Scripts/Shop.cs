using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }
    public void PurchaseBulletTower()
    {
        _buildManager.SetTowerToBuild(_buildManager.bulletTowerPrefab);
    }
    public void PurchaseEnergyTower()
    {
        _buildManager.SetTowerToBuild(_buildManager.energyTowerPrefab);
    }
}
