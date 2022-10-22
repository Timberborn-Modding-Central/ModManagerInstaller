namespace Timbermod_installer.GamePathFinder
{
    public interface IGamePlatform
    {
        public string Name { get; }

        public bool TryGetGamePath(out string gamePath);
    }
}