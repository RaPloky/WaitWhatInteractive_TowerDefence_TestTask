using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector] public bool isUpgraded = false;

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

        if (_buildManager.HasMoney)
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
        if (PlayerStats.Money < blueprint.cost)
            return;
        PayCostForTowerBuild(blueprint);

        GameObject newTower = Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        tower = newTower;
        towerBlueprint = blueprint;

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);
    }
    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.upgradeCost)
            return;
        PayCostForTowerUpgrade(towerBlueprint);
        // Get rid of the old tower:
        Destroy(tower);
        // Build a new one:
        GameObject newTower = Instantiate(towerBlueprint.upgradedPrefab, transform.position, Quaternion.identity);
        tower = newTower;

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);

        isUpgraded = true;
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

        else if (_buildManager.HasMoney)
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
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.cost, 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
    private void PayCostForTowerUpgrade(Blueprint blueprint)
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.upgradeCost, 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
}
