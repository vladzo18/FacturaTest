using UnityEngine;

namespace FacturaGame.Scripts.Utils.Assets
{
    public interface IAssetProvider
    {
        public T GetAsset<T>(string path) where T : UnityEngine.Object;
    }
}