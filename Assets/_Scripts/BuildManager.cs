using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject bulletTowerPrefab;
    public GameObject energyTowerPrefab;
    [SerializeField] NodeUI nodeUI;

    private Blueprint _towerToBuild;
    private Node _selectedNode;

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
        _selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }
        _selectedNode = node;
        _towerToBuild = null;
        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        _selectedNode = null;
        nodeUI.Hide();
    }
    public Blueprint GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
