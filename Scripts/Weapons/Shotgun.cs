using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

	public Shotgun(WeaponCondition condition)
    {
        this.name = "shotgun";
        this.type = WeaponType.FIREARM;
        this.condition = condition;
        switch (condition)
        {
            case WeaponCondition.GOOD:
                this.damage = 80;
                break;
            case WeaponCondition.FAIR:
                this.damage = 60;
                break;
            case WeaponCondition.POOR:
                this.damage = 45;
                break;
            case WeaponCondition.BROKEN:
                this.damage = 0;
                break;
        }
    }

    override
    public void degrade()
    {
        int degradeChance = Random.Range(1, 100);
        if (degradeChance <= 5)
        {
            switch (this.condition)
            {
                case WeaponCondition.GOOD:
                    this.condition = WeaponCondition.FAIR;
                    this.damage = 60;
                    break;
                case WeaponCondition.FAIR:
                    this.condition = WeaponCondition.POOR;
                    this.damage = 45;
                    break;
                case WeaponCondition.POOR:
                    this.condition = WeaponCondition.BROKEN;
                    this.damage = 0;
                    break;
            }
        }
    }

    override
    public void repair()
    {
        switch (this.condition)
        {
            case WeaponCondition.FAIR:
                this.condition = WeaponCondition.GOOD;
                this.damage = 80;
                break;
            case WeaponCondition.POOR:
                this.condition = WeaponCondition.FAIR;
                this.damage = 60;
                break;
            case WeaponCondition.BROKEN:
                this.condition = WeaponCondition.POOR;
                this.damage = 45;
                break;
        }
    }
}
