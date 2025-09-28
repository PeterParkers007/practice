using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static class PlayerEvent
    {
        public delegate void PlayerDeathEventHandler();
        public static event PlayerDeathEventHandler OnPlayerDeathEvent;

        public static void TriggerPlayerDeath()
        {
            OnPlayerDeathEvent?.Invoke();
        }

        public delegate void PlayerSpawnEventHandler();
        public static event PlayerSpawnEventHandler OnPlayerSpawnEvent;

        public static void TriggerPlayerSpawn()
        {
            OnPlayerSpawnEvent?.Invoke();
        }

        public delegate void PlayerAttackEventHandler();
        public static event PlayerAttackEventHandler OnPlayerAttackEvent;

        public static void TriggerPlayerAttack()
        {
            OnPlayerAttackEvent?.Invoke();
        }
    }
}
