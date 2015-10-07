using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace Ghostpunch.OnlyDown.Game.Commands
{
    public class SetupLevelCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView { get; set; }

        public override void Execute()
        {
            if (injectionBinder.GetBinding<GameObject>(GameElement.Environment) == null)
            {
                var prototype = Resources.Load<GameObject>("Environment/Environment");
                var gameField = GameObject.Instantiate<GameObject>(prototype);

                gameField.transform.localPosition = Vector3.zero;
                gameField.transform.parent = ContextView.transform;

                // Bind it so we can use it elsewhere
                injectionBinder.Bind<GameObject>().ToValue(gameField).ToName(GameElement.Environment);
            }

            if (injectionBinder.GetBinding<GameObject>(GameElement.Player) == null)
            {
                var prototype = Resources.Load<GameObject>(GameElement.Player.ToString());
                var playerGO = GameObject.Instantiate<GameObject>(prototype);

                playerGO.transform.localPosition = Vector3.up * 5.5f;
                playerGO.transform.parent = ContextView.transform;

                injectionBinder.Bind<GameObject>().ToValue(playerGO).ToName(GameElement.Player);
            }
        }
    }
}
