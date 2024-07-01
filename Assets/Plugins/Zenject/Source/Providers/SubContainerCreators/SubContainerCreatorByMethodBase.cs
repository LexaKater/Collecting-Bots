using System;
using System.Collections.Generic;

namespace Zenject
{
    [NoReflectionBaking]
    public abstract class SubContainerCreatorByMethodBase : ISubContainerCreator
    {
        readonly DiContainer _container;
        readonly SubContainerCreatorBindInfo _containerBindInfo;

        public SubContainerCreatorByMethodBase(
            DiContainer container, SubContainerCreatorBindInfo containerBindInfo)
        {
            _container = container;
            _containerBindInfo = containerBindInfo;
        }

        public abstract DiContainer CreateSubContainer(
            List<TypeValuePair> args, InjectContext context, out Action injectAction);

        protected DiContainer CreateEmptySubContainer()
        {
            var subContainer = _container.CreateSubContainer();
            SubContainerCreatorUtil.ApplyBindSettings(_containerBindInfo, subContainer);
            return subContainer;
        }
    }
}