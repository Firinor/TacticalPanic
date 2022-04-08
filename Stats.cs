using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public int HP;
    public int MP;
    public int CP;
    public int SP;

    public int Strenght;
    public int Agility;
    public int Intelect;
    public int Wisdom;

    public enum Points { HP, MP, CP, SP };

    private float _currentHP;
    private float _currentMP;
    private float _currentCP;
    private float _currentSP;

    public float CurrentAttackSpeed;

    [SerializeField]
    private GameObject _gameObjectHP;
    [SerializeField]
    private GameObject _gameObjectMP;
    [SerializeField]
    private GameObject _gameObjectCP;
    [SerializeField]
    private GameObject _gameObjectSP;

    private Slider _sliderHP;
    private Slider _sliderMP;
    private Slider _sliderCP;
    private Slider _sliderSP;

    void Start()
    {
        if (_gameObjectHP != null)
        { 
            _gameObjectHP.transform.GetChild(0).TryGetComponent<Slider>(out _sliderHP);
            _sliderHP.maxValue = HP; 
        }
        if (_gameObjectMP != null)
        {
            _gameObjectMP.transform.GetChild(0).TryGetComponent<Slider>(out _sliderMP);
            _sliderMP.maxValue = MP;
        }
        if (_gameObjectCP != null)
        {
            _gameObjectCP.transform.GetChild(0).TryGetComponent<Slider>(out _sliderCP);
            _sliderCP.maxValue = CP;
        }
        if (_gameObjectSP != null)
        {
            _gameObjectSP.transform.GetChild(0).TryGetComponent<Slider>(out _sliderSP);
            _sliderSP.maxValue = SP;
        }

        _currentHP = (float) HP;
        _currentMP = (float) MP;
        _currentCP = (float) CP;
        _currentSP = (float) SP;

        RefreshBar();

        CurrentAttackSpeed = (float) Agility;
    }
    public void Death()
    {
        DisableAllScripts();
        AnimDeath();
    }

    private void DisableAllScripts()
    {
        gameObject.GetComponent<MoveEnemy>().Deactivate();
        gameObject.GetComponent<Fight>().Deactivate();
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
        {
            collider2D.enabled = false;
        }
    }

    private void AnimDeath()
    {
        gameObject.GetComponentInChildren<Animator>().Play("Death");
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage, Points points)
    {
        switch(points)
        {
            case Points.HP:
                if(_sliderHP != null)
                    Damage(damage, _sliderHP);
                break;
            case Points.MP:
                if (_sliderMP != null)
                    Damage(damage, _sliderMP);
                break;
            case Points.CP:
                if (_sliderCP != null)
                    Damage(damage, _sliderCP);
                break;
            case Points.SP:
                if (_sliderSP != null)
                    Damage(damage, _sliderSP);
                break;
            default:
                return;
        }
    }

    public void Damage(float damage, Slider barsSlider)
    {

        if (barsSlider == _sliderHP)
        {
            _currentHP -= damage;
            _currentHP = math.min(_currentHP, HP);
            _sliderHP.value = math.max(_currentHP, 0);
        }
        else if (barsSlider == _sliderMP)
        {
            _currentMP -= damage;
            _currentMP = math.min(_currentMP, MP);
            _sliderMP.value = math.max(_currentMP, 0);
        }
        else if (barsSlider == _sliderCP)
        {
            _currentCP -= damage;
            _currentCP = math.min(_currentCP, CP);
            _sliderCP.value = math.max(_currentCP, 0);
            CurrentAttackSpeed = Agility * _currentCP / CP;
        }
        else if (barsSlider == _sliderSP)
        {
            _currentSP -= damage;
            _currentSP = math.min(_currentSP, SP);
            _sliderSP.value = math.max(_currentSP, 0);
        }
        else
            return;

        if (_currentHP <= 0)
        {
            Death();
        }
    }

    private void RefreshBar()
    {
        if(_sliderHP != null)
            _sliderHP.value = _currentHP;

        if(_sliderMP != null)
            _sliderHP.value = _currentMP;

        if(_sliderCP != null)
            _sliderCP.value = _currentCP;

        if(_sliderSP != null)
            _sliderSP.value = _currentSP;
    }

    public void Deploy()
    {
        
    }

    public bool CheckTermsToDeploy()
    {
        return false;
    }
}
