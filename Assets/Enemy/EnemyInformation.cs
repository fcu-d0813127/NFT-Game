using UnityEngine;

[System.Serializable]
public class EnemyInformation{
    internal static int _flyingEye;
    internal static int _goblin;
    internal static int _mushroom;
    internal static int _skeleton;
    internal static int _bringerOfDeath;
    internal static void InitEnemyInformation(){
        _flyingEye = 0;
        _goblin = 0;
        _mushroom = 0;
        _skeleton = 0;
        _bringerOfDeath = 0;
    }
    internal static void SetFlyingEye(){
        _flyingEye+=1;
    }
    internal static int GetFlyingEye(){
        return _flyingEye;
    }
    internal static void SetGoblin(){
        _goblin+=1;
    }
    internal static int GetGoblin(){
        return _goblin;
    }
    internal static void SetMushroom(){
        _mushroom+=1;
    }
    internal static int GetMushroom(){
        return _mushroom;
    }
    internal static void SetSkeleton(){
        _skeleton+=1;
    }
    internal static int GetSkeleton(){
        return _skeleton;
    }
    internal static void SetBringerOfDeath(){
        _bringerOfDeath+=1;
    }
    internal static int GetBringerOfDeath(){
        return _bringerOfDeath;
    }
}
