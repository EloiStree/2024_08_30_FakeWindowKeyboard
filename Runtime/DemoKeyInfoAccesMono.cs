using System.Collections.Generic;
using UnityEngine;





public class DemoKeyInfoAccesMono : MonoBehaviour {


    public string m_keyToGuess = "0x20";
    public KeyInfoAsInteger m_guess;
    public bool m_found;
    public bool m_isPressType;
    public bool m_isReleaseType;

    public void OnValidate()
    {
        KeyWindowIntegerKeyMap.Instance.TryToGuessKeyInfo(m_keyToGuess, out m_found, out m_guess, out m_isPressType, out m_isReleaseType);
    }

}


[System.Serializable]
public class KeyInfoAsInteger
{
    public string m_windowKeyName;
    public string m_windowKeyNameLower;
    public int m_windowIntegerId255;
    public string m_windowHexadecimal;
    public int m_pressAsInteger;
    public int m_releaseAsInteger;

    
    public KeyInfoAsInteger(string name, int intId, string hexadecimal, int press, int release)
    {
        m_windowKeyName = name;
        m_windowKeyNameLower = name.ToLower();
        m_windowIntegerId255 = intId;
        m_windowHexadecimal = hexadecimal;
        m_pressAsInteger = press;
        m_releaseAsInteger = release;
    }

    public override string ToString()
    {
        return $"KeyInfo(name='{m_windowKeyName}', decimal={m_windowIntegerId255}, hexadecimal='{m_windowHexadecimal}', press={m_pressAsInteger}, release={m_releaseAsInteger})";
    }
}

[System.Serializable]
public class KeyWindowIntegerKeyMap
{
    public  List< KeyInfoAsInteger>        KeysList = new List<KeyInfoAsInteger>();
    public  Dictionary<int, KeyInfoAsInteger> m_keyMapRelease = new Dictionary<int, KeyInfoAsInteger>();
    public  Dictionary<int, KeyInfoAsInteger> m_keyMapPress = new Dictionary<int, KeyInfoAsInteger>();
    public  Dictionary<int, KeyInfoAsInteger> m_keyMapIntId = new Dictionary<int, KeyInfoAsInteger>();
    public  Dictionary<string, KeyInfoAsInteger> m_keyMapHexadecimal = new Dictionary<string, KeyInfoAsInteger>();
    public  Dictionary<string, KeyInfoAsInteger> m_keyMapName = new Dictionary<string, KeyInfoAsInteger>();
    public int[] m_pressIntegerArray= new int[256];
    public int[] m_releaseIntegerArray= new int[256];
    

    public  void TryToGuessKeyInfo(string givenValue, out bool found, out KeyInfoAsInteger info, out bool isPressType, out bool isReleaseType)
    {
        givenValue = givenValue.Trim().ToLower();
        if (int.TryParse(givenValue, out int pressValue))
        {
            GetKeyInfoFromPress(pressValue, out found, out info);
            isPressType = true;
            isReleaseType = false;
            if(found)   
                return;
        }

        if (int.TryParse(givenValue, out int releaseValue))
        {
            GetKeyInfoFromRelease(releaseValue, out found, out info);
            isPressType = false;
            isReleaseType = true; 
            if (found)
                return;

        }
        if (int.TryParse(givenValue, out int decimalValue))
        {
            GetKeyInfoFromIntId(decimalValue, out found, out info);
            isPressType = true;
            isReleaseType = true; 
            if (found)
                return;

        }

        if (m_keyMapHexadecimal.ContainsKey(givenValue))
        {
            GetKeyInfoFromHexadecimal(givenValue, out found, out info);
            isPressType = true;
            isReleaseType = true;
            if (found)
                return;

        }

        if (m_keyMapName.ContainsKey(givenValue))
        {
            GetKeyInfoFromName(givenValue, out found, out info);
            isPressType = true;
            isReleaseType = true;
            if (found)
                return;

        }


        found = false;
        info = null;
        isPressType = false;
        isReleaseType = false;
    }


    public  void GetKeyInfoFromIntId(int integerValue,out bool found, out KeyInfoAsInteger info)
    {
        if(m_keyMapIntId.ContainsKey(integerValue))
        {
            found = true;
            info = m_keyMapIntId[integerValue];
        }
        else
        {
            found = false;
            info = null;
        }
    }

