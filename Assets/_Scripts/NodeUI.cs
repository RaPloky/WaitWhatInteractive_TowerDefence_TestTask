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
            upgradeCost.text = "$" + _target.towerBlueprint.towerCosts[_target.currentUpgradeLvl + 1];
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
