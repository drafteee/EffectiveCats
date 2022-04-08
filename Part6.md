# Логирование

> Можем свой создать через LoggerFactory.Create().CreateLogger()
> Разные провайдоры(консоль,debug, Event Tracing for Windows,Windows Event Log)
> можно создать свою конфигурацию в appsettings
> или добавлять фильтры логгирования в program

> Чтобы добавить логгирование, то нужно IWebHostBuilder.ConfigureLogging 