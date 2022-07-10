using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] TextMeshProUGUI upgradeCost;
    [SerializeField] Button upgradeButton;
    private Node _target;

    public void SetTarget(Node target)
    {
        UI.SetActive(true);
        _target = target;
        transform.position = target.transform.position;
        if (!target.isMaxUpgraded)
        {
            // !!!
            // Maybe unexpected value:
            upgradeCost.text = "$" + _target.towerBlueprint.costs[_target.nextTowerLvlIndex];
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX LEVEL";
            upgradeButton.interactable = false;
        }
    }
    public void Hide()
    {
        UI.SetActive(false);
    }
    public void Upgrade()
    {
        _target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }
}
