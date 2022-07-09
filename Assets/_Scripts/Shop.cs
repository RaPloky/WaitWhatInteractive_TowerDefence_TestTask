using UnityEngine;

public class Shop : MonoBehaviour
{
    public Blueprint bulletTower;
    public Blueprint energyTower;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }
    public void SelectBulletTower()
    {
        _buildManager.SelectTowerToBuild(bulletTower);
    }
    public void SelectEnergyTower()
    {
        _buildManager.SelectTowerToBuild(energyTower);
    }
}
