using System.Collections.Generic;

namespace FacturaGame.Scripts.Gameplay.Services.StoperService
{
    public class SystemsStopService
    {
        private readonly List<IStopable> _stopable = new ();

        public void AddStopable(IStopable stopable)
        {
            _stopable.Add(stopable);
        }

        public void RemoveStopable(IStopable stopable)
        {
            _stopable.Remove(stopable);
        }

        public void StopStopables()
        {
            foreach (var stopable in _stopable)
            {
                stopable.OnStop();
            }
        }
    }
}