using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class SubContainerCreatorByNewPrefabMethod : SubContainerCreatorByNewPrefabDynamicContext
    {
        readonly Action<DiContainer> _installerMethod;

        public SubContainerCreatorByNewPrefabMethod(
            DiContainer container, IPrefabProvider prefabProvider,
            GameObjectCreationParameters gameObjectBindInfo,
            Action<DiContainer> installerMethod)
            : base(container, prefabProvider, gameObjectBindInfo)
        {
            _installerMethod = installerMethod;
        }

        protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
        {
            Assert.That(args.IsEmpty());
            context.AddNormalInstaller(
                new ActionInstaller(_installerMethod));
        }
    }
}