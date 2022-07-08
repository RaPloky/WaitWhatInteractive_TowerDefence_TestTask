using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject bulletTowerPrefab;
    public GameObject energyTowerPrefab;

    private GameObject _towerToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;
    }
    public GameObject GetTowerToBuild()
    {
        return _towerToBuild;
    }
    public void SetTowerToBuild(GameObject tower)
    {
        _towerToBuild = tower;
    }
}