    public  void GetKeyInfoFromHexadecimal(string hexadecimalValue,out bool found, out KeyInfoAsInteger info)
    {
        if(m_keyMapHexadecimal.ContainsKey(hexadecimalValue))
        {
            found = true;
            info = m_keyMapHexadecimal[hexadecimalValue];
        }
        else
        {
            found = false;
            info = null;
        }
    }

    public  void GetKeyInfoFromName(string name,out bool found, out KeyInfoAsInteger info)
    {
        name = name.ToLower();
        if(m_keyMapName.ContainsKey(name))
        {
            found = true;
            info = m_keyMapName[name];
        }
        else
        {
            found = false;
            info = null;
        }
    }

    public  void GetKeyInfoFromPress(int pressValue,out bool found, out KeyInfoAsInteger info)
    {
        if(m_keyMapPress.ContainsKey(pressValue))
        {
            found = true;
            info = m_keyMapPress[pressValue];
        }
        else
        {
            found = false;
            info = null;
        }
    }


    public  void GetKeyInfoFromRelease(int releaseValue,out bool found, out KeyInfoAsInteger info)
    {
        if(m_keyMapRelease.ContainsKey(releaseValue))
        {
            found = true;
            info = m_keyMapRelease[releaseValue];
        }
        else
        {
            found = false;
            info = null;
        }
    }



    public KeyWindowIntegerKeyMap() {
        ResetToInitialValue();
    }

    public static KeyWindowIntegerKeyMap Instance = new KeyWindowIntegerKeyMap();

    static KeyWindowIntegerKeyMap() { 
    
        Instance= new KeyWindowIntegerKeyMap();
        
        
    }


