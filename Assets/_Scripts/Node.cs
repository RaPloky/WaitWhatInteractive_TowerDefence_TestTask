using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] SpriteRenderer highlightSprite;

    private Color _defaultColor;
    private GameObject _tower;
    private BuildManager _buildManager;

    private void Start()
    {
        _defaultColor = highlightSprite.color;
        _buildManager = BuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if (_buildManager.GetTowerToBuild() == null)
            return;
        highlightSprite.color = newHighlightColor;
    }
    private void OnMouseExit()
    {
        highlightSprite.color = _defaultColor;
    }
    private void OnMouseDown()
    {
        if (_buildManager.GetTowerToBuild() == null)
            return;
        if (_tower != null)
            Debug.LogWarning("There's already tower!");

        GameObject towerToBuild = _buildManager.GetTowerToBuild();
        _tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
