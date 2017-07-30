using DroneWars.Model;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.Controller
{
    public class SoundController
    {
        private Dictionary<string, SoundEffectInstance> sounds;
        private World world;

        public SoundController(World world, Dictionary<string, SoundEffect> sounds)
        {
            this.world = world;

            sounds = new Dictionary<string, SoundEffect>();
            foreach(var sound in sounds)
            {
                var sei = sound.Value.CreateInstance();
                sei.IsLooped = true;

                this.sounds.Add(sound.Key, sei);
            }
        }

        public void Update()
        {
            PlayDroneSounds();
        }

        private void PlayDroneSounds()
        {
            foreach(Drone drone in world.Drones)
            {
                if (!drone.OnGround)
                {
                    var flyingSound = sounds["flying"];
                    flyingSound.Pitch = 1 + drone.Vel.Length() * 0.01f;
                    flyingSound.Pan = (drone.Pos.X - World.WIDTH / 2) / World.WIDTH;
                    flyingSound.Play();
                }
                    
            }
        }
    }
}
