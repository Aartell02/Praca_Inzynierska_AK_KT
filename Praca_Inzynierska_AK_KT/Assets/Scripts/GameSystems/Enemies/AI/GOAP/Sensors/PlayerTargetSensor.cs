using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
public class PlayerTargetSensor : LocalTargetSensorBase
{
    public override void Created() { }
    public override void Update() { }

    public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return null;

        Transform playerTransform = player.transform;
        // Jeśli mamy istniejący TransformTarget, wykorzystaj go (cel się dynamicznie aktualizuje)
        if (existingTarget is TransformTarget transformTarget)
        {
            return transformTarget.SetTransform(playerTransform);
        }
        // W przeciwnym wypadku stwórz nowy cel docelowy
        return new TransformTarget(playerTransform);
    }
}
}
