using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class SubContainerCreatorByNewGameObjectMethod : SubContainerCreatorByNewGameObjectDynamicContext
    {
        readonly Action<DiContainer> _installerMethod;

        public SubContainerCreatorByNewGameObjectMethod(
            DiContainer container,
            GameObjectCreationParameters gameObjectBindInfo,
            Action<DiContainer> installerMethod)
            : base(container, gameObjectBindInfo)
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