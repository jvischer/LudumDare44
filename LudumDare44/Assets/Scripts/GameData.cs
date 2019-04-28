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

    public static GameData GetDefault() {
        GameData defaultGameData = new GameData();
        defaultGameData.PlayerHealthLvl = 0;
        defaultGameData.PlayerDamageLvl = 0;
        defaultGameData.PlayerWeaponLvl = 0;
        return defaultGameData;
    }

}
