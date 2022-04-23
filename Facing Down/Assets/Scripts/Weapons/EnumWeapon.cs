using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumWeapon
{
    public static Weapon GetWeapon(WeaponChoice weapon)
    {
        switch (weapon)
        {
            case WeaponChoice.Katana:
                return new Katana();
            case WeaponChoice.Wings:
                return new Wings();
            case WeaponChoice.WarAxe:
                return new WarAxe();
            case WeaponChoice.Daggers:
                return new Daggers();
            case WeaponChoice.Gun:
                return new Gun();
            case WeaponChoice.DimensionBreaker:
                return new DimensionBreaker();
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
            case WeaponChoice.DimensionBreaker:
                return typeof(DimensionBreaker);
        }
        return null;
    }

    public static Weapon getRandomWeapon()
    {
        WeaponChoice choice = (WeaponChoice)Random.Range(0, System.Enum.GetValues(typeof(WeaponChoice)).Length - 1);

        return GetWeapon(choice);
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
        Katana, Wings, WarAxe, Daggers, Gun, DimensionBreaker
    }
}
