using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] SpriteRenderer highlightSprite;
    [SerializeField] SpriteRenderer nodeSprite;
    [SerializeField] GameObject buildEffect;
    [SerializeField] GameObject upgradeEffect;
    [SerializeField] GameObject sellEffect;

    [HideInInspector] public GameObject tower;
    [HideInInspector] public Blueprint towerBlueprint;
    [HideInInspector] public bool isMaxUpgraded = false;
    [HideInInspector] public int currentUpgradeLvl;

    private Color _defaultColor;
    private BuildManager _buildManager;
    private Material _defaultNodeMaterial;
    private Sprite _defaultNodeSprite;
    private const float DESTROY_EFFECT_DELAY = 2f;

    private void Start()
    {
        _defaultColor = highlightSprite.color;
        _buildManager = BuildManager.instance;
        _defaultNodeMaterial = nodeSprite.material;
        _defaultNodeSprite = nodeSprite.sprite;
    }
    private void OnMouseEnter()
    {
        if (!_buildManager.IsBuildingAllowedHere || GameplayManager.IsGameEnded || tower != null)
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

        PayCost(blueprint);
        GameObject newTower = Instantiate(blueprint.prefabs[currentUpgradeLvl], transform.position, Quaternion.identity);
        tower = newTower;
        towerBlueprint = blueprint;

        InstantiateEffect(buildEffect);
    }
    public void UpgradeTower()
    {
        // [@index + 1] because next updgrade level is after current 
        if (PlayerStats.Money < towerBlueprint.towerCosts[currentUpgradeLvl + 1] || isMaxUpgraded)
            return;

        currentUpgradeLvl++;
        if (!isMaxUpgraded)
        {
            PayCost(towerBlueprint);
            GetRidOfOldTower();
            // Build a new one after removing previous
            GameObject newTower = Instantiate(towerBlueprint.prefabs[currentUpgradeLvl], transform.position, Quaternion.identity);
            tower = newTower;

            InstantiateEffect(upgradeEffect);
        }
        if (IsMaxUpdgradeLevelReached())
        {
            isMaxUpgraded = true;
            return;
        }
    }
    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount(currentUpgradeLvl);
        // Remove info about sold tower blueprint
        towerBlueprint = null;
        currentUpgradeLvl = 0;
        EnableNodeTexture();
        InstantiateEffect(sellEffect);
        Destroy(tower);
    }
    private void OnMouseDown()
    {
        if (tower != null)
        {
            _buildManager.SelectNode(this);
            return;
        }

        if (!_buildManager.IsBuildingAllowedHere)
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
    private void EnableNodeTexture()
    {
        nodeSprite.sprite = _defaultNodeSprite;
        nodeSprite.material = _defaultNodeMaterial;
    }
    private void PayCost(Blueprint blueprint)
    {
        PlayerStats.Money = (int)Mathf.Clamp(PlayerStats.Money - blueprint.towerCosts[currentUpgradeLvl], 0, Mathf.Infinity);
        PlayerStats.instance.UpdateMoneyText();
    }
    private void InstantiateEffect(GameObject effectPrefab)
    {
        GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, DESTROY_EFFECT_DELAY);
    } 
    private void GetRidOfOldTower()
    {
        Destroy(tower);
    }
    private bool IsMaxUpdgradeLevelReached()
    {
        return Mathf.Approximately(currentUpgradeLvl, towerBlueprint.prefabs.Length - 1);
    }
}
