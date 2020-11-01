using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MainGameData : ScriptableObject {
    public readonly HashSet<GameTarget> hitTargets = new HashSet<GameTarget>();
}
