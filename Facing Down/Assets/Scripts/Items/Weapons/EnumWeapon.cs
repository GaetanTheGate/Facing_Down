using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumWeapon
{
    public static Weapon GetWeapon(WeaponChoice weapon, string target)
    {
        switch (weapon)
        {
            case WeaponChoice.Katana:
                return new Katana(target);
            case WeaponChoice.Wings:
                return new Wings(target);
            case WeaponChoice.WarAxe:
                return new WarAxe(target);
            case WeaponChoice.Daggers:
                return new Daggers(target);
            case WeaponChoice.Gun:
                return new Gun(target);
            case WeaponChoice.Shuriken:
                return new Shuriken(target);
            case WeaponChoice.DimensionBreaker:
                return new DimensionBreaker(target);
        }
        return null;
    }

    public static System.Type GetWeaponType(WeaponChoice weapon)
    {
        switch (weapon)
        {
            case WeaponChoice.Katana:
                return typeof(Katana);
            case WeaponChoice.Wings:
                return typeof(Wings);
            case WeaponChoice.WarAxe:
                return typeof(WarAxe);
            case WeaponChoice.Daggers:
                return typeof(Daggers);
            case WeaponChoice.Gun:
                return typeof(Gun);
            case WeaponChoice.Shuriken:
                return typeof(Shuriken);
            case WeaponChoice.DimensionBreaker:
                return typeof(DimensionBreaker);
        }
        return null;
    }

    public static Weapon getRandomWeapon(string target)
    {
        WeaponChoice choice = (WeaponChoice)Random.Range(0, System.Enum.GetValues(typeof(WeaponChoice)).Length - 1);

        return GetWeapon(choice, target);
    }

    public static WeaponChoice getRandomWeaponChoice()
    {
        return (WeaponChoice)Random.Range(0, System.Enum.GetValues(typeof(WeaponChoice)).Length);
    }

    public static System.Type getRandomWeaponType()
    {
        WeaponChoice choice = (WeaponChoice)Random.Range(0, System.Enum.GetValues(typeof(WeaponChoice)).Length);

        return GetWeaponType(choice);
    }


    public enum WeaponChoice
    {
        Katana, Wings, WarAxe, Daggers, Gun, Shuriken, DimensionBreaker
    }
}
