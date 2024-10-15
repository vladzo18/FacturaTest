using UnityEngine;

namespace FacturaGame.Scripts.Utils.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public T GetAsset<T>(string path) where T : UnityEngine.Object
        {
            T asset = Resources.Load<T>(path);
            return asset;
        }
    }
}