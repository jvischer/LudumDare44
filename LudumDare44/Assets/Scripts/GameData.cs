using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class GameData {

    public int PlayerHealthLvl;
    public int PlayerDamageLvl;
    public int PlayerWeaponLvl;

    public int PlayerCurrentHealth;

    public static GameData GetDefault() {
        GameData defaultGameData = new GameData();
        defaultGameData.PlayerHealthLvl = 0;
        defaultGameData.PlayerDamageLvl = 0;
        defaultGameData.PlayerWeaponLvl = 0;
        defaultGameData.PlayerCurrentHealth = defaultGameData.GetPlayerHealth();
        return defaultGameData;
    }

    public int GetPlayerHealth() {
        return 3 + PlayerHealthLvl;
    }

    public int GetPlayerDamage() {
        return 1 + PlayerDamageLvl;
    }

    public int GetPlayerWeapon() {
        return 1 + PlayerWeaponLvl;
    }

}
