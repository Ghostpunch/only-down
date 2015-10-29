using UnityEngine;
using System.Collections;
using Zenject;
using System;
using Ghostpunch.OnlyDown.Messaging;

namespace Ghostpunch.OnlyDown
{
    /// <summary>
    /// This installer binds anything that needs to be shared across scenes.
    /// e.g. Player data, Messaging system, etc.
    /// </summary>
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // There should only be one messaging system and it should
            // be injected into any active scene
            Container.Bind<IMessageSystem>().ToSingle<MessageSystem>();

            // There should only be one AudioListener
            Container.InstantiateComponentOnNewGameObject<AudioListener>("Audio Object");
        }
    }
}
