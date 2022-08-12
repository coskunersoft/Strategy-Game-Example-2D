using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.Services
{
    public class ServicePack
    {
        public List<IService> Allservices;
        public ServicePack(ServiceDataProvider serviceDataProvider)
        {
            Allservices = new List<IService>();

            userProfile = new UserProfile();

            Allservices.Add(userProfile);

            foreach (var item in Allservices)
            {
                item.Initialize(serviceDataProvider);
            }
        }
        public UserProfile userProfile;
    }
}

