using App.Internal;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes.Internal
{
    public class GameLoop : MonoBehaviour
    {
        public static bool running = true;
        List<Hero> heroes;

        // Game loop methods
        public void Enter()
        {
            // Loader code
            FSLoader.Load();

            // Create a copy of the list of heroes
            heroes = new List<Hero>(HeroManager.heroes);

            // // Update the game loop
            // while (running)
            // {

            // }
        }
        // Restart the game loop
        public void Restart()
        {
            // Reset the game loop
            running = true;

            // Destroy all heroes
            HeroManager.WorldDestroyAllHeroes();

            // Restart the game loop
            Enter();
        }

        public void DestroyObj()
        {
            // Invoke the destroy method for each hero
            foreach (Hero hero in HeroManager.heroes)
            {
                hero.OnDestroy();
            }
        }
        public void Update()
        {
            // Invoke the update method for each hero
            foreach (Hero hero in heroes)
            {
                hero.OnUpdate(); // Update the hero

                if (!running) break; // Quit

                // Rebuild the list of heroes
                if (HeroManager.RebuildList)
                {
                    // Rebuild the copied list of heroes
                    heroes = new List<Hero>(HeroManager.heroes);

                    // Reset the rebuild list
                    HeroManager.RebuildList = false;
                }
            }
        }
    }
}