using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Exceptions 
{
    public class GameJam_Exception : Exception 
    {
        public GameJam_Exception(string message):base(message) 
        { Debug.LogError(message); }
        public static GameJam_Exception GetNullModuleInitialization(string moduleName) =>
            new GameJam_Exception(moduleName + " doesn't exists.");
        public static GameJam_Exception GetWrondModuleType<T>(string moduleName) =>
            new GameJam_Exception($"Wrong type of {moduleName}. Module must be type {typeof(T).Name}");
    }
}
