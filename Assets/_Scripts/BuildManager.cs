using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton
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
    public bool IsBuildingAllowedHere { get { return _towerToBuild != null; } }
    public bool HasMoney(Node selectedNode) 
    { 
         return PlayerStats.Money >= _towerToBuild.towerCosts[selectedNode.currentUpgradeLvl];
    }
    public void SelectTowerToBuild(Blueprint selectedTower)
    {
        // For tower build button: sets prefered tower type
        // and unsets previously choosen node
        _towerToBuild = selectedTower;
        _selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectNode(Node node)
    {
        // Double-click deselects node and hides UI
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }
        // For node: sets prefered node and unsets
        // tower type to build button
        _selectedNode = node;
        _towerToBuild = null;
        // Place upgrade/sell buttons in right place (choosen tower)
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
