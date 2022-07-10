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
    [HideInInspector] public bool isMaxUpgraded = false;
    [HideInInspector] public int nextTowerLvlIndex = 0;

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
        if (PlayerStats.Money < blueprint.costs[nextTowerLvlIndex])
            return;
        PayCostForTowerBuild(blueprint);

        GameObject newTower = Instantiate(blueprint.prefabs[nextTowerLvlIndex], transform.position, Quaternion.identity);
        tower = newTower;
        towerBlueprint = blueprint;
        nextTowerLvlIndex++;

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);
    }
    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.costs[nextTowerLvlIndex])
            return;
        PayCostForTowerUpgrade(towerBlueprint);
        // Get rid of the old tower:
        Destroy(tower);
        // Build a new one:
        GameObject newTower = Instantiate(towerBlueprint.prefabs[nextTowerLvlIndex], transform.position, Quaternion.identity);
        tower = newTower;
        nextTowerLvlIndex++;

        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);

        if (Mathf.Approximately(nextTowerLvlIndex, towerBlueprint.prefabs.Length))
            isMaxUpgraded = true;
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
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.costs[nextTowerLvlIndex], 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
    private void PayCostForTowerUpgrade(Blueprint blueprint)
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.costs[nextTowerLvlIndex], 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
}
