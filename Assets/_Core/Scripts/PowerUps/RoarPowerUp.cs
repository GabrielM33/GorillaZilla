using System.Collections;
using System.Collections.Generic;
using GorillaZilla;
using UnityEngine;

namespace GorillaZilla
{
    public class RoarPowerUp : PowerUp
    {
        public override void Activate()
        {
            base.Activate();
            Player player = GameObject.FindObjectOfType<Player>();
            player.ActivateAbility("SoundBlaster");
        }
        public override void Deactivate()
        {
            base.Deactivate();
            // Player player = GameObject.FindObjectOfType<Player>();
            // player.DeactavateAbility();
        }
    }
}

