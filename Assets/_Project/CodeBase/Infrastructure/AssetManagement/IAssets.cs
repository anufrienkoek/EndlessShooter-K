using _Project.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}