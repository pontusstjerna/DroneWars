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
        private SoundEffectInstance test;

        public SoundController(World world, Dictionary<string, SoundEffect> sounds)
        {
            this.world = world;

            this.sounds = new Dictionary<string, SoundEffectInstance>();
            foreach(KeyValuePair<string, SoundEffect> sound in sounds)
            {
                SoundEffectInstance sei = sound.Value.CreateInstance();
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
                    SoundEffectInstance flyingSound = sounds["flying"];
                    flyingSound.Pitch = Math.Min(Math.Max((drone.Vel.Length() * 0.1f), 1), -1);
                    flyingSound.Pan = (drone.Pos.X - World.WIDTH / 2) / World.WIDTH;
                    flyingSound.Play();
                }
                    
            }
        }
    }
}
