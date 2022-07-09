using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject bulletTowerPrefab;
    public GameObject energyTowerPrefab;
    [SerializeField] GameObject buildEffect;

    private Blueprint _towerToBuild;
    private const float DESTROY_EFFECT_DELAY = 2f;

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
        PayCostForTower();
        GameObject tower = Instantiate(_towerToBuild.prefab, node.transform.position, Quaternion.identity);
        node.tower = tower;
        GameObject effect = Instantiate(buildEffect, node.transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);
    }
    private void PayCostForTower()
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - _towerToBuild.cost, 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
}
