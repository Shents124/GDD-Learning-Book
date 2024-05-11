using UnityEngine;

namespace SaveLoad
{

    public enum UserType
    {
        free = 1, 
        iap = 2, 
        ads = 3
    }
    
    public static class SaveLoadService
    {
        public static UserType LoadUserType()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.USER_TYPE))
                return (UserType) PlayerPrefs.GetInt(PlayerPrefsKey.USER_TYPE);

            return UserType.free;
        }
    }
}