using System;
using System.Configuration;
using System.Data.Entity;
using CrossCutting.Core.IOC;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.Logging;
using Microsoft.Practices.Unity;
using UPictures.Data;
using UPictures.Application;

namespace CrossCutting.MainModule.IOC
{
    public class IocUnityContainer : IContainer
    {
        private static UnityContainer _unityContainer;

        public IocUnityContainer() : this(new UnityContainer())
        {}

        public IocUnityContainer(UnityContainer container)
        {
            _unityContainer = container;
            RegisterTypes();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public static void RegisterTypes()
        {
            bool realContainer = true;
            if (ConfigurationManager.AppSettings["IocRealContainer"] != null)
            {
                if (bool.TryParse(ConfigurationManager.AppSettings["IocRealContainer"], out realContainer) == false)
                {
                    realContainer = true;
                }
            }

            if (realContainer)
            {
                RegisterRealTypes();
            }
        }

        private static void RegisterRealTypes()
        {
            _unityContainer.RegisterType<ILogService, FileLogService>();

            _unityContainer.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            _unityContainer.RegisterType<DbContext, UPicturesEntities>(new HierarchicalLifetimeManager());

            _unityContainer.RegisterType<IAlbumRepository, AlbumRepository>();
            _unityContainer.RegisterType<IPictureRepository, PictureRepository>();
            _unityContainer.RegisterType<IDownloadRepository, DownloadRepository>();

            _unityContainer.RegisterType<IPictureService, PictureService>();
            _unityContainer.RegisterType<IAlbumService, AlbumService>();
            _unityContainer.RegisterType<IPictureService, PictureService>();
            _unityContainer.RegisterType<IDownloadService, DownloadService>();
        }
    }
}