using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject bulletTowerPrefab;
    public GameObject energyTowerPrefab;

    private Blueprint _towerToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;
    }
    public bool CanBuild { get { return _towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= _towerToBuild.cost; } }
    public void SelectTowerToBuild(Blueprint selectedTower)
    {
        _towerToBuild = selectedTower;
    }
    public void BuildTowerOn(Node node)
    {
        if (PlayerStats.Money < _towerToBuild.cost)
            return;

        PlayerStats.Money -= _towerToBuild.cost;
        GameObject tower = Instantiate(_towerToBuild.prefab, node.transform.position, Quaternion.identity);
        node.tower = tower;
    }
}
