public static class AutoFacService 
{
  private static IContainer _container;

  public static void Configure () {
    var builder = new ContainerBuilder();

    // Get your HttpConfiguration.
    var config = GlobalConfiguration.Configuration;

    // Register your Web API controllers.
    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

    // OPTIONAL: Register the Autofac filter provider.
    builder.RegisterWebApiFilterProvider(config);

    // OPTIONAL: Register the Autofac model binder provider.
    builder.RegisterWebApiModelBinderProvider();

    builder.RegisterModule(new AutoMapperModule());
    builder.RegisterType<WcfEmailClient>().As<IWcfEmail>().SingleInstance();

    // Set the dependency resolver to be Autofac.
    _container = builder.Build();
    config.DependencyResolver = 
      new AutofacWebApiDependencyResolver(_container);
  }
}