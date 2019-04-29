using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class GameData {

    public JamScene LoadedJamScene;

    public int PlayerMaxHealth;
    public int PlayerDamageLvl;
    public int PlayerWeaponLvl;

    public int PlayerCurrentHealth;

    public static GameData GetDefault() {
        GameData defaultGameData = new GameData();
        defaultGameData.LoadedJamScene = JamScene.Invalid;
        defaultGameData.PlayerMaxHealth = 3;
        defaultGameData.PlayerDamageLvl = 0;
        defaultGameData.PlayerWeaponLvl = 0;
        defaultGameData.PlayerCurrentHealth = defaultGameData.GetPlayerMaxHealth();
        return defaultGameData;
    }

    public void TryRespawn() {
        PlayerCurrentHealth = GetPlayerMaxHealth();
    }

    public int GetPlayerMaxHealth() {
        return PlayerMaxHealth;
    }

    public int GetPlayerDamage() {
        return 1 + PlayerDamageLvl;
    }

    public int GetPlayerWeapon() {
        return 1 + PlayerWeaponLvl;
    }

    public int GetExtraDamageFromWeapon(int weaponLvl) {
        return 3 * weaponLvl;
    }

    public int GetCurrentDamage(int weaponUsed) {
        return GetPlayerDamage() + GetExtraDamageFromWeapon(weaponUsed);
    }

}
