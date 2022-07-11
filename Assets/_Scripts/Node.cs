using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] SpriteRenderer highlightSprite;
    [SerializeField] SpriteRenderer nodeSprite;
    [SerializeField] GameObject buildEffect;

    [HideInInspector] public GameObject tower;
    [HideInInspector] public Blueprint towerBlueprint;
    [HideInInspector] public bool isMaxUpgraded = false;
    [HideInInspector] public int currentUpgradeLvl;

    private Color _defaultColor;
    private BuildManager _buildManager;
    private const float DESTROY_EFFECT_DELAY = 2f;

    private void Start()
    {
        _defaultColor = highlightSprite.color;
        _buildManager = BuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if (!_buildManager.CanBuild || GameplayManager.IsGameEnded || tower != null)
            return;

        if (_buildManager.HasMoney(this))
            highlightSprite.color = newHighlightColor;
        else
            highlightSprite.color = notEnoughMoneyColor;
    }
    private void OnMouseExit()
    {
        highlightSprite.color = _defaultColor;
    }
    private void BuildTower(Blueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.towerCosts[currentUpgradeLvl])
            return;
        PayCostForTowerBuild(blueprint);

        GameObject newTower = Instantiate(blueprint.prefabs[currentUpgradeLvl], transform.position, Quaternion.identity);
        tower = newTower;
        towerBlueprint = blueprint;

        InstantiateEffect();
    }
    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.towerCosts[currentUpgradeLvl] || isMaxUpgraded)
            return;

        currentUpgradeLvl++;
        if (!isMaxUpgraded)
        {
            PayCostForTowerUpgrade(towerBlueprint);
            GetRidOfOldTower();
            // Build a new one:
            GameObject newTower = Instantiate(towerBlueprint.prefabs[currentUpgradeLvl], transform.position, Quaternion.identity);
            tower = newTower;

            InstantiateEffect();
        }
        if (Mathf.Approximately(currentUpgradeLvl, towerBlueprint.prefabs.Length - 1))
        {
            isMaxUpgraded = true;
            return;
        }
    }
    private void OnMouseDown()
    {
        if (tower != null)
        {
            _buildManager.SelectNode(this);
            return;
        }

        if (!_buildManager.CanBuild)
            return;

        else if (_buildManager.HasMoney(this))
        {
            BuildTower(_buildManager.GetTowerToBuild());
            DisableNodeTexture();
        }
    }
    private void DisableNodeTexture()
    {
        nodeSprite.sprite = null;
        nodeSprite.material = null;
    }
    private void PayCostForTowerBuild(Blueprint blueprint)
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.towerCosts[currentUpgradeLvl], 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
    private void PayCostForTowerUpgrade(Blueprint blueprint)
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.towerCosts[currentUpgradeLvl], 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
    private void InstantiateEffect()
    {
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);
    }
    private void GetRidOfOldTower()
    {
        Destroy(tower);
    }
}
