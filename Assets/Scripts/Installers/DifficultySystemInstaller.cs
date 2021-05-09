using Data;
using Difficulty;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DifficultySystemInstaller : MonoInstaller
    {
        [SerializeField] private DifficultySettings _Settings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo(typeof(DifficultySystem)).AsSingle().WithArguments(_Settings);
        }
    }
}