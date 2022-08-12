using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.Services
{
    public abstract class IService
    {
        public ServiceDataProvider serviceDataProvider;
        public void Initialize(ServiceDataProvider serviceDataProvider) => this.serviceDataProvider = serviceDataProvider;
    }

}
