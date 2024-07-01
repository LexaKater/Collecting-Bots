using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject
{
    [NoReflectionBaking]
    public class SubContainerCreatorByNewGameObjectInstaller : SubContainerCreatorByNewGameObjectDynamicContext
    {
        readonly Type _installerType;
        readonly List<TypeValuePair> _extraArgs;

        public SubContainerCreatorByNewGameObjectInstaller(
            DiContainer container,
            GameObjectCreationParameters gameObjectBindInfo,
            Type installerType, List<TypeValuePair> extraArgs)
            : base(container, gameObjectBindInfo)
        {
            _installerType = installerType;
            _extraArgs = extraArgs;

            Assert.That(installerType.DerivesFrom<InstallerBase>(),
                "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
        }

        protected override void AddInstallers(List<TypeValuePair> args, GameObjectContext context)
        {
            context.AddNormalInstaller(
                new ActionInstaller(subContainer =>
                {
                    var extraArgs = ZenPools.SpawnList<TypeValuePair>();

                    extraArgs.AllocFreeAddRange(_extraArgs);
                    extraArgs.AllocFreeAddRange(args);

                    var installer = (InstallerBase)subContainer.InstantiateExplicit(
                        _installerType, extraArgs);

                    ZenPools.DespawnList(extraArgs);

                    installer.InstallBindings();
                }));
        }
    }
}