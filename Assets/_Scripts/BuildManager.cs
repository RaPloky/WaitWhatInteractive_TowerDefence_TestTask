using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private GameObject _turretToBuild;

    [SerializeField] GameObject standardTower;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        _turretToBuild = standardTower;
    }
    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }
}
