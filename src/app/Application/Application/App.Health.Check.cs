﻿using GarageGroup.Infra;
using PrimeFuncPack;

namespace GarageGroup.Internal.Timesheet;

partial class Application
{
    [HandlerApplicationExtension(HttpMethodName.Get, "/health")]
    internal static Dependency<IHealthCheckHandler> UseHealthCheck()
        =>
        UseSqlApi().UseServiceHealthCheckApi("SqlApi").UseHealthCheckHandler();
}