// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.DotNet.RemoteExecutor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Fakes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Microsoft.Extensions.Hosting.Tests
{
    public class HostBuilderTests
    {
        [Fact]
        public void DefaultConfigIsMutable()
        {
            var host = new HostBuilder()
                .Build();

            using (host)
            {
                var config = host.Services.GetRequiredService<IConfiguration>();
                config["key1"] = "value";
                Assert.Equal("value", config["key1"]);
            }
        }

        [ConditionalFact(typeof(RemoteExecutor), nameof(RemoteExecutor.IsSupported))]
        public void BuildFiresEvents()
        {
            using var _ = RemoteExecutor.Invoke(() =>
            {
                IHostBuilder hostBuilderFromEvent = null;
                IHost hostFromEvent = null;

                var listener = new HostingListener((pair) =>
                {
                    if (pair.Key == "HostBuilding")
                    {
                        hostBuilderFromEvent = (IHostBuilder)pair.Value;
                    }

                    if (pair.Key == "HostBuilt")
                    {
                        hostFromEvent = (IHost)pair.Value;
                    }
                });

                using var sub = DiagnosticListener.AllListeners.Subscribe(listener);

                var hostBuilder = new HostBuilder();
                var host = hostBuilder.Build();

                Assert.Same(hostBuilder, hostBuilderFromEvent);
                Assert.Same(host, hostFromEvent);
            });
        }

        [Fact]
        public void ConfigureHostConfigurationPropagated()
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("key1", "value1")
                    });
                })
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("key2", "value2")
                    });
                })
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddInMemoryCollection(new[]
                    {
                        // Hides value2
                        new KeyValuePair<string, string>("key2", "value3")
                    });
                })
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    Assert.Equal("value1", context.Configuration["key1"]);
                    Assert.Equal("value3", context.Configuration["key2"]);
                    var config = configBuilder.Build();
                    Assert.Equal("value1", config["key1"]);
                    Assert.Equal("value3", config["key2"]);
                })
                .Build();

            using (host)
            {
                var config = host.Services.GetRequiredService<IConfiguration>();
                Assert.Equal("value1", config["key1"]);
                Assert.Equal("value3", config["key2"]);
            }
        }

        [Fact]
        public void CanConfigureAppConfigurationAndRetrieveFromDI()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                            new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("key1", "value1")
                            });
                })
                .ConfigureAppConfiguration((configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                            new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("key2", "value2")
                            });
                })
                .ConfigureAppConfiguration((configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                            new KeyValuePair<string, string>[]
                            {
                                // Hides value2
                                new KeyValuePair<string, string>("key2", "value3")
                            });
                });

            using (var host = hostBuilder.Build())
            {
                var config = host.Services.GetService<IConfiguration>();
                Assert.NotNull(config);
                Assert.Equal("value1", config["key1"]);
                Assert.Equal("value3", config["key2"]);
            }
        }

        [Fact]
        public void CanConfigureAppConfigurationFromFile()
        {
            var hostBuilder = new HostBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.AddJsonFile("appSettings.json", optional: false);
                });

            using (var host = hostBuilder.Build())
            {
                var config = host.Services.GetService<IConfiguration>();
                Assert.NotNull(config);
                Assert.Equal("value", config["key"]);
            }
        }

        [Fact]
        public void DefaultIHostEnvironmentValues()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, appConfig) =>
                {
                    var env = hostContext.HostingEnvironment;
                    Assert.Equal(Environments.Production, env.EnvironmentName);
#if NET
                    Assert.NotNull(env.ApplicationName);
#elif NETFRAMEWORK
                    // Note GetEntryAssembly returns null for the net4x console test runner.
                    Assert.Equal(string.Empty, env.ApplicationName);
#else
#error TFMs need to be updated
#endif
                    Assert.Equal(AppContext.BaseDirectory, env.ContentRootPath);
                    Assert.IsAssignableFrom<PhysicalFileProvider>(env.ContentRootFileProvider);
                });

            using (var host = hostBuilder.Build())
            {
                var env = host.Services.GetRequiredService<IHostEnvironment>();
                Assert.Equal(Environments.Production, env.EnvironmentName);
#if NET
                Assert.NotNull(env.ApplicationName);
#elif NETFRAMEWORK
                // Note GetEntryAssembly returns null for the net4x console test runner.
                Assert.Equal(string.Empty, env.ApplicationName);
#else
#error TFMs need to be updated
#endif
                Assert.Equal(AppContext.BaseDirectory, env.ContentRootPath);
                Assert.IsAssignableFrom<PhysicalFileProvider>(env.ContentRootFileProvider);
            }
        }

        [Fact]
        public void ConfigBasedSettingsConfigBasedOverride()
        {
            var settings = new Dictionary<string, string>
            {
                { HostDefaults.EnvironmentKey, "EnvA" }
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            var overrideSettings = new Dictionary<string, string>
            {
                { HostDefaults.EnvironmentKey, "EnvB" }
            };

            var overrideConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(overrideSettings)
                .Build();

            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder => configBuilder.AddConfiguration(config))
                .ConfigureHostConfiguration(configBuilder => configBuilder.AddConfiguration(overrideConfig));

            using (var host = hostBuilder.Build())
            {
                Assert.Equal("EnvB", host.Services.GetRequiredService<IHostEnvironment>().EnvironmentName);
            }
        }

        [Fact]
        public void UseEnvironmentIsNotOverridden()
        {
            var vals = new Dictionary<string, string>
            {
                { "ENV", "Dev" },
            };
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(vals);
            var config = builder.Build();

            var expected = "MY_TEST_ENVIRONMENT";

            using (var host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder => configBuilder.AddConfiguration(config))
                .UseEnvironment(expected)
                .Build())
            {
                Assert.Equal(expected, host.Services.GetService<IHostEnvironment>().EnvironmentName);
            }
        }

        [Fact]
        public void BuildAndDispose()
        {
            using (var host = new HostBuilder()
                .Build()) { }
        }

        [Fact]
        public void UseBasePathConfiguresBasePath()
        {
            var vals = new Dictionary<string, string>
            {
                { "ENV", "Dev" },
            };
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(vals);
            var config = builder.Build();

            using (var host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder => configBuilder.AddConfiguration(config))
                .UseContentRoot("/")
                .Build())
            {
                Assert.Equal("/", host.Services.GetService<IHostEnvironment>().ContentRootPath);
            }
        }

        [Fact]
        public void HostConfigParametersReadCorrectly()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "applicationName", "MyProjectReference" },
                { "environment", Environments.Development },
                { "contentRoot", Path.GetFullPath(".") }
            };

            using (var host = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(parameters);
                }).Build())
            {
                var env = host.Services.GetRequiredService<IHostEnvironment>();

                Assert.Equal("MyProjectReference", env.ApplicationName);
                Assert.Equal(Environments.Development, env.EnvironmentName);
                Assert.Equal(Path.GetFullPath("."), env.ContentRootPath);
            }
        }

        [Fact]
        public void RelativeContentRootIsResolved()
        {
            using (var host = new HostBuilder()
                .UseContentRoot("testroot")
                .Build())
            {
                var basePath = host.Services.GetRequiredService<IHostEnvironment>().ContentRootPath;
                Assert.True(Path.IsPathRooted(basePath));
                Assert.EndsWith(Path.DirectorySeparatorChar + "testroot", basePath);
            }
        }

        [Fact]
        public void DefaultContentRootIsApplicationBasePath()
        {
            using (var host = new HostBuilder()
                .Build())
            {
                var appBase = AppContext.BaseDirectory;
                Assert.Equal(appBase, host.Services.GetService<IHostEnvironment>().ContentRootPath);
            }
        }

        [Fact]
        public void DefaultServicesAreAvailable()
        {
            using (var host = new HostBuilder()
                .Build())
            {
#pragma warning disable CS0618 // Type or member is obsolete
                Assert.NotNull(host.Services.GetRequiredService<IHostingEnvironment>());
#pragma warning restore CS0618 // Type or member is obsolete
                Assert.NotNull(host.Services.GetRequiredService<IHostEnvironment>());
                Assert.NotNull(host.Services.GetRequiredService<IConfiguration>());
                Assert.NotNull(host.Services.GetRequiredService<HostBuilderContext>());
#pragma warning disable CS0618 // Type or member is obsolete
                Assert.NotNull(host.Services.GetRequiredService<IApplicationLifetime>());
#pragma warning restore CS0618 // Type or member is obsolete
                Assert.NotNull(host.Services.GetRequiredService<IHostApplicationLifetime>());
                Assert.NotNull(host.Services.GetRequiredService<ILoggerFactory>());
                Assert.NotNull(host.Services.GetRequiredService<IOptions<FakeOptions>>());
            }
        }

        [Fact]
        public void DefaultCreatesLoggerFactory()
        {
            var hostBuilder = new HostBuilder();

            using (var host = hostBuilder.Build())
            {
                Assert.NotNull(host.Services.GetService<ILoggerFactory>());
            }
        }

        public static IEnumerable<object[]> ConfigureHostOptionsTestInput = new[]
        {
            new object[] { BackgroundServiceExceptionBehavior.Ignore, TimeSpan.FromDays(3) },
            new object[] { BackgroundServiceExceptionBehavior.StopHost, TimeSpan.FromTicks(long.MaxValue) },
        };

        [Theory]
        [MemberData(nameof(ConfigureHostOptionsTestInput))]
        public void CanConfigureHostOptionsWithOptionsOverload(
            BackgroundServiceExceptionBehavior testBehavior, TimeSpan testShutdown)
        {
            using var host = new HostBuilder()
                .ConfigureDefaults(Array.Empty<string>())
                .ConfigureHostOptions(
                    options =>
                    {
                        options.BackgroundServiceExceptionBehavior = testBehavior;
                        options.ShutdownTimeout = testShutdown;
                    })
                .Build();

            var options = host.Services.GetRequiredService<IOptions<HostOptions>>();
            Assert.NotNull(options.Value);

            var hostOptions = options.Value;
            Assert.Equal(testBehavior, hostOptions.BackgroundServiceExceptionBehavior);
            Assert.Equal(testShutdown, hostOptions.ShutdownTimeout);
        }

        [Theory]
        [MemberData(nameof(ConfigureHostOptionsTestInput))]
        public void CanConfigureHostOptionsWithContenxtAndOptionsOverload(
            BackgroundServiceExceptionBehavior testBehavior, TimeSpan testShutdown)
        {
            using var host = new HostBuilder()
                .ConfigureDefaults(Array.Empty<string>())
                .ConfigureHostOptions(
                    (context, options) =>
                    {
                        context.HostingEnvironment.ApplicationName = "TestApp";
                        context.HostingEnvironment.EnvironmentName = Environments.Staging;

                        options.BackgroundServiceExceptionBehavior = testBehavior;
                        options.ShutdownTimeout = testShutdown;
                    })
                .Build();

            var options = host.Services.GetRequiredService<IOptions<HostOptions>>();
            Assert.NotNull(options.Value);

            var hostOptions = options.Value;
            Assert.Equal(testBehavior, hostOptions.BackgroundServiceExceptionBehavior);
            Assert.Equal(testShutdown, hostOptions.ShutdownTimeout);

            var env = host.Services.GetRequiredService<IHostEnvironment>();

            Assert.Equal("TestApp", env.ApplicationName);
            Assert.Equal(Environments.Staging, env.EnvironmentName);
        }

        [Fact]
        public void MultipleConfigureLoggingInvokedInOrder()
        {
            var callCount = 0; //Verify ordering
            var hostBuilder = new HostBuilder()
                .ConfigureLogging((hostContext, loggerFactory) =>
                {
                    Assert.Equal(0, callCount++);
                })
                .ConfigureLogging((hostContext, loggerFactory) =>
                {
                    Assert.Equal(1, callCount++);
                });

            using (hostBuilder.Build())
            {
                Assert.Equal(2, callCount);
            }
        }

        [Fact]
        public void HostingContextContainsAppConfigurationDuringConfigureServices()
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureAppConfiguration((configBuilder) =>
                    configBuilder.AddInMemoryCollection(
                        new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("key1", "value1")
                        }))
                 .ConfigureServices((context, factory) =>
                 {
                     Assert.Equal("value1", context.Configuration["key1"]);
                 });

            using (hostBuilder.Build()) { }
        }

        [Fact]
        public void ConfigureDefaultServiceProvider()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((s) =>
                {
                    s.AddTransient<ServiceD>();
                    s.AddScoped<ServiceC>();
                })
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>("Key", "Value"),
                    });
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    Assert.NotNull(context);
                    Assert.Equal("Value", context.Configuration["Key"]);
                    Assert.NotNull(options);
                    options.ValidateScopes = true;
                });
            using (var host = hostBuilder.Build())
            {
                Assert.Throws<InvalidOperationException>(() => { host.Services.GetRequiredService<ServiceC>(); });
            }
        }

        [Fact]
        public void ConfigureCustomServiceProvider()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((hostContext, s) =>
                {
                    s.AddTransient<ServiceD>();
                    s.AddScoped<ServiceC>();
                })
                .UseServiceProviderFactory(new FakeServiceProviderFactory())
                .ConfigureContainer<FakeServiceCollection>((container) =>
                {
                    Assert.Null(container.State);
                    container.State = "1";
                })
                .ConfigureContainer<FakeServiceCollection>((container) =>
                 {
                     Assert.Equal("1", container.State);
                     container.State = "2";
                 });
            using (var host = hostBuilder.Build())
            {
                var fakeServices = host.Services.GetRequiredService<FakeServiceCollection>();
                Assert.Equal("2", fakeServices.State);
            }
        }

        [Fact]
        public void CustomContainerTypeMismatchThrows()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((s) =>
                {
                    s.AddTransient<ServiceD>();
                    s.AddScoped<ServiceC>();
                })
                .UseServiceProviderFactory(new FakeServiceProviderFactory())
                .ConfigureContainer<IServiceCollection>((container) =>
                {
                });
            Assert.Throws<InvalidCastException>(() => hostBuilder.Build());
        }

        [Fact]
        public void ScopeValidationEnabledInDevelopment()
        {
            using var host = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureServices(serices =>
                {
                    serices.AddScoped<ServiceA>();
                })
                .Build();

            Assert.Throws<InvalidOperationException>(() => { host.Services.GetRequiredService<ServiceA>(); });
        }

        [Fact]
        public void ValidateOnBuildEnabledInDevelopment()
        {
            var hostBuilder = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureServices(serices =>
                {
                    serices.AddSingleton<ServiceC>();
                });

            Assert.Throws<AggregateException>(() => hostBuilder.Build());
        }

        [Fact]
        public void ScopeValidationNotEnabledInDevelopmentWithServiceProviderChanges()
        {
            using var host = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureServices(serices =>
                {
                    serices.AddScoped<ServiceA>();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = false;
                })
                .Build();

            Assert.NotNull(host.Services.GetRequiredService<ServiceA>());
        }
        [Fact]
        public void ScopeValidationtEnabledInDevelopmentWithServiceProviderChanges()
        {
            var host = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureServices(services =>
                {
                    services.AddScoped<ServiceA>();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = true;
                })
                .Build();

            Assert.Throws<InvalidOperationException>(() => host.Services.GetRequiredService<ServiceA>());
        }
        [Fact]
        public void ValidateOnBuildNotEnabledInDevelopmentWithServiceProviderChanges()
        {
            using var host = new HostBuilder()
                .UseEnvironment(Environments.Development)
                .ConfigureServices(serices =>
                {
                    serices.AddSingleton<ServiceC>();
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = false;
                })
                .Build();

            Assert.NotNull(host);
        }

        [Fact]
        public void HostingContextContainsAppConfigurationDuringConfigureLogging()
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureAppConfiguration((configBuilder) =>
                    configBuilder.AddInMemoryCollection(
                        new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("key1", "value1")
                        }))
                 .ConfigureLogging((context, factory) =>
                 {
                     Assert.Equal("value1", context.Configuration["key1"]);
                 });

            using (hostBuilder.Build()) { }
        }

        [Fact]
        public void ConfigureServices_CanBeCalledMultipleTimes()
        {
            var callCount = 0; // Verify ordering
            var hostBuilder = new HostBuilder()
                .ConfigureServices((services) =>
                {
                    Assert.Equal(0, callCount++);
                    services.AddTransient<ServiceA>();
                })
                .ConfigureServices((services) =>
                {
                    Assert.Equal(1, callCount++);
                    services.AddTransient<ServiceB>();
                });

            using (var host = hostBuilder.Build())
            {
                Assert.Equal(2, callCount);

                Assert.NotNull(host.Services.GetRequiredService<ServiceA>());
                Assert.NotNull(host.Services.GetRequiredService<ServiceB>());
            }
        }

        [Fact]
        public void Build_DoesNotAllowBuildingMuiltipleTimes()
        {
            var builder = new HostBuilder();
            using (builder.Build())
            {
                var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
                Assert.Equal("Build can only be called once.", ex.Message);
            }
        }

        [Fact]
        public void SetsFullPathToContentRoot()
        {
            using (var host = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ContentRootKey, Path.GetFullPath("."))
                    });
                })
                .Build())
            {
                var env = host.Services.GetRequiredService<IHostEnvironment>();

                Assert.Equal(Path.GetFullPath("."), env.ContentRootPath);
                Assert.IsAssignableFrom<PhysicalFileProvider>(env.ContentRootFileProvider);
            }
        }

        [Fact]
        public void BuilderPropertiesAreAvailableInBuilderAndContext()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    Assert.Equal("value", hostContext.Properties["key"]);
                });

            hostBuilder.Properties.Add("key", "value");

            Assert.Equal("value", hostBuilder.Properties["key"]);

            using (hostBuilder.Build()) { }
        }

        [Fact]
        public void DisposingHostDisposesContentFileProvider()
        {
            var host = new HostBuilder()
                .Build();

            var env = host.Services.GetRequiredService<IHostEnvironment>();
            var fileProvider = new FakeFileProvider();
            env.ContentRootFileProvider = fileProvider;

            host.Dispose();
            Assert.True(fileProvider.Disposed);
        }

        [Fact]
        public void HostServicesSameServiceProviderAsInHostBuilder()
        {
            var hostBuilder = Host.CreateDefaultBuilder();
            var host = hostBuilder.Build();

            // Use typeof so that trimming can see the field being used below
            var type = typeof(HostBuilder);
            Assert.Equal(hostBuilder.GetType(), type);
            var field = type.GetField("_appServices", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var appServicesFromHostBuilder = (IServiceProvider)field.GetValue(hostBuilder)!;
            Assert.Same(appServicesFromHostBuilder, host.Services);
        }

        [Fact]
        public void HostBuilderConfigureDefaultsInterleavesMissingConfigValues()
        {
            IHostBuilder hostBuilder = new HostBuilder();
            hostBuilder.ConfigureDefaults(args: null);

            using var host = hostBuilder.Build();
            var env = host.Services.GetRequiredService<IHostEnvironment>();

            var expectedContentRootPath = Directory.GetCurrentDirectory();
            Assert.Equal(expectedContentRootPath, env.ContentRootPath);
        }

        [Fact]
        public void HostBuilderConfigureDefaultsDoesntThrowInDevelopment()
        {
            using (var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, "MyProjectReference"),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, Environments.Development)
                    });
                })
                .Build())
            {
                var env = host.Services.GetRequiredService<IHostEnvironment>();

                Assert.Equal("MyProjectReference", env.ApplicationName);
                Assert.Equal(Environments.Development, env.EnvironmentName);
            }
        }

        [Theory]
        [InlineData(BackgroundServiceExceptionBehavior.Ignore)]
        [InlineData(BackgroundServiceExceptionBehavior.StopHost)]
        public void HostBuilderCanConfigureBackgroundServiceExceptionBehavior(
            BackgroundServiceExceptionBehavior testBehavior)
        {
            using IHost host = new HostBuilder()
                .ConfigureServices(
                    services =>
                        services.Configure<HostOptions>(
                            options =>
                            options.BackgroundServiceExceptionBehavior = testBehavior))
                .Build();

            var options = host.Services.GetRequiredService<IOptions<HostOptions>>();

            Assert.Equal(
                testBehavior,
                options.Value.BackgroundServiceExceptionBehavior);
        }

        private class HostingListener : IObserver<DiagnosticListener>, IObserver<KeyValuePair<string, object?>>
        {
            private IDisposable? _disposable;
            private readonly Action<KeyValuePair<string, object?>> _callback;

            public HostingListener(Action<KeyValuePair<string, object?>> callback)
            {
                _callback = callback;
            }

            public void OnCompleted() { _disposable?.Dispose(); }
            public void OnError(Exception error) { }
            public void OnNext(DiagnosticListener value)
            {
                if (value.Name == "Microsoft.Extensions.Hosting")
                {
                    _disposable = value.Subscribe(this);
                }
            }

            public void OnNext(KeyValuePair<string, object?> value)
            {
                _callback(value);
            }
        }

        private class FakeFileProvider : IFileProvider, IDisposable
        {
            public bool Disposed { get; private set; }
            public void Dispose() => Disposed = true;
            public IDirectoryContents GetDirectoryContents(string subpath) => throw new NotImplementedException();
            public IFileInfo GetFileInfo(string subpath) => throw new NotImplementedException();
            public IChangeToken Watch(string filter) => throw new NotImplementedException();
        }

        private class ServiceC
        {
            public ServiceC(ServiceD serviceD) { }
        }

        [Fact]
        public void ConfigureDefaults_LoadsApplicationSpecificSettings()
        {
            using TempDirectory tempDir = new();
            string appSettingsPath = Path.Combine(tempDir.Path, "testapp.settings.json");
            string appSettingsEnvPath = Path.Combine(tempDir.Path, "testapp.settings.Production.json");

            // Create test configuration files
            File.WriteAllText(appSettingsPath, """{"TestKey": "AppValue"}""");
            File.WriteAllText(appSettingsEnvPath, """{"TestKey": "AppEnvValue", "EnvKey": "EnvValue"}""");

            using var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .UseContentRoot(tempDir.Path)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, "testapp"),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, "Production")
                    });
                })
                .Build();

            var configuration = host.Services.GetRequiredService<IConfiguration>();

            // Verify that app-specific environment settings override app-specific settings
            Assert.Equal("AppEnvValue", configuration["TestKey"]);
            Assert.Equal("EnvValue", configuration["EnvKey"]);
        }

        [Fact]
        public void ConfigureDefaults_LoadsApplicationSpecificSettings_WithDevelopmentEnvironment()
        {
            using TempDirectory tempDir = new();
            string appSettingsPath = Path.Combine(tempDir.Path, "testapp.settings.json");
            string appSettingsEnvPath = Path.Combine(tempDir.Path, "testapp.settings.Production.json");

            // Create test configuration files
            File.WriteAllText(appSettingsPath, """{"TestKey": "AppValue"}""");
            File.WriteAllText(appSettingsEnvPath, """{"TestKey": "ProductionValue", "ProductionKey": "ProductionValue"}""");

            using var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .UseContentRoot(tempDir.Path)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, "testapp"),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, "Development")
                    });
                })
                .Build();

            var configuration = host.Services.GetRequiredService<IConfiguration>();

            // Verify that Production-specific file is not loaded when running in Development environment
            Assert.Equal("AppValue", configuration["TestKey"]); // Should come from base settings, not Production
            Assert.Null(configuration["ProductionKey"]); // Should not be loaded from Production file
        }

        [Fact]
        public void ConfigureDefaults_DoesNotLoadApplicationSpecificSettings_WhenApplicationNameIsEmpty()
        {
            using TempDirectory tempDir = new();
            string appSettingsPath = Path.Combine(tempDir.Path, ".settings.json");

            // Create test configuration file that should NOT be loaded
            File.WriteAllText(appSettingsPath, """{"TestKey": "ShouldNotBeLoaded"}""");

            using var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .UseContentRoot(tempDir.Path)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, ""),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, "Production")
                    });
                })
                .Build();

            var configuration = host.Services.GetRequiredService<IConfiguration>();

            // Verify that app-specific settings are not loaded when ApplicationName is empty
            Assert.Null(configuration["TestKey"]);
        }

        [Fact]
        public void ConfigureDefaults_ReplacesPathSeparatorsInApplicationName()
        {
            using TempDirectory tempDir = new();
            string appSettingsPath = Path.Combine(tempDir.Path, "my_app.settings.json");

            // Create test configuration file with path separators replaced by underscores
            File.WriteAllText(appSettingsPath, """{"TestKey": "PathSeparatorValue"}""");

            using var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .UseContentRoot(tempDir.Path)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, "my/app"),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, "Production")
                    });
                })
                .Build();

            var configuration = host.Services.GetRequiredService<IConfiguration>();
            var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();

            // Verify that ApplicationName retains original value (not sanitized)
            Assert.Equal("my/app", hostEnvironment.ApplicationName);
            
            // Verify that path separators are replaced with underscores for file loading
            Assert.Equal("PathSeparatorValue", configuration["TestKey"]);
        }

        [Fact]
        public void ConfigureDefaults_ApplicationSpecificSettingsOverrideAppSettings()
        {
            using TempDirectory tempDir = new();
            string appSettingsPath = Path.Combine(tempDir.Path, "appsettings.json");
            string appSpecificSettingsPath = Path.Combine(tempDir.Path, "myapp.settings.json");

            // Create test configuration files
            File.WriteAllText(appSettingsPath, """{"SharedKey": "AppSettingsValue", "AppKey": "AppSettingsOnly"}""");
            File.WriteAllText(appSpecificSettingsPath, """{"SharedKey": "AppSpecificValue", "SpecificKey": "AppSpecificOnly"}""");

            using var host = new HostBuilder()
                .ConfigureDefaults(args: null)
                .UseContentRoot(tempDir.Path)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new[]
                    {
                        new KeyValuePair<string, string>(HostDefaults.ApplicationKey, "myapp"),
                        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey, "Development")
                    });
                })
                .Build();

            var configuration = host.Services.GetRequiredService<IConfiguration>();

            // Verify that app-specific settings override general appsettings
            Assert.Equal("AppSpecificValue", configuration["SharedKey"]);
            Assert.Equal("AppSettingsOnly", configuration["AppKey"]);
            Assert.Equal("AppSpecificOnly", configuration["SpecificKey"]);
        }

        internal class ServiceD { }

        internal class ServiceA { }

        internal class ServiceB { }
    }
}
