using Timbermod_installer.GamePathFinder.Platforms;
using Timbermod_installer.LogSystem;

namespace Timbermod_installer.GamePathFinder
{
    public class GamePathFinderService
    {
        private static readonly IGamePlatform[] GamePlatforms = {
            new Steam()
        };

        public bool TryGetGamePath(out string gamePath)
        {
            LogService.Send("Searching for game path...");
            gamePath = "";

            foreach (IGamePlatform gamePlatform in GamePlatforms)
            {
                if(!gamePlatform.TryGetGamePath(out gamePath))
                {
                    continue;
                }

                LogService.Send($"[GameFinder: {gamePlatform.Name}] Found game path!");
                return true;
            }

            LogService.Send("Failed to find game path");
            return false;
        }
    }
}