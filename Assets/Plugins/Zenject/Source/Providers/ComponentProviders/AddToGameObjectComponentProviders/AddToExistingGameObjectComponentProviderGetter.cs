using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject
{
    [NoReflectionBaking]
    public class AddToExistingGameObjectComponentProviderGetter : AddToGameObjectComponentProviderBase
    {
        readonly Func<InjectContext, GameObject> _gameObjectGetter;

        public AddToExistingGameObjectComponentProviderGetter(
            Func<InjectContext, GameObject> gameObjectGetter, DiContainer container, Type componentType,
            List<TypeValuePair> extraArguments, object concreteIdentifier,
            Action<InjectContext, object> instantiateCallback)
            : base(container, componentType, extraArguments, concreteIdentifier, instantiateCallback)
        {
            _gameObjectGetter = gameObjectGetter;
        }

        // This will cause [Inject] to be triggered after awake / start
        // We could return true, but what if toggling active has other negative repercussions?
        // For now let's just not do anything
        protected override bool ShouldToggleActive
        {
            get { return false; }
        }

        protected override GameObject GetGameObject(InjectContext context)
        {
            var gameObj = _gameObjectGetter(context);
            Assert.IsNotNull(gameObj, "Provided Func<InjectContext, GameObject> returned null value for game object when using FromComponentOn");
            return gameObj;
        }
    }
}