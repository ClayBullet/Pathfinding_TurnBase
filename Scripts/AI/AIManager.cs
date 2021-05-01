using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHealthSystem
{
    float maxHealth { get; set; }
    float currentHealth { get; set; }
    void HealthImplement(float currentHealth);
    void ReceiveDamage(float damageReceived);

}

public interface ISearchAPlace
{
    float searchRadius { get; set; }
    void ChooseTheTarget(TypeTiles typeTile);
    void ChooseTheCover();
}