    public void ResetToInitialValue()
    {
        KeysList.Clear();
 
       KeysList.Add(           new KeyInfoAsInteger("Backspace", 8, "0x08", 1008, 2008                            ));
       KeysList.Add(           new KeyInfoAsInteger("Tab", 9, "0x09", 1009, 2009                            ));
       KeysList.Add(     new KeyInfoAsInteger("Clear", 12, "0x0C", 1012, 2012                            ));
       KeysList.Add(     new KeyInfoAsInteger("Enter", 13, "0x0D", 1013, 2013                            ));
       KeysList.Add(     new KeyInfoAsInteger("Shift", 16, "0x10", 1016, 2016                            ));
       KeysList.Add(    new KeyInfoAsInteger("Ctrl", 17, "0x11", 1017, 2017                            ));
       KeysList.Add(   new KeyInfoAsInteger("Alt", 18, "0x12", 1018, 2018                            ));
       KeysList.Add(     new KeyInfoAsInteger("Pause", 19, "0x13", 1019, 2019                            ));
       KeysList.Add(        new KeyInfoAsInteger("CapsLock", 20, "0x14", 1020, 2020                            ));
       KeysList.Add(   new KeyInfoAsInteger("Esc", 27, "0x1B", 1027, 2027                            ));
       KeysList.Add(      new KeyInfoAsInteger("Escape", 27, "0x1B", 1027, 2027                            ));
       KeysList.Add(     new KeyInfoAsInteger("Space", 32, "0x20", 1032, 2032                            ));
       KeysList.Add(      new KeyInfoAsInteger("PageUp", 33, "0x21", 1033, 2033                            ));
       KeysList.Add(        new KeyInfoAsInteger("PageDown", 34, "0x22", 1034, 2034                            ));
       KeysList.Add(   new KeyInfoAsInteger("End", 35, "0x23", 1035, 2035                            ));
       KeysList.Add(    new KeyInfoAsInteger("Home", 36, "0x24", 1036, 2036                            ));
       KeysList.Add(         new KeyInfoAsInteger("LeftArrow", 37, "0x25", 1037, 2037                            ));
       KeysList.Add(    new KeyInfoAsInteger("Left", 37, "0x25", 1037, 2037                            ));
       KeysList.Add(       new KeyInfoAsInteger("UpArrow", 38, "0x26", 1038, 2038                            ));
       KeysList.Add(  new KeyInfoAsInteger("Up", 38, "0x26", 1038, 2038                            ));
       KeysList.Add(          new KeyInfoAsInteger("RightArrow", 39, "0x27", 1039, 2039                            ));
       KeysList.Add(     new KeyInfoAsInteger("Right", 39, "0x27", 1039, 2039                            ));
       KeysList.Add(         new KeyInfoAsInteger("DownArrow", 40, "0x28", 1040, 2040                            ));
       KeysList.Add(    new KeyInfoAsInteger("Down", 40, "0x28", 1040, 2040                            ));
       KeysList.Add(      new KeyInfoAsInteger("Select", 41, "0x29", 1041, 2041                            ));
       KeysList.Add(     new KeyInfoAsInteger("Print", 42, "0x2A", 1042, 2042                            ));
       KeysList.Add(       new KeyInfoAsInteger("Execute", 43, "0x2B", 1043, 2043                            ));
       KeysList.Add(           new KeyInfoAsInteger("PrintScreen", 44, "0x2C", 1044, 2044                            ));
       KeysList.Add(      new KeyInfoAsInteger("Insert", 45, "0x2D", 1045, 2045                            ));
       KeysList.Add(      new KeyInfoAsInteger("Delete", 46, "0x2E", 1046, 2046                            ));
       KeysList.Add( new KeyInfoAsInteger("0", 48, "0x30", 1048, 2048                            ));
       KeysList.Add( new KeyInfoAsInteger("1", 49, "0x31", 1049, 2049                            ));
       KeysList.Add( new KeyInfoAsInteger("2", 50, "0x32", 1050, 2050                            ));
       KeysList.Add( new KeyInfoAsInteger("3", 51, "0x33", 1051, 2051                            ));
       KeysList.Add( new KeyInfoAsInteger("4", 52, "0x34", 1052, 2052                            ));
       KeysList.Add( new KeyInfoAsInteger("5", 53, "0x35", 1053, 2053                            ));
       KeysList.Add( new KeyInfoAsInteger("6", 54, "0x36", 1054, 2054                            ));
       KeysList.Add( new KeyInfoAsInteger("7", 55, "0x37", 1055, 2055                            ));
       KeysList.Add( new KeyInfoAsInteger("8", 56, "0x38", 1056, 2056                            ));
       KeysList.Add( new KeyInfoAsInteger("9", 57, "0x39", 1057, 2057                            ));
       KeysList.Add( new KeyInfoAsInteger("A", 65, "0x41", 1065, 2065                            ));
       KeysList.Add( new KeyInfoAsInteger("B", 66, "0x42", 1066, 2066                            ));
       KeysList.Add( new KeyInfoAsInteger("C", 67, "0x43", 1067, 2067                            ));
       KeysList.Add( new KeyInfoAsInteger("D", 68, "0x44", 1068, 2068                            ));
       KeysList.Add( new KeyInfoAsInteger("E", 69, "0x45", 1069, 2069                            ));
       KeysList.Add( new KeyInfoAsInteger("F", 70, "0x46", 1070, 2070                            ));
       KeysList.Add( new KeyInfoAsInteger("G", 71, "0x47", 1071, 2071                            ));
       KeysList.Add( new KeyInfoAsInteger("H", 72, "0x48", 1072, 2072                            ));
       KeysList.Add( new KeyInfoAsInteger("I", 73, "0x49", 1073, 2073                            ));
       KeysList.Add( new KeyInfoAsInteger("J", 74, "0x4A", 1074, 2074                            ));
       KeysList.Add( new KeyInfoAsInteger("K", 75, "0x4B", 1075, 2075                            ));
       KeysList.Add( new KeyInfoAsInteger("L", 76, "0x4C", 1076, 2076                            ));
       KeysList.Add( new KeyInfoAsInteger("M", 77, "0x4D", 1077, 2077                            ));
       KeysList.Add( new KeyInfoAsInteger("N", 78, "0x4E", 1078, 2078                            ));
       KeysList.Add( new KeyInfoAsInteger("O", 79, "0x4F", 1079, 2079                            ));
       KeysList.Add( new KeyInfoAsInteger("P", 80, "0x50", 1080, 2080                            ));
       KeysList.Add( new KeyInfoAsInteger("Q", 81, "0x51", 1081, 2081                            ));
       KeysList.Add( new KeyInfoAsInteger("R", 82, "0x52", 1082, 2082                            ));
       KeysList.Add( new KeyInfoAsInteger("S", 83, "0x53", 1083, 2083                            ));
       KeysList.Add( new KeyInfoAsInteger("T", 84, "0x54", 1084, 2084                            ));
       KeysList.Add( new KeyInfoAsInteger("U", 85, "0x55", 1085, 2085                            ));
       KeysList.Add( new KeyInfoAsInteger("V", 86, "0x56", 1086, 2086                            ));
       KeysList.Add( new KeyInfoAsInteger("W", 87, "0x57", 1087, 2087                            ));
       KeysList.Add( new KeyInfoAsInteger("X", 88, "0x58", 1088, 2088                            ));
       KeysList.Add( new KeyInfoAsInteger("Y", 89, "0x59", 1089, 2089                            ));
       KeysList.Add( new KeyInfoAsInteger("Z", 90, "0x5A", 1090, 2090                            ));
        KeysList.Add(new KeyInfoAsInteger("LeftWindows", 91, "0x5B", 1091, 2091));
        KeysList.Add(new KeyInfoAsInteger("Windows", 91, "0x5B", 1091, 2091));
        KeysList.Add(            new KeyInfoAsInteger("RightWindows", 92, "0x5C", 1092, 2092                            ));
       KeysList.Add(            new KeyInfoAsInteger("Applications", 93, "0x5D", 1093, 2093                            ));
       KeysList.Add(     new KeyInfoAsInteger("Sleep", 95, "0x5F", 1095, 2095                            ));
        KeysList.Add(new KeyInfoAsInteger("Numpad0", 96, "0x60", 1096, 2096));
        KeysList.Add(new KeyInfoAsInteger("Numpad1", 97, "0x61", 1097, 2097));
        KeysList.Add(new KeyInfoAsInteger("Numpad2", 98, "0x62", 1098, 2098));
        KeysList.Add(new KeyInfoAsInteger("Numpad3", 99, "0x63", 1099, 2099));
        KeysList.Add(new KeyInfoAsInteger("Numpad4", 100, "0x64", 1100, 2100));
        KeysList.Add(new KeyInfoAsInteger("Numpad5", 101, "0x65", 1101, 2101));
        KeysList.Add(new KeyInfoAsInteger("Numpad6", 102, "0x66", 1102, 2102));
        KeysList.Add(new KeyInfoAsInteger("Numpad7", 103, "0x67", 1103, 2103));
        KeysList.Add(new KeyInfoAsInteger("Numpad8", 104, "0x68", 1104, 2104));
        KeysList.Add(new KeyInfoAsInteger("Numpad9", 105, "0x69", 1105, 2105));
        KeysList.Add(new KeyInfoAsInteger("NP0", 96, "0x60", 1096, 2096));
        KeysList.Add(new KeyInfoAsInteger("NP1", 97, "0x61", 1097, 2097));
        KeysList.Add(new KeyInfoAsInteger("NP2", 98, "0x62", 1098, 2098));
        KeysList.Add(new KeyInfoAsInteger("NP3", 99, "0x63", 1099, 2099));
        KeysList.Add(new KeyInfoAsInteger("NP4", 100, "0x64", 1100, 2100));
        KeysList.Add(new KeyInfoAsInteger("NP5", 101, "0x65", 1101, 2101));
        KeysList.Add(new KeyInfoAsInteger("NP6", 102, "0x66", 1102, 2102));
        KeysList.Add(new KeyInfoAsInteger("NP7", 103, "0x67", 1103, 2103));
        KeysList.Add(new KeyInfoAsInteger("NP8", 104, "0x68", 1104, 2104));
        KeysList.Add(new KeyInfoAsInteger("NP9", 105, "0x69", 1105, 2105));
        KeysList.Add(new KeyInfoAsInteger("Multiply", 106, "0x6A", 1106, 2106));
        KeysList.Add(new KeyInfoAsInteger("Add", 107, "0x6B", 1107, 2107));
        KeysList.Add(new KeyInfoAsInteger("Separator", 108, "0x6C", 1108, 2108));
        KeysList.Add(new KeyInfoAsInteger("Subtract", 109, "0x6D", 1109, 2109));
        KeysList.Add(new KeyInfoAsInteger("Decimal", 110, "0x6E", 1110, 2110));
        KeysList.Add(new KeyInfoAsInteger("Divide", 111, "0x6F", 1111, 2111));
        KeysList.Add(new KeyInfoAsInteger("*", 106, "0x6A", 1106, 2106));
        KeysList.Add(new KeyInfoAsInteger("+", 107, "0x6B", 1107, 2107));
        KeysList.Add(new KeyInfoAsInteger("-", 109, "0x6D", 1109, 2109));
        KeysList.Add(new KeyInfoAsInteger("/", 111, "0x6F", 1111, 2111));
        KeysList.Add(  new KeyInfoAsInteger("F1", 112, "0x70", 1112, 2112                            ));
       KeysList.Add(  new KeyInfoAsInteger("F2", 113, "0x71", 1113, 2113                            ));
       KeysList.Add(  new KeyInfoAsInteger("F3", 114, "0x72", 1114, 2114                            ));
       KeysList.Add(  new KeyInfoAsInteger("F4", 115, "0x73", 1115, 2115                            ));
       KeysList.Add(  new KeyInfoAsInteger("F5", 116, "0x74", 1116, 2116                            ));
       KeysList.Add(  new KeyInfoAsInteger("F6", 117, "0x75", 1117, 2117                            ));
       KeysList.Add(  new KeyInfoAsInteger("F7", 118, "0x76", 1118, 2118                            ));
       KeysList.Add(  new KeyInfoAsInteger("F8", 119, "0x77", 1119, 2119                            ));
       KeysList.Add(  new KeyInfoAsInteger("F9", 120, "0x78", 1120, 2120                            ));
       KeysList.Add(   new KeyInfoAsInteger("F10", 121, "0x79", 1121, 2121                            ));
       KeysList.Add(   new KeyInfoAsInteger("F11", 122, "0x7A", 1122, 2122                            ));
       KeysList.Add(   new KeyInfoAsInteger("F12", 123, "0x7B", 1123, 2123                            ));
       KeysList.Add(   new KeyInfoAsInteger("F13", 124, "0x7C", 1124, 2124                            ));
       KeysList.Add(   new KeyInfoAsInteger("F14", 125, "0x7D", 1125, 2125                            ));
       KeysList.Add(   new KeyInfoAsInteger("F15", 126, "0x7E", 1126, 2126                            ));
       KeysList.Add(   new KeyInfoAsInteger("F16", 127, "0x7F", 1127, 2127                            ));
       KeysList.Add(   new KeyInfoAsInteger("F17", 128, "0x80", 1128, 2128                            ));
       KeysList.Add(   new KeyInfoAsInteger("F18", 129, "0x81", 1129, 2129                            ));
       KeysList.Add(   new KeyInfoAsInteger("F19", 130, "0x82", 1130, 2130                            ));
       KeysList.Add(   new KeyInfoAsInteger("F20", 131, "0x83", 1131, 2131                            ));
       KeysList.Add(   new KeyInfoAsInteger("F21", 132, "0x84", 1132, 2132                            ));
       KeysList.Add(   new KeyInfoAsInteger("F22", 133, "0x85", 1133, 2133                            ));
       KeysList.Add(   new KeyInfoAsInteger("F23", 134, "0x86", 1134, 2134                            ));
       KeysList.Add(   new KeyInfoAsInteger("F24", 135, "0x87", 1135, 2135                            ));
       KeysList.Add(       new KeyInfoAsInteger("NumLock", 144, "0x90", 1144, 2144                            ));
       KeysList.Add(          new KeyInfoAsInteger("ScrollLock", 145, "0x91", 1145, 2145                            ));
        KeysList.Add(new KeyInfoAsInteger("LeftShift", 160, "0xA0", 1160, 2160));
        KeysList.Add(new KeyInfoAsInteger("Shift", 160, "0xA0", 1160, 2160));
        KeysList.Add(          new KeyInfoAsInteger("RightShift", 161, "0xA1", 1161, 2161                            ));
        KeysList.Add(new KeyInfoAsInteger("LeftCtrl", 162, "0xA2", 1162, 2162));
        KeysList.Add(new KeyInfoAsInteger("Ctrl", 162, "0xA2", 1162, 2162));
        KeysList.Add(         new KeyInfoAsInteger("RightCtrl", 163, "0xA3", 1163, 2163                            ));
        KeysList.Add(new KeyInfoAsInteger("LeftMenu", 164, "0xA4", 1164, 2164));
        KeysList.Add(new KeyInfoAsInteger("Menu", 164, "0xA4", 1164, 2164));
        KeysList.Add(         new KeyInfoAsInteger("RightMenu", 165, "0xA5", 1165, 2165                            ));
       KeysList.Add(           new KeyInfoAsInteger("BrowserBack", 166, "0xA6", 1166, 2166                            ));
       KeysList.Add(              new KeyInfoAsInteger("BrowserForward", 167, "0xA7", 1167, 2167                            ));
       KeysList.Add(              new KeyInfoAsInteger("BrowserRefresh", 168, "0xA8", 1168, 2168                            ));
       KeysList.Add(           new KeyInfoAsInteger("BrowserStop", 169, "0xA9", 1169, 2169                            ));
       KeysList.Add(             new KeyInfoAsInteger("BrowserSearch", 170, "0xAA", 1170, 2170                            ));
       KeysList.Add(                new KeyInfoAsInteger("BrowserFavorites", 171, "0xAB", 1171, 2171                            ));
       KeysList.Add(           new KeyInfoAsInteger("BrowserHome", 172, "0xAC", 1172, 2172                            ));
       KeysList.Add(          new KeyInfoAsInteger("VolumeMute", 173, "0xAD", 1173, 2173                            ));
       KeysList.Add(          new KeyInfoAsInteger("VolumeDown", 174, "0xAE", 1174, 2174                            ));
       KeysList.Add(        new KeyInfoAsInteger("VolumeUp", 175, "0xAF", 1175, 2175                            ));
       KeysList.Add(              new KeyInfoAsInteger("MediaNextTrack", 176, "0xB0", 1176, 2176                            ));
       KeysList.Add(              new KeyInfoAsInteger("MediaPrevTrack", 177, "0xB1", 1177, 2177                            ));
       KeysList.Add(         new KeyInfoAsInteger("MediaStop", 178, "0xB2", 1178, 2178                            ));
       KeysList.Add(              new KeyInfoAsInteger("MediaPlayPause", 179, "0xB3", 1179, 2179                            ));
       KeysList.Add(          new KeyInfoAsInteger("LaunchMail", 180, "0xB4", 1180, 2180                            ));
       KeysList.Add(                 new KeyInfoAsInteger("LaunchMediaSelect", 181, "0xB5", 1181, 2181                            ));
       KeysList.Add(          new KeyInfoAsInteger("LaunchApp1", 182, "0xB6", 1182, 2182                            ));
       KeysList.Add(new KeyInfoAsInteger("LaunchApp2", 183, "0xB7", 1183, 2183));


        foreach (var key in KeysList)
        {
          
            AddKeyInfo(key);

        }
       
    }

