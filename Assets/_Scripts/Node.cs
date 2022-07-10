using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] Color notEnoughMoneyColor;
    [SerializeField] SpriteRenderer highlightSprite;
    [SerializeField] SpriteRenderer nodeSprite;
    [Header("Optional")]
    public GameObject tower;

    private Color _defaultColor;
    private BuildManager _buildManager;

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
            _buildManager.BuildTowerOn(this);
            DisableNodeTexture();
        }
    }
    private void DisableNodeTexture()
    {
        nodeSprite.sprite = null;
        nodeSprite.material = null;
    }
}
