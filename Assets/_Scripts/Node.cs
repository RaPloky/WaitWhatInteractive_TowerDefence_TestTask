using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] SpriteRenderer highlightSprite;

    private Color _defaultColor;
    private GameObject _tower;

    private void Awake()
    {
        _defaultColor = highlightSprite.color;
    }
    private void OnMouseEnter()
    {
        highlightSprite.color = newHighlightColor;
    }
    private void OnMouseExit()
    {
        highlightSprite.color = _defaultColor;
    }
    private void OnMouseDown()
    {
        if (_tower != null)
        {
            Debug.LogWarning("There's already tower!");
        }
        GameObject towerToBuild = BuildManager.instance.GetTurretToBuild();
        _tower = (GameObject)Instantiate(towerToBuild, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