    public void AddKeyInfo(KeyInfoAsInteger key)
    {
        if (m_keyMapRelease.ContainsKey(key.m_releaseAsInteger))
            m_keyMapRelease[key.m_releaseAsInteger] = key;
        else m_keyMapRelease.Add(key.m_releaseAsInteger, key);


        if (m_keyMapPress.ContainsKey(key.m_pressAsInteger))
            m_keyMapPress[key.m_pressAsInteger] = key;
        else m_keyMapPress.Add(key.m_pressAsInteger, key);

        if (m_keyMapIntId.ContainsKey(key.m_windowIntegerId255))
            m_keyMapIntId[key.m_windowIntegerId255] = key;
        else m_keyMapIntId.Add(key.m_windowIntegerId255, key);

        key.m_windowHexadecimal = key.m_windowHexadecimal.ToLower().Trim();
        if (m_keyMapHexadecimal.ContainsKey(key.m_windowHexadecimal))
            m_keyMapHexadecimal[key.m_windowHexadecimal] = key;
        else m_keyMapHexadecimal.Add(key.m_windowHexadecimal, key);


        key.m_windowKeyNameLower = key.m_windowKeyNameLower.ToLower().Trim();
        if (m_keyMapName.ContainsKey(key.m_windowKeyNameLower))
            m_keyMapName[key.m_windowKeyNameLower] = key;
        else m_keyMapName.Add(key.m_windowKeyNameLower, key);


        if(m_pressIntegerArray==null || m_pressIntegerArray.Length!= 256)
        {
            m_pressIntegerArray = new int[256];
        }
        if(m_releaseIntegerArray == null || m_releaseIntegerArray.Length!= 256)
        {
            m_releaseIntegerArray = new int[256];
        }

        m_pressIntegerArray[key.m_windowIntegerId255]= key.m_pressAsInteger;
        m_releaseIntegerArray[key.m_windowIntegerId255]= key.m_releaseAsInteger;
    }
}
