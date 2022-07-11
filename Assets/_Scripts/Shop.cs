using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Blueprint bulletTower;
    public Blueprint energyTower;

    [SerializeField] TextMeshProUGUI bulletTowerCost;
    [SerializeField] TextMeshProUGUI energyTowerCost;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
        bulletTowerCost.text = "$" + bulletTower.towerCosts[0];
        energyTowerCost.text = "$" + energyTower.towerCosts[0];
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
